namespace EdbotClientAPI.Communication.Web
{
    using System;

    public interface IWebClient
    {
        event EventHandler Connected;
        event EventHandler Disconnected;

        void Connect();
        void Close();
        void Send(string data);
    }
}