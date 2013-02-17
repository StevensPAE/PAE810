using System;
using System.Collections.Generic;
using System.Linq;
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
using Nextbus1.Datamodel;

namespace NextBusWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //load agencies
            NextBus NB = new NextBus(true);

            //display agencies in dropdown
            Dropdown_Agencies.ItemsSource = NB.AgencyList;
            //http://stackoverflow.com/questions/3063320/combobox-adding-text-and-value-to-an-item-no-binding-source
            Dropdown_Agencies.DataContext = NB.AgencyList.ToString();
            Dropdown_Agencies.SelectedIndex = 0;
        }

        private void Dropdown_Agencies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //load routes for selected agency
            //Ask Gregor about better way to cast:
            Agency curAgency = (Agency) Dropdown_Agencies.SelectedItem;

            if (curAgency.busRoutes.Count == 0)
            {
                curAgency.getBusRoutes();
            }
            List_Routes.ItemsSource = curAgency.busRouteTitles;
            List_Routes.SelectedIndex = 0;
        }
    }
}
