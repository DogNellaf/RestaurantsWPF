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
using WpfDrawing.Charts;

namespace ui.WorkerWorkspacePages
{
    /// <summary>
    /// Логика взаимодействия для Diagram.xaml
    /// </summary>
    public partial class Diagram : Window
    {
        private Window _previous;
        private Worker _worker;
        public Diagram(Window previous, Worker worker)
        {
            InitializeComponent();
            _previous = previous;
            _worker = worker;

            Chart chart = new LineChart();

            // Добавляем новую диаграмму на поле контейнера для графиков.
            GridForChart.Children.Add(chart.ChartBackground);

            // Принудительно обновляем размеры контейнера для графика.
            GridForChart.UpdateLayout();

            // Создаём график.
            CreateChart(chart);
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            _previous.Show();
            Close();
        }

        private static void CreateChart(Chart chart)
        {
            chart.Clear();

            Random random = new();

            for (int i = 0; i < random.Next(1, 25); i++)
            {
                chart.AddValue(random.Next(0, 2001));
            }
        }
    }
}
