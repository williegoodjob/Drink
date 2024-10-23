using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using CsvHelperApplication;
using System.Diagnostics;
using Microsoft.Win32;

namespace Drink
{
    public partial class MainWindow : Window
    {
        Dictionary<string, int> drinks = new Dictionary<string, int>
        {
            //{"紅茶大杯",60},
            //{"紅茶小杯",40},
            //{"綠茶大杯",50},
            //{"綠茶小杯",30},
            //{"可樂大杯",50},
            //{"可樂小杯",30},
            //{"咖啡大杯",80},
            //{"咖啡小杯",50},
        };

        Dictionary<string, int> orders = new Dictionary<string, int>();
        string takeout = "";

        public MainWindow()
        {
            InitializeComponent();
            //CsvInit("D:\\DevProgram\\VS\\source\\Drink\\Drink\\drinkitem.csv", drinks);
            openSourceFile(drinks);
            loadDrink();
        }

        private void openSourceFile(Dictionary<string, int> drinks)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv|Text files (*.txt)|*.txt|All files|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                CsvInit(openFileDialog.FileName, drinks);
            }

        }
        private void CsvInit(string fileName, Dictionary<string, int> drinks)
        {
            try
            {
                var readConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false
                };
                using (var reader = new StreamReader(fileName))
                using (var csv = new CsvReader(reader, readConfiguration))
                {
                    var records = csv.GetRecords<DrinksFormat>();
                    foreach (var D in records)
                    {
                        drinks.Add(D.Name, D.Price);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void loadDrink()
        {
            drinkList.Height = 46 * drinks.Count;
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

                var lb = new Label
                {
                    Content = $"{drink.Value}元",
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
                    Margin = new Thickness(50, 5, 5, 5),
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
                cp.Children.Add(lb);
                cp.Children.Add(slider);
                cp.Children.Add(num);
                drinkList.Children.Add(cp);
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var rb = sender as RadioButton;
            if (rb.IsChecked == true)
            {
                takeout = rb.Content.ToString();
            }
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < drinkList.Children.Count; i++)
            {
                var cp = drinkList.Children[i] as StackPanel;
                var cb = cp.Children[0] as CheckBox;
                var slider = cp.Children[2] as Slider;
                var amount = (int)slider.Value;

                if (cb.IsChecked == true && slider.Value > 0)
                {
                    if (orders.ContainsKey(cb.Content.ToString()))
                    {
                        orders[cb.Content.ToString()] = amount;
                    }
                    else
                    {
                        orders.Add(cb.Content.ToString(), amount);
                    }
                }
            }
            GenerateOutputList();
        }

        private void GenerateOutputList()
        {
            int total = 0;
            var result = new StringBuilder();
            result.Append($"內用/外帶：{takeout}\n");
            result.Append("訂購清單:\n");
            result.Append("==========\n");

            foreach (var order in orders)
            {
                result.Append($"{order.Key} x {order.Value}\n");
                total += drinks[order.Key] * order.Value;
            }
            //增加一個分隔線
            result.Append("==========\n");
            result.Append($"小計：{total}元\n");
            result.Append("優惠項目:\n");
            result.Append("----------------\n");
            int sellPrice = total;
            if (total >= 500)
            {
                sellPrice = (int)(total * 0.8);
                result.Append("滿500元打8折\n");
            }
            else if (total >= 300)
            {
                sellPrice = (int)(total * 0.9);
                result.Append("滿300元打9折\n");
            }
            else
            {
                result.Append("無折扣\n");
            }
            result.Append("----------------\n");
            result.Append($"應付:{sellPrice}\n");

            ResultTextBlock.Text = result.ToString();
        }
    }
}