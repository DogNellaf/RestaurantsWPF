using RestaurantsClasees.OrderSystem;
using RestaurantsClasses.Enums;
using RestaurantsClasses.KontragentsSystem;
using RestaurantsClasses.OnlineSystem;
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

namespace ui.WorkerWorkspacePages
{
    /// <summary>
    /// Логика взаимодействия для WorkerOrders.xaml
    /// </summary>
    /// 

    public partial class OnlineOrders : Window
    {
        private Window _previous;
        private Worker _worker;
        private Client _client;
        private bool _isEndOfLoading = false;

        public OnlineOrders(Window previous, Worker worker = null, Client client = null)
        {
            InitializeComponent();
            _previous = previous;
            _worker = worker;
            _client = client;

            var orders = RequestClient.GetObjects<OnlineOrder>();
            if (client is not null)
            {
                orders = orders.Where(x => x.ClientId == client.id).ToList();
            }

            var showButtonTemplate = new FrameworkElementFactory(typeof(Button));
            showButtonTemplate.AddHandler(Button.ClickEvent, new RoutedEventHandler((o, e) => showMealsButton(o, e)));

            ordersGrid.ItemsSource = orders;

            ordersGrid.Columns.Add(
                new DataGridTemplateColumn()
                    {
                        Header = "Посмотреть содержимое",
                        CellTemplate = new DataTemplate() { VisualTree = showButtonTemplate },
                        Width = 150
                    }
            );

            if (client is null)
            {
                var completeButtonTemplate = new FrameworkElementFactory(typeof(Button));
                completeButtonTemplate.AddHandler(Button.ClickEvent, new RoutedEventHandler((o, e) => setCompleteButton_Click(o, e)));
                ordersGrid.Columns.Add(
                    new DataGridTemplateColumn()
                    {
                        Header = "Отметить выполненным",
                        CellTemplate = new DataTemplate() { VisualTree = completeButtonTemplate },
                        Width = 150
                    }
                );
            }
        }

        private void showMealsButton(object sender, RoutedEventArgs e)
        {
            if (_isEndOfLoading)
            {
                var button = (Button)e.OriginalSource;
                var dataGridRow = VisualTreeHelpers.FindAncestor<DataGridRow>(button);
                var order = (OnlineOrder)dataGridRow.DataContext;
                new MealsInOnlineOrder(this, order).Show();
                Hide();
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            _previous.Show();
            Close(); 
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //ordersGrid.Columns.RemoveAt(0);
            ordersGrid.Columns.RemoveAt(ordersGrid.Columns.Count - 2);
            ordersGrid.Columns[ordersGrid.Columns.Count - 1].IsReadOnly = true;
            ordersGrid.Columns[ordersGrid.Columns.Count - 3].IsReadOnly = true;
            _isEndOfLoading = true;
        }

        private bool isManualEditCommit;

        private void ordersGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (!isManualEditCommit)
            {
                isManualEditCommit = true;
                DataGrid grid = (DataGrid)sender;

                int selectedRowIndex = grid.SelectedIndex;

                grid.CommitEdit(DataGridEditingUnit.Row, true);

                var items = ((DataGrid)sender).Items;

                var orderData = (OnlineOrder)items[selectedRowIndex];

                if (orderData.address == string.Empty)
                {
                    MessageBox.Show("Перед сохранением введите адрес доставки");
                    return;
                }

                RequestClient.CreateOnlineOrder(_client.id, orderData.address);

                MessageBox.Show("Заказ был успешно создан");
                exitButton_Click(sender, null);


                isManualEditCommit = false;
            }
        }

        private void setCompleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isEndOfLoading)
            {
                var button = (Button)e.OriginalSource;
                var dataGridRow = VisualTreeHelpers.FindAncestor<DataGridRow>(button);
                var order = (OnlineOrder)dataGridRow.DataContext;

                RequestClient.SetOnlineOrderComplete(order.id);
                exitButton_Click(sender, e);
            }
        }
    }
}
