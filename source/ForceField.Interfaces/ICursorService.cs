using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForceField.Domain;
using ForceField.Domain.GameLogic;
using Microsoft.Xna.Framework;

namespace ForceField.Interfaces
{
    public interface ICursorService
    {
        Action<Unit> CheckIsSelect { get; set; }
        void ShowSystemCursor();
        void SetCursor(Cursor cur);
        void CheckSelect(Unit unit);
        void ShowGameCursor();
        void Draw(GameTime gameTime);
        void Update(GameTime gameTime);
    }
}
