using EdbotClientAPI;
using EdbotClientAPI.Communication.Requests;
using EdbotClientAPI.Communication.Web;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace EdbotClientDemonstrator
{
    /// <summary>
    /// Interaction logic for EdbotDemonstrator.xaml
    /// </summary>
    public partial class EdbotDemonstrator : Window
    {
        private EdbotClient edBotClient;

        private string selectedEdbot;

        public EdbotDemonstrator()
        {
            InitializeComponent();
            this.Closed += new EventHandler(EdbotDemonstrator_Closed);
            EdbotOverviewImage.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "resources/edbot.png", UriKind.Absolute));

            IWebClient webClient = new WebClient("localhost", 8080, "/api/reporter/User/CSharp/2");
            edBotClient = new EdbotClient(webClient, new EdbotWebRequestFactory());
            edBotClient.ListedEdbots += OnListedEdbots;
            edBotClient.Connect();
        }

        private void EdbotDemonstrator_Closed(object sender, EventArgs e)
        {
            edBotClient.ListedEdbots -= OnListedEdbots;
        }

        private void OnListedEdbots(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate {
                foreach (string name in edBotClient.ConnectedEdbotNames)
                {
                    ConnectedEdbots.Items.Add(new ListBoxItem() { Content = name });
                }
                if (ConnectedEdbots.Items.Count > 0) ConnectedEdbots.SelectedIndex = 0;
                ConnectedEdbots.Items.Refresh();
                selectedEdbot = (ConnectedEdbots.SelectedItem as ListBoxItem).Content as string;
            });
        }

        private void LedsOnButton_Click(object sender, RoutedEventArgs e)
        {
            edBotClient.SetServoLED(selectedEdbot, "0/1");
        }

        private void LedsOffButton_Click(object sender, RoutedEventArgs e)
        {
            edBotClient.SetServoLED(selectedEdbot, "0/0");
        }

        private void Edbots_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            selectedEdbot = (ConnectedEdbots.SelectedItem as ListBoxItem).Content as string;
        }
    }
}
