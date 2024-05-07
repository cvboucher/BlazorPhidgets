export var channel;
const phidgets = [];
var dotNetObject;
export function setDotNetObjectReference(dotNetObjectReference) {
    dotNetObject = dotNetObjectReference;
}

export function newDigitalInput(id) {
    var phidget = new phidget22.DigitalInput();
    phidgets.push(phidget);
    phidget.onStateChange = function (state) {
        dotNetObject.invokeMethodAsync("OnStateChange", this.id, state);
    }
    newPhidget(phidget);
    phidget.id = id;
}
export function newDigitalOutput(id) {
    var phidget = new phidget22.DigitalOutput();
    phidgets.push(phidget);
    newPhidget(phidget);
    phidget.id = id;
}

function newPhidget(phidget) {
    
    phidget.onAttach = function () {
        if (this.id)
            dotNetObject?.invokeMethodAsync("OnAttach", this.id);
    };
    phidget.onDetach = function () {
        if (this.id)
            dotNetObject?.invokeMethodAsync("OnDetach", this.id);
    };
    phidget.onError = function (code, msg) {
        if (this.id)
            dotNetObject?.invokeMethodAsync("OnError", this.id, code, msg);
    };
    phidget.onPropertyChange = function (propertyName) {
        if (this.id)
            dotNetObject?.invokeMethodAsync("OnPropertyChange", this.id, propertyName);
    };
}

export function deletePhidget(id) {
    var index = phidgets.findIndex(x => x.id == id);
    if (index >= 0) {
        if (phidgets[index].isOpen)
            phidgets[index].close()
        phidgets.splice(index, 1);
    }
}

export function getProperty(id, propertyName) {
    return phidgets.find(x => x.id === id)[propertyName];
}

export function setProperty(id, propertyName, value) {
    phidgets.find(x => x.id === id)[propertyName] = value;
}

export function getLibraryVersion() {
    return Phidget.libraryVersion;
}

export function close(id) {
    phidgets.find(x => x.id === id).close();
}

export function setHubPortSpeed(id, hubPortSpeed) {
    phidgets.find(x => x.id === id).setHubPortSpeed(hubPortSpeed);
}

export function open(id, timeout) {
    if (timeout)
        phidgets.find(x => x.id === id).open(timeout);
    else
        phidgets.find(x => x.id === id).open();
}

export function writeDeviceLabel(id, deviceLabel) {
    phidgets.find(x => x.id === id).writeDeviceLabel(deviceLabel);
}

export function closeAll() {
    for (var i = 0; i < phidgets.length; i++) {
        if (phidgets[i].isOpen)
            phidgets[i].close();
    }
    phidgets.length = 0;
}

export function setInputMode(id, inputMode) {
    phidgets.find(x => x.id === id).setInputMode(inputMode);
}

export function setPowerSupply(id, powerSupply) {
    phidgets.find(x => x.id === id).setPowerSupply(powerSupply);
}

export function setState(id, state) {
    phidgets.find(x => x.id === id).setState(state);
}