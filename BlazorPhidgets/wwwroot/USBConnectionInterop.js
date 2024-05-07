export var conn;

var dotNetObject;

export function setDotNetObjectReference(dotNetObjectReference) {
    dotNetObject = dotNetObjectReference;
}
export function main() {
    conn = new phidget22.USBConnection(
    {
        onError: function (code, msg) {
            dotNetObject?.invokeMethodAsync("OnError", code, msg);
        }
    });
}

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