using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEditor.Domain
{
    public class ToolEventCreate
    {
        public ToolEventCreate(EventHandler handler, string name)
        {
            Event = handler;
            Name = name;
        }

        public EventHandler Event { get; set; }
        public string Name { get; set; }
    }
}
