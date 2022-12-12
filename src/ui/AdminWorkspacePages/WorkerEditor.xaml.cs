using RestaurantsClasses.WorkersSystem;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ui.Helper;

namespace ui.AdminWorkspacePages
{
    /// <summary>
    /// Логика взаимодействия для WorkerEditor.xaml
    /// </summary>
    /// 

    public class WorkerItem
    {
        public int Id { get; set;}
        public string FirstName { get; set;}
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ButtonText { get; set; }
    }

    public partial class WorkerEditor : Window
    {
        private Window _previous;
        private Worker _admin;
        public WorkerEditor(Window previous, Worker admin)
        {
            InitializeComponent();
            _previous = previous;
            _admin = admin;

            var workers = RequestClient.GetWorkers();

            var genNewPassword = new FrameworkElementFactory(typeof(Button));
            genNewPassword.SetBinding(Button.NameProperty, new Binding("Id"));
            genNewPassword.SetBinding(Button.ContentProperty, new Binding("ButtonText"));
            genNewPassword.AddHandler(Button.ClickEvent, new RoutedEventHandler((o, e) => getNewPassword(o, e)));

            workersGrid.Columns.Add(
                new DataGridTextColumn()
                {
                    Header = "Имя",
                    Binding = new Binding("FirstName"),
                    Width = 200
                }
            );

            workersGrid.Columns.Add(
                new DataGridTextColumn()
                {
                    Header = "Фамилия",
                    Binding = new Binding("LastName"),
                    Width = 110
                }
            );

            workersGrid.Columns.Add(
                new DataGridTextColumn()
                {
                    Header = "Имя пользователя",
                    Binding = new Binding("Username"),
                    Width = 110
                }
            );

            workersGrid.Columns.Add(
                new DataGridTemplateColumn()
                {
                    Header = "Пароль",
                    CellTemplate = new DataTemplate() { VisualTree = genNewPassword },
                    Width = 200
                }
            );


            foreach (var worker in workers)
            {
                workersGrid.Items.Add(new WorkerItem()
                {
                    Id = worker.id,
                    FirstName = worker.FirstName,
                    LastName = worker.LastName,
                    Username = worker.Username,
                    Password = worker.Password,
                    ButtonText = $"Сгенерировать новый пароль для {worker.id}"
                });
            }
        }

        private void getNewPassword(object sender, RoutedEventArgs e)
        {
            string rawWorkerId = ((Button)sender).Content.ToString().Split(' ').Last();
            if (!int.TryParse(rawWorkerId, out int workerId))
                return;

            string password = RequestClient.GenerateNewPassword(workerId, _admin.id);
            newPasswordBox.Text = password;
            newPasswordBox.IsEnabled = true;
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            _previous.Show();
            Hide(); 
        }
    }
}
