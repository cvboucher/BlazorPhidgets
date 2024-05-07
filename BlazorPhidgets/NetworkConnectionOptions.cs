using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPhidgets
{
    public class NetworkConnectionOptions
    {
        public string? Hostname { get; set; }
        public int? Port { get; set; }
        public string? Name { get; set; }
        public string? Passwd { get; set; }
    }
}
