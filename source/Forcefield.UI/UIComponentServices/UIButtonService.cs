using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using ForceField.UI.UIComponentServices;
using ForceField.UI.UIElements;
using ForceField.UI.Enums;
using ForceField.Interfaces;
using ForceField.Domain.Input;
using ForceField.Domain.Renderer;

namespace ForceField.UI.UIComponentServices
  {
    public class UIButtonService : UIComponentService
    {
        public UIButtonService(ISpriteBatchService spriteBatchService, IUserInputService inputService)
            : base(spriteBatchService, inputService)
        {
        }

        

        public UIButtonService(Game game)
            : base(game)
        {
        }

        public Button CreateButton(string spriteName, string fontName, ButtonAlign align, float indent, 
            string text, Vector2 location)
        {
            Sprite sprite = SpriteService.GetByName(spriteName);
            SpriteText font = SpriteService.GetSpriteText(fontName);
            Button button = new Button(sprite, font, align, indent, text, location);

            return button;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (Button button in Elements)
            {
                if (CheckCursorEntered(button))
                {
                    if (CheckClicked(button))
                    {
                        button.Pushed = true;
                    }
                    else if (CheckPushed(button))
                    {
                        button.Pushed = true;
                    }
                    else
                    {
                        button.Pushed = false;
                    }

                    if (button.Pushed)
                    {
                        button.SpriteColor = button.FocusColor;
                    }
                    else
                    {
                        button.SpriteColor = button.NormalColor;
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        #region protected

        protected bool CheckPushed(Button button)
        {
            if (!CheckCursorEntered(button))
            {
                return false;
            }

            return InputService.IsMouseButtonPressed(MouseButton.LeftButton);
        }

        #endregion
    }

}
