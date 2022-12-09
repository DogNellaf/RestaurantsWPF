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
    /// Логика взаимодействия для NewOrders.xaml
    /// </summary>
    public partial class NewOrders : Window
    {
        private Window _previous;
        public NewOrders(Window previous)
        {
            InitializeComponent();
            _previous = previous;
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            _previous.Show();
            Close();
        }
    }
}
