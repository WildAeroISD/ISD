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

        private Camera camera;

        public Player PlayerOne;
        public Player PlayerTwo;
        public Player PlayerThree;

        public SpriteFont font;
        private Dictionary<string, Texture2D> AvailableTextures;

        public PlayingState()
        {
            Paused = false;
            Input = new InputManager();
            camera = new Camera();

            PlayerOne = new Player();
            PlayerTwo = new Player();
            PlayerThree = new Player();

            AvailableTextures = new Dictionary<string, Texture2D>();
        }

        public virtual void Initialize()
        {
            PlayerOne.Initialize(AvailableTextures["Player"], 1, new Vector2(100, 100));
            PlayerTwo.Initialize(AvailableTextures["Player"], 2, new Vector2(200, 100));
            PlayerThree.Initialize(AvailableTextures["Player"], 3, new Vector2(300, 100));
            
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

            KeyboardState currentKeyboard = Input.GetCurrentKeyboardState();

            if (currentKeyboard.IsKeyDown(Keys.Right))
                camera.Move(new Vector2(10.0f, 0.0f));
            if (currentKeyboard.IsKeyDown(Keys.Left))
                camera.Move(new Vector2(-10.0f, 0.0f));

            if (currentKeyboard.IsKeyDown(Keys.Down))
                camera.Move(new Vector2(0.0f, 10.0f));
            if (currentKeyboard.IsKeyDown(Keys.Up))
                camera.Move(new Vector2(0.0f, -10.0f));

            if (!Paused)
            {
                if(Input.AnyPlayerPressed(Buttons.Start))
                    Paused = true;

                PlayerOne.Update(gameTime, Input);
                PlayerThree.Update(gameTime, Input);
                PlayerTwo.Update(gameTime, Input);
            }
            else
            {
                if(Input.AnyPlayerPressed(Buttons.Start))
                    Paused = false;
            }            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Matrix transform = camera.GetTransform();

            spriteBatch.Begin();
            if (Paused)
            {
                spriteBatch.DrawString(font, "PAUSED!", new Vector2(0, 0), Color.Black);
            }
            spriteBatch.End();

            PlayerOne.Draw(spriteBatch, transform);
            PlayerTwo.Draw(spriteBatch, transform);
            PlayerThree.Draw(spriteBatch, transform);
        }

        public virtual void End()
        {

        }
    }
}
