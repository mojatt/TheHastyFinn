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

namespace TheHastyFinn
{
    /// <summary>
    /// Interaction logic for XFactorUI.xaml
    /// </summary>
    public partial class XFactorUI : Window
    {
        public XFactorHandler XFH { get; set; }

        public XFactorUI(XFactorHandler xfh)
        {
            InitializeComponent();

            XFH = xfh;

            this.DataContext = XFH.xFactorGraphModel;

            UpdatePeriodBox();
        }

        private void UpdatePeriodBox()
        {
            foreach (int period in XFH.xFactor.Periods)
            {
                listBox_Periods.Items.Add(period);
            }
        }

        private void listBox_Periods_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            XFH.xFactorGraphModel.PeriodsToPlot.Clear();

            foreach (Object item in listBox_Periods.SelectedItems)
            {
                int period = Convert.ToInt32(item);
                XFH.xFactorGraphModel.PeriodsToPlot.Add(period);
            }

            XFH.xFactorGraphModel.UpdateModel();
            this.BotPlot.InvalidatePlot(true);
        }
    }
}
