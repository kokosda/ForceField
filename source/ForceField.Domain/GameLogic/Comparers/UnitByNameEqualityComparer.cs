using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ForceField.Domain.GameLogic;

namespace ForceField.Domain.GameLogic.Comparers
{
    public class UnitByNameEqualityComparer : IEqualityComparer<Unit>
    {
        public bool Equals(Unit x, Unit y)
        {
            return x.Name.Equals(y.Name);
        }

        public int GetHashCode(Unit obj)
        {
            return obj.GetHashCode();
        }
    }
}
