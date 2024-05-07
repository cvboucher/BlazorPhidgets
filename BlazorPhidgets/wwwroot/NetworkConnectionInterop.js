export var conn;

export function init(dotNetObject, options) {
    conn = new phidget22.NetworkConnection(
    {
        hostname: options?.hostname,
        port: options?.port,
        name: options?.name,
        passwd: options?.passwd,
        onConnect: function() { dotNetObject.invokeMethodAsync("OnConnect"); },
        onDisconnect: function () { dotNetObject.invokeMethodAsync("OnDisconnect"); },
        onAuthenticationNeeded: function () { dotNetObject.invokeMethodAsync("OnAuthenticationNeeded"); },
        onError: function(code, msg) { dotNetObject.invokeMethodAsync("OnError", code, msg); }
    });
}