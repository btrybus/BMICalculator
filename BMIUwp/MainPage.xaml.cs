using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Gpio;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BMIUwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        BMI test = new BMI();

        bool isRaspberry = GpioController.GetDefault() != null;

        Windows.Devices.Gpio.GpioPin[] buttons;
        int[] buttonpins = new int[] { 17, 27, 22, 26 };

        public MainPage()
        {
            this.InitializeComponent();

            if (isRaspberry && GpioController.GetDefault().PinCount > 0)
            {

                buttons = new GpioPin[buttonpins.Length];
                for (int i = 0; i < buttonpins.Length; i++)
                {
                    buttons[i] = GpioController.GetDefault().OpenPin(buttonpins[i]);

                    if (buttons[i].IsDriveModeSupported(GpioPinDriveMode.InputPullUp))
                        buttons[i].SetDriveMode(GpioPinDriveMode.InputPullUp);
                    else
                        buttons[i].SetDriveMode(GpioPinDriveMode.Input);

                    buttons[i].DebounceTimeout = TimeSpan.FromMilliseconds(50);
                    buttons[i].ValueChanged += MainPage_ValueChanged;

                }
            }

            this.DataContext = test;
        }
  
        private async void MainPage_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
        {

            int buttonIdx = Array.FindIndex(buttons, b => b.PinNumber == sender.PinNumber);

            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                switch (buttonIdx)
                {
                    case 0:
                        test.Masa++;
                        break;
                    case 1:
                        test.Masa--;
                        break;
                    case 2:
                        test.Wzrost++;
                        break;
                    case 3:
                        test.Wzrost--;
                        break;
                }
                Powiedz(test.Opis);
            });
        }

        SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = null;
            this.DataContext = test;

           
        }

        private async void Powiedz(string message)
        {
            var stream = await speechSynthesizer.SynthesizeTextToStreamAsync(message);
            media.SetSource(stream, stream.ContentType);
            media.Play();
        }
    }
}
