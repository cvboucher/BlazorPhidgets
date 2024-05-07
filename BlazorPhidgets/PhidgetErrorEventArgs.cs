using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPhidgets
{
    public class PhidgetErrorEventArgs : EventArgs
    {
        //public PhidgetErrorEventArgs() { }
        public PhidgetErrorEventArgs(string code, string message, string id = null)
        {
            this.Id = id;
            this.Code = code;
            this.Message = message;
        }

        public string Id { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
