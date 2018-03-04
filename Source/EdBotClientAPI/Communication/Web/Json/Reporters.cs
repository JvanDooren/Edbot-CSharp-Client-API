namespace EdbotClientAPI.Communication.Web.Json
{
    using Newtonsoft.Json;

    public class Reporters
    {
        [JsonProperty("servo-01")]
        public Servo Servo_01 { get; set; }

        [JsonProperty("servo-02")]
        public Servo Servo_02 { get; set; }

        [JsonProperty("servo-03")]
        public Servo Servo_03 { get; set; }

        [JsonProperty("servo-04")]
        public Servo Servo_04 { get; set; }

        [JsonProperty("servo-05")]
        public Servo Servo_05 { get; set; }

        [JsonProperty("servo-06")]
        public Servo Servo_06 { get; set; }

        [JsonProperty("servo-07")]
        public Servo Servo_07 { get; set; }

        [JsonProperty("servo-08")]
        public Servo Servo_08 { get; set; }

        [JsonProperty("servo-09")]
        public Servo Servo_09 { get; set; }

        [JsonProperty("servo-10")]
        public Servo Servo_10 { get; set; }

        [JsonProperty("servo-11")]
        public Servo Servo_11 { get; set; }

        [JsonProperty("servo-12")]
        public Servo Servo_12 { get; set; }

        [JsonProperty("servo-13")]
        public Servo Servo_13 { get; set; }

        [JsonProperty("servo-14")]
        public Servo Servo_14 { get; set; }

        [JsonProperty("servo-15")]
        public Servo Servo_15 { get; set; }

        [JsonProperty("servo-16")]
        public Servo Servo_16 { get; set; }

        [JsonProperty("motion-complete")]
        public int MotionComplete { get; set; }

        [JsonProperty("speech-complete")]
        public int SpeechComplete { get; set; }

        [JsonProperty("port1")]
        public int Port1 { get; set; }

        [JsonProperty("port2")]
        public int Port2 { get; set; }

        [JsonProperty("port3")]
        public int Port3 { get; set; }

        [JsonProperty("port4")]
        public int Port4 { get; set; }

        //unknown type
        [JsonProperty("busy")]
        public object Busy { get; set; }
    }
}
