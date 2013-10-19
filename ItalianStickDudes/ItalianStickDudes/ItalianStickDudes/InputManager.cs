using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ItalianStickDudes
{
    class InputManager
    {
        private KeyboardState PreviousKeyboardState;
        private KeyboardState CurrentKeyboardState;

        private MouseState CurrentMouseState;
        private MouseState PreviousMouseState;

        private GamePadState[] PreviousGamePadStates;
        private GamePadState[] CurrentGamePadStates;

        public InputManager()
        {
            PreviousGamePadStates = new GamePadState[4];
            CurrentGamePadStates = new GamePadState[4];

            PreviousKeyboardState = new KeyboardState();
            CurrentKeyboardState = new KeyboardState();

            CurrentMouseState = new MouseState();
            PreviousMouseState = new MouseState();
        }

        public void Update()
        {
            PreviousKeyboardState = CurrentKeyboardState;
            PreviousMouseState = CurrentMouseState;

            for (int i = 0; i < 4; i++)
            {
                PreviousGamePadStates[i] = CurrentGamePadStates[i];
            }

            CurrentMouseState = Mouse.GetState();

            CurrentKeyboardState = Keyboard.GetState();
            CurrentGamePadStates[0] = GamePad.GetState(PlayerIndex.One);
            CurrentGamePadStates[1] = GamePad.GetState(PlayerIndex.Two);
            CurrentGamePadStates[2] = GamePad.GetState(PlayerIndex.Three);
            CurrentGamePadStates[3] = GamePad.GetState(PlayerIndex.Four);
        }

        public KeyboardState GetCurrentKeyboardState()
        {
            return CurrentKeyboardState;
        }

        public KeyboardState GetPreviousKeyboardState()
        {
            return PreviousKeyboardState;
        }

        public MouseState GetCurrentMouseState()
        {
            return CurrentMouseState;
        }

        public MouseState GetPreviousMouseState()
        {
            return PreviousMouseState;
        }

        public bool IsNewKeyDown(Keys key)
        {
            if(CurrentKeyboardState.IsKeyDown(key) &&
                PreviousKeyboardState.IsKeyUp(key))
            {
                return true;
            }
            return false;
        }

        public GamePadState GetCurrentGamePadState(int index)
        {
            return CurrentGamePadStates[index - 1];
        }

        public GamePadState GetPreviousGamePadState(int index)
        {
            return PreviousGamePadStates[index - 1];
        }

        public bool AnyPlayerPressed(Buttons button)
        {
            for (int i = 0; i < 4; i++)
            {
                if (CurrentGamePadStates[i].IsButtonDown(button) && 
                    PreviousGamePadStates[i].IsButtonUp(button))
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsNewLeftMouseDown()
        {
            if (CurrentMouseState.LeftButton == ButtonState.Pressed &&
                PreviousMouseState.LeftButton == ButtonState.Released)
                return true;
            return false;
        }

        public string GetInputString()
        {
            string str = "";

            foreach (Keys key in CurrentKeyboardState.GetPressedKeys())
            {
                if (PreviousKeyboardState.IsKeyUp(key))
                {
                    if (key != Keys.LeftShift && key != Keys.RightShift && key != Keys.LeftControl
                        && key != Keys.RightControl && key != Keys.RightAlt && key != Keys.LeftAlt
                         && key != Keys.Back)
                    {
                        if (key == Keys.Space)
                        {
                            str += " ";
                        }
                        else
                        {
                            if (CurrentKeyboardState.IsKeyDown(Keys.LeftShift))
                                str += key.ToString();
                            else
                                str += key.ToString().ToLower();
                        }
                    }
                }
            }

            return str;
        }
    }
}
