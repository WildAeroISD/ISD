using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace ItalianStickDudes
{
    class Player : AnimatedSprite
    {
        private int PlayerNumber;

        private Vector2 Velocity;

        public Player()
        {

        }

        public void Initialize(int WhichPlayer, Vector2 StartPosition)
        {
            Position = StartPosition;
            PlayerNumber = WhichPlayer;
            this.InitializeAnimation(0, 0);
        }

        public void Update(GameTime gameTime, InputManager Input)
        {
            GamePadState gamePad = Input.GetCurrentGamePadState(PlayerNumber);

            Velocity.X += gamePad.ThumbSticks.Right * 0.10f;
        }
    }
}
