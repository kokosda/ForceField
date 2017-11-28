using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForceField.Interfaces
{
    public interface IStateSaveService
    {
        void Save(object data, string file);
    }
}
