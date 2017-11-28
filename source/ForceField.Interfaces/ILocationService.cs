using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ForceField.Domain.GameLogic;

namespace ForceField.Interfaces
{
    public interface ILocationService
    {
        void SomeMethod();

        Location CurrentLocation
        {
            get;
        }
    }
}
