using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ForceField.Interfaces;
using ForceField.Domain.GameLogic;

namespace ForceField.Core.Managers.Services
{
    public class LocationService : ILocationService
    {
        public LocationService()
        {
            currentLocation = null;
        }

        public void SomeMethod()
        {
        }

        #region Properties

        public Location CurrentLocation
        {
            get
            {
                return currentLocation;
            }
        }

        #endregion

        #region private

        private Location currentLocation;

        #endregion
    }
}
