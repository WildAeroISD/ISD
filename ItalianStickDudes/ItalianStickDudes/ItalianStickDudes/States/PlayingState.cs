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

        public Player PlayerOne;

        public SpriteFont font;
        private Dictionary<string, Texture2D> AvailableTextures;

        public PlayingState()
        {
            Paused = false;
            Input = new InputManager();
            PlayerOne = new Player();
            AvailableTextures = new Dictionary<string, Texture2D>();
        }

        public virtual void Initialize()
        {
            PlayerOne.Initialize(AvailableTextures["Player"], 1, new Vector2(100, 100));
            
        }

        public bool AddNewTexture(string name, Texture2D texture)
        {
            if(AvailableTextures.ContainsKey(name))
                return false;
            
            AvailableTextures.Add(name, texture);

            return true;
        }

        public virtual void Update(GameTime gameTime)
        {
            Input.Update();
            if (!Paused)
            {
                if(Input.AnyPlayerPressed(Buttons.Start))
                    Paused = true;

                PlayerOne.Update(gameTime, Input);
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

            PlayerOne.Draw(spriteBatch);
            spriteBatch.End();
        }

        public virtual void End()
        {

        }
    }
}
