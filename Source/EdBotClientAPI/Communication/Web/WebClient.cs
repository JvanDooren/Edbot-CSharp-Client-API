namespace EdbotClientAPI.Communication.Web
{
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

        public event EventHandler<EventArgs> Connected;

        public event EventHandler<CloseEventArgs> Disconnected;

        public event EventHandler<MessageEventArgs> OnMessage;

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

        public void Send(WebHeaderCollection customHeader, string data)
        {
            if (string.IsNullOrEmpty(data)) throw new ArgumentNullException("Invalid data");
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("http://{0}:{1}{2}", server, port, data);
            Get(customHeader, builder.ToString());
        }

        private void IncomingMessage(Object sender, MessageEventArgs e)
        {
            logger.Debug("Data received {0}", e);
            OnMessage?.Invoke(this, e);
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

        private string Get(WebHeaderCollection customHeader, string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.Headers = customHeader;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
