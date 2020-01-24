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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ICE3StarterCode
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            if(!((bool)rdoLinear.IsChecked || (bool)rdoDB.IsChecked))
            {
                MessageBox.Show("Please select a radio button");
            }
            else if (txtConvert.Text.Equals(""))
            {
                MessageBox.Show("Please enter a value to convert");
            }
            else if((bool)rdoDB.IsChecked)
            {
                LinearToDB();
            }
            else if ((bool)rdoLinear.IsChecked)
            {
                DBToLinear();
            }
        }

        private void LinearToDB()
        {
            double dB = 0;
            double SNR = Convert.ToDouble(txtConvert.Text);
            dB = Math.Round(10 * Math.Log10(SNR),4);
            String txt = $"{dB}dB";
            txtConvertOutput.Text = txt;
        }

        private void DBToLinear()
        {
            double SNR = 0;
            double db = Convert.ToDouble(txtConvert.Text);
            SNR = Math.Round(Math.Pow(10.0, (db / 10.0)),4);
            String txt = $"{SNR}";
            txtConvertOutput.Text = txt;
        }

        private void btnComputeNoise_Click(object sender, RoutedEventArgs e)
        {
            double temp = Convert.ToDouble(txtTemperature.Text);
            if(cboTemperatureUnit.SelectedIndex == 1)
            {
                temp = CTOK(temp);
            }
            else if(cboTemperatureUnit.SelectedIndex == 2)
            {
                temp = FTOK(temp);
            }
            calcNoiseInDb(temp);
        }

        private void calcNoiseInDb(double t)
        {
            double k = -228.6;
            double bandwidth = Convert.ToDouble(txtBandwidth.Text)*1000000;

            txtNoiseOutput.Text = Math.Round(((k + (10 * Math.Log10(t)) + (10 * Math.Log10(bandwidth)))),4).ToString();
        }

        private double CTOK(double temp)
        {
            return temp + 273.15;
        }

        private double FTOK(double temp)
        {
            return CTOK((temp - 32) * (5 / 9));
        }
    }
}
