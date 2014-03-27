using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace AutoFare
{
    public static class Util
    {
        public static City selectedCity;
        public static string selectedCountry;
        public static string COMMA = ",";
        public static SelectedMode mode;

        public static double minDist, minFare, perKMCharge;
        public static double taxiMinDist, taxiMinFare, taxiPerKMCharge;

        public enum SelectedMode
        {
          auto,taxi  
        };

        public enum City
        {
            delhi, bangalore, mumbai, pune, ahemadabad, chandighar, coimbatore, hyderabad, dharwad, indore, gurgon, nagpur, mangalore,trivandrum,kolkata
        };


        public static double getAutoFare(double distance, Util.City selectedCity)
        {
            double value, fare;
            if (distance >= 0.0 && distance <= minDist)
                return minFare;
            else
            {
                value = distance - minDist;
                fare = value * perKMCharge;
                return (minFare + fare);
            }
        }

        public static double getTaxiFare(double distance, Util.City selectedCity)
        {
            double value, fare;
            if (distance >= 0.0 && distance <= taxiMinDist)
                return taxiMinFare;
            else
            {
                value = distance - taxiMinDist;
                fare = value * taxiPerKMCharge;
                return (taxiMinFare + fare);
            }
        }

        public static void selectTaxiRates(Util.City selectedCity)
        {
            switch (selectedCity)
            {
                case Util.City.delhi: taxiMinDist = 1.0;
                    taxiMinFare = 20.0;
                    taxiPerKMCharge = 11.0;
                    break;

                case Util.City.mumbai: taxiMinDist = 1.6;
                    taxiMinFare = 16.0;
                    taxiPerKMCharge = 10.0;
                    break;

                case Util.City.trivandrum: taxiMinDist = 3.0;
                    taxiMinFare = 60.0;
                    taxiPerKMCharge = 8.0;
                    break;
                case Util.City.kolkata: taxiMinDist = 2.0;
                    taxiMinFare = 22.0;
                    taxiPerKMCharge = 10.0;
                    break;

                default: taxiMinDist = 0;
                    taxiMinFare = 0;
                    taxiPerKMCharge = 0;
                    break;

            }
        }

        public static void selectAutoRates(Util.City selectedCity)
        {
            switch (selectedCity)
            {
                case Util.City.mangalore: minDist = 1.5;
                    minFare = 17.0;
                    perKMCharge = 12.0;
                    break;
                case Util.City.bangalore: minDist = 2.0;
                    minFare = 17.0;
                    perKMCharge = 9.0;
                    break;
                case Util.City.delhi: minDist = 2.0;
                    minFare = 19.0;
                    perKMCharge = 6.5;
                    break;
                case Util.City.pune: minDist = 1.0;
                    minFare = 11.0;
                    perKMCharge = 10.0;
                    break;
                case Util.City.mumbai: minDist = 1.6;
                    minFare = 11.0;
                    perKMCharge = 7.0;
                    break;
                case Util.City.ahemadabad: minDist = 1.4;
                    minFare = 11.0;
                    perKMCharge = 7.5;
                    break;
                case Util.City.chandighar: minDist = 2.0;
                    minFare = 20.0;
                    perKMCharge = 6.5;
                    break;
                case Util.City.coimbatore: minDist = 2.0;
                    minFare = 14.0;
                    perKMCharge = 7.0;
                    break;
                case Util.City.hyderabad: minDist = 1.5;
                    minFare = 14.0;
                    perKMCharge = 8.0;
                    break;
                case Util.City.indore: minDist = 1.0;
                    minFare = 12.0;
                    perKMCharge = 9.0;
                    break;
                case Util.City.dharwad: minDist = 1.6;
                    minFare = 15.0;
                    perKMCharge = 10.0;
                    break;
                case Util.City.gurgon: minDist = 2.0;
                    minFare = 20.0;
                    perKMCharge = 6.5;
                    break;

                case Util.City.nagpur: minDist = 1.0;
                    minFare = 11.0;
                    perKMCharge = 8.0;
                    break;



            }
        } 
    }
}
