namespace EdbotClientAPI.Communication.Requests
{
    using NLog;
    using System.Text;
    using System.Web;

    public class EdbotWebRequestFactory : IRequestFactory
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private StringBuilder builder = new StringBuilder();

        public string CreateRunMotion(string edBotName, int motionNumber)
        {
            builder.Clear();
            builder.AppendFormat("/api/motion/{0}/{1}", HttpUtility.HtmlEncode(edBotName), motionNumber);
            return builder.ToString();
        }

        public string CreateSetServoMode(string edBotName, string path)
        {
            builder.Clear();
            builder.AppendFormat("/api/servo_mode/{0}/{1}", HttpUtility.HtmlEncode(edBotName), path);
            return builder.ToString();
        }

        public string CreateSetServoTorque(string edBotName, string path)
        {
            builder.Clear();
            builder.AppendFormat("/api/servo_torque/{0}/{1}", HttpUtility.HtmlEncode(edBotName), path);
            return builder.ToString();
        }

        public string CreateSetServoLED(string edBotName, string path)
        {
            builder.Clear();
            builder.AppendFormat("/api/servo_led/{0}/{1}", HttpUtility.HtmlEncode(edBotName), path);
            return builder.ToString();
        }

        public string CreateSetServoSpeed(string edBotName, string path)
        {
            builder.Clear();
            builder.AppendFormat("/api/servo_speed/{0}/{1}", HttpUtility.HtmlEncode(edBotName), path);
            return builder.ToString();
        }

        public string CreateSetServoPosition(string edBotName, string path)
        {
            builder.Clear();
            builder.AppendFormat("/api/servo_position/{0}/{1}", HttpUtility.HtmlEncode(edBotName), path);
            return builder.ToString();
        }

        public string CreateSetServoPID(string edBotName, string path)
        {
            builder.Clear();
            builder.AppendFormat("/api/servo_pid/{0}/{1}", HttpUtility.HtmlEncode(edBotName), path);
            return builder.ToString();
        }

        public string CreateSetServoCombined(string edBotName, string path)
        {
            builder.Clear();
            builder.AppendFormat("/api/servo_combined/{0}/{1}", HttpUtility.HtmlEncode(edBotName), path);
            return builder.ToString();
        }

        public string CreateSetBuzzer(string edBotName, string path)
        {
            builder.Clear();
            builder.AppendFormat("/api/buzzer/{0}/{1}", HttpUtility.HtmlEncode(edBotName), path);
            return builder.ToString();
        }

        public string CreateSay(string edBotName, string text)
        {
            builder.Clear();
            builder.AppendFormat("/api/say/{0}/{1}", HttpUtility.HtmlEncode(edBotName), HttpUtility.HtmlEncode(text));
            return builder.ToString();
        }

        public string CreateReset(string edBotName)
        {
            builder.Clear();
            builder.AppendFormat("/api/reset/{0}", HttpUtility.HtmlEncode(edBotName));
            return builder.ToString();
        }

        public string CreateSetOptions(string edBotName, string path)
        {
            builder.Clear();
            builder.AppendFormat("/api/options/{0}/{1}", HttpUtility.HtmlEncode(edBotName), path);
            return builder.ToString();
        }

        public string CreateSetCustom(string edBotName, string path)
        {
            builder.Clear();
            builder.AppendFormat("/api/custom/{0}/{1}", HttpUtility.HtmlEncode(edBotName), path);
            return builder.ToString();
        }
    }
}
