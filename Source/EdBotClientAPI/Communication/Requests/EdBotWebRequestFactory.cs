namespace EdbotClientAPI.Communication.Requests
{
    using NLog;
    using System;
    using System.Text;
    using System.Web;

    public class EdbotWebRequestFactory : IRequestFactory
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private StringBuilder builder = new StringBuilder();

        public string CreateRunMotion(string edBotName, int motionNumber)
        {
            VerifyName(edBotName);
            if (motionNumber < 0) throw new System.ArgumentException("motionNumber must be positive");

            builder.Clear();
            builder.AppendFormat("/api/motion/{0}/{1}", HttpUtility.HtmlEncode(edBotName), motionNumber);
            return builder.ToString();
        }

        public string CreateSetServoMode(string edBotName, string path)
        {
            return CreateRequest("/api/servo_mode/{0}/{1}", edBotName, path);
        }

        public string CreateSetServoTorque(string edBotName, string path)
        {
            return CreateRequest("/api/servo_torque/{0}/{1}", edBotName, path);
        }

        public string CreateSetServoLED(string edBotName, string path)
        {
            return CreateRequest("/api/servo_led/{0}/{1}", edBotName, path);
        }

        public string CreateSetServoSpeed(string edBotName, string path)
        {
            return CreateRequest("/api/servo_speed/{0}/{1}", edBotName, path);
        }

        public string CreateSetServoPosition(string edBotName, string path)
        {
            return CreateRequest("/api/servo_position/{0}/{1}", edBotName, path);
        }

        public string CreateSetServoPID(string edBotName, string path)
        {
            return CreateRequest("/api/servo_pid/{0}/{1}", edBotName, path);
        }

        public string CreateSetServoCombined(string edBotName, string path)
        {
            return CreateRequest("/api/servo_combined/{0}/{1}", edBotName, path);
        }

        public string CreateSetBuzzer(string edBotName, string path)
        {
            return CreateRequest("/api/buzzer/{0}/{1}", edBotName, path);
        }

        public string CreateSay(string edBotName, string text)
        {
            VerifyName(edBotName);
            if (string.IsNullOrEmpty(text)) throw new ArgumentException("text must be at least 1 character");

            builder.Clear();
            builder.AppendFormat("/api/say/{0}/{1}", HttpUtility.HtmlEncode(edBotName), HttpUtility.HtmlEncode(text));
            return builder.ToString();
        }

        public string CreateReset(string edBotName)
        {
            VerifyName(edBotName);

            builder.Clear();
            builder.AppendFormat("/api/reset/{0}", HttpUtility.HtmlEncode(edBotName));
            return builder.ToString();
        }

        public string CreateSetOptions(string edBotName, string path)
        {
            return CreateRequest("/api/options/{0}/{1}", edBotName, path);
        }

        public string CreateSetCustom(string edBotName, string path)
        {
            return CreateRequest("/api/custom/{0}/{1}", edBotName, path);
        }

        private string CreateRequest(string format, string edBotName, string path)
        {
            VerifyName(edBotName);
            VerifyPath(path);

            builder.Clear();
            builder.AppendFormat(format, HttpUtility.HtmlEncode(edBotName), path);
            return builder.ToString();
        }

        private void VerifyName(string edBotName)
        {
            if (string.IsNullOrEmpty(edBotName)) throw new ArgumentException("edBotName must be at least 1 character");
        }

        private void VerifyPath(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("path must be at least 1 character");
            if (path.StartsWith("/")) throw new ArgumentException("path must not start with '/'");
        }
    }
}
