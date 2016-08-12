using System;
using System.Collections.Generic;
using System.IO;
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

        private XFactorUI xfui = null;

        private string _holdingFileName = Directory.GetCurrentDirectory() + "\\currentHoldings.xml";

        List<FScoreHolding> Holdings { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Holdings = new List<FScoreHolding>();
            UpdateHoldingStocks();
        }

        // update the list in the UI by reading the xml file
        private void UpdateHoldingStocks()
        {
            Holdings.Clear();
            List<FScoreHolding> holdings = FScoreHoldingXmlHelper.FromXmlFile<List<FScoreHolding>>(_holdingFileName);
            if (holdings != null)
            {
                Holdings = holdings;
            }

            // now populate the listbox
            listBox_holding.Items.Clear();
            foreach (FScoreHolding fsh in Holdings)
            {
                listBox_holding.Items.Add(fsh.StockTicker);
            }
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

            // load up the stock and analize
            XFactorHandler xfh = new XFactorHandler(stock);
            // display the data
            XFactorUI xfui = new XFactorUI(xfh);
            xfui.Show();
        }

        // refresh button
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (fscorelist == null) return;
            if (xfui == null) return;
        }

        private void button_addholding_Click(object sender, RoutedEventArgs e)
        {
            string addme = textBox_holding.Text;
            // eventually add stock checker and object filler

            FScoreHolding hold = new FScoreHolding();
            hold.StockName = addme;
            hold.StockTicker = addme;

            Holdings.Add(hold);

            FScoreHoldingXmlHelper.ToXmlFile(Holdings, _holdingFileName);

            UpdateHoldingStocks();
        }

        private void button_removeholding_Click(object sender, RoutedEventArgs e)
        {
            string toRemove = listBox_holding.SelectedValue.ToString();
            foreach(FScoreHolding fsh in Holdings)
            {
                if(fsh.StockTicker.Equals(toRemove))
                {
                    Holdings.Remove(fsh);
                    break; 
                }
            }
            FScoreHoldingXmlHelper.ToXmlFile(Holdings, _holdingFileName);
            UpdateHoldingStocks();
        }

        private void listBox_holding_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox box = (sender as ListBox);
            string stock = box.SelectedItem.ToString();

            // load up the stock and analize
            XFactorHandler xfh = new XFactorHandler(stock);
            // display the data
            XFactorUI xfui = new XFactorUI(xfh);
            xfui.Show();
        }
    }
}
