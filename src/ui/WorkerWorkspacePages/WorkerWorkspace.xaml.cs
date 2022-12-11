using Microsoft.Win32;
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

namespace ui
{
    /// <summary>
    /// Логика взаимодействия для WorkerWorkspace.xaml
    /// </summary>
    public partial class WorkerWorkspace : Window
    {
        private Window _previous;
        private Worker _worker;
        public WorkerWorkspace(Window previous, Worker worker)
        {
            InitializeComponent();
            _previous = previous;
            _worker = worker;

            nameLabel.Content = $"Добро пожаловать, {worker.FirstName} {worker.LastName}!";
            roleLabel.Content = $"{RequestClient.GetPositionName(worker.PositionId)}";

            if (RequestClient.CheckIsItAdmin(worker.PositionId))
            {
                exportButton.IsEnabled = true;
                editorWorkerButton.IsEnabled = true;
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            _previous.Show();
            Close();
        }

        private void newOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            new NewOrders(this, _worker).Show();
            Hide();
        }

        // экпорт всех оффлайн и онлайн заказов в csv формате
        private void exportButton_Click(object sender, RoutedEventArgs e)
        {
            // запись в файл
            var saveFileDialog1 = new SaveFileDialog()
            {
                Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*",
                RestoreDirectory = true
            };

            if (saveFileDialog1.ShowDialog() == true)
            {
                // считывание из базы 
                var orders = RequestClient.GetAllOfflineOrders();
                var csv = "Id;ServerId;TableId;Status;Created\n";

                foreach (var order in orders)
                {
                    csv += $"{order.id};{order.ServerId};{order.TableId};{order.Status};{order.Created}\n";
                }

                File.WriteAllText(saveFileDialog1.FileName, csv);
                MessageBox.Show($"Файл успешно сохранен по пути: {saveFileDialog1.FileName}");
            }
        }

        // посмотреть заказы текущего работника
        private void yourOrdersButton_Click(object sender, RoutedEventArgs e)
        {

        }

        // посмотреть онлайн заказы
        private void onlineOrdersButton_Click(object sender, RoutedEventArgs e)
        {

        }

        // открыть редактор работников
        private void editorWorkerButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
