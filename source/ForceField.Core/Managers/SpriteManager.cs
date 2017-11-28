using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ForceField.Interfaces;
using ForceField.Core.Services;
using ForceField.Domain.Renderer;

namespace ForceField.Core.Managers
{
    public class SpriteManager : DrawableGameComponent
    {
        public SpriteManager(Game game) : base(game)
        {
            spriteService = new SpriteService(new List<Sprite>(),new List<SpriteText>());
            game.Services.AddService(typeof(ISpriteService), SpriteService);
            Game.Components.Add(this);
        }

        public override void Initialize()
        {
            var textureService = Game.Services.GetService(typeof(IResourceService<Texture2D>)) as IResourceService<Texture2D>;
            var effectsService = Game.Services.GetService(typeof(IResourceService<Effect>)) as IResourceService<Effect>;
            var fontsService = Game.Services.GetService(typeof(IResourceService<SpriteFont>)) as IResourceService<SpriteFont>;
            RegisterResourceServices(textureService, effectsService,fontsService);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteService.CreateSpritesFromsResourceServices();
            SpriteService.CreateSpriteTextsFromsResourceService();
            base.LoadContent();
        }

        public override string ToString()
        {
            return string.Format("Sprite services count = {0}", this.GetType().GetProperties().Where(p => p.PropertyType.Name.Contains("SpriteService")).Count());
        }

        #region Properties

        public SpriteService SpriteService
        {
            get
            {
                return spriteService;
            }
        }

        #endregion

        #region private

        private void RegisterResourceServices(IResourceService<Texture2D> textureService, IResourceService<Effect> effectsService,IResourceService<SpriteFont> fontsService)
        {
            SpriteService.SetTextureResourceService(textureService);
            SpriteService.SetEffectsService(effectsService);
            SpriteService.SetFontsService(fontsService);
        }

        #region fields

        private SpriteService spriteService;

        #endregion

        #endregion
    }
}
