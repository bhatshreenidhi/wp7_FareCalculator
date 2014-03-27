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
using System.Xml.Linq;

namespace AutoFare
{
    public partial class MainPage : PhoneApplicationPage
    {
        public bool _specialCharSource = false;
        public bool _specialCharDest = false;
        public bool _Check = false;
        public bool _blank = false;
        public bool _blankSource = false;
        public bool _blankDest = false;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            getCitiesList();

        }

        private void getCitiesList()
        {
            List<Cities> source = new List<Cities>();
            source.Add(new Cities() { Name = "Bengaluru", city = Util.City.bangalore, Country = "India" });
            source.Add(new Cities() { Name = "Mumbai", city = Util.City.mumbai, Country = "India" });
            source.Add(new Cities() { Name = "New Delhi", city = Util.City.delhi, Country = "India" });
            source.Add(new Cities() { Name = "Nagpur", city = Util.City.nagpur, Country = "India" });
            source.Add(new Cities() { Name = "Ahemadabad", city = Util.City.ahemadabad, Country = "India" });
            source.Add(new Cities() { Name = "Chandighar", city = Util.City.chandighar, Country = "India" });
            source.Add(new Cities() { Name = "Coimbatore", city = Util.City.coimbatore, Country = "India" });
            source.Add(new Cities() { Name = "Dharwad", city = Util.City.dharwad, Country = "India" });
            source.Add(new Cities() { Name = "Gurgaon", city = Util.City.gurgon, Country = "India" });
            source.Add(new Cities() { Name = "Hyderabad", city = Util.City.hyderabad, Country = "India" });
            source.Add(new Cities() { Name = "Indore", city = Util.City.indore, Country = "India" });
            source.Add(new Cities() { Name = "Pune", city = Util.City.pune, Country = "India" });
            source.Add(new Cities() { Name = "Mangalore", city = Util.City.mangalore, Country = "India" });
            source.Add(new Cities() { Name = "Kolkata", city = Util.City.kolkata, Country = "India" });
            source.Add(new Cities() { Name = "Trivandrum", city = Util.City.trivandrum, Country = "India" });
            this.listPicker.ItemsSource = source;

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            _specialCharSource = false;
            _specialCharDest = false;
            _Check = false;
            _blank = false;
            _blankSource = false;
            _blankDest = false;

            prgBar.Visibility = Visibility.Visible;
            Cities selectedCity = (Cities)this.listPicker.SelectedItem;
            Util.selectedCity = selectedCity.city;
            Util.selectedCountry = selectedCity.Country;
            Util.mode = Util.SelectedMode.auto;

            if (isSelected() && validateFields())
            {
                getFareDetails();

            }
            else
            {
                if (_Check)
                    MessageBox.Show("Please select Auto Fare or Taxi fare");
                else
                {
                    if (_blank)
                    {
                        if (_blankSource)
                            MessageBox.Show("Please enter Source address");
                        else if (_blankDest)
                            MessageBox.Show("Please enter Destination address");
                        else
                            MessageBox.Show("Please select Auto Fare or Taxi fare");

                    }
                    else
                    {
                        if (_specialCharSource)
                            MessageBox.Show("Please enter valid Source address");
                        else if (_specialCharDest)
                            MessageBox.Show("Please enter valid Destination address");

                    }
                }
            }
        }

        private bool validateFields()
        {
            if (txbSource.Text.Trim().Equals(string.Empty))
            {
                _blank = true;
                _blankSource = true;
                return false;
            }
            if (txbDestination.Text.Trim().Equals(string.Empty))
            {
                _blank = true;
                _blankDest = true;
                return false;
            }
            else
            {
                if (txbSource.Text.Trim().Contains('!') || txbSource.Text.Trim().Contains('$') || txbSource.Text.Trim().Contains('%') || txbSource.Text.Trim().Contains('^') || txbSource.Text.Trim().Contains('&') || txbSource.Text.Trim().Contains('*'))
                {
                    _specialCharSource = true;
                    return false;
                }
                else if (txbDestination.Text.Trim().Contains('!') || txbDestination.Text.Trim().Contains('$') || txbDestination.Text.Trim().Contains('%') || txbDestination.Text.Trim().Contains('^') || txbDestination.Text.Trim().Contains('&') || txbDestination.Text.Trim().Contains('*'))
                {

                    _specialCharDest = true;
                    return false;
                }
                return true;
            }
        }

        private bool isSelected()
        {
            if ((bool)this.chkbxAuto.IsChecked)
                return true;
            else if ((bool)this.chkbxTaxi.IsChecked)
                return true;
            else
            {
                _Check = true;
                return false;
            }
        }

        private void getFareDetails()
        {
            string webUrl = "http://maps.googleapis.com/maps/api/distancematrix/xml?origins={0}&destinations={1}&mode={2}&language=en&sensor=false";
            string origin = txbSource.Text;
            string dest = txbDestination.Text;
            string mode = "driving";

            string originString = origin + Util.COMMA + Util.selectedCity + Util.COMMA + Util.selectedCountry;
            string destString = dest + Util.COMMA + Util.selectedCity + Util.COMMA + Util.selectedCountry;

            webUrl = string.Format(webUrl, originString, destString, mode);

            WebClient request = new WebClient();

            request.DownloadStringCompleted += new DownloadStringCompletedEventHandler(request_DownloadStringCompleted);
            request.DownloadStringAsync(new Uri(webUrl));
        }

        void request_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            prgBar.Visibility = Visibility.Collapsed;

            try
            {
                string strResult = e.Result;
                double distance = 0.0;

                XElement document = XElement.Parse(strResult);

                if (document.Element("status").Value.ToString().Equals("OK"))
                {

                    string sourceAddress = document.Element("origin_address").Value.ToString();
                    string destinationAddress = document.Element("destination_address").Value.ToString();

                    if (!(sourceAddress == null || sourceAddress.Trim().Equals(string.Empty)) && !(destinationAddress == null || destinationAddress.Trim().Equals(string.Empty)))
                    {
                        string dist = document.Element("row").Element("element").Element("distance").Element("value").Value.ToString();
                        string duration = document.Element("row").Element("element").Element("duration").Element("text").Value.ToString();


                        distance = double.Parse(dist) / 1000;

                        SearchDetails.destinationAddress = destinationAddress;
                        SearchDetails.originAddress = sourceAddress;
                        SearchDetails.duration = "Approximate Duration (mins) : " + duration;
                        SearchDetails.distance = "Distance (km) : " + distance.ToString();

                        if ((bool)this.chkbxAuto.IsChecked)
                        {
                            Util.selectAutoRates(Util.selectedCity);
                            SearchDetails.autoFare = "Auto Fare (INR) : " + Util.getAutoFare(distance, Util.selectedCity).ToString();
                        }
                        else
                        {
                            SearchDetails.autoFare = "";
                        }

                        if ((bool)this.chkbxTaxi.IsChecked)
                        {
                            Util.selectTaxiRates(Util.selectedCity);
                            SearchDetails.taxiFare = "Taxi Fare (INR) : " + Util.getTaxiFare(distance, Util.selectedCity).ToString();
                        }
                        else
                        {
                            SearchDetails.taxiFare = "";
                        }


                        NavigationService.Navigate(new Uri("/DetailsPage.xaml", UriKind.Relative));
                    }

                    else
                    {
                        MessageBox.Show("Could not retrive details for source and destination given");
                    }

                }
                else
                    MessageBox.Show("Could not retrive details for source and destination given");

            }
            catch (WebException)
            {
                MessageBox.Show("Unable to connect to internet...!!! Make sure that connectivity exists");
            }


        }


        public class Cities
        {
            public string Name
            {
                get;
                set;
            }

            public Util.City city
            {
                get;
                set;
            }

            public string Country
            {
                get;
                set;
            }

            public string Currency
            {
                get;
                set;
            }
        }
    }
}

