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

        private bool Running = false;
        private bool Once = false;

        public Player()
        {

        }

        public virtual void Initialize(Texture2D playerTexture, int WhichPlayer, Vector2 StartPosition)
        {
            Position = StartPosition;
            SpriteTexture = playerTexture;
            PlayerNumber = WhichPlayer;
            
            InitializeAnimation(1, 7);
            PlayAnimation(0, 2, 800);
        }

        public virtual void Update(GameTime gameTime, InputManager Input)
        {
            base.Update(gameTime);
            GamePadState gamePad = Input.GetCurrentGamePadState(PlayerNumber);

         
            Velocity.X += gamePad.ThumbSticks.Left.X;

            if (gamePad.ThumbSticks.Left.X == 0.0f)
            {
                if (Velocity.X > 1.0f)
                {
                    Velocity.X -= 0.6f;
                    Running = true;
                }
                else if (Velocity.X < -1.0f)
                {
                    Running = true;
                    Velocity.X += 0.6f;
                }

                if (Velocity.X < 1.0f && Velocity.X > -1.0f)
                {
                    Velocity.X = 0.0f;
                    Once = false;
                    Running = false;
                    PlayAnimation(0, 2, 800);
                }
            }

            if (!Once)
            {
                if (Running)
                {
                    PlayAnimation(3, 6, 100);
                    Once = true;
                }
            }

            if (Velocity.X >= 30.0f)
                Velocity.X = 30.0f;
            else if (Velocity.X <= -30.0f)
                Velocity.X = -30.0f;

            Position += Velocity;
        }
    }
}
