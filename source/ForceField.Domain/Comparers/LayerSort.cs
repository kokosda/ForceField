using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForceField.Domain.Renderer;

namespace ForceField.Domain.Comparers
{
    public class LayerSort : IComparer<RenderData>
    {
        public LayerSort()
        {
        }

        public int Compare(RenderData x, RenderData y)
        {
            float oneLayer = x.Layer;
            float twoLayer = y.Layer;

            if (oneLayer > twoLayer)
            {
                return -1;
            }

            return oneLayer < twoLayer ? 1 : 0;
        }
    }
}
