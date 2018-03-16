using EdbotClientAPI;
using EdbotClientAPI.Communication.Requests;
using EdbotClientAPI.Communication.Web;
using System;
using System.Linq;
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

        public EdbotDemonstrator()
        {
            InitializeComponent();
            this.Closed += new EventHandler(EdbotDemonstrator_Closed);
            EdbotOverviewImage.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "resources/edbot.png", UriKind.Absolute));

            IWebClient webClient = new WebClient("localhost", 8080, "/api/reporter/User/CSharp/2");
            edBotClient = new EdbotClient(webClient, new EdbotWebRequestFactory());
            edBotClient.ListedEdbots += OnListedEdbots;
            edBotClient.ListedServoColours += OnListedColours;
            edBotClient.ListedMotions += OnListedMotions;
            edBotClient.Connect();
        }

        private void EdbotDemonstrator_Closed(object sender, EventArgs e)
        {
            edBotClient.ListedEdbots -= OnListedEdbots;
            edBotClient.ListedServoColours -= OnListedColours;
            edBotClient.ListedMotions -= OnListedMotions;
        }

        private void OnListedEdbots(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate {
                //check if names were added
                bool added = false;
                foreach (string name in edBotClient.ConnectedEdbotNames)
                {
                    if (!ConnectedEdbotsComboBox.Items.Contains(name))
                    {
                        added = true;
                        ConnectedEdbotsComboBox.Items.Add(name);
                    }
                }
                if (ConnectedEdbotsComboBox.Items.Count > 0 && ConnectedEdbotsComboBox.SelectedIndex == -1) ConnectedEdbotsComboBox.SelectedIndex = 0;
                if (added) ConnectedEdbotsComboBox.Items.Refresh();
            });
        }

        private void OnListedColours(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate {
                //check if names were added
                bool added = false;
                foreach (string name in edBotClient.EdbotServoColours.Keys.ToList())
                {
                    if (!ServoColoursComboBox.Items.Contains(name))
                    {
                        added = true;
                        ServoColoursComboBox.Items.Add(name);
                    }
                }
                if (ServoColoursComboBox.Items.Count > 0 && ServoColoursComboBox.SelectedIndex == -1) ServoColoursComboBox.SelectedIndex = 0;
                if (added) ServoColoursComboBox.Items.Refresh();
            });
        }

        private void OnListedMotions(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate {
                //check if names were added
                bool added = false;
                foreach (string name in edBotClient.EdbotMotions.Keys.ToList())
                {
                    if (!MotionsComboBox.Items.Contains(name))
                    {
                        added = true;
                        MotionsComboBox.Items.Add(name);
                    }
                }
                if (MotionsComboBox.Items.Count > 0 && MotionsComboBox.SelectedIndex == -1) MotionsComboBox.SelectedIndex = 0;
                if (added) MotionsComboBox.Items.Refresh();
            });
        }

        private void LedsOnButton_Click(object sender, RoutedEventArgs e)
        {
            if (ConnectedEdbotsComboBox.SelectedIndex < 0) return;
            if (ServoColoursComboBox.SelectedIndex < 0) return;

            int colourNumber = edBotClient.EdbotServoColours[ServoColoursComboBox.SelectedItem as string];
            edBotClient.SetServoLED(ConnectedEdbotsComboBox.SelectedItem as string, string.Format("0/{0}", colourNumber));
        }

        private void LedsOffButton_Click(object sender, RoutedEventArgs e)
        {
            if (ConnectedEdbotsComboBox.SelectedIndex < 0) return;

            int colourNumber = edBotClient.EdbotServoColours["Off"];
            edBotClient.SetServoLED(ConnectedEdbotsComboBox.SelectedItem as string, string.Format("0/{0}", colourNumber));
        }

        private void ActivateMotionButton_Click(object sender, RoutedEventArgs e)
        {
            if (ConnectedEdbotsComboBox.SelectedIndex < 0) return;
            if (MotionsComboBox.SelectedIndex < 0) return;

            int motionNumber = edBotClient.EdbotMotions[MotionsComboBox.SelectedItem as string];
            edBotClient.RunMotion(ConnectedEdbotsComboBox.SelectedItem as string, motionNumber);
        }

        private void SpeakButton_Click(object sender, RoutedEventArgs e)
        {
            if (ConnectedEdbotsComboBox.SelectedIndex < 0) return;
            if (string.IsNullOrWhiteSpace(SpeakTextBox.Text)) return;

            edBotClient.Say(ConnectedEdbotsComboBox.SelectedItem as string, SpeakTextBox.Text);
        }
    }
}
