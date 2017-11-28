using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ForceField.Domain.GameLogic;

namespace ForceField.Interfaces
{
    public interface IGlobalMapService
    {
        GlobalMap CurrentGlobalMap
        {
            get;
        }
    }
}
