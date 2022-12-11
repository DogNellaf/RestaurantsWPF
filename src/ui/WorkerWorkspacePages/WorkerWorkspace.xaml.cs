using Microsoft.Win32;
using RestaurantsClasses.WorkersSystem;
using System.IO;
using System.Windows;
using ui.Helper;
using ui.WorkerWorkspacePages;

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
                dishesEditorButton.IsEnabled = true;
                ingredientsEditorButton.IsEnabled = true;
            }
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

        // кнопка выхода
        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            _previous.Show();
            Close();
        }

        // переход на страницу с новыми заказами
        private void newOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            new NewOrders(this, _worker).Show();
            Hide();
        }


        // посмотреть заказы текущего работника
        private void yourOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            new WorkerOrders(this, _worker).Show();
            Hide();
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
