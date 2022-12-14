using RestaurantsClasses.Enums;
using RestaurantsClasses.WorkersSystem;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using ui.Helper;

namespace ui.AdminWorkspacePages
{
    /// <summary>
    /// Логика взаимодействия для WorkerEditor.xaml
    /// </summary>
    /// 

    //public class WorkerItem
    //{
    //    public int Id { get; set;}
    //    public string FirstName { get; set;}
    //    public string LastName { get; set; }
    //    public string Phone { get; set; }
    //    public string Username { get; set; }
    //    public string Password { get; set; }
    //    public string ButtonText { get; set; }
    //}

    public partial class WorkerEditor : Window
    {
        private Window _previous;
        private Worker _worker;
        public WorkerEditor(Window previous, Worker worker)
        {
            InitializeComponent();
            _previous = previous;
            _worker = worker;

            var workers = RequestClient.GetWorkers();

            workersGrid.ItemsSource = workers;

            var positions = RequestClient.GetObjects<Position>();
            var position = RequestClient.GetObjects<Position>().Where(x => x.id == worker.PositionId).First();

            if (position.Role != WorkerRole.Admin)
            {
                positions = positions.Where(x => x.Role != WorkerRole.Admin && x.Role != WorkerRole.HR).ToList();
            }
            roleComboBox.ItemsSource = positions;
            roleComboBox.SelectedItem = roleComboBox.Items[0];
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            _previous.Show();
            Hide(); 
        }

        private bool isManualEditCommit;

        private void workersGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (!isManualEditCommit)
            {
                isManualEditCommit = true;
                DataGrid grid = (DataGrid)sender;

                int selectedRowIndex = grid.SelectedIndex;

                grid.CommitEdit(DataGridEditingUnit.Row, true);

                var items = ((DataGrid)sender).Items;

                var workerData = (Worker)items[selectedRowIndex];

                if (workerData.Username == string.Empty)
                {
                    MessageBox.Show("Перед сохранением введите логин пользователя");
                    return;
                }

                if (workerData.FirstName == string.Empty)
                {
                    MessageBox.Show("Перед сохранением введите имя пользователя");
                    return;
                }

                if (workerData.LastName == string.Empty)
                {
                    MessageBox.Show("Перед сохранением введите фамилию пользователя");
                    return;
                } 

                var workers = RequestClient.GetWorkers();

                var worker = workers.Where(x => x.Username == workerData.Username).FirstOrDefault();

                if (worker is null)
                {
                    RequestClient.CreateWorker(workerData.Username, workerData.FirstName, workerData.LastName, workerData.Phone);

                    MessageBox.Show("Пользователь был успешно создан");

                    workerIdBox.Text = RequestClient.GetWorkers().Last().id.ToString();
                    changePasswordButton_Click(sender, null);
                }
                else
                {
                    RequestClient.UpdateWorker(worker.id, workerData.Username, workerData.FirstName, workerData.LastName, workerData.Phone);

                    MessageBox.Show("Данные пользователя успешно обновлены");

                    exitButton_Click(sender, null);
                }

                isManualEditCommit = false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            workersGrid.Columns.RemoveAt(workersGrid.Columns.Count - 2);
            workersGrid.Columns[workersGrid.Columns.Count-1].IsReadOnly = true;
        }

        private void changePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            var rawWorkerId = workerIdBox.Text;

            if (!int.TryParse(rawWorkerId, out int workerId))
            {
                MessageBox.Show("Введите корректное число");
                return;
            }
                

            var worker = RequestClient.GetWorkers().Where(x => x.id == workerId).FirstOrDefault();
            if (worker is null)
            {
                MessageBox.Show("Такого сотрудника не существует");
                return;
            }

            string password = RequestClient.GenerateNewPassword(workerId, _worker.id);
            newPasswordBox.Text = password;
            newPasswordBox.IsEnabled = true;
        }

        private void removeWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            var rawWorkerId = workerIdBox.Text;

            if (!int.TryParse(rawWorkerId, out int workerId))
            {
                MessageBox.Show("Введите корректное число");
                return;
            }


            var worker = RequestClient.GetWorkers().Where(x => x.id == workerId).FirstOrDefault();
            if (worker is null)
            {
                MessageBox.Show("Такого сотрудника не существует");
                return;
            }

            if (worker.id == _worker.id)
            {
                MessageBox.Show("Себя нельзя удалить");
                return;
            }

            RequestClient.DeleteWorker(worker.id);
            MessageBox.Show("Сотрудник успешно удален");
            exitButton_Click(null, null);
        }

        private void saveRoleButton_Click(object sender, RoutedEventArgs e)
        {
            var rawWorkerId = workerIdBox.Text;

            if (!int.TryParse(rawWorkerId, out int workerId))
            {
                MessageBox.Show("Введите корректное число");
                return;
            }

            var worker = RequestClient.GetWorkers().Where(x => x.id == workerId).FirstOrDefault();
            if (worker is null)
            {
                MessageBox.Show("Такого сотрудника не существует");
                return;
            }

            if (worker.id == _worker.id)
            {
                MessageBox.Show("Себя нельзя удалить");
                return;
            }

            var rawPosition = roleComboBox.SelectedValue.ToString();

            var position = RequestClient.GetObjects<Position>().Where(x => x.ToString() == rawPosition).FirstOrDefault();
            if (position is null)
            {
                MessageBox.Show("Такой должности не существует");
                return;
            }

            RequestClient.UpdatePosition(worker.id, position.id);
            MessageBox.Show("Должность успешно обновлена");
            exitButton_Click(null, null);
        }
    }
}
