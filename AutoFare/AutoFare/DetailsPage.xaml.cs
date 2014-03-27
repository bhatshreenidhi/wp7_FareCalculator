using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace AutoFare
{
    public partial class DetailsPage : PhoneApplicationPage
    {
        public DetailsPage()
        {
            InitializeComponent();

            LoadPage();
        }

        private void LoadPage()
        {
            txbSource.Text = SearchDetails.originAddress;
            txbDestination.Text = SearchDetails.destinationAddress;
            txtDist.Text = SearchDetails.distance;
            txtDuration.Text = SearchDetails.duration;
            txtTaxiFare.Text = SearchDetails.taxiFare;
            txtAutoFare.Text = SearchDetails.autoFare;
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/AboutPage.xaml", UriKind.Relative));
        }
    }
}