using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ForceField.Interfaces;
using ForceField.Domain.GameLogic;
using ForceField.Domain.Content;
using ForceField.Domain.Renderer;
using ForceField.Core.Services;
using ForceField.Core.Managers;

namespace ForceField.Core.Services
{
    public class UnitsService<U> : IUnitsService<U> where U : Unit, new ()
    {
        public UnitsService(IList<U> units, UnitsManager unitsManager)
        {
            Units = units;
            this.unitsManager = unitsManager;

            spriteBatchService = unitsManager.Game.Services.GetService(typeof(ISpriteBatchService)) as ISpriteBatchService;
            animationService = unitsManager.Game.Services.GetService(typeof(IAnimationService)) as IAnimationService;
            camera = unitsManager.Game.Services.GetService(typeof(ICamera2DService)) as ICamera2DService;
        }

        public void Update(GameTime gameTime)
        { 
            var millisecondsPassed = gameTime.ElapsedGameTime.Milliseconds;

            if (camera.Data.IsUpdate == true)
            {
                Vector2 cameraLocation = camera.Data.Translation;
                Sprite sprite = null;
                for (int i = 0; i < Units.Count; ++i)
                {
                    sprite = Units[i].Sprite;
                    sprite.TranslationPosition = sprite.Location + cameraLocation;
                    animationService.Update(sprite.CurrentAnimation, millisecondsPassed);
                }
            }
            else
            {
                for (int i = 0; i < Units.Count; ++i)
                {
                    animationService.Update(Units[i].Sprite.CurrentAnimation, millisecondsPassed);
                }
            }

        }

        public void Draw(GameTime gameTime)
        {
            foreach (Unit unit in Units)
            {
                spriteBatchService.Draw(unit.Sprite);
            }
        }

        public Unit GetRandom()
        {
            var random = new Random();

            var val = random.Next(0, Units.Count - 1);

            return Units[val];
        }

        public Texture2D GetFirstTexture(U unit)
        {
            return spriteService.GetTexture(unit.Sprite, 0);
        }

        #region Internal

        public void CreateUnitsFromServiceByResourcePath(string templatePath)
        {
            if (spriteService != null)
            {
                var spritesFromTemplate = spriteService.Sprites.Where(p => p.TextureResources.ElementAt(0).ResourcePath.Contains(templatePath));
                
                for (var i = 0; i < spritesFromTemplate.Count(); i++)
                {
                    var sprite = spritesFromTemplate.ElementAt(i);

                    var unit = new U()
                    {
                        Name = sprite.Name,
                        Sprite = Sprite.Clone(sprite, null)
                    };

                    unit.Sprite.Unit = unit;

                    Units.Add(unit);
                }
            }
            else
            {
                Debug.WriteLine(string.Format("{0}: {1} was not set", GetType().Name, spriteService.GetType().Name));
            }
        }

        public void SetSpriteService(ISpriteService spriteService)
        {
            this.spriteService = spriteService;
        }

        public override string ToString()
        {
            return string.Format("Units<{0}> = {1}", typeof(U).Name, Units != null ? Units.Count() : 0);
        }

        #endregion

        #region Properties

        public IList<U> Units { get; set; }

        #endregion

        #region private

        #region fields
        private ISpriteBatchService spriteBatchService;
        private ISpriteService spriteService;
        private UnitsManager unitsManager;
        private IAnimationService animationService;
        private ICamera2DService camera;
        #endregion

        #endregion
    }
}
