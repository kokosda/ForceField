using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEditor.Domain
{
    public class ToolEventClick
    {
        public delegate void ClickEvent(object sender, EventArgs e);

        public ClickEvent Click { get; set; }
        public string ItemName { get; set; }
    }
}
