using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;

using ForceField.Domain.Renderer;
using ForceField.Domain.Renderer.Base;

namespace ForceField.Interfaces
{
    public interface IAnimationService
    {
        #region Properties

        IList<Animation> Animations
        {
            get;
            set;
        }

        #endregion

        void LoadAllAnimations(string rootDirectory);

        void Reset(Animation anim);

        KeyFrame NextKeyFrame(Animation anim);

        bool IsTimeToSlideFrame(Animation anim);

        void Update(Animation anim, int delta);

        Texture2D GetCurrentTexture(Animation anim);
    }
}
