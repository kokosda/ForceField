using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using ForceField.Domain;

namespace ForceField.Domain.Renderer.Base
{
    public class RenderEntity : Entity
    {
        public RenderEntity()
        {
        }

        public RenderEntity(Vector2 location)
        {
            this.location = location;
        }

        #region Properties

        public float Angle { get; set; }

        public Vector2 Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
            }
        }

        #endregion

        #region private

        private Vector2 location;

        #endregion
    }
}
