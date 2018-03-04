namespace EdbotClientAPI.Communication.Web
{
    using EdbotClientAPI.Communication.Web.Json;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using NLog;
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using WebSocketSharp;

    public class WebClient : IDisposable, IWebClient
    {
        private static NLog.Logger logger = LogManager.GetCurrentClassLogger();

        private readonly WebSocket clientConnection;
        private string server;
        private int port;

        private string Auth;

        [NonSerialized]
        private JsonSerializerSettings jsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver(),
            TypeNameHandling = TypeNameHandling.All
        };

        public WebClient(string server, int port, string path)
        {
            if (string.IsNullOrEmpty(server)) throw new ArgumentNullException("Invalid server");
            if (port < 0) throw new ArgumentOutOfRangeException("Portnumber cannot be negative");
            if (string.IsNullOrEmpty(path)) path = "/";
            else if (!path.StartsWith("/")) throw new ArgumentException("Path must start with '/'");

            this.server = server;
            this.port = port;

            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("ws://{0}:{1}{2}", server, port, path);

            logger.Debug("Creating websocket for url {0}", builder.ToString());

            clientConnection = new WebSocket(builder.ToString());
            clientConnection.OnOpen += OnOpen;
            clientConnection.OnMessage += IncomingMessage;
            clientConnection.OnClose += OnClose;
            clientConnection.OnError += Error;
            clientConnection.Compression = CompressionMethod.None;
        }

        public event EventHandler Connected;

        public event EventHandler Disconnected;

        public void Dispose()
        {
            clientConnection.Close();
        }

        public void Connect()
        {
            logger.Debug("Connect in state {0}", clientConnection.ReadyState);
            if (clientConnection.ReadyState == WebSocketState.Open) return;
            clientConnection.Connect();
        }

        public void Close()
        {
            logger.Debug("Close in state {0}", clientConnection.ReadyState);
            if (clientConnection.ReadyState == WebSocketState.Closed || clientConnection.ReadyState == WebSocketState.Closing) return;
            clientConnection.Close();
        }

        public void Send(string data)
        {
            if (string.IsNullOrEmpty(data)) throw new ArgumentNullException("Invalid data");
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("http://{0}:{1}{2}", server, port, data);
            Get(builder.ToString());
        }

        private void IncomingMessage(Object sender, MessageEventArgs e)
        {
            //"{\"edbots\":{\"Robotis Mini\":{\"connected\":true,\"reporters\":{\"servo-02\":{\"aligning\":false,\"load\":-0.8,\"torque\":false,\"position\":151.9,\"extended\":null},\"servo-13\":{\"aligning\":false,\"load\":0.0,\"torque\":false,\"position\":131.4,\"extended\":null},\"servo-03\":{\"aligning\":false,\"load\":-0.8,\"torque\":false,\"position\":68.0,\"extended\":null},\"servo-14\":{\"aligning\":false,\"load\":0.0,\"torque\":false,\"position\":149.9,\"extended\":null},\"servo-11\":{\"aligning\":false,\"load\":0.0,\"torque\":false,\"position\":144.6,\"extended\":null},\"servo-01\":{\"aligning\":false,\"load\":0.8,\"torque\":false,\"position\":141.9,\"extended\":null},\"servo-12\":{\"aligning\":false,\"load\":0.0,\"torque\":false,\"position\":145.7,\"extended\":null},\"servo-10\":{\"aligning\":false,\"load\":0.8,\"torque\":false,\"position\":117.6,\"extended\":null},\"motion-complete\":0,\"servo-08\":{\"aligning\":false,\"load\":0.0,\"torque\":false,\"position\":142.2,\"extended\":null},\"servo-09\":{\"aligning\":false,\"load\":0.0,\"torque\":false,\"position\":182.4,\"extended\":null},\"port3\":1021,\"servo-06\":{\"aligning\":false,\"load\":0.0,\"torque\":false,\"position\":142.5,\"extended\":null},\"port4\":296,\"servo-07\":{\"aligning\":false,\"load\":0.0,\"torque\":false,\"position\":148.7,\"extended\":null},\"port1\":462,\"servo-04\":{\"aligning\":false,\"load\":0.0,\"torque\":false,\"position\":244.0,\"extended\":null},\"servo-15\":{\"aligning\":false,\"load\":-0.8,\"torque\":false,\"position\":146.6,\"extended\":null},\"port2\":319,\"servo-05\":{\"aligning\":false,\"load\":-0.8,\"torque\":false,\"position\":157.2,\"extended\":null},\"servo-16\":{\"aligning\":false,\"load\":0.8,\"torque\":false,\"position\":137.5,\"extended\":null},\"speech-complete\":0,\"busy\":[]},\"activeUser\":\"Scratch <Doefus@[192.168.56.1]:0>\",\"motions\":{\"All\":[{\"name\":\"backward roll\",\"id\":24},{\"name\":\"bow 1\",\"id\":5},{\"name\":\"bow 2\",\"id\":6},{\"name\":\"break dance\",\"id\":40},{\"name\":\"break dance flip\",\"id\":41},{\"name\":\"crouch\",\"id\":3},{\"name\":\"forward roll\",\"id\":21},{\"name\":\"gangnam\",\"id\":42},{\"name\":\"get up\",\"id\":2},{\"name\":\"goalie block\",\"id\":34},{\"name\":\"goalie left\",\"id\":35},{\"name\":\"goalie right\",\"id\":36},{\"name\":\"goalie spread\",\"id\":37},{\"name\":\"head stand\",\"id\":38},{\"name\":\"initial position\",\"id\":1},{\"name\":\"karate left 1\",\"id\":25},{\"name\":\"karate left 2\",\"id\":27},{\"name\":\"karate right 1\",\"id\":26},{\"name\":\"karate right 2\",\"id\":28},{\"name\":\"left hook\",\"id\":11},{\"name\":\"left jab\",\"id\":10},{\"name\":\"left kick\",\"id\":30},{\"name\":\"left side kick\",\"id\":32},{\"name\":\"left uppercut\",\"id\":12},{\"name\":\"left wave\",\"id\":14},{\"name\":\"push\",\"id\":29},{\"name\":\"push up\",\"id\":23},{\"name\":\"right hook\",\"id\":8},{\"name\":\"right jab\",\"id\":7},{\"name\":\"right kick\",\"id\":31},{\"name\":\"right side kick\",\"id\":33},{\"name\":\"right uppercut\",\"id\":9},{\"name\":\"right wave\",\"id\":13},{\"name\":\"run forwards\",\"id\":39},{\"name\":\"sidestep left\",\"id\":18},{\"name\":\"sidestep right\",\"id\":17},{\"name\":\"sit up\",\"id\":22},{\"name\":\"stand\",\"id\":4},{\"name\":\"turn left\",\"id\":16},{\"name\":\"turn right\",\"id\":15},{\"name\":\"walk backwards\",\"id\":20},{\"name\":\"walk forwards\",\"id\":19}],\"Basic\":[{\"name\":\"crouch\",\"id\":3},{\"name\":\"get up\",\"id\":2},{\"name\":\"initial position\",\"id\":1},{\"name\":\"run forwards\",\"id\":39},{\"name\":\"sidestep left\",\"id\":18},{\"name\":\"sidestep right\",\"id\":17},{\"name\":\"stand\",\"id\":4},{\"name\":\"turn left\",\"id\":16},{\"name\":\"turn right\",\"id\":15},{\"name\":\"walk backwards\",\"id\":20},{\"name\":\"walk forwards\",\"id\":19}],\"Sport\":[{\"name\":\"goalie block\",\"id\":34},{\"name\":\"goalie left\",\"id\":35},{\"name\":\"goalie right\",\"id\":36},{\"name\":\"goalie spread\",\"id\":37},{\"name\":\"left kick\",\"id\":30},{\"name\":\"left side kick\",\"id\":32},{\"name\":\"right kick\",\"id\":31},{\"name\":\"right side kick\",\"id\":33}],\"Greet\":[{\"name\":\"bow 1\",\"id\":5},{\"name\":\"bow 2\",\"id\":6},{\"name\":\"left wave\",\"id\":14},{\"name\":\"right wave\",\"id\":13}],\"Dance\":[{\"name\":\"break dance\",\"id\":40},{\"name\":\"break dance flip\",\"id\":41},{\"name\":\"gangnam\",\"id\":42}],\"Gym\":[{\"name\":\"backward roll\",\"id\":24},{\"name\":\"forward roll\",\"id\":21},{\"name\":\"head stand\",\"id\":38},{\"name\":\"push up\",\"id\":23},{\"name\":\"sit up\",\"id\":22}],\"Fight\":[{\"name\":\"karate left 1\",\"id\":25},{\"name\":\"karate left 2\",\"id\":27},{\"name\":\"karate right 1\",\"id\":26},{\"name\":\"karate right 2\",\"id\":28},{\"name\":\"left hook\",\"id\":11},{\"name\":\"left jab\",\"id\":10},{\"name\":\"left uppercut\",\"id\":12},{\"name\":\"push\",\"id\":29},{\"name\":\"right hook\",\"id\":8},{\"name\":\"right jab\",\"id\":7},{\"name\":\"right uppercut\",\"id\":9}]},\"type\":\"ERM161\",\"enabled\":true,\"colours\":{\"red\":1,\"magenta\":5,\"green\":2,\"blue\":4,\"white\":7,\"yellow\":3,\"cyan\":6,\"off\":0}}},\"auth\":\"67002ddb-d0ff-41bf-8d91-05f2670d9cd8\",\"user\":\"CSharp <User@[192.168.56.1]:54998>\"}"
            //"{\"users\":[\"CSharp <User@[192.168.56.1]:50548>\",\"Scratch <Doefus@[192.168.56.1]:0>\"]}"
            //"{\"edbots\":{\"Robotis Mini\":{\"reporters\":{\"servo-02\":{\"aligning\":false,\"load\":0.8,\"torque\":false,\"position\":151.9,\"extended\":null}}}}}"
            //"{\"edbots\":{\"Robotis Mini\":{\"reporters\":{\"servo-03\":{\"aligning\":false,\"load\":0.8,\"torque\":false,\"position\":68.0,\"extended\":null}}}}}"
            string jsonMessage = e.Data;
            EdbotServerMessage edbotMessage = JsonConvert.DeserializeObject<EdbotServerMessage>(jsonMessage, jsonSettings);
            if (edbotMessage.Auth != null) Auth = edbotMessage.Auth;

            logger.Debug("Data received {0}", e);
        }

        private void OnOpen(Object sender, EventArgs e)
        {
            logger.Debug("Connected {0}", e);
            Connected?.Invoke(this, e);
        }

        private void OnClose(Object sender, CloseEventArgs e)
        {
            logger.Debug("Disconnected {0}", e);
            Disconnected?.Invoke(this, e);
        }

        private void Error(Object sender, WebSocketSharp.ErrorEventArgs e)
        {
            logger.Debug("Error {0}", e);
        }

        private string Get(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            WebHeaderCollection customheader = new WebHeaderCollection
            {
                { "X-Edbot-Auth", Auth }
            };
            request.Headers = customheader;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
