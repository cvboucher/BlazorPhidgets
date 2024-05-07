using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPhidgets
{
    public class NetworkConnection : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;
        public event EventHandler? AuthenticationNeeded;
        public event EventHandler? Connected;
        public event EventHandler? Disconnected;
        public event EventHandler<PhidgetErrorEventArgs>? Error;

        public NetworkConnection(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                    "import", "./_content/BlazorPhidgets/NetworkConnectionInterop.js").AsTask());
        }

        public async Task Init(NetworkConnectionOptions? options = null)
        {
            var module = await moduleTask.Value;
            var dotNetReference = DotNetObjectReference.Create(this);
            if (options == null)
                await module.InvokeVoidAsync("init", dotNetReference);
            else
                await module.InvokeVoidAsync("init", dotNetReference, options);
        }

        public async Task Delete()
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("conn.delete");
        }

        public async Task Close()
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("conn.close");
        }

        public async Task Connect()
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("conn.connect");
        }

        [JSInvokable]
        public void OnAuthenticationNeeded()
        {
            AuthenticationNeeded?.Invoke(this, EventArgs.Empty);
        }

        [JSInvokable]
        public void OnConnect()
        {
            Connected?.Invoke(this, EventArgs.Empty);
        }

        [JSInvokable]
        public void OnDisconnect()
        {
            Disconnected?.Invoke(this, EventArgs.Empty);
        }

        [JSInvokable]
        protected void OnError(string code, string message)
        {
            Error?.Invoke(this, new PhidgetErrorEventArgs(code, message));
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
