using EdbotClientAPI;
using EdbotClientAPI.Communication.Requests;
using EdbotClientAPI.Communication.Web;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace EdbotClientDemonstrator
{
    /// <summary>
    /// Interaction logic for EdbotDemonstrator.xaml
    /// </summary>
    public partial class EdbotDemonstrator : Window
    {
        private EdbotClient edBotClient;

        public EdbotDemonstrator()
        {
            InitializeComponent();
            EdbotOverviewImage.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "resources/edbot.png", UriKind.Absolute));

            IWebClient webClient = new WebClient("localhost", 8080, "/api/reporter/User/CSharp/2");
            edBotClient = new EdbotClient(webClient, new EdbotWebRequestFactory());
            edBotClient.Connected += OnConnected;
            edBotClient.Connect();
        }

        private void OnConnected(object sender, EventArgs e)
        {
            ServerStatusTextBox.Text = "Connected to server!";
        }

        private void LedsOnButton_Click(object sender, RoutedEventArgs e)
        {
            edBotClient.SetServoLED("Robotis Mini", "0/1");
        }

        private void LedsOffButton_Click(object sender, RoutedEventArgs e)
        {
            edBotClient.SetServoLED("Robotis Mini", "0/0");
        }
    }
}
