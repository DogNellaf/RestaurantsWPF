using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ui.Helper;
using WpfDrawing.Charts;

//====================================================
// Описание работы классов и методов исходника на:
// https://www.interestprograms.ru
// Исходные коды программ и игр
//====================================================

namespace WpfDrawing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Diagram2 : Window
    {
        private Window _previous;
        public Diagram2(Window window)
        {
            InitializeComponent();
            _previous = window;
        }

        private void ChartsButton_Click(object sender, RoutedEventArgs e)
        {
            // Удаляем прежний график.
            GridForChart.Children.OfType<Canvas>().ToList().ForEach(p => GridForChart.Children.Remove(p));

            Chart chart = null;

            Button button = sender as Button;

            // Создаём новый график выбранного вида.
            switch (button.Name)
            {
                case "BarsButton":
                    if ((chart is BarChart) == false)
                    {
                        chart = new BarChart();
                    }

                    break;
                case "LineButton":
                    if ((chart is LineChart) == false)
                    {
                        chart = new LineChart();
                    }

                    break;
                case "PieButton":
                    if ((chart is PieChart) == false)
                    {
                        chart = new PieChart();
                    }

                    break;
            }

            // Добавляем новую диаграмму на поле контейнера для графиков.
            GridForChart.Children.Add(chart.ChartBackground);

            // Принудительно обновляем размеры контейнера для графика.
            GridForChart.UpdateLayout();

            // Создаём график.
            CreateChart(chart);

        }

        private static void CreateChart(Chart chart)
        {
            chart.Clear();

            var data = RequestClient.GetMonthData();
            foreach (var value in data)
            {
                chart.AddValue(value);
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            _previous.Show();
            Close();
        }
    }
}
