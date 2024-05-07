using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BlazorPhidgets
{
    public class Manager : IAsyncDisposable
    {
        public event EventHandler<Channel>? Attach;
        public event EventHandler<Channel>? Detach;
        public event EventHandler<Channel>? DeviceAttach;
        public event EventHandler<Channel>? DeviceDetach;
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;
        private DotNetObjectReference<Manager>? dotNetReference;
        //private Task initTask;
        public Manager(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                    "import", "./_content/BlazorPhidgets/ManagerInterop.js").AsTask());
            var initTask = Init();
        }

        public async Task<IJSObjectReference> Init()
        {
            var module = await moduleTask.Value;
            if (dotNetReference == null)
            {
                dotNetReference = DotNetObjectReference.Create(this);
                await module.InvokeVoidAsync("setDotNetObjectReference", dotNetReference);
                await module.InvokeVoidAsync("manager.open");
            }
            return module;
        }

        public async Task Open()
        {
            var module = await Init();
            await module.InvokeVoidAsync("manager.open");
        }

        public async Task Close()
        {
            var module = await Init();
            await module.InvokeVoidAsync("manager.close");
        }

        public async Task<List<Channel>> GetDevices()
        {
            var module = await Init();
            var devices = await module.InvokeAsync<List<Channel>>("getDevices");
            return devices ?? new();
        }

        public async Task<List<Channel>> GetChannels()
        {
            var module = await Init();
            var channels = await module.InvokeAsync<List<Channel>>("getChannels");
            return channels ?? new();
        }

        [JSInvokable]
        public void OnAttach(Channel channel)
        {
            Attach?.Invoke(this, channel);
        }

        [JSInvokable]
        public void OnDetach(Channel channel)
        {
            Detach?.Invoke(this, channel);
        }

        [JSInvokable]
        public void OnDeviceAttach(Channel channel)
        {
            DeviceAttach?.Invoke(this, channel);
        }

        [JSInvokable]
        public void OnDeviceDetach(Channel channel)
        {
            DeviceDetach?.Invoke(this, channel);
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await Close();
                await module.DisposeAsync();
            }
        }
    }
}
