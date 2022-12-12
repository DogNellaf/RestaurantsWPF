using Microsoft.Win32;
using RestaurantsClasses.WorkersSystem;
using System.IO;
using System.Windows;
using ui.Helper;

namespace ui.AdminWorkspacePages
{
    /// <summary>
    /// Логика взаимодействия для AdminWorkspace.xaml
    /// </summary>
    public partial class AdminWorkspace : Window
    {
        private Window _previous;
        private Worker _worker;
        public AdminWorkspace(Window previous, Worker worker)
        {
            InitializeComponent();
            _previous = previous;
            _worker = worker;

            nameLabel.Content = $"Добро пожаловать, {worker.FirstName} {worker.LastName}!";
            roleLabel.Content = $"{RequestClient.GetPositionName(worker.PositionId)}";
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            _previous.Show();
            Close();
        }

        // открыть редактор работников
        public void editorWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new WorkerEditor(this, _worker).Show();
        }

        public void dishesEditorButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new MealsEditor(this, _worker).Show();
        }

        public void ingredientsEditorButton_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
