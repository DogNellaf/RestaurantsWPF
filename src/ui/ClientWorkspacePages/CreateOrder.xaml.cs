using Newtonsoft.Json;
using RestaurantsClasees.OrderSystem;
using RestaurantsClasses.BookingSystem;
using RestaurantsClasses.KontragentsSystem;
using RestaurantsClasses.OnlineSystem;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using ui.Helper;

namespace ui.AdminWorkspacePages
{
    public partial class CreateOrder : Window
    {
        private Window _previous;
        private List<Meal> _meals;
        public CreateOrder(Window previous)
        {
            InitializeComponent();
            _previous = previous;
            _meals = new List<Meal>();

            var checkboxTemplate = new FrameworkElementFactory(typeof(CheckBox));
            checkboxTemplate.SetBinding(CheckBox.IsCheckedProperty, new Binding("IsContains"));
            checkboxTemplate.AddHandler(CheckBox.CheckedEvent, new RoutedEventHandler((o, e) => checkBox_Check(o, e)));
            checkboxTemplate.AddHandler(CheckBox.UncheckedEvent, new RoutedEventHandler((o, e) => checkBox_Uncheck(o, e)));

            ingredientsGrid.Columns.Add(new DataGridTemplateColumn()
            {
                Header = "Содержится в заказе?",
                CellTemplate = new DataTemplate() { VisualTree = checkboxTemplate },
                Width = 150
            });

            ingredientsGrid.ItemsSource = RequestClient.GetObjects<Meal>();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ingredientsGrid.Columns.RemoveAt(ingredientsGrid.Columns.Count - 1);
            ingredientsGrid.Columns.RemoveAt(ingredientsGrid.Columns.Count - 1);
            ingredientsGrid.Columns[ingredientsGrid.Columns.Count - 1].IsReadOnly = true;
        }

        private void checkBox_Check(object sender, RoutedEventArgs e)
        {
            var checkBox = (CheckBox)e.OriginalSource;
            var dataGridRow = VisualTreeHelpers.FindAncestor<DataGridRow>(checkBox);
            var meal = (Meal)dataGridRow.DataContext;

            _meals.Add(meal);
        }

        private void checkBox_Uncheck(object sender, RoutedEventArgs e)
        {
            var checkBox = (CheckBox)e.OriginalSource;
            var dataGridRow = VisualTreeHelpers.FindAncestor<DataGridRow>(checkBox);
            var meal = (Meal)dataGridRow.DataContext;

            _meals.Remove(meal);
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var rawId = tableIdBox.Text;

            if (!int.TryParse(rawId, out int id))
            {
                MessageBox.Show("Введите корректное число");
                return;
            }

            var table = RequestClient.GetObjects<Table>().Where(x => x.id == id).FirstOrDefault();
            if (table is null)
            {
                MessageBox.Show("Такой столик не существует");
                return;
            }

            var json = JsonConvert.SerializeObject(_meals);
            RequestClient.CreateOfflineOrder(table.id, json);
            MessageBox.Show("Заказ успешно оставлен");
            exitButton_Click(null, null);
        }


        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            _previous.Show();
            Close();
        }
    }
}
