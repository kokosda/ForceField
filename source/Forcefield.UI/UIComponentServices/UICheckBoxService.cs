using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using ForceField.UI.UIElements;
using ForceField.Domain.Renderer;
using ForceField.Interfaces;

namespace ForceField.UI.UIComponentServices
{
    public class UICheckBoxService : UIComponentService
    {
        public UICheckBoxService(ISpriteBatchService spriteBatchService, IUserInputService inputService)
            : base(spriteBatchService, inputService)
        {
        }

        public override void Update(GameTime gameTime)
        {
            foreach (CheckBox checkbox in Elements)
            {
                if (checkbox.Button.Clicked)
                {
                    if (checkbox.State == Enums.CheckBoxState.Select)
                    {
                        checkbox.State = Enums.CheckBoxState.Unknow;
                        checkbox.Button.Sprite = checkbox.NormalSprite;
                    }
                    else
                    {
                        checkbox.State = Enums.CheckBoxState.Select;
                        checkbox.Button.Sprite = checkbox.SelectedSprite;
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
