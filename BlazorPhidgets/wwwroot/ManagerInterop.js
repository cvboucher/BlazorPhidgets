export var manager;

const devices = [];
const channels = [];

function getPhidgetProperties(phidget)
{
    var newPhidget = {
        Attached: phidget.attached,
        channel: phidget.channel,
        IsChannel: phidget.isChannel,
        ChannelClassName: phidget.channelClassName,
        DeviceClassName: phidget.deviceClassName,
        DeviceLabel: phidget.deviceLabel,
        DeviceName: phidget.deviceName,
        DeviceSerialNumber: phidget.deviceSerialNumber,
        DeviceSKU: phidget.deviceSKU,
        deviceVersion: phidget.deviceVersion,
        hubPort: phidget.hubPort,
        hubPortCount: phidget.hubPortCount,
        isHubPortDevice: phidget.isHubPortDevice,
        //hubPortSpeed: phidget.hubPortSpeed,
        //maxHubPortSpeed: phidget.maxHubPortSpeed,
        //hubPortSupportsAutoSpeed: phidget.hubPortSupportsAutoSpeed,
        //hubPortSupportsSetSpeed: phidget.hubPortSupportsSetSpeed,
        key: phidget.key,
        isLocal: phidget.isLocal,
        isOpen: phidget.isOpen,
        isRemote: phidget.isRemote
    };
    return newPhidget;
}

var dotNetObject;

export function setDotNetObjectReference(dotNetObjectReference) {
    dotNetObject = dotNetObjectReference;
}

function main() {
    
    manager = new phidget22.Manager({
        onAttach: function (channel) {
            channels.push(channel);
            dotNetObject?.invokeMethodAsync("OnAttach", getPhidgetProperties(channel));
        },
        onDetach: function (channel) {
            channelIndex = channels.findIndex((x) => x.key == channel.key);
            if (channelIndex > -1)
                delete channels[channelIndex];
            dotNetObject?.invokeMethodAsync("OnDetach", getPhidgetProperties(channel));
        },
        onDeviceAttach: function (device) {
            devices.push(device);
            //dotNetObject?.invokeMethodAsync("OnDeviceAttach", getPhidgetProperties(device));
        },
        onDeviceDetach: function (device) {
            deviceIndex = devices.findIndex((x) => x.key == device.key);
            if (deviceIndex > -1)
                delete devices[deviceIndex];
            //dotNetObject?.invokeMethodAsync("OnDeviceDetach", getPhidgetProperties(device));
        },

        
    });
};


export function getDevices() {
    return devices.map((x) => getPhidgetProperties(x));
};

export function getChannels() {
    return channels.map(getPhidgetProperties);
};

switch (document.readyState) {
    case "complete":
        main();
        break;
    default:
        window.addEventListener("load", (event) => {
            main();
        });
        break;
}



//document.addEventListener('DOMContentLoaded', () => {
//    main();
//});

//$(document).ready(function () {
//    main();
//});

//window.addEventListener('DOMContentLoaded', () => {
//    main();
//});
