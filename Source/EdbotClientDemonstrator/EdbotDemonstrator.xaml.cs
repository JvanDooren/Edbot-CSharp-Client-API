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
                foreach (string name in edBotClient.ConnectedEdbotNames)
                {
                    ConnectedEdbotsComboBox.Items.Add(new ComboBoxItem() { Content = name });
                }
                if (ConnectedEdbotsComboBox.Items.Count > 0) ConnectedEdbotsComboBox.SelectedIndex = 0;
                ConnectedEdbotsComboBox.Items.Refresh();
            });

            edBotClient.ListedEdbots -= OnListedEdbots;
        }

        private void OnListedColours(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate {
                foreach (string name in edBotClient.EdbotServoColours.Keys.ToList())
                {
                    ServoColoursComboBox.Items.Add(new ComboBoxItem() { Content = name });
                }
                if (ServoColoursComboBox.Items.Count > 0) ServoColoursComboBox.SelectedIndex = 0;
                ServoColoursComboBox.Items.Refresh();
            });

            edBotClient.ListedServoColours -= OnListedColours;
        }

        private void OnListedMotions(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate {
                foreach (string name in edBotClient.EdbotMotions.Keys.ToList())
                {
                    MotionsComboBox.Items.Add(new ComboBoxItem() { Content = name });
                }
                if (MotionsComboBox.Items.Count > 0) MotionsComboBox.SelectedIndex = 0;
                MotionsComboBox.Items.Refresh();
            });

            edBotClient.ListedMotions -= OnListedMotions;
        }

        private void LedsOnButton_Click(object sender, RoutedEventArgs e)
        {
            if (ConnectedEdbotsComboBox.SelectedIndex < 0) return;
            if (ServoColoursComboBox.SelectedIndex < 0) return;

            int colourNumber = edBotClient.EdbotServoColours[(ServoColoursComboBox.SelectedItem as ListBoxItem).Content as string];
            edBotClient.SetServoLED((ConnectedEdbotsComboBox.SelectedItem as ListBoxItem).Content as string, string.Format("0/{0}", colourNumber));
        }

        private void LedsOffButton_Click(object sender, RoutedEventArgs e)
        {
            if (ConnectedEdbotsComboBox.SelectedIndex < 0) return;

            int colourNumber = edBotClient.EdbotServoColours["Off"];
            edBotClient.SetServoLED((ConnectedEdbotsComboBox.SelectedItem as ListBoxItem).Content as string, string.Format("0/{0}", colourNumber));
        }

        private void ActivateMotionButton_Click(object sender, RoutedEventArgs e)
        {
            if (ConnectedEdbotsComboBox.SelectedIndex < 0) return;
            if (MotionsComboBox.SelectedIndex < 0) return;

            int motionNumber = edBotClient.EdbotMotions[(MotionsComboBox.SelectedItem as ListBoxItem).Content as string];
            edBotClient.RunMotion((ConnectedEdbotsComboBox.SelectedItem as ListBoxItem).Content as string, motionNumber);
        }
    }
}
