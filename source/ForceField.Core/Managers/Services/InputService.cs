using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using ForceField.Domain.Input;
using ForceField.Interfaces;
namespace ForceField.Core.Service
{
    public class InputService : IUserInputService
    {
        public const int MaxInputs = 4;

        public readonly KeyboardState[] CurrentKeyboardStates;
        public readonly GamePadState[] CurrentGamePadStates;

        public readonly KeyboardState[] LastKeyboardStates;
        public readonly GamePadState[] LastGamePadStates;

        public readonly bool[] GamePadWasConnected;

        public Rectangle SelectRegion;

        public MouseState CurrentMouseState;
        public MouseState LastMouseState;
        public TouchCollection TouchState;

        public readonly List<GestureSample> Gestures = new List<GestureSample>();

        public InputService()
        {
            CurrentKeyboardStates = new KeyboardState[MaxInputs];
            CurrentGamePadStates = new GamePadState[MaxInputs];
            CurrentMouseState = new MouseState();

            LastKeyboardStates = new KeyboardState[MaxInputs];
            LastGamePadStates = new GamePadState[MaxInputs];
            LastMouseState = new MouseState();

            GamePadWasConnected = new bool[MaxInputs];
        }

        public void Update()
        {
            LastMouseState = CurrentMouseState;
            for (int i = 0; i < MaxInputs; i++)
            {
                LastKeyboardStates[i] = CurrentKeyboardStates[i];
                LastGamePadStates[i] = CurrentGamePadStates[i];

                CurrentKeyboardStates[i] = Keyboard.GetState((PlayerIndex)i);
                CurrentGamePadStates[i] = GamePad.GetState((PlayerIndex)i);

                if (CurrentGamePadStates[i].IsConnected)
                {
                    GamePadWasConnected[i] = true;
                }
            }

            CurrentMouseState = Mouse.GetState();
            TouchState = TouchPanel.GetState();
            Gestures.Clear();
            //while (TouchPanel.IsGestureAvailable)
            //{
            //    Gestures.Add(TouchPanel.ReadGesture());
            //}
        }

        public bool IsKeyPressed(Keys keys)
        {
            return CurrentKeyboardStates[0].IsKeyDown(keys);
        }

        public bool IsKeyPress(Keys keys)
        {
            return CurrentKeyboardStates[0].IsKeyDown(keys) && CurrentKeyboardStates[0].IsKeyUp(keys);
        }

        public bool IsMouseButtonPressed(MouseButton button)
        {
            if (button == MouseButton.LeftButton)
            {
                return CurrentMouseState.LeftButton == ButtonState.Pressed;
            }
            else
            if (button == MouseButton.RightButton)
            {
                return CurrentMouseState.RightButton == ButtonState.Pressed;
            }
            else
            if (button == MouseButton.MiddleButton)
            {
                return CurrentMouseState.MiddleButton == ButtonState.Pressed;
            }

            return false;
        }

        public bool IsMouseButtonPress(MouseButton button)
        {
            if (button == MouseButton.LeftButton)
            {
                return CurrentMouseState.LeftButton == ButtonState.Pressed && LastMouseState.LeftButton == ButtonState.Released;
            }
            else
            if (button == MouseButton.RightButton)
            {
                return CurrentMouseState.RightButton == ButtonState.Pressed && LastMouseState.RightButton == ButtonState.Released;
            }
            else
            if (button == MouseButton.MiddleButton)
            {
                return CurrentMouseState.MiddleButton == ButtonState.Pressed && LastMouseState.MiddleButton == ButtonState.Released;
            }

            return false;
        }

        public Point IsMousePosition()
        {
            return new Point(CurrentMouseState.X, CurrentMouseState.Y);
        }

        public bool IsMouseScrollPressed(MouseScroll scroll)
        {
            if (scroll == MouseScroll.MouseScrollDown)
            {
                return CurrentMouseState.ScrollWheelValue < LastMouseState.ScrollWheelValue;
            }
            else
            if(scroll == MouseScroll.MouseScrollUp)
            {
                return CurrentMouseState.ScrollWheelValue > LastMouseState.ScrollWheelValue;
            }

            return false;
        }

        public bool IsKeyPressed(Keys key, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue)
            {
                playerIndex = controllingPlayer.Value;

                int i = (int)playerIndex;

                return CurrentKeyboardStates[i].IsKeyDown(key);
            }
            else
            {
                return (IsKeyPressed(key, PlayerIndex.One, out playerIndex) ||
                        IsKeyPressed(key, PlayerIndex.Two, out playerIndex) ||
                        IsKeyPressed(key, PlayerIndex.Three, out playerIndex) ||
                        IsKeyPressed(key, PlayerIndex.Four, out playerIndex));
            }
        }

        public bool IsButtonPressed(Buttons button, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue)
            {
                playerIndex = controllingPlayer.Value;

                int i = (int)playerIndex;

                return CurrentGamePadStates[i].IsButtonDown(button);
            }
            else
            {
                return (IsButtonPressed(button, PlayerIndex.One, out playerIndex) ||
                        IsButtonPressed(button, PlayerIndex.Two, out playerIndex) ||
                        IsButtonPressed(button, PlayerIndex.Three, out playerIndex) ||
                        IsButtonPressed(button, PlayerIndex.Four, out playerIndex));
            }
        }

        public bool IsKeyPress(Keys key, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue)
            {
                playerIndex = controllingPlayer.Value;

                int i = (int)playerIndex;

                return (CurrentKeyboardStates[i].IsKeyDown(key) &&
                        LastKeyboardStates[i].IsKeyUp(key));
            }
            else
            {
                return (IsKeyPress(key, PlayerIndex.One, out playerIndex) ||
                        IsKeyPress(key, PlayerIndex.Two, out playerIndex) ||
                        IsKeyPress(key, PlayerIndex.Three, out playerIndex) ||
                        IsKeyPress(key, PlayerIndex.Four, out playerIndex));
            }
        }

        public bool IsButtonPress(Buttons button, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue)
            {
                playerIndex = controllingPlayer.Value;

                int i = (int)playerIndex;

                return (CurrentGamePadStates[i].IsButtonDown(button) &&
                        LastGamePadStates[i].IsButtonUp(button));
            }
            else
            {
                return (IsButtonPress(button, PlayerIndex.One, out playerIndex) ||
                        IsButtonPress(button, PlayerIndex.Two, out playerIndex) ||
                        IsButtonPress(button, PlayerIndex.Three, out playerIndex) ||
                        IsButtonPress(button, PlayerIndex.Four, out playerIndex));
            }
        }
    }
}
