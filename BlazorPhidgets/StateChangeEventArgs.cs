﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPhidgets
{
    public class StateChangeEventArgs : EventArgs
    {
        public StateChangeEventArgs(string id, bool state, DeviceId deviceId)
        {
            this.Id = id;
            this.State = state;
            this.DeviceId = deviceId;
        }

        public string Id { get; private set; }
        public bool State {  get; private set; }
        public DeviceId DeviceId { get; private set; }
    }
}
