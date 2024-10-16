using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Drink
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string, int> drinks = new Dictionary<string, int>
        {
            {"紅茶大杯",60},
            {"紅茶小杯",40},
            {"綠茶大杯",50},
            {"綠茶小杯",30},
            {"可樂大杯",50},
            {"可樂小杯",30},
        };
        List<string> orderList = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            loadDrink();
        }
        private void loadDrink()
        {
            foreach (var drink in drinks)
            {
                var cp = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(3),
                    Background = Brushes.LightPink,
                    Height = 40
                };

                var cb = new CheckBox
                {
                    Content = drink.Key,
                    FontSize = 16,
                    FontWeight = FontWeights.Bold,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(5)
                };

                var slider = new Slider
                {
                    Width = 180,
                    Minimum = 0,
                    Maximum = 10,
                    Value = 0,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(50,5,5,5),
                    IsSnapToTickEnabled = true,
                    TickFrequency = 1
                };

                var num = new Label
                {
                    Content = "0",
                    FontSize = 16,
                    FontWeight = FontWeights.Bold,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(5)
                };

                num.SetBinding(Label.ContentProperty, new Binding("Value") { Source = slider });

                cp.Children.Add(cb);
                cp.Children.Add(slider);
                cp.Children.Add(num);
                drinkList.Children.Add(cp);
            }
        }
        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
        }
    }
}