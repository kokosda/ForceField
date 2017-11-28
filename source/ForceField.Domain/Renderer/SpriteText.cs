using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ForceField.Domain.Renderer
{
    public class SpriteText : Sprite, ICloneable
    {
        public SpriteText(Vector2 location) 
            : base(location)
        {
            Name = "";
        }

        public SpriteText()
        {
            Name = "";
        }

        public SpriteText(Sprite sprite)
        {
            Angle = sprite.Angle;
            Animations = sprite.Animations;
            CanDraw = sprite.CanDraw;
            Color = sprite.Color;
            CurrentAction = sprite.CurrentAction;
            Effect = sprite.Effect;
            Location = sprite.Location;
            Name = sprite.Name;
            RenderData = sprite.RenderData;
            Scale = sprite.Scale;
            TextureResources = sprite.TextureResources;
            Unit = sprite.Unit;
        }

        #region Properties

        public SpriteFont SpriteFont { get; set; }

        public string Text { get; set; }

        public new Rectangle BoundingRectangle
        {
            get
            {
                Vector2 text = SpriteFont.MeasureString(Name);
                return new Rectangle((int)Location.X, (int)Location.Y, (int)(text.X * Scale), (int)(text.Y * Scale));
            }
        }

        public override Vector2 Origin
        {
            get
            {
                return new Vector2(0, 0);
            }
        }

        #endregion

        public object Clone()
        {
            SpriteText newSpriteText = new SpriteText(Sprite.Clone(this));
            newSpriteText.Text = Text;
            newSpriteText.SpriteFont = SpriteFont;

            return newSpriteText;
        }

        
    }
}
