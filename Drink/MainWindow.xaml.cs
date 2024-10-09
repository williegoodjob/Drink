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
        }

        //private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    var target = sender as TextBox;

        //    int amount;
        //    bool success = int.TryParse(target.Text, out amount);
        //    if (!success)
        //    {
        //        MessageBox.Show("請輸入數字");
        //        return;
        //    }
        //    else if (amount <= 0)
        //    {
        //        MessageBox.Show("請輸入正整數");
        //        return;
        //    }
        //    else
        //    {
        //        var targetStackPanel = target.Parent as StackPanel;
        //        var targetPrice = targetStackPanel.Children[0] as Label;
        //        var drinkName = targetPrice.Content.ToString();

        //        MessageBox.Show(drinkName + " " + amount + "杯，共" + drinks[drinkName] * amount + "元");
        //    }
        //}

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var target = sender as Slider;
            var targetStackPanel = target.Parent as StackPanel;
            var drink = targetStackPanel.Children[0] as Label;
            var drinkName = drink.Content.ToString();
            MessageBox.Show(drinkName + " " + target.Value + "杯，共" + drinks[drinkName] * target.Value + "元");

        }
    }
}