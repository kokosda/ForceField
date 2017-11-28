using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForceField.Domain.GameLogic
{
    public class SpellLibrary
    {
        public SpellLibrary()
        {
            Slots = new SpellLibrarySlot[15];
        }

        #region Properties

        public SpellLibrarySlot[] Slots { get; set; }

        #endregion
    }
}
