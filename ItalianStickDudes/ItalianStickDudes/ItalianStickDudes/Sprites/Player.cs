using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace ItalianStickDudes
{
    public struct MovementState
    {
        public bool left, right;
    }

    public struct MoveState
    {
       public bool canMoveRight, canMoveLeft;
       public bool canMoveDown, canMoveUp;
    }
    

    class Player : AnimatedSprite
    {
        private int PlayerNumber;
        public Vector2 Velocity;

        private MovementState movementState = new MovementState();

        private bool lastDireciton = false;

        public MoveState moveState = new MoveState();

        public bool OnGround = false;
        public bool Jumping = false;
        public bool Falling = true;

        public Player()
        {
            moveState.canMoveLeft = true;
            moveState.canMoveRight = true;
            moveState.canMoveUp = true;
            moveState.canMoveDown = true;

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
                if(moveState.canMoveRight)
                    Velocity.X += 1.0f;
            }
            else if (movementState.left)
            {
                PlayAnimation("running");
                lastDireciton = true;
                if (moveState.canMoveLeft)
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

            if (!moveState.canMoveLeft || !moveState.canMoveRight)
                Velocity.X = 0.0f;
            if (!moveState.canMoveDown || !moveState.canMoveUp)
                Velocity.Y = 0.0f;

            if (gamePad.Buttons.A == ButtonState.Pressed)
            {
                Jumping = true;
                OnGround = false;
                Velocity.Y -= 2.0f;
            }
            else
            {
                Jumping = false;
                if (!OnGround)
                    Falling = true;
            }

            if (Falling)
                Velocity.Y += 1.0f;

            if (OnGround)
                Velocity.Y = 0.0f;

            Position += Velocity;

            int width = SpriteTexture.Width / Columns;
            int height = SpriteTexture.Height / Rows;
            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, width, height);

            moveState.canMoveLeft = true;
            moveState.canMoveRight = true;
            moveState.canMoveUp = true;
            moveState.canMoveDown = true;

        }

        public virtual void Draw(SpriteBatch spriteBatch, Matrix transform)
        {
            int width = SpriteTexture.Width / Columns;
            int height = SpriteTexture.Height / Rows;
            int row = (int)((float)CurrentFrame / (float)Columns);
            int col = CurrentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * col, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, width, height);


            if(lastDireciton)
                spriteBatch.Draw(SpriteTexture, destinationRectangle, sourceRectangle, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0.4f);
            else
                spriteBatch.Draw(SpriteTexture, destinationRectangle, sourceRectangle, Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0.4f);
        }
    }
}
