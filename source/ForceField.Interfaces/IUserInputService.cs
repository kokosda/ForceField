using ForceField.Domain.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ForceField.Interfaces
{
    public interface IUserInputService
    {
         void Update();
         bool IsMouseButtonPressed(MouseButton button);
         bool IsMouseButtonPress(MouseButton button);
         Point IsMousePosition();
         bool IsMouseScrollPressed(MouseScroll scroll);
         bool IsKeyPressed(Keys key, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex);
         bool IsButtonPressed(Buttons button, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex);
         bool IsKeyPress(Keys key, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex);
         bool IsButtonPress(Buttons button, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex);
         bool IsKeyPress(Keys keys);
         bool IsKeyPressed(Keys keys);
    }
}
