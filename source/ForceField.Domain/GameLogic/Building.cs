using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForceField.Domain.GameLogic
{
    public class Building : Unit
    {
        public override string ToString()
        {
            return Name;
        }
    }
}
