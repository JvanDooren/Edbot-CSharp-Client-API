namespace EdbotClientAPI.Communication.Web
{
    using System;
    using System.Net;
    using WebSocketSharp;

    public interface IWebClient
    {
        event EventHandler<EventArgs> Connected;
        event EventHandler<CloseEventArgs> Disconnected;
        event EventHandler<MessageEventArgs> OnMessage;

        void Connect();
        void Close();
        void Send(WebHeaderCollection customHeader, string data);
    }
}