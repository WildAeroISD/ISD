using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace ItalianStickDudes
{
    public struct PlayerCollisionState
    {
        public bool moveLeft;
        public bool moveRight;
        public bool moveUp;
        public bool moveDown;
    }

    public struct MovementState
    {
        public bool left, right;
    }

    class Player : AnimatedSprite
    {
        private int PlayerNumber;
        private Vector2 Velocity;

        public PlayerCollisionState collisionState = new PlayerCollisionState();
        private MovementState movementState = new MovementState();

        private bool lastDireciton = false;
        public bool OnGround = false;
        public bool Jumping = false;
        public bool Falling = true;
        private float JumpOffset = 0.0f;

        public Player()
        {
            collisionState.moveDown = true;
            collisionState.moveUp = true;
            collisionState.moveLeft = true;
            collisionState.moveRight = true;

            movementState.left = false;
            movementState.right = false;
        }

        public virtual void Initialize(Texture2D playerTexture, int WhichPlayer, Vector2 StartPosition)
        {
            Position = StartPosition;
            SpriteTexture = playerTexture;
            PlayerNumber = WhichPlayer;
            
            InitializeAnimation(5, 8);
            AddAnimation("idle", 0, 2, 800);
            AddAnimation("running", 2, 9, 600);
            AddAnimation("jump", 10, 16, 600);
            PlayAnimation("idle");
        }

        public virtual void Update(GameTime gameTime, InputManager Input)
        {
            base.Update(gameTime);
            GamePadState gamePad = Input.GetCurrentGamePadState(PlayerNumber);

            //Check input
            if (gamePad.ThumbSticks.Left.X < 0.0f)
            {
                movementState.right = false;
                movementState.left = true;
            }
            else if (gamePad.ThumbSticks.Left.X > 0.0f)
            {
                movementState.left = false;
                movementState.right = true;
            }
            else
            {
                movementState.right = false;
                movementState.left = false;
            }

            if (movementState.right)
            {
                PlayAnimation("running");
                
                lastDireciton = false;
                Velocity.X += 1.0f;
            }
            else if (movementState.left)
            {
                PlayAnimation("running");
                lastDireciton = true;
                Velocity.X -= 1.0f;
    
            }
            else
            {
                if (Velocity.X > 0.0f)
                    Velocity.X -= 0.6f;
                if (Velocity.X < 0.0f)
                    Velocity.X += 0.6f;

                PlayAnimation("idle");
            }

            //Clip velocity
            if (Velocity.X > -0.6f && Velocity.X < 0.6f)
                Velocity.X = 0.0f;

            else if (Velocity.X > 20.0f)
                Velocity.X = 20.0f;
            else if (Velocity.X < -20.0f)
                Velocity.X = -20.0f;

            //Update Position
            if (Velocity.X > 0.0f && collisionState.moveRight)
                Position.X += Velocity.X;
            else if (Velocity.X < 0.0f && collisionState.moveLeft)
                Position.X += Velocity.X;
            else
                Velocity.X = 0.0f;

            
            
            if (!Jumping && OnGround)
            {
                if (gamePad.Buttons.A == ButtonState.Pressed)
                {
                    Jumping = true;
                    OnGround = false;
                    JumpOffset = Position.Y - 300.0f;
                }
            }
            else if (Jumping)
            {
                if (Position.Y < JumpOffset)
                {
                    Jumping = false;
                    Falling = true;
                    Velocity.Y = 0.0f;
                }
                else
                {
                    if (collisionState.moveUp)
                        Velocity.Y -= 5.0f;
                    else
                    {
                        Velocity.Y = 0.0f;
                        Jumping = false;
                        Falling = true;
                    }
                }
            }


            if (collisionState.moveDown)
            {
                OnGround = false;
            }

            if (!collisionState.moveDown)
                Falling = false;

            Position.Y += Velocity.Y;

            if (!OnGround && !Jumping)
                Position.Y += 9.4f;
          
            BoundingBox.X = (int)Position.X;
            BoundingBox.Y = (int)Position.Y;

            collisionState.moveDown = true;
            collisionState.moveUp = true;
            collisionState.moveLeft = true;
            collisionState.moveRight = true;
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

            if(lastDireciton)
                spriteBatch.Draw(SpriteTexture, destinationRectangle, sourceRectangle, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0.4f);
            else
                spriteBatch.Draw(SpriteTexture, destinationRectangle, sourceRectangle, Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0.4f);
            spriteBatch.End();
        }
    }
}
