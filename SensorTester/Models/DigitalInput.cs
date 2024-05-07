using BlazorPhidgets;
using System.Text.Json.Serialization;

namespace SensorTester.Models;

public class DigitalInput
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? Description { get; set; }
    public int? DeviceSerialNumber { get; set; }
    public int? HubPort { get; set; }
    public bool IsHubPortDevice { get; set; }
    public int? Channel { get; set; }
    public bool IsLocal { get; set; }
    public bool IsRemote { get; set; }
    public int HighCount { get; set; }
    public int LowCount { get; set; }
    [JsonIgnore]
    public bool IsOpen { get; set; }
    public InputMode? InputMode { get; set; }
    public PowerSupply? PowerSupply { get; set; }
    [JsonIgnore]
    public bool IsOpening { get; set; }
}
