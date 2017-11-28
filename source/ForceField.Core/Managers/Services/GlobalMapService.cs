using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ForceField.Interfaces;
using ForceField.Domain.GameLogic;

namespace ForceField.Core.Managers.Services
{
    public class GlobalMapService : IGlobalMapService
    {
        public GlobalMapService()
        {
            currentGlobalMap = null;
        }

        #region Properties

        public GlobalMap CurrentGlobalMap
        {
            get
            {
                return currentGlobalMap;
            }
        }

        #endregion

        #region private

        private GlobalMap currentGlobalMap;

        #endregion
    }
}
