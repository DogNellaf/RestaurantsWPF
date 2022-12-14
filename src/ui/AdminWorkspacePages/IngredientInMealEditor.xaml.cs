using RestaurantsClasees.OrderSystem;
using RestaurantsClasses.KontragentsSystem;
using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using ui.Helper;

namespace ui.AdminWorkspacePages
{
    public partial class IngredientInMealEditor : Window
    {
        private Window _previous;
        private Meal _meal;
        private bool _isEndOfLoading = false;
        public IngredientInMealEditor(Window previous, Meal meal)
        {
            InitializeComponent();
            _previous = previous;
            _meal = meal;

            var checkboxTemplate = new FrameworkElementFactory(typeof(CheckBox));
            checkboxTemplate.SetBinding(CheckBox.IsCheckedProperty, new Binding("IsContains"));
            checkboxTemplate.AddHandler(CheckBox.CheckedEvent, new RoutedEventHandler((o, e) => checkBox_Check(o, e)));
            checkboxTemplate.AddHandler(CheckBox.UncheckedEvent, new RoutedEventHandler((o, e) => checkBox_Uncheck(o, e)));

            ingredientsGrid.Columns.Add(new DataGridTemplateColumn()
            {
                Header = "Содержится в блюде?",
                CellTemplate = new DataTemplate() { VisualTree = checkboxTemplate },
                Width = 150
            });

            var ingredients = RequestClient.GetObjects<Ingredient>();
            var mealIngredients = RequestClient.GetIngredientsByMeal(_meal);
            var ids = mealIngredients.Select(x => x.id).ToList();

            foreach (var ingredient in ingredients)
            {
                if (ids.Contains(ingredient.id))
                {
                    ingredient.IsContains = true;
                }
            }

            ingredientsGrid.ItemsSource = ingredients;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ingredientsGrid.Columns.RemoveAt(ingredientsGrid.Columns.Count - 1);
            ingredientsGrid.Columns.RemoveAt(ingredientsGrid.Columns.Count - 1);
            ingredientsGrid.Columns[ingredientsGrid.Columns.Count - 1].IsReadOnly = true;
            _isEndOfLoading = true;
        }

        private void checkBox_Check(object sender, RoutedEventArgs e)
        {
            if (_isEndOfLoading)
            {
                var checkBox = (CheckBox)e.OriginalSource;
                var dataGridRow = VisualTreeHelpers.FindAncestor<DataGridRow>(checkBox);
                var ingerdient = (Ingredient)dataGridRow.DataContext;

                RequestClient.AddIngredientsToMeal(_meal.id, ingerdient.id);
            }
        }

        private void checkBox_Uncheck(object sender, RoutedEventArgs e)
        {
            if (_isEndOfLoading)
            {
                var checkBox = (CheckBox)e.OriginalSource;
                var dataGridRow = VisualTreeHelpers.FindAncestor<DataGridRow>(checkBox);
                var ingerdient = (Ingredient)dataGridRow.DataContext;

                RequestClient.DeleteIngredientByMeal(_meal.id, ingerdient.id);
            }
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            var rawId = ingredientIdBox.Text;

            if (!int.TryParse(rawId, out int id))
            {
                MessageBox.Show("Введите корректное число");
                return;
            }

            var ingredient = RequestClient.GetObjects<Ingredient>().Where(x => x.id == id).FirstOrDefault();
            if (ingredient is null)
            {
                MessageBox.Show("Такой ингредиент не существует");
                return;
            }

            RequestClient.DeleteIngredientByMeal(_meal.id, ingredient.id);
            exitButton_Click(null, null);
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var rawId = ingredientIdBox.Text;

            if (!int.TryParse(rawId, out int id))
            {
                MessageBox.Show("Введите корректное число");
                return;
            }

            var ingredient = RequestClient.GetObjects<Ingredient>().Where(x => x.id == id).FirstOrDefault();
            if (ingredient is null)
            {
                MessageBox.Show("Такой ингредиент не существует");
                return;
            }

            RequestClient.AddIngredientsToMeal(_meal.id, ingredient.id);
            MessageBox.Show("Ингредиент добавлен в блюдо");
            exitButton_Click(null, null);
        }


        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            _previous.Show();
            Close();
        }
    }
}
