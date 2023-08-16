using Newtonsoft.Json;

namespace CMG.BallMazeGame.Models
{
    public class DeviceMagnetometerDataBase
    {
        [JsonProperty("roll")] public float North { get; set; }

        [JsonProperty("x_raw")] public float XRaw { get; set; }

        [JsonProperty("y_raw")] public float YRaw { get; set; }

        [JsonProperty("z_raw")] public float ZRaw { get; set; }
    }
}