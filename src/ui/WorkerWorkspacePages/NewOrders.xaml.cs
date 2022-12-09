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
using ui.Helper;

namespace ui
{
    
    class Item
    {
        public OrderStatus Status { get; set; }
        public int TableNum { get; set; }
        public int Id { get; set; }
    }

    public partial class NewOrders : Window
    {
        private Window _previous;
        private Worker _worker;
        public NewOrders(Window previous, Worker worker)
        {
            InitializeComponent();
            _previous = previous;
            _worker = worker;
            var orders = RequestClient.NewOrders();

            var buttonTemplate = new FrameworkElementFactory(typeof(Button));
            buttonTemplate.SetBinding(Button.ContentProperty, new Binding("Name"));
            buttonTemplate.AddHandler(
                Button.ClickEvent,
                new RoutedEventHandler((o, e) => MessageBox.Show("hi"))
            );
            ordersGrid.Columns.Add(
                new DataGridTextColumn()
                {
                    Header = "Статус",
                    Binding = new Binding("Status"),
                    Width = 110
                }
            );

            ordersGrid.Columns.Add(
                new DataGridTextColumn()
                {
                    Header = "Столик",
                    Binding = new Binding("TableNum"),
                    Width = 110
                }
            );

            ordersGrid.Columns.Add(
                new DataGridTemplateColumn()
                {
                    Header = "Закрепить за собой",
                    CellTemplate = new DataTemplate() { VisualTree = buttonTemplate },
                    Width = 200
                }
            );
                

            foreach (var order in orders)
            {
                ordersGrid.Items.Add(new Item() { Status = order.Status, TableNum = order.TableId, Id = order.id });
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            _previous.Show();
            Close();
        }
        private void setMeButton(object sender, RoutedEventArgs e, int orderId)
        {
            RequestClient.SetOrderToWorker(orderId, _worker.id);
            exitButton_Click(sender, e);
        }
    }
}
