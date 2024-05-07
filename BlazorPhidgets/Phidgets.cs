using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace BlazorPhidgets
{
    public class Phidgets : IAsyncDisposable
    {
        protected readonly Lazy<Task<IJSObjectReference>> moduleTask;
        public event EventHandler<PhidgetErrorEventArgs>? Error;
        public event EventHandler<string>? Attach;
        public event EventHandler<string>? Detach;
        public event EventHandler<PropertyChangeEventArgs>? PropertyChange;
        public event EventHandler<StateChangeEventArgs>? StateChange;

        private DotNetObjectReference<Phidgets>? dotNetReference;

        public Phidgets(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                    "import", "./_content/BlazorPhidgets/PhidgetInterop.js").AsTask());
            var initTask = Init();
        }

        public async Task<IJSObjectReference> Init()
        {
            var module = await moduleTask.Value;
            if (dotNetReference == null)
            {
                dotNetReference = DotNetObjectReference.Create(this);
                await module.InvokeVoidAsync("setDotNetObjectReference", dotNetReference);
            }
            return module;
        }

        // Digital Input

        public async Task NewDigitalInput(string id)
        {
            var module = await Init();
            await module.InvokeVoidAsync("newDigitalInput", id);
        }

        public async Task<InputMode> GetInputMode(string id)
        {
            return await GetProperty<InputMode>(id, "inputMode");
        }

        public async Task Delete(string id)
        {
            var module = await Init();
            await module.InvokeVoidAsync("deletePhidget", id);
        }

        public async Task SetInputMode(string id, InputMode inputMode)
        {
            var module = await Init();
            await module.InvokeVoidAsync("setInputMode", id, inputMode);
        }

        public async Task<PowerSupply> GetPowerSupply(string id)
        {
            return await GetProperty<PowerSupply>(id, "powerSupply");
        }

        public async Task SetPowerSupply(string id, PowerSupply powerSupply)
        {
            var module = await Init();
            await module.InvokeVoidAsync("setPowerSupply", id, powerSupply);
        }

        public async Task<bool> GetState(string id)
        {
            return await GetProperty<bool>(id, "state");
        }

        [JSInvokable]
        public void OnStateChange(string id, bool state)
        {
            StateChange?.Invoke(this, new StateChangeEventArgs(id, state));
        }

        // Digital Output
        public async Task NewDigitalOutput(string id)
        {
            var module = await Init();
            await module.InvokeVoidAsync("newDigitalOutput", id);
        }

        public async Task SetState(string id, bool state)
        {
            var module = await Init();
            await module.InvokeVoidAsync("setState", id, state);
        }

        [JSInvokable]
        public void OnError(string id, string code, string message)
        {
            Error?.Invoke(this, new PhidgetErrorEventArgs(code, message, id));
        }

        [JSInvokable]
        public void OnAttach(string id)
        {
            Attach?.Invoke(this, id);
        }

        [JSInvokable]
        public void OnDetach(string id)
        {
            Detach?.Invoke(this, id);
        }

        [JSInvokable]
        public void OnPropertyChange(string id, string propertyName)
        {
            PropertyChange?.Invoke(this, new PropertyChangeEventArgs(id, propertyName));
        }

        
        public async Task<T> GetProperty<T>(string id, string property)
        {
            var module = await Init();
            return await module.InvokeAsync<T>("getProperty", id, property);
        }

        public async Task SetProperty<T>(string id, string property, T value)
        {
            var module = await Init();
            await module.InvokeVoidAsync("setProperty", id, property, value);
        }

        public async Task<string> GetLibraryVersion()
        {
            var module = await Init();
            return await module.InvokeAsync<string>("getLibraryVersion");
        }
        
        public async Task<bool> GetAttached(string id)
        {
            return await GetProperty<bool>(id, "attached");
        }

        public async Task<int> GetChannel(string id)
        {
            return await GetProperty<int>(id, "channel");
        }
        public async Task SetChannel(string id, int channel)
        {
            await SetProperty(id, "channel", channel);
        }

        public async Task<bool> GetIsChannel(string id)
        {
            return await GetProperty<bool>(id, "isChannel");
        }

        public async Task<ChannelClass> GetChannelClass(string id)
        {
            return await GetProperty<ChannelClass>(id, "channelClass");
        }

        public async Task<string> GetChannelClassName(string id)
        {
            return await GetProperty<string>(id, "channelClassName");
        }

        public async Task<string> GetChannelName(string id)
        {
            return await GetProperty<string>(id, "channelName");
        }

        // GetDeviceClass

        public async Task<string> GetDeviceClassName(string id)
        {
            return await GetProperty<string>(id, "deviceClassName");
        }

        // GetDeviceId

        public async Task<string> GetDeviceLabel(string id)
        {
            return await GetProperty<string>(id, "deviceLabel");
        }

        public async Task SetDeviceLabel(string id, string deviceLabel)
        {
            await SetProperty(id, nameof(deviceLabel), deviceLabel);
        }

        public async Task<int> GetDeviceSerialNumber(string id)
        {
            return await GetProperty<int>(id, "deviceSerialNumber");
        }

        public async Task SetDeviceSerialNumber(string id, int deviceSerialNumber)
        {
            await SetProperty(id, nameof(deviceSerialNumber), deviceSerialNumber);
        }

        public async Task<string> GetDeviceSKU(string id)
        {
            return await GetProperty<string>(id, "deviceSKU");
        }

        public async Task<int> GetDeviceVersion(string id)
        {
            return await GetProperty<int>(id, "deviceVersion");
        }

        // GetHub

        public async Task<int> GetHubPort(string id)
        {
            return await GetProperty<int>(id, "hubPort");
        }

        public async Task SetHubPort(string id, int hubPort)
        {
            await SetProperty(id, nameof(hubPort), hubPort);
        }

        public async Task<int> GetHubPortCount(string id)
        {
            return await GetProperty<int>(id, "hubPortCount");
        }

        public async Task<bool> GetIsHubPortDevice(string id)
        {
            return await GetProperty<bool>(id, "isHubPortDevice");
        }

        public async Task SetIsHubPortDevice(string id, bool isHubPortDevice)
        {
            await SetProperty(id, nameof(isHubPortDevice), isHubPortDevice);
        }

        public async Task<int> GetHubPortSpeed(string id)
        {
            return await GetProperty<int>(id, "hubPortSpeed");
        }

        public async Task<int> GetMaxHubPortSpeed(string id)
        {
            return await GetProperty<int>(id, "maxHubPortSpeed");
        }

        public async Task<bool> GetHubPortSupportsAutoSetSpeed(string id)
        {
            return await GetProperty<bool>(id, "hubPortSupportsAutoSetSpeed");
        }

        public async Task<bool> GetHubPortSupportsSetSpeed(string id)
        {
            return await GetProperty<bool>(id, "hubPortSupportsSetSpeed");
        }
        public async Task<string> GetKey(string id)
        {
            return await GetProperty<string>(id, "key");
        }

        public async Task<bool> GetIsLocal(string id)
        {
            return await GetProperty<bool>(id, "isLocal");
        }

        public async Task SetIsLocal(string id, bool isLocal)
        {
            await SetProperty(id, nameof(isLocal), isLocal);
        }

        public async Task<bool> GetIsOpen(string id)
        {
            return await GetProperty<bool>(id, "isOpen");
        }

        // Parent

        public async Task<bool> GetIsRemote(string id)
        {
            return await GetProperty<bool>(id, "isRemote");
        }

        public async Task SetIsRemote(string id, bool isRemote)
        {
            await SetProperty(id, nameof(isRemote), isRemote);
        }

        // maxVINTDeviceSpeed

        // VINTDeviceSupportsAutoSetSpeed

        // VINTDeviceSupportsSetSpeed

        public async Task Close(string id)
        {
            var module = await Init();
            await module.InvokeVoidAsync("close", id);
        }

        // getDeviceChannelCount

        public async Task SetHubPortSpeed(string id, int hubPortSpeed)
        {
            var module = await Init();
            await module.InvokeVoidAsync("setHubPortSpeed", id, hubPortSpeed);
        }

        public async Task Open(string id)
        {
            var module = await Init();
            await module.InvokeVoidAsync("open", id);
        }

        public async Task Open(string id, int timeout)
        {
            var module = await Init();
            await module.InvokeVoidAsync("open", id, timeout);
        }

        public async Task WriteDeviceLabel(string id, string deviceLabel)
        {
            var module = await Init();
            await module.InvokeVoidAsync("writeDeviceLabel", id, deviceLabel);
        }

        public async Task CloseAll()
        {
            var module = await Init();
            await module.InvokeVoidAsync("closeAll");
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await Init();
                await CloseAll();
                await module.DisposeAsync();
            }
        }

        //public string? LibraryVersion { get; set; }
        //public bool Attached { get; set; }
        //public int Channel { get; set; }
        //public bool IsChannel { get; set; }
        //public string? ChannelClassName { get; set; }
        //public string? ChannelName { get; set; }
        //public string? DeviceClassName { get; set; }
        //public string? DeviceLabel { get; set; }
        //public string? DeviceName { get; set; }
        //public int DeviceSerialNumber { get; set; }
        //public string? DeviceSKU { get; set; }
        //public int DeviceVersion { get; set; }
        //public int HubPort { get; set; }
        //public int HubPortCount { get; set; }
        //public bool IsHubPortDevice { get; set; }
        //public int HubPortSpeed { get; set; }
        //public int MaxHubPortSpeed { get; set; }

        //public bool HubPortSupportsAutoSpeed { get; set; }
        //public bool HubPortSupportsSetSpeed { get; set; }
        //public string? Key { get; set; }
        //public bool IsLocal { get; set; }
        //public bool IsOpen { get; set; }
        //public bool IsRemote { get; set; }
        //public int MaxVINTDeviceSpeed { get; set; }
        //public bool VINTDeviceSupportsAutoSetSpeed { get; set; }
        //public bool VINTDeviceSupportsSetSpeed { get; set; }
    }
}
