using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForceField.Interfaces;
using ForceField.Domain;
using Microsoft.Xna.Framework;
using ForceField.Domain.GameLogic;
using ForceField.Domain.Input;

namespace ForceField.Core.Services
{
    public class CursorService : ICursorService
    {
        public Action<Unit> CheckIsSelect { get; set; }

        public CursorService(Game game)
        {
            this.game = game;
            input = game.Services.GetService(typeof(IUserInputService)) as IUserInputService;
            spriteBatchService = game.Services.GetService(typeof(ISpriteBatchService)) as ISpriteBatchService;
            CheckIsSelect = new Action<Unit>(DefaultSelect);
        }

        public void SetCursor(Cursor cur)
        {
            cursor = cur;
        }

        public void ShowSystemCursor()
        {
            if (isVisible == true)
            {
                isVisible = false;
            }

            game.IsMouseVisible = true;
            
        }

        public void CheckSelect(Unit unit)
        {
            CheckIsSelect(unit);
        }

        public void ShowGameCursor()
        {
            if (game.IsMouseVisible == true)
            {
                game.IsMouseVisible = false;
            }

            isVisible = true;
        }

        public void Update(GameTime gameTime)
        {
            Point position = input.IsMousePosition();
            cursor.Sprite.Location = new Vector2(position.X,position.Y);
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatchService.Draw(cursor.Sprite);
        }

        private void DefaultSelect(Unit unit)
        {
            Point mousePosition = input.IsMousePosition();

            if (input.IsMouseButtonPress(MouseButton.LeftButton))
            {
                unit.SetSelect(unit.BoundingBox.Contains(new Vector2(mousePosition.X, mousePosition.Y)));
            }
        }
     
        private ISpriteBatchService spriteBatchService;
        private Cursor cursor;
        private IUserInputService input;
        private bool isVisible;
        private Game game;
    }
}
