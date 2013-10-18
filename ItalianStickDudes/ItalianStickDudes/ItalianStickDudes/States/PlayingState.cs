using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace ItalianStickDudes
{
    class PlayingState
    {

        public InputManager Input;
        private bool Paused;

        public PlayingState()
        {
            Paused = false;
            Input = new InputManager();
        }

        public virtual void Initialize()
        {
            
        }

        public virtual void Update(GameTime gameTime)
        {
            Input.Update();

            if (!Paused)
            {

            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public virtual void End()
        {

        }
    }
}
