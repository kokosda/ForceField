using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ForceField.Domain.Renderer.Base;
using ForceField.Domain.Renderer;
using ForceField.Domain.GameLogic;
using ForceField.Domain.GameLogic.Comparers;
using ForceField.Core.Services;
using ForceField.Interfaces;


namespace ForceField.Core.Managers
{
    public class AnimationManager : DrawableGameComponent
    {
        public AnimationManager(Game game, string animationsFilesDirectory) : base(game)
        {
            this.animationsFilesDirectory = animationsFilesDirectory;

            animationService = new AnimationService(new List<Animation>());
            Game.Services.AddService(typeof(IAnimationService), animationService);
            Game.Components.Add(this);
        }

        #region Properties

        public string Version
        {
            get
            {
                return "0.1";
            }
        }

        public string AnimationsFilesDirectory
        {
            get
            {
                return animationsFilesDirectory;
            }
        }

        public string AnimationFileExtension
        {
            get
            {
                return ".anim";
            }
        }

        #endregion

        public override void Initialize()
        {
            var spriteService = Game.Services.GetService(typeof(ISpriteService)) as ISpriteService;
            animationService.SetSpriteService(spriteService);

            AnimationTypes.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            animationService.LoadAllAnimations(animationsFilesDirectory);

            base.LoadContent();
        }

        #region private fields

        private string animationsFilesDirectory = string.Empty;
        private AnimationService animationService;

        #endregion
    }
}