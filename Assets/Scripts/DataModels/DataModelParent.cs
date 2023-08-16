using System;
using Newtonsoft.Json;

namespace CMG.BallMazeGame.Models
{
    public class DataModelParent
    {
        [JsonProperty("session_id")] public Guid SessionID;
        [JsonProperty("timestamp")] public DateTime TimeStamp;
        [JsonProperty("data")]public Data Data;
    }
}