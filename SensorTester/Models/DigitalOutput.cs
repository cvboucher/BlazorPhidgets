using System.Text.Json.Serialization;

namespace SensorTester.Models
{
    public class DigitalOutput
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Description { get; set; }
        public int? DeviceSerialNumber { get; set; }
        public int? HubPort { get; set; }
        public bool IsHubPortDevice { get; set; }
        public int? Channel { get; set; }
        public bool IsLocal { get; set; }
        public bool IsRemote { get; set; }
        [JsonIgnore]
        public bool IsOpen { get; set; }
        public int MillisecondsBetweenStateChange { get; set; } = 100;
        public int NumberOfStateChanges { get; set; } = 100;
        public CancellationTokenSource? CancellationTokenSource { get; set; }
        [JsonIgnore]
        public bool IsOpening { get; set; }
    }
}
