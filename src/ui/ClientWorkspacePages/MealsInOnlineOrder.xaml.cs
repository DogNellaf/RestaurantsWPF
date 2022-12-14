using RestaurantsClasees.OrderSystem;
using RestaurantsClasses.KontragentsSystem;
using RestaurantsClasses.OnlineSystem;
using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using ui.Helper;

namespace ui.AdminWorkspacePages
{
    public partial class MealsInOnlineOrder : Window
    {
        private Window _previous;
        private OnlineOrder _order;
        private bool _isEndOfLoading = false;
        public MealsInOnlineOrder(Window previous, OnlineOrder order)
        {
            InitializeComponent();
            _previous = previous;
            _order = order;

            var checkboxTemplate = new FrameworkElementFactory(typeof(CheckBox));
            checkboxTemplate.SetBinding(CheckBox.IsCheckedProperty, new Binding("IsContains"));
            checkboxTemplate.AddHandler(CheckBox.CheckedEvent, new RoutedEventHandler((o, e) => checkBox_Check(o, e)));
            checkboxTemplate.AddHandler(CheckBox.UncheckedEvent, new RoutedEventHandler((o, e) => checkBox_Uncheck(o, e)));

            mealsGrid.Columns.Add(new DataGridTemplateColumn()
            {
                Header = "Содержится в заказе?",
                CellTemplate = new DataTemplate() { VisualTree = checkboxTemplate },
                Width = 150
            });

            var meals = RequestClient.GetObjects<Meal>();
            var onlineMeals = RequestClient.GetMealsByOrder(order.id, true);
            if (onlineMeals is not null)
            {
                var ids = onlineMeals.Select(x => x.id);

                foreach (var meal in meals)
                {
                    if (ids.Contains(meal.id))
                    {
                        meal.IsContains = true;
                    }
                }
            }
            mealsGrid.ItemsSource = meals;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mealsGrid.Columns.RemoveAt(mealsGrid.Columns.Count - 1);
            mealsGrid.Columns.RemoveAt(mealsGrid.Columns.Count - 1);
            mealsGrid.Columns[mealsGrid.Columns.Count - 1].IsReadOnly = true;
            _isEndOfLoading = true;
        }

        private void checkBox_Check(object sender, RoutedEventArgs e)
        {
            if (_isEndOfLoading)
            {
                var checkBox = (CheckBox)e.OriginalSource;
                var dataGridRow = VisualTreeHelpers.FindAncestor<DataGridRow>(checkBox);
                var meal = (Meal)dataGridRow.DataContext;

                RequestClient.AddMealToOnlineOrder(meal.id, _order.id);
            }
        }

        private void checkBox_Uncheck(object sender, RoutedEventArgs e)
        {
            if (_isEndOfLoading)
            {
                var checkBox = (CheckBox)e.OriginalSource;
                var dataGridRow = VisualTreeHelpers.FindAncestor<DataGridRow>(checkBox);
                var meal = (Meal)dataGridRow.DataContext;

                RequestClient.RemoveMealToOnlineOrder(meal.id, _order.id);
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            _previous.Show();
            Close();
        }
    }
}
