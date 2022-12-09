using RestaurantsClasses.WorkersSystem;
using System;
using System.Collections.Generic;
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
                addWorkerButton.IsEnabled = true;
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            _previous.Show();
            Close();
        }

        private void newOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            new NewOrders(this).Show();
            Hide();
        }
    }
}
