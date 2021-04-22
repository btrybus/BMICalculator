using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BMIUwp
{
    public class BMI : INotifyPropertyChanged
    {
        double masa = 75;
        double wzrost = 178;

        public double Masa
        {
            get
            {
                return masa;
            }

            set
            {
                masa = value;
                OnPropertyChanged("Masa");
                OnPropertyChanged("Opis");
            }
        }

        public double Wzrost
        {
            get
            {
                return wzrost;
            }

            set
            {
                wzrost = value;
                OnPropertyChanged("Wzrost");
                OnPropertyChanged("Opis");
            }
        }

        public string Opis
        {
            get
            {
                try
                {
                    string opis = "OK :)";

                    double BMI = masa / ((wzrost / 100.0) * (wzrost / 100.0));

                    if (BMI < 18.5)
                        opis = "niedowaga";
                    else if (BMI > 30)
                        opis = "otyłość";
                    else if (BMI >= 25)
                        opis = "nadwaga";

                    return opis;
                }
                catch (FormatException)
                {
                    return "Błędne dane !";
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;

            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(property);
                handler(this, e);
            }
        }


    }
}
