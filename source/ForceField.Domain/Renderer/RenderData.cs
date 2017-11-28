using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ForceField.Domain.Content;

namespace ForceField.Domain.Renderer
{
    public class RenderData
    {
        public RenderData()
        {
            blendState = BlendState.AlphaBlend;
            DepthStencilState = null;
            Effects = null;
            Layer = 1f;
        }

        #region Properties

        public IEnumerable<ContentResource<Effect>> Effects { get; set; }

        public BlendState blendState { get; set; }

        public DepthStencilState DepthStencilState { get; set; }

        public float Layer { get; set; }

        #endregion
    }
}
