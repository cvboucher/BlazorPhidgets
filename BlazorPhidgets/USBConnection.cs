using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPhidgets;

public class USBConnection : IAsyncDisposable
{
    public event EventHandler<PhidgetErrorEventArgs>? Error;
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;
    private DotNetObjectReference<USBConnection>? dotNetReference;

    public USBConnection(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/BlazorPhidgets/USBConnectionInterop.js").AsTask());
        var initTask = Init();
    }

    public async Task<IJSObjectReference> Init()
    {
        var module = await moduleTask.Value;
        if (dotNetReference == null)
        {
            dotNetReference = DotNetObjectReference.Create(this);
            await module.InvokeVoidAsync("setDotNetObjectReference", dotNetReference);
            await module.InvokeVoidAsync("conn.connect");
        }
        return module;
    }

    public async Task Delete()
    {
        var module = await Init();
        await module.InvokeVoidAsync("conn.delete");
    }

    public async Task Close()
    {
        var module = await Init();
        await module.InvokeVoidAsync("conn.close");
    }

    public async Task RequestWebUSBDeviceAccess()
    {
        var module = await Init();
        await module.InvokeVoidAsync("conn.requestWebUSBDeviceAccess");
    }

    public async Task Connect()
    {
        var module = await Init();
        await module.InvokeAsync<Task>("conn.connect");
    }

    [JSInvokable]
    public void OnError(string code, string message)
    {
        Error?.Invoke(this, new PhidgetErrorEventArgs(code, message));
    }

    public async ValueTask DisposeAsync()
    {
        if (moduleTask.IsValueCreated)
        {
            var module = await moduleTask.Value;
            await Close();
            await Delete();
            await module.DisposeAsync();
        }
    }
}
