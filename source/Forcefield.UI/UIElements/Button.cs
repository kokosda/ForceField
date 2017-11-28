
using ForceField.UI.Enums;
using Microsoft.Xna.Framework;
using ForceField.Domain.Renderer;


namespace ForceField.UI.UIElements
{
    public class Button : UIComponent
    {
        public Button(Sprite sprite, SpriteText spriteText, ButtonAlign align, float indent, string text, Vector2 location)
        {
            this.Sprite = sprite;
            this.SpriteText = spriteText;
            this.align = align;
            Indent = indent;

            Text = text;
            Location = location;
            //Align = align;
            Clickable = true;
            FocusColor = DefaultFocusColor;
            NormalColor = DefaultNormalColor;
            SpriteColor = NormalColor;
        }

        public Button(Sprite sprite, Vector2 location)
            : this(sprite, null, ButtonAlign.Center, 0, "", location)
        {
        }

        public Button(Sprite sprite, SpriteText spriteText, ButtonAlign align, float indent)
            : this(sprite, spriteText, align, indent, "", new Vector2(0, 0))
        {
        }

        public Button(Sprite sprite)
            : this(sprite, null, ButtonAlign.Center, 0, "", new Vector2(0, 0))
        {
        }

        static Button()
        {
            DefaultNormalColor = Color.White;
            DefaultFocusColor = Color.Turquoise;
        }

        #region Properties

        public static Color DefaultNormalColor { get; set; }

        public static Color DefaultFocusColor { get; set; }

        public bool Pushed { get; set; }

        public float Indent { get; set; }

        public bool IsFocus { get; set; }

        public Color FocusColor { get; set; }

        public Color NormalColor { get; set; }

        public ButtonAlign Align
        {
            get
            {
                return align;
            }
            set
            {
                align = value;

                if (SpriteText != null)
                {
                    SetAlign();
                }
            }
        }

        public Vector2 Location
        {
            get
            {
                return Sprite.Location;
            }
            set
            {
                Sprite.Location = value;

                if (SpriteText != null)
                {
                    SetAlign();
                }
            }
        }

        public string Text
        {
            get
            {
                if (SpriteText == null)
                {
                    return "";
                }
                else
                {
                    return SpriteText.Text;
                }
            }
            set
            {
                if (SpriteText != null)
                {
                    SpriteText.Text = value;
                }
            }
        }

        public Color SpriteColor
        {
            get
            {
                return Sprite.Color;
            }
            set
            {
                Sprite.Color = value;
            }
        }

        #endregion

        #region private

        private void SetAlign()
        {
            Rectangle region = Sprite.BoundingRectangle;
            Rectangle textRegion = SpriteText.BoundingRectangle;

            // Работет верно только для ButtonAlign.Center
            switch (align)
            {
                case ButtonAlign.Center:
                    //SpriteText.Location = new Vector2(region.Center.X - textRegion.Center.X - (Indent/2), region.Center.Y - textRegion.Center.Y);
                    Vector2 newPosition = new Vector2(region.Center.X - (textRegion.Width / 2), region.Center.Y - (textRegion.Height / 2));
                    SpriteText.Location = newPosition;
                    break;

                case ButtonAlign.Left:
                    SpriteText.Location = new Vector2(region.Left + (Indent / 2), region.Center.Y - textRegion.Center.Y);
                    break;

                case ButtonAlign.Right:
                    SpriteText.Location = new Vector2(region.Right - (Indent / 2), region.Center.Y - textRegion.Center.Y);
                    break;
                
                default:
                    SpriteText.Location = new Vector2(region.Center.X - textRegion.Center.X, region.Center.Y - textRegion.Center.Y);
                    break;
            }
        }

        #region fields

        private ButtonAlign align;

        #endregion

        #endregion
    }
}
