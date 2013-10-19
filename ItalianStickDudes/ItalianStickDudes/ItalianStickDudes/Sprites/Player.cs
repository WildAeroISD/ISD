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
        private bool Falling = false;

        private bool Jumping;
        private Vector2 JumpPosition;

        public Player()
        {

        }

        public virtual void Initialize(Texture2D playerTexture, int WhichPlayer, Vector2 StartPosition)
        {
            Position = StartPosition;
            SpriteTexture = playerTexture;
            PlayerNumber = WhichPlayer;

            Jumping = false;
            JumpPosition = Vector2.Zero;
            
            InitializeAnimation(5, 8);
            AddAnimation("idle", 0, 2, 800);
            AddAnimation("running", 2, 8, 600);
            PlayAnimation("idle");
        }

        public virtual void Update(GameTime gameTime, InputManager Input)
        {
            base.Update(gameTime);
            GamePadState gamePad = Input.GetCurrentGamePadState(PlayerNumber);

            float amount = gamePad.ThumbSticks.Left.X;

            if(amount > 0.0f)
                Flip = false;
            else if(amount < 0.0f)
                Flip = true;

            if (amount == 0.0f)
            {
                if (Velocity.X > 0.6f)
                {
                    Velocity.X -= 0.6f;
                    Running = true;
                }
                else if (Velocity.X < -0.6)
                {
                    Running = true;
                    Velocity.X += 0.6f;
                }

                if (Velocity.X < 0.6 && Velocity.X > -0.6)
                {
                    Velocity.X = 0.0f;
                    Running = false;
                }
            }
            else
            {
                Velocity.X += amount;
                Running = true;
            }

            if (Running)
                PlayAnimation("running");
            else if(!Running)
                PlayAnimation("idle");

            if (!Jumping && !Falling)
            {
                if (Input.GetCurrentGamePadState(PlayerNumber).Buttons.A == ButtonState.Pressed)
                {
                    JumpPosition = Position;
                    Jumping = true;
                }

            }
            else
            {
                if(JumpPosition.Y - Position.Y > 300)
                {
                    Falling = true;
                    Jumping = false;
                }

                if (!Falling)
                {
                    Velocity.Y -= 20.0f;
                }
                else
                {
                    Velocity.Y += 15.0f;

                    if(Position.Y >= JumpPosition.Y)
                    {
                        Falling = false;
                        Position.Y = JumpPosition.Y;
                        Velocity.Y = 0.0f;
                    }
                }
            }

            if (Velocity.X >= 20.0f)
                Velocity.X = 20.0f;
            else if (Velocity.X <= -20.0f)
                Velocity.X = -20.0f;

            if (Velocity.Y >= 20.0f)
                Velocity.Y = 20.0f;
            else if (Velocity.Y <= -20.0f)
                Velocity.Y = -20.0f;

            
            Position += Velocity;
        }

        public virtual void Draw(SpriteBatch spriteBatch, Matrix transform)
        {
            int width = SpriteTexture.Width / Columns;
            int height = SpriteTexture.Height / Rows;
            int row = (int)((float)CurrentFrame / (float)Columns);
            int col = CurrentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * col, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, width, height);

            spriteBatch.Begin(SpriteSortMode.BackToFront,
                BlendState.AlphaBlend,
                null, null, null, null, transform);
            if(Flip)
                spriteBatch.Draw(SpriteTexture, destinationRectangle, sourceRectangle, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0.4f);
            else
                spriteBatch.Draw(SpriteTexture, destinationRectangle, sourceRectangle, Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0.4f);
            spriteBatch.End();
        }
    }
}
