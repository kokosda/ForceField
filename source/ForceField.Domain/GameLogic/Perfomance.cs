using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ForceField.Domain.Renderer;

namespace ForceField.Domain.GameLogic
{
    public class Perfomance : Entity
    {
        public Perfomance()
        {
        }

        public string ScriptPath { get; set; }

        public string ScriptName { get; set; }

        public Sprite Icon { get; set; }
    }
}
