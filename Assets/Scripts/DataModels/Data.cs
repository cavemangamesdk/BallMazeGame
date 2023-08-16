using Newtonsoft.Json;

namespace CMG.BallMazeGame.Models
{
    public class Data
    {
        [JsonProperty("accelerometer_sensor")] public DeviceAccelerometerDataBase AccelerometerDataBase;
        [JsonProperty("gyroscope_sensor")] public DeviceGyroscopeDataBase GyroscopeDataBase;
        [JsonProperty("magnetometer_sensor")] public DeviceMagnetometerDataBase MagnetometerDataBase;
        [JsonProperty("orientation_sensor")] public DeviceOrientationDataBase OrientationDataBase;
    }
}