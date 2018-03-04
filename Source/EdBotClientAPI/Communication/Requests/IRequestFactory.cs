namespace EdbotClientAPI.Communication.Requests
{
    public interface IRequestFactory
    {
        string CreateRunMotion(string edBotName, int motionNumber);
        string CreateSetServoMode(string edBotName, string path);
        string CreateSetServoTorque(string edBotName, string path);
        string CreateSetServoLED(string edBotName, string path);
        string CreateSetServoSpeed(string edBotName, string path);
        string CreateSetServoPosition(string edBotName, string path);
        string CreateSetServoPID(string edBotName, string path);
        string CreateSetServoCombined(string edBotName, string path);
        string CreateSetBuzzer(string edBotName, string path);
        string CreateSay(string edBotName, string text);
        string CreateReset(string edBotName);
        string CreateSetOptions(string edBotName, string path);
        string CreateSetCustom(string edBotName, string path);
    }
}