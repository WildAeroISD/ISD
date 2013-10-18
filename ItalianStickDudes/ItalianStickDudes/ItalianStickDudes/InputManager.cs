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

        private GamePadState[] PreviousGamePadStates;
        private GamePadState[] CurrentGamePadStates;

        public InputManager()
        {
            PreviousGamePadStates = new GamePadState[4];
            CurrentGamePadStates = new GamePadState[4];

            PreviousKeyboardState = new KeyboardState();
            CurrentKeyboardState = new KeyboardState();
        }

        public void Update()
        {
            PreviousKeyboardState = CurrentKeyboardState;
            PreviousGamePadStates = CurrentGamePadStates;

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

        public GamePadState GetCurrentGamePadState(int index)
        {
            return CurrentGamePadStates[index];
        }

        public GamePadState GetPreviousGamePadState(int index)
        {
            return PreviousGamePadStates[index];
        }

        public bool IfAnyPlayerPressed(Buttons button)
        {
            for (int i = 0; i < 4; i++)
            {
                return true;
            }

            return false;
        }
    }
}
