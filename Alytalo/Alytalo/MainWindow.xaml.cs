using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
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
using System.Windows.Threading;

namespace Alytalo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Lights kitchen = new Lights();
        Lights livingRoom = new Lights();
        Thermostat thermostat = new Thermostat();
        Sauna sauna;
        public DispatcherTimer HeatingTimer = new DispatcherTimer();
        public DispatcherTimer CooldownTimer = new DispatcherTimer();
        SpeechSynthesizer synthesizer = new SpeechSynthesizer();

        public MainWindow()
        {
            InitializeComponent();
            thermostat.Temperature = 20;
            sauna = new Sauna(thermostat);
            sauna.SaunaMaxTemp = 80;
            txtCurrentTemp.Text = thermostat.Temperature.ToString();
            txtSaunaTemp.Text = thermostat.Temperature.ToString();
            txtSaunaMax.Text = sauna.SaunaMaxTemp.ToString();

            HeatingTimer.Tick += HeatingTimer_Tick;
            HeatingTimer.Interval = new TimeSpan(0, 0, 1);
            CooldownTimer.Tick += CooldownTimer_Tick;
            CooldownTimer.Interval = new TimeSpan(0, 0, 1);
        }

        private void HeatingTimer_Tick(object sender, EventArgs e)
        {
            if (sauna.SaunaTemperature < sauna.SaunaMaxTemp)
            {
                btnSaunaOn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else
            {
                HeatingTimer.Stop();
            }
        }

        private void CooldownTimer_Tick(object sender, EventArgs e)
        {
            if (sauna.SaunaTemperature > thermostat.Temperature) 
            { 
                btnSaunaOff.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else 
            { 
                CooldownTimer.Stop(); 
            }
        }

        private void slLivingRoom_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            livingRoom.Dimmer = e.NewValue.ToString();
            if (livingRoom.Switched == true)
            {
                btnLight1.Background = Brushes.Yellow;
            }
            else
            {
                btnLight1.Background = Brushes.Gray;
            }
        }

        private void slKitchen_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            kitchen.Dimmer = e.NewValue.ToString();
            if (kitchen.Switched == true)
            {
                btnLight2.Background = Brushes.Yellow;
            }
            else
            {
                btnLight2.Background = Brushes.Gray;
            }
        }

        private void btnSetTemp_Click(object sender, RoutedEventArgs e)
        {
            Boolean ok = true;
            try
            {
                thermostat.SetTemperature(int.Parse(txtNewTemp.Text));
                txtCurrentTemp.Text = txtNewTemp.Text.ToString();
                txtSaunaTemp.Text = txtCurrentTemp.Text.ToString();
            }
            catch (Exception) 
            {
                MessageBox.Show("Lämpötilan täytyy olla välillä 15-30 C");
                ok = false;
            }
            
            if (ok == true) 
            {
                txtNewTemp.Text = "";
            }
            sauna.SaunaTemperature = thermostat.Temperature;
        }

        private void btnSaunaOn_Click(object sender, RoutedEventArgs e)
        {
            sauna.SaunaOn();
            if (sauna.SaunaSwitched == true)
            {
                btnSaunaIndicator.Background = Brushes.Green;
                txtSaunaStatus.Text = "Sauna päällä";
            
                if (!HeatingTimer.IsEnabled) 
                { 
                    HeatingTimer.Start();
                }
                
                txtSaunaTemp.Text = sauna.SaunaTemperature.ToString();
            }
        }

        private void btnSaunaOff_Click(object sender, RoutedEventArgs e)
        {
            HeatingTimer.Stop();

            sauna.SaunaOff();
            if (sauna.SaunaSwitched == false) 
            {
                btnSaunaIndicator.Background = Brushes.Gray;
                txtSaunaStatus.Text = "";
                if (!CooldownTimer.IsEnabled) 
                { 
                   CooldownTimer.Start ();
                }
            }
            txtSaunaTemp.Text = sauna.SaunaTemperature.ToString();
        }

        private void btnSpeech_Click(object sender, RoutedEventArgs e)
        {
            string saunaStatus;
            if (sauna.SaunaSwitched == true)
            {
                saunaStatus = "Sauna is on";
            }
            else
            {
                saunaStatus = "Sauna is not on";
            }

            synthesizer.SpeakAsyncCancelAll();
            string text = "Living room lights, " + txtLivingRoomLights.Text + "percent, " + "kitchen lights, " + txtKitchenLights.Text + "percent, " + "\n" +
            "temperature" + txtCurrentTemp.Text + "degrees, " + saunaStatus;
            string speak = text;
            synthesizer.SpeakAsync(speak);
        }
    }
}
