using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForceField.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using ForceField.Domain.Renderer;
using ForceField.Domain.GameLogic;
using Microsoft.Xna.Framework;
using ForceField.Domain.Content;
using ForceField.Domain.Renderer.Base;
using System.Collections;
using ForceField.Domain;
using ForceField.Domain.Comparers;


namespace ForceField.Core.Services
{
    public class SpriteBatchService : ISpriteBatchService
    {
        public ICamera2DService Camera2D { get; set; }

        public SpriteBatchService()
        {
            spriteBlocks = new Dictionary<RenderData, List<Sprite>>();
            spriteTextBlocks = new List<SpriteText>();
        }

        public void Initialize(Game game)
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            animationService = game.Services.GetService(typeof(IAnimationService)) as IAnimationService;
            spriteService = game.Services.GetService(typeof(ISpriteService)) as ISpriteService;
            Camera2D = game.Services.GetService(typeof(ICamera2DService)) as ICamera2DService;
            cameraData = Camera2D.Data;
            InitializeSprites();
        }

        public void Reset()
        {
            spriteBlocks = new Dictionary<RenderData, List<Sprite>>();
            InitializeSprites();
        }

        public void DrawAll(GameTime gameTime)
        {
            DrawAllSprites(gameTime);
            DrawAllText();
        }

        public void Draw(SpriteText text)
        {
            if (!text.CanDraw)
            {
                return;
            }

            Rectangle view = spriteBatch.GraphicsDevice.Viewport.Bounds;
            Rectangle rect = text.BoundingRectangle;

            if (!view.Intersects(rect))
            {
                return;
            }

            spriteTextBlocks.Add(text);
        }

        public void Draw(Sprite sprite)
        {
            if (!sprite.CanDraw)
            {
                return;
            }

            Rectangle view = spriteBatch.GraphicsDevice.Viewport.Bounds;
            Rectangle rect = sprite.BoundingRectangle;

            if (!view.Intersects(rect))
            {
                return;
            }

            spriteBlocks[sprite.RenderData].Add(sprite);
        }

        #region private
        private void DrawAllSprites(GameTime gameTime)
        {
            RenderData[] renderData = spriteBlocks.Keys.ToArray();
            for (int i = 0; i < renderData.Length; ++i)
            {
                RenderData data = renderData[i];
                var sprites = spriteBlocks[data];
                int count = sprites.Count;

                foreach (var resourceEffect in data.Effects)
                {
                        spriteBatch.Begin(SpriteSortMode.FrontToBack,
                        data.blendState,
                        null,
                        data.DepthStencilState,
                        null,
                        resourceEffect.Data,
                        Camera2D.Matrix);

                    DrawSpriteBlock(data, sprites, gameTime);

                    spriteBatch.End();
                }
                sprites.Clear();
            }
        }

        private void DrawAllText()
        {
            spriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend);
            for (int i = 0; i < spriteTextBlocks.Count; ++i)
            {
                SpriteText text = spriteTextBlocks[i];
                spriteBatch.DrawString(text.SpriteFont, text.Text, text.Location, text.Color, text.Angle, text.Origin, text.Scale, text.Effect, 1);
            }
            spriteBatch.End();

            spriteTextBlocks.Clear();
        }

        private void DrawSpriteBlock(RenderData renderData,List<Sprite> sprites,GameTime gameTime)
        {
            Sprite sprite;

            var millisecondsPassed = gameTime.ElapsedGameTime.Milliseconds;

            for (int j = 0; j < sprites.Count; ++j)
            {
                sprite = sprites[j];


                animationService.Update(sprite.CurrentAnimation, millisecondsPassed);
                spriteBatch.Draw(sprite.CurrentAnimation.CurrentKeyFrame.Texture,
                                 sprite.TranslationPosition,
                                 sprite.ClientRectangle,
                                 sprite.Color,
                                 sprite.Angle,
                                 //or Sprite.Origin
                                 Vector2.Zero,
                                 sprite.Scale,
                                 sprite.Effect,
                                 sprite.Layer);
            }
        }

        private void InitializeSprites()
        {
            List<RenderData> renderData = new List<RenderData>();

            for (int i = 0; i < spriteService.Sprites.Count; ++i)
            {
                if (!renderData.Contains(spriteService.Sprites[i].RenderData))
                {
                    renderData.Add(spriteService.Sprites[i].RenderData);
                }
            }

            RenderData[] compareRenderData = new RenderData[renderData.Count];

            renderData.CopyTo(compareRenderData);

            Array.Sort<RenderData>(compareRenderData, new LayerSort());

            for (int i = 0; i < renderData.Count; ++i)
            {
                spriteBlocks.Add(compareRenderData[i], new List<Sprite>());
            }
        }

        private CameraData cameraData;
        private SpriteBatch spriteBatch;
        private IAnimationService animationService;
        private ISpriteService spriteService;
        private List<SpriteText> spriteTextBlocks;
        private Dictionary<RenderData, List<Sprite>> spriteBlocks;
        #endregion
    }
}
