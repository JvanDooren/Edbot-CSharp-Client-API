namespace EdbotClientAPI
{
    using EdbotClientAPI.Communication.Requests;
    using EdbotClientAPI.Communication.Web;
    using EdbotClientAPI.Communication.Web.Json;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using NLog;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using WebSocketSharp;

    public class EdbotClient
    {
        private static NLog.Logger logger = LogManager.GetCurrentClassLogger();

        private readonly IWebClient client;
        private readonly IRequestFactory requestFactory;

        public event EventHandler<EventArgs> Connected;
        public event EventHandler<CloseEventArgs> Disconnected;
        public event EventHandler<EventArgs> ListedEdbots;

        public List<string> ConnectedEdbotNames { private set; get; }

        private string Auth;

        [NonSerialized]
        private JsonSerializerSettings jsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver(),
            TypeNameHandling = TypeNameHandling.All
        };

        public EdbotClient(IWebClient client, IRequestFactory requestFactory)
        {
            this.client = client;
            client.Connected += OnWebClientConnected;
            client.Disconnected += OnWebClientDisconnected;
            client.OnMessage += OnWebClientMessage;
            this.requestFactory = requestFactory;

            ConnectedEdbotNames = new List<string>();
        }

        /// <summary>
        /// Connects to the Edbot server
        /// </summary>
        public void Connect()
        {
            client.Connect();
        }

        /// <summary>
        /// Called when the Edbot server connection was established
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWebClientConnected(object sender, EventArgs e)
        {
            Connected?.Invoke(this, e);
        }

        /// <summary>
        /// Called when the Edbot server connection was terminated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWebClientDisconnected(object sender, CloseEventArgs e)
        {
            Disconnected?.Invoke(this, e);
        }

        /// <summary>
        /// Called when a message is received from the Edbot server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWebClientMessage(object sender, MessageEventArgs e)
        {
            //"{\"edbots\":{\"Robotis Mini\":{\"connected\":true,\"reporters\":{\"servo-02\":{\"aligning\":false,\"load\":-0.8,\"torque\":false,\"position\":151.9,\"extended\":null},\"servo-13\":{\"aligning\":false,\"load\":0.0,\"torque\":false,\"position\":131.4,\"extended\":null},\"servo-03\":{\"aligning\":false,\"load\":-0.8,\"torque\":false,\"position\":68.0,\"extended\":null},\"servo-14\":{\"aligning\":false,\"load\":0.0,\"torque\":false,\"position\":149.9,\"extended\":null},\"servo-11\":{\"aligning\":false,\"load\":0.0,\"torque\":false,\"position\":144.6,\"extended\":null},\"servo-01\":{\"aligning\":false,\"load\":0.8,\"torque\":false,\"position\":141.9,\"extended\":null},\"servo-12\":{\"aligning\":false,\"load\":0.0,\"torque\":false,\"position\":145.7,\"extended\":null},\"servo-10\":{\"aligning\":false,\"load\":0.8,\"torque\":false,\"position\":117.6,\"extended\":null},\"motion-complete\":0,\"servo-08\":{\"aligning\":false,\"load\":0.0,\"torque\":false,\"position\":142.2,\"extended\":null},\"servo-09\":{\"aligning\":false,\"load\":0.0,\"torque\":false,\"position\":182.4,\"extended\":null},\"port3\":1021,\"servo-06\":{\"aligning\":false,\"load\":0.0,\"torque\":false,\"position\":142.5,\"extended\":null},\"port4\":296,\"servo-07\":{\"aligning\":false,\"load\":0.0,\"torque\":false,\"position\":148.7,\"extended\":null},\"port1\":462,\"servo-04\":{\"aligning\":false,\"load\":0.0,\"torque\":false,\"position\":244.0,\"extended\":null},\"servo-15\":{\"aligning\":false,\"load\":-0.8,\"torque\":false,\"position\":146.6,\"extended\":null},\"port2\":319,\"servo-05\":{\"aligning\":false,\"load\":-0.8,\"torque\":false,\"position\":157.2,\"extended\":null},\"servo-16\":{\"aligning\":false,\"load\":0.8,\"torque\":false,\"position\":137.5,\"extended\":null},\"speech-complete\":0,\"busy\":[]},\"activeUser\":\"Scratch <Doefus@[192.168.56.1]:0>\",\"motions\":{\"All\":[{\"name\":\"backward roll\",\"id\":24},{\"name\":\"bow 1\",\"id\":5},{\"name\":\"bow 2\",\"id\":6},{\"name\":\"break dance\",\"id\":40},{\"name\":\"break dance flip\",\"id\":41},{\"name\":\"crouch\",\"id\":3},{\"name\":\"forward roll\",\"id\":21},{\"name\":\"gangnam\",\"id\":42},{\"name\":\"get up\",\"id\":2},{\"name\":\"goalie block\",\"id\":34},{\"name\":\"goalie left\",\"id\":35},{\"name\":\"goalie right\",\"id\":36},{\"name\":\"goalie spread\",\"id\":37},{\"name\":\"head stand\",\"id\":38},{\"name\":\"initial position\",\"id\":1},{\"name\":\"karate left 1\",\"id\":25},{\"name\":\"karate left 2\",\"id\":27},{\"name\":\"karate right 1\",\"id\":26},{\"name\":\"karate right 2\",\"id\":28},{\"name\":\"left hook\",\"id\":11},{\"name\":\"left jab\",\"id\":10},{\"name\":\"left kick\",\"id\":30},{\"name\":\"left side kick\",\"id\":32},{\"name\":\"left uppercut\",\"id\":12},{\"name\":\"left wave\",\"id\":14},{\"name\":\"push\",\"id\":29},{\"name\":\"push up\",\"id\":23},{\"name\":\"right hook\",\"id\":8},{\"name\":\"right jab\",\"id\":7},{\"name\":\"right kick\",\"id\":31},{\"name\":\"right side kick\",\"id\":33},{\"name\":\"right uppercut\",\"id\":9},{\"name\":\"right wave\",\"id\":13},{\"name\":\"run forwards\",\"id\":39},{\"name\":\"sidestep left\",\"id\":18},{\"name\":\"sidestep right\",\"id\":17},{\"name\":\"sit up\",\"id\":22},{\"name\":\"stand\",\"id\":4},{\"name\":\"turn left\",\"id\":16},{\"name\":\"turn right\",\"id\":15},{\"name\":\"walk backwards\",\"id\":20},{\"name\":\"walk forwards\",\"id\":19}],\"Basic\":[{\"name\":\"crouch\",\"id\":3},{\"name\":\"get up\",\"id\":2},{\"name\":\"initial position\",\"id\":1},{\"name\":\"run forwards\",\"id\":39},{\"name\":\"sidestep left\",\"id\":18},{\"name\":\"sidestep right\",\"id\":17},{\"name\":\"stand\",\"id\":4},{\"name\":\"turn left\",\"id\":16},{\"name\":\"turn right\",\"id\":15},{\"name\":\"walk backwards\",\"id\":20},{\"name\":\"walk forwards\",\"id\":19}],\"Sport\":[{\"name\":\"goalie block\",\"id\":34},{\"name\":\"goalie left\",\"id\":35},{\"name\":\"goalie right\",\"id\":36},{\"name\":\"goalie spread\",\"id\":37},{\"name\":\"left kick\",\"id\":30},{\"name\":\"left side kick\",\"id\":32},{\"name\":\"right kick\",\"id\":31},{\"name\":\"right side kick\",\"id\":33}],\"Greet\":[{\"name\":\"bow 1\",\"id\":5},{\"name\":\"bow 2\",\"id\":6},{\"name\":\"left wave\",\"id\":14},{\"name\":\"right wave\",\"id\":13}],\"Dance\":[{\"name\":\"break dance\",\"id\":40},{\"name\":\"break dance flip\",\"id\":41},{\"name\":\"gangnam\",\"id\":42}],\"Gym\":[{\"name\":\"backward roll\",\"id\":24},{\"name\":\"forward roll\",\"id\":21},{\"name\":\"head stand\",\"id\":38},{\"name\":\"push up\",\"id\":23},{\"name\":\"sit up\",\"id\":22}],\"Fight\":[{\"name\":\"karate left 1\",\"id\":25},{\"name\":\"karate left 2\",\"id\":27},{\"name\":\"karate right 1\",\"id\":26},{\"name\":\"karate right 2\",\"id\":28},{\"name\":\"left hook\",\"id\":11},{\"name\":\"left jab\",\"id\":10},{\"name\":\"left uppercut\",\"id\":12},{\"name\":\"push\",\"id\":29},{\"name\":\"right hook\",\"id\":8},{\"name\":\"right jab\",\"id\":7},{\"name\":\"right uppercut\",\"id\":9}]},\"type\":\"ERM161\",\"enabled\":true,\"colours\":{\"red\":1,\"magenta\":5,\"green\":2,\"blue\":4,\"white\":7,\"yellow\":3,\"cyan\":6,\"off\":0}}},\"auth\":\"67002ddb-d0ff-41bf-8d91-05f2670d9cd8\",\"user\":\"CSharp <User@[192.168.56.1]:54998>\"}"
            //"{\"users\":[\"CSharp <User@[192.168.56.1]:50548>\",\"Scratch <Doefus@[192.168.56.1]:0>\"]}"
            //"{\"edbots\":{\"Robotis Mini\":{\"reporters\":{\"servo-02\":{\"aligning\":false,\"load\":0.8,\"torque\":false,\"position\":151.9,\"extended\":null}}}}}"
            //"{\"edbots\":{\"Robotis Mini\":{\"reporters\":{\"servo-03\":{\"aligning\":false,\"load\":0.8,\"torque\":false,\"position\":68.0,\"extended\":null}}}}}"
            try
            {
                EdbotServerMessage edbotMessage = JsonConvert.DeserializeObject<EdbotServerMessage>(e.Data, jsonSettings);
                if (edbotMessage == null) return;
                UpdateAuth(edbotMessage);
                UpdateConnectedEdBotNamesList(edbotMessage);
            }
            catch (Exception ex)
            {
                logger.Debug(ex);
            }
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
            WebHeaderCollection customheader = new WebHeaderCollection
            {
                { "X-Edbot-Auth", Auth }
            };
            client.Send(customheader, request);
        }

        private void UpdateAuth(EdbotServerMessage edbotMessage)
        {
            if (edbotMessage.Auth != null) Auth = edbotMessage.Auth;
        }

        private void UpdateConnectedEdBotNamesList(EdbotServerMessage edbotMessage)
        {
            if (edbotMessage.Edbots != null && edbotMessage.Edbots.Count > 0)
            {
                foreach (string name in edbotMessage.Edbots.Keys)
                {
                    if (!ConnectedEdbotNames.Contains(name)) ConnectedEdbotNames.Add(name);
                }
            }

            ListedEdbots?.Invoke(this, EventArgs.Empty);
        }
    }
}
