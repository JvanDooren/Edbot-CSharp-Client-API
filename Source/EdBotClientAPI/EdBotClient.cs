namespace EdbotClientAPI
{
    using EdbotClientAPI.Communication.Requests;
    using EdbotClientAPI.Communication.Web;
    using NLog;
    using System;

    public class EdbotClient
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private readonly IWebClient client;
        private readonly IRequestFactory requestFactory;

        public event EventHandler Connected;
        public event EventHandler Disconnected;

        public EdbotClient(IWebClient client, IRequestFactory requestFactory)
        {
            this.client = client;
            client.Connected += OnWebClientConnected;
            client.Disconnected += OnWebClientDisconnected;
            this.requestFactory = requestFactory;
        }

        public void Connect()
        {
            this.client.Connect();
        }

        private void OnWebClientConnected(object sender, EventArgs e)
        {
            Connected?.Invoke(this, e);
        }

        private void OnWebClientDisconnected(object sender, EventArgs e)
        {
            Disconnected?.Invoke(this, e);
        }

        public void RunMotion(string edBotName, int motionNumber)
        {
            string request = requestFactory.CreateRunMotion(edBotName, motionNumber);
            APIRequest(request);
        }

        public void SetServoMode(string edBotName, string path)
        {
            string request = requestFactory.CreateSetServoMode(edBotName, path);
            APIRequest(request);
        }

        public void SetServoTorque(string edBotName, string path)
        {
            string request = requestFactory.CreateSetServoTorque(edBotName, path);
            APIRequest(request);
        }

        public void SetServoLED(string edBotName, string path)
        {
            string request = requestFactory.CreateSetServoLED(edBotName, path);
            APIRequest(request);
        }

        public void SetServoSpeed(string edBotName, string path)
        {
            string request = requestFactory.CreateSetServoSpeed(edBotName, path);
            APIRequest(request);
        }

        public void SetServoPosition(string edBotName, string path)
        {
            string request = requestFactory.CreateSetServoPosition(edBotName, path);
            APIRequest(request);
        }

        public void SetServoPID(string edBotName, string path)
        {
            string request = requestFactory.CreateSetServoPID(edBotName, path);
            APIRequest(request);
        }

        public void SetServoCombined(string edBotName, string path)
        {
            string request = requestFactory.CreateSetServoCombined(edBotName, path);
            APIRequest(request);
        }

        public void SetBuzzer(string edBotName, string path)
        {
            string request = requestFactory.CreateSetBuzzer(edBotName, path);
            APIRequest(request);
        }

        public void Say(string edBotName, string text)
        {
            string request = requestFactory.CreateSay(edBotName, text);
            APIRequest(request);
        }

        public void Reset(string edBotName)
        {
            string request = requestFactory.CreateReset(edBotName);
            APIRequest(request);
        }

        public void SetOptions(string edBotName, string path)
        {
            string request = requestFactory.CreateSetOptions(edBotName, path);
            APIRequest(request);
        }

        public void SetCustom(string edBotName, string path)
{
            string request = requestFactory.CreateSetCustom(edBotName, path);
            APIRequest(request);
        }

        private void APIRequest(string request)
        {
            client.Send(request);
        }
    }
}
