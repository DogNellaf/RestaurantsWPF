using Microsoft.Win32;
using RestaurantsClasees.OrderSystem;
using RestaurantsClasses.KontragentsSystem;
using RestaurantsClasses.OnlineSystem;
using RestaurantsClasses.WorkersSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ui.Helper;

namespace ui.AdminWorkspacePages
{
    /// <summary>
    /// Логика взаимодействия для Export.xaml
    /// </summary>
    public partial class Export : Window
    {
        private Window _previous;
        public Export(Window previous)
        {
            InitializeComponent();
            _previous = previous;
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            _previous.Show();
            Close();
        }

        private void positionExportButton_Click(object sender, RoutedEventArgs e)
        {
            string path = getPath();

            if (!string.IsNullOrEmpty(path))
            {
                var positions = RequestClient.GetObjects<Position>();
                var csv = "Id;Название;Ставка;Доля премии;Уровень доступа\n";

                foreach (var position in positions)
                {
                    csv += $"{position.id};{position.Name};{position.Salary};{position.Prize};{position.Role}\n";
                }

                File.WriteAllText(path, csv);
                MessageBox.Show($"Файл успешно сохранен по пути: {path}");
            }
        }

        private void onlineOrderExport_Click(object sender, RoutedEventArgs e)
        {
            string path = getPath();

            if (!string.IsNullOrEmpty(path))
            {
                var orders = RequestClient.GetObjects<OnlineOrder>();
                var csv = "Id;Id клиента;Когда был создан;Адрес доставки;Завершен ли?\n";

                foreach (var order in orders)
                {
                    csv += $"{order.id};{order.ClientId};{order.created};{order.address};{order.isComplited}\n";
                }

                File.WriteAllText(path, csv);
                MessageBox.Show($"Файл успешно сохранен по пути: {path}");
            }
        }

        private void ordersExport_Click(object sender, RoutedEventArgs e)
        {
            string path = getPath();

            if (!string.IsNullOrEmpty(path))
            {
                var orders = RequestClient.GetAllOfflineOrders();
                var csv = "Id;Id официанта;Номер столика;Текущий статус;Когда был создан\n";

                foreach (var order in orders)
                {
                    csv += $"{order.id};{order.ServerId};{order.TableId};{order.Status};{order.Created}\n";
                }

                File.WriteAllText(path, csv);
                MessageBox.Show($"Файл успешно сохранен по пути: {path}");
            }
        }

        private void ingredientsExportButton_Click(object sender, RoutedEventArgs e)
        {
            string path = getPath();

            if (!string.IsNullOrEmpty(path))
            {
                var ingredietns = RequestClient.GetObjects<Ingredient>();
                var csv = "Id;Название\n";

                foreach (var ingredient in ingredietns)
                {
                    csv += $"{ingredient.id};{ingredient.Name}\n";
                }

                File.WriteAllText(path, csv);
                MessageBox.Show($"Файл успешно сохранен по пути: {path}");
            }
        }

        private void dishesExportButton_Click(object sender, RoutedEventArgs e)
        {
            string path = getPath();

            if (!string.IsNullOrEmpty(path))
            {
                var meals = RequestClient.GetObjects<Meal>();
                var csv = "Id;Название;Стоимость;Вес;Количество порций\n";

                foreach (var meal in meals)
                {
                    csv += $"{meal.id};{meal.Name};{meal.Cost};{meal.Weight};{meal.ServingsNumber}\n";
                }

                File.WriteAllText(path, csv);
                MessageBox.Show($"Файл успешно сохранен по пути: {path}");
            }
        }

        private void workersExportButton_Click(object sender, RoutedEventArgs e)
        {
            string path = getPath();

            if (!string.IsNullOrEmpty(path))
            {
                var workers = RequestClient.GetObjects<Worker>();
                var csv = "Id;Id должности;Имя;Фамилия;Номер телефона;Логин;Хэш пароля\n";

                foreach (var worker in workers)
                {
                    csv += $"{worker.id};{worker.PositionId};{worker.FirstName};{worker.LastName};{worker.Phone};{worker.Username};{worker.Password}\n";
                }

                File.WriteAllText(path, csv);
                MessageBox.Show($"Файл успешно сохранен по пути: {path}");
            }
        }

        private static string getPath()
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*",
                RestoreDirectory = true
            };

            if (dialog.ShowDialog() == true)

                return dialog.FileName;

            return "";
        }
    }
}
