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

        private InputManager Input;
        private bool Paused;

        private Camera camera;
        private List<Player> Players;
        public List<Sprite> MapTiles;

        public SpriteFont font;
        public Collision collision;
        private Dictionary<string, Texture2D> AvailableTextures;

        public Vector2 FurthestAway;

        private string FPSText;


        public PlayingState()
        {
            Paused = false;
            Input = new InputManager();
            camera = new Camera();
            Players = new List<Player>();
            MapTiles = new List<Sprite>();
            collision = new Collision();
            FPSText = "";
            AvailableTextures = new Dictionary<string, Texture2D>();
        }

        public void Initialize(int numOfPlayers)
        {
            if (numOfPlayers > 4)
                numOfPlayers = 4;
            for (int p = 0; p < numOfPlayers; p++)
            {
                Players.Add(new Player());
                Players[p].Initialize(AvailableTextures["Player" + (p+1)], p + 1, new Vector2(520, 1500));
            }
        }

        public bool AddNewTexture(string name, Texture2D texture)
        {
            if(AvailableTextures.ContainsKey(name))
                return false;
            
            AvailableTextures.Add(name, texture);

            return true;
        }

        public void Update(GameTime gameTime)
        {
            FPSText = "FPS: " + (1 / gameTime.ElapsedGameTime.TotalSeconds);
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

            if (Input.GetCurrentMouseState().ScrollWheelValue < Input.GetPreviousMouseState().ScrollWheelValue)
            {
                float zoom = camera.GetZoom() - 0.02f;
                camera.SetZoom(zoom);
            }
            else if (Input.GetCurrentMouseState().ScrollWheelValue > Input.GetPreviousMouseState().ScrollWheelValue)
            {
                float zoom = camera.GetZoom() + 0.02f;
                camera.SetZoom(zoom);
            }

            if (!Paused)
            {
                if(Input.AnyPlayerPressed(Buttons.Start))
                    Paused = true;

                collision.CheckCollisions(Players, MapTiles);

                for (int p = 0; p < Players.Count; p++)
                {
                    Players[p].Update(gameTime, Input);

                    Vector2 pos = camera.GetPosition();
                    pos = Vector2.Transform(pos, Matrix.Invert(camera.GetTransform()));

                    Vector2 pPos = Players[p].Position;
                    pPos = Vector2.Transform(pPos, Matrix.Invert(camera.GetTransform()));

                    if (pPos.X < (pos.X - 1080))
                    {
                        camera.Move(new Vector2(-20.0f, 0.0f));

                    }
                    else if (pPos.X > (pos.X + 1080))
                    {
                        camera.Move(new Vector2(20.0f, 0.0f));
                  
                    }

                    if (pPos.Y < (pos.Y - 720))
                    {
                        camera.Move(new Vector2(0.0f, -20.0f));
                  
                    }
                    else if (pPos.Y > (pos.Y + 720))
                    {
                        camera.Move(new Vector2(0.0f, 20.0f));
                 
                    }

                }
            }
            else
            {
                if(Input.AnyPlayerPressed(Buttons.Start))
                    Paused = false;
            }            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Matrix transform = camera.GetTransform();

            spriteBatch.Begin();
            if (Paused)
            {
                spriteBatch.DrawString(font, "PAUSED!", new Vector2(0, 0), Color.Black);
            }

            spriteBatch.DrawString(font, FPSText, new Vector2(0, 0), Color.Black);
            spriteBatch.End();

            

            spriteBatch.Begin(SpriteSortMode.BackToFront,
                BlendState.AlphaBlend,
                null, null, null, null, camera.GetTransform());

            for (int m = 0; m < MapTiles.Count; m++)
            {
                MapTiles[m].Draw(spriteBatch);
            }

            for (int p = 0; p < Players.Count; p++)
            {
                Players[p].Draw(spriteBatch, transform);
            }

            spriteBatch.End();

            

            
        }

        public virtual void End()
        {

        }
    }
}
