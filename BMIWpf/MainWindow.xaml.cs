#define RASPBERRY

using System;
using System.Windows;
using Windows.Devices.Gpio;
using Windows.Media.SpeechSynthesis;
using Windows.Foundation;

namespace BMIWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BMI test = new BMI();


        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = test;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = null;
            this.DataContext = test;
        }


    }


}

