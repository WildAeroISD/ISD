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

        public SpriteFont font;

        public PlayingState()
        {
            Paused = false;
            Input = new InputManager();
        }

        public virtual void Initialize()
        {
            Player playerone = new Player();
        }

        public virtual void Update(GameTime gameTime)
        {
            Input.Update();
            if (!Paused)
            {
                if(Input.AnyPlayerPressed(Buttons.Start))
                    Paused = true;
            }
            else
            {
                if(Input.AnyPlayerPressed(Buttons.Start))
                    Paused = false;
            }            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            if (Paused)
            {
                spriteBatch.DrawString(font, "PAUSED!", new Vector2(0, 0), Color.Black);
            }
            spriteBatch.End();
        }

        public virtual void End()
        {

        }
    }
}
