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

        public virtual void Initialize(Texture2D playerTexture, int WhichPlayer, Vector2 StartPosition)
        {
            Position = StartPosition;
            SpriteTexture = playerTexture;
            PlayerNumber = WhichPlayer;
            
            InitializeAnimation(1, 2);
            PlayAnimation(0, 2, 800);
        }

        public virtual void Update(GameTime gameTime, InputManager Input)
        {
            base.Update(gameTime);
            GamePadState gamePad = Input.GetCurrentGamePadState(PlayerNumber);

         
            Velocity.X += gamePad.ThumbSticks.Left.X;
            

            Position += Velocity;
        }
    }
}
