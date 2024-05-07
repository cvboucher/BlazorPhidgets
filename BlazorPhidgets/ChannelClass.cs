using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPhidgets
{
    public enum ChannelClass
    {
        None = 0,
        ACCELEROMETER = 0x1,
        DIGITAL_INPUT = 0x5,
        VOLTAGE_INPUT = 0x1d,
        VOLTAGE_OUTPUT = 0x1e
    }
}
