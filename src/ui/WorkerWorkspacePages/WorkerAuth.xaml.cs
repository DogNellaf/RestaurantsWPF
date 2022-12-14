using RestaurantsClasses.Enums;
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
using ui.AdminWorkspacePages;
using ui.Helper;
using ui.WorkerWorkspacePages;

namespace ui
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class WorkerAuth : Window
    {
        private Window _previous;
        public WorkerAuth(Window previous)
        {
            InitializeComponent();
            _previous = previous;
        }

        private void authButton_Click(object sender, RoutedEventArgs e)
        {
            var username = usernameTextBox.Text;
            var password = passwordTextBox.Text;
            var worker = RequestClient.AuthWorker(username, password);
            if (worker is not null)
            {
                var positions = RequestClient.GetObjects<Position>();
                var position = positions.Where(x => x.id == worker.PositionId).First();
                switch (position.Role)
                {
                    case WorkerRole.Admin:
                        new AdminWorkspace(this, worker).Show();
                        break;
                    case WorkerRole.Manager:
                        new NewOrders(this, worker).Show();
                        break;
                    case WorkerRole.HR:
                        new WorkerEditor(this, worker).Show();
                        break;
                    case WorkerRole.Accountant:
                        new Diagram(this, worker).Show();
                        break;
                    default:
                        new WorkerWorkspace(this, worker).Show();
                        break;
                }
                Hide();
            }
            else
            {
                MessageBox.Show("Введены неверные данные");
            }
        }

        private void clientButton_Click(object sender, RoutedEventArgs e)
        {
            _previous.Show();
            Close();
        }
    }
}
