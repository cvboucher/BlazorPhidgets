using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPhidgets
{
    public class PropertyChangeEventArgs : EventArgs
    {
        public PropertyChangeEventArgs(string id, string propertyName) 
        {
            this.Id = id;
            this.PropertyName = propertyName;
        }

        public string Id { get; private set; }
        public string PropertyName { get; private set; }
        
    }
}
