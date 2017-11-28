using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForceField.Domain
{
    public class SaveTask
    {
        public object Data { get; set; }
        public string File { get; set; }

        public SaveTask(object data, string file)
        {
            Data = data;
            File = file;
        }

    }
}
