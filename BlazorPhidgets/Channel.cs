using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorPhidgets
{
    public class Channel
    {
        public string? LibraryVersion { get; set; }
        public bool Attached { get; set; }
        [JsonPropertyName("channel")]
        public int ChannelNumber { get; set; }
        public bool IsChannel { get; set; }
        public string? ChannelClassName { get; set; }
        public string? ChannelName { get; set; }
        public string? DeviceClassName { get; set; }
        public string? DeviceLabel { get; set; }
        public string? DeviceName { get; set; }
        public int DeviceSerialNumber { get; set; }
        public string? DeviceSKU { get; set; }
        public int DeviceVersion { get; set; }
        public int HubPort { get; set; }
        public int HubPortCount { get; set; }
        public bool IsHubPortDevice { get; set; }
        public int HubPortSpeed { get; set; }
        public int MaxHubPortSpeed { get; set; }

        public bool HubPortSupportsAutoSpeed { get; set; }
        public bool HubPortSupportsSetSpeed { get; set; }
        public string? Key { get; set; }
        public bool IsLocal { get; set; }
        public bool IsOpen { get; set; }
        public bool IsRemote { get; set; }
        public int MaxVINTDeviceSpeed { get; set; }
        public bool VINTDeviceSupportsAutoSetSpeed { get; set; }
        public bool VINTDeviceSupportsSetSpeed { get; set; }
    }
}
