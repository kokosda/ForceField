using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

using ForceField.Domain.Renderer;

namespace ForceField.UI.ViewModels
{
    public class SpellLibrarySlotModel
    {
        public Sprite Icon { get; set; }

        public int InHandNumber { get; set; }

        public int RemainNumber { get; set; }
    }
}
