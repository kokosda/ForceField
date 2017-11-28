using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using ForceField.Domain.Renderer;

namespace ForceField.UI.UIElements
{
    public class Label : UIComponent
    {
        public Label(SpriteText spriteText, Vector2 location, string text)
        {
            Sprite = null;
            this.SpriteText = spriteText;
            Clickable = false;

            Location = location;
            Text = text;
            TextColor = new Color(0.0f, 0.0f, 0.0f);
        }

        public Label(SpriteText spriteText)
            : this(spriteText, new Vector2(0, 0), "")
        {
        }

        #region Properties

        public Vector2 Location 
        {
            get
            {
                return SpriteText.Location;
            }
            set
            {
                SpriteText.Location = value;
            }
        }

        public string Text
        {
            get
            {
                return SpriteText.Text;
            }
            set
            {
                SpriteText.Text = value;
            }
        }

        public Color TextColor
        {
            get
            {
                return SpriteText.Color;
            }
            set
            {
                SpriteText.Color = value;
            }
        }

        #endregion
    }
}
