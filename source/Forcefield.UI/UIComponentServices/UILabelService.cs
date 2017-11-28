using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

using ForceField.Interfaces;
using ForceField.UI.UIComponentServices;
using ForceField.UI.UIElements;
using ForceField.Domain.Renderer;
using ForceField.Domain.Input;

namespace ForceField.UI.UIComponentServices
{
    public class UILabelService : UIComponentService
    {
        public UILabelService(ISpriteBatchService spriteBatchService, IUserInputService inputService)
            : base(spriteBatchService, inputService)
        {
        }

        public UILabelService(Game game)
            : base(game)
        {
        }

        public Label CreateLabel(string fontName, Vector2 location, string text)
        {
            SpriteText spriteText = SpriteService.GetSpriteText(fontName);
            Label label = new Label(spriteText, location, text);

            return label;
        }

        public void DeleteLabel(string text)
        {
            Elements.Remove(Elements.First(p => p.SpriteText.Text == text));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
