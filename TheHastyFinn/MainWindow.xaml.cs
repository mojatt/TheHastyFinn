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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TheHastyFinn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FScore fscorelist;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            fscorelist = new FScore();

            foreach(FScoreStock stock in fscorelist.StockList)
            {
                listBox.Items.Add(stock.Ticker);
            }
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (fscorelist == null) return;

            ListBox box = (sender as ListBox);
            string stock = box.SelectedItem.ToString();

            XFactor xf = new XFactor();
            xf.GenPoints(stock);


        }
    }
}
