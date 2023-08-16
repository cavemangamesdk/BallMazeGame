using Newtonsoft.Json;

namespace CMG.BallMazeGame.Models
{
    public class OptimizedDeviceData
    {
        [JsonProperty("pitch")] public float pitch;
        [JsonProperty("roll")] public float roll;
    }
}