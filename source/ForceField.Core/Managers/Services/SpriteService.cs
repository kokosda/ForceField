using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using ForceField.Interfaces;
using ForceField.Domain.Renderer;
using ForceField.Domain.Content;
using ForceField.Domain.Renderer.Base;


namespace ForceField.Core.Services
{
    public class SpriteService : ISpriteService
    {
        public SpriteService(IList<Sprite> sprites, IList<SpriteText> spriteTexts)
        {
            Sprites = sprites;
            SpriteTexts = spriteTexts;
        }

        #region Internal

        public void SetTextureResourceService(IResourceService<Texture2D> resourceService)
        {
            TexturesService = resourceService;
        }

        public void SetEffectsService(IResourceService<Effect> effectsService)
        {
            EffectsService = effectsService;
        }

        public void SetFontsService(IResourceService<SpriteFont> fontsService)
        {
            FontsService = fontsService;
        }

        public void CreateSpritesFromsResourceServices()
        {
            var effects = EffectsService.Resources;
            var resources = TexturesService.Resources;

            //Заглушка на время
            RenderData renderData = new RenderData();

            for (var i = 0; i < resources.Count(); i++)
            {
                var res = resources.ElementAt(i);
                var name = Regex.Replace(res.Name, noDigitsPattern, string.Empty);
                var justSprite = Sprites.FirstOrDefault(p => p.Name == name);

                if (justSprite == null)
                {
                    var newSprite = new Sprite()
                    {
                        TextureResources = new List<ContentResource<Texture2D>>(),
                        Name = name,
                        Color = new Color(1.0f, 1.0f, 1.0f,1.0f)
                    };
                    newSprite.RenderData = renderData;
                    newSprite.RenderData.Effects = effects;
                    newSprite.TextureResources.Add(res);
                    newSprite.CanDraw = true;
                    Sprites.Add(newSprite);
                }
                else
                {
                    justSprite.TextureResources.Add(res);
                }
            }
        }

        public void CreateSpriteTextsFromsResourceService()
        {
            var fonts = FontsService.Resources;
            RenderData renderData = new RenderData();
            for (int i = 0; i < fonts.Count; ++i)
            {
                SpriteText text = new SpriteText();
                text.CanDraw = true;
                text.Name = fonts[i].Name;
                text.Scale = 1f;
                text.SpriteFont = fonts[i].Data;
                text.RenderData = renderData;

                SpriteTexts.Add(text);
            }
        }

        #endregion

        public Sprite GetByName(string name)
        {
            return Sprite.Clone(Sprites.FirstOrDefault(p => p.Name == name));
        }

        public SpriteText GetSpriteText(string name)
        {
            return SpriteTexts.First(p => p.Name == name).Clone() as SpriteText;
        }

        public Texture2D GetTexture(Sprite sprite, string textureName)
        {
            var texture = sprite.TextureResources.FirstOrDefault(p => p.Name == textureName);

            return texture != null ? texture.Data : null;
        }

        public Texture2D GetTexture(Sprite sprite, int index)
        {
            return sprite.TextureResources[index].Data;
        }

        public void SetAnimatedAction(AnimatedAction action, Sprite onSprite)
        {
            onSprite.CurrentAction = action;
        }

        public override string ToString()
        {
            return string.Format("Sprites count = {0}", Sprites.Count);
        }

        #region Properties

        public IList<Sprite> Sprites { get; set; }

        public IList<SpriteText> SpriteTexts { get; set; }

        protected IResourceService<Texture2D> TexturesService { get; set; }

        protected IResourceService<Effect> EffectsService { get; set; }

        protected IResourceService<SpriteFont> FontsService { get; set; }

        #endregion

        #region private

        private readonly string noDigitsPattern = @"[\d]+";

        #endregion
    }
}
