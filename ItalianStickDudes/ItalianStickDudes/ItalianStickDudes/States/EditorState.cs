using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace ItalianStickDudes
{
    class EditorState
    {
        private InputManager Input;
        private Camera camera;
        private Dictionary<string, Texture2D> AvailableTextures;

        private SpriteFont font;

        private bool ChosenFile;
        private bool ChosenMethod;
        private bool LoadFile;

        private bool FileLoadError;

        private string InputString;

        public bool GoMenu;

        private Map map;
        private List<Sprite> MapTiles;
        private ContentManager Content;

        private int CurrentTexture;
        private float CurrentDepth;

        public EditorState()
        {
            ChosenFile = false;
            ChosenMethod = false;
            LoadFile = false;
            GoMenu = false;
            FileLoadError = false;

            InputString = "";
            Input = new InputManager();
            camera = new Camera();
            AvailableTextures = new Dictionary<string, Texture2D>();
            MapTiles = new List<Sprite>();            
            map = new Map();

            CurrentTexture = 0;
            CurrentDepth = 1.0f;
        }

        public void Initialize(ContentManager Content)
        {
            this.Content = Content;
            font = Content.Load<SpriteFont>("DebugFont");

            AvailableTextures.Add("Test", Content.Load<Texture2D>("test"));
            AvailableTextures.Add("Test2", Content.Load<Texture2D>("test2"));
        }

        public void Update(GameTime gameTime)
        {
            Input.Update();

            if (!ChosenFile)
            {
                if (!ChosenMethod)
                {
                    if (Input.IsNewKeyDown(Keys.D1))
                    {
                        ChosenMethod = true;
                        LoadFile = true;
                    }
                    else if (Input.IsNewKeyDown(Keys.D2))
                    {
                        ChosenMethod = true;
                        LoadFile = false;
                    }
                }
                else
                {
                    InputString += Input.GetInputString();

                    if (Input.IsNewKeyDown(Keys.Back))
                    {
                        InputString = InputString.Remove(InputString.Length - 1);
                    }

                    if (Input.IsNewKeyDown(Keys.Enter))
                    {
                        if (LoadFile)
                        {
                            Stream stream = File.Open(InputString, FileMode.Open);
                            BinaryFormatter bFormatter = new BinaryFormatter();
                            map = (Map)bFormatter.Deserialize(stream);
                            stream.Close();
                             

                            //Build list...
                            for (int x = 0; x < map.MapTiles.Count; x++)
                            {
                                Sprite sp = new Sprite();
                                sp.Initialize(AvailableTextures[map.MapTiles[x].TextureName], map.MapTiles[x].Position, map.MapTiles[x].Depth);
                                MapTiles.Add(sp);
                            }
                        }

                        ChosenFile = true;
                    }

                }
            }
            else
            {

                //Editor logic here.

                KeyboardState currentKeyboard = Input.GetCurrentKeyboardState();

                if (currentKeyboard.IsKeyDown(Keys.W))
                {
                    camera.Move(new Vector2(0.0f, -10.0f));
                }
                if (currentKeyboard.IsKeyDown(Keys.S))
                {
                    camera.Move(new Vector2(0.0f, 10.0f));
                }

                if (currentKeyboard.IsKeyDown(Keys.A))
                {
                    camera.Move(new Vector2(-10.0f, 0.0f));
                }
                if (currentKeyboard.IsKeyDown(Keys.D))
                {
                    camera.Move(new Vector2(10.0f, 0.0f));
                }
                
                if(currentKeyboard.IsKeyDown(Keys.R))
                {
                    camera.SetZoom(1.0f);
                }

                if (Input.GetCurrentMouseState().ScrollWheelValue < Input.GetPreviousMouseState().ScrollWheelValue)
                {
                    float zoom = camera.GetZoom() - 0.2f;
                    camera.SetZoom(zoom);
                }
                else if (Input.GetCurrentMouseState().ScrollWheelValue > Input.GetPreviousMouseState().ScrollWheelValue)
                {
                    float zoom = camera.GetZoom() + 0.2f;
                    camera.SetZoom(zoom);
                }

                if(currentKeyboard.IsKeyDown(Keys.PageUp))
                {
                    if (CurrentTexture != AvailableTextures.Keys.Count - 1)
                    {
                        CurrentTexture++;
                    }
                }
                else if (currentKeyboard.IsKeyDown(Keys.PageDown))
                {
                    if (CurrentTexture != 0)
                    {
                        CurrentTexture--;
                    }
                }

                if (Input.IsNewKeyDown(Keys.Add))
                {
                    CurrentDepth += 0.1f;

                    if (CurrentDepth > 1.0f)
                        CurrentDepth = 1.0f;
                }
                else if (Input.IsNewKeyDown(Keys.Subtract))
                {
                    CurrentDepth -= 0.1f;

                    if (CurrentDepth < 0.0f)
                        CurrentDepth = 0.0f;
                }
 

                if (Input.IsNewLeftMouseDown())
                {
                    Sprite ns = new Sprite();
                    ns.Initialize(AvailableTextures.ElementAt(CurrentTexture).Value, new Vector2(0, 0), CurrentDepth);
                    Vector2 loc = new Vector2(Input.GetCurrentMouseState().X - (ns.SpriteTexture.Width / 2), Input.GetCurrentMouseState().Y - (ns.SpriteTexture.Height / 2));
                    loc = Vector2.Transform(loc, Matrix.Invert(camera.GetTransform()));
                    ns.SetPosition(loc);
                    MapTiles.Add(ns);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!ChosenFile)
            {
                spriteBatch.Begin();

                if (!ChosenMethod)
                {
                    spriteBatch.DrawString(font, "Do you want to either: ", new Vector2(0, 0), Color.Black);
                    spriteBatch.DrawString(font, "1) Load An Existing File", new Vector2(0, 20), Color.Black);
                    spriteBatch.DrawString(font, "2) Create A New File", new Vector2(0, 40), Color.Black);
                }

                else
                {
                    spriteBatch.DrawString(font, "Enter File Name", new Vector2(0, 0), Color.Black);
                    spriteBatch.DrawString(font, InputString, new Vector2(0, 20), Color.Black);
                    if (FileLoadError)
                    {
                       spriteBatch.DrawString(font, "File Does Not Exist!!!", new Vector2(0, 40), Color.Black);
                    }
                }

                spriteBatch.End();
            }
            else
            {
                spriteBatch.Begin(SpriteSortMode.BackToFront,
                BlendState.AlphaBlend,
                null, null, null, null, camera.GetTransform());

                for (int x = 0; x < MapTiles.Count; x++)
                {
                    MapTiles[x].Draw(spriteBatch);
                }

                spriteBatch.End();

                spriteBatch.Begin();

                Texture2D tex = AvailableTextures.ElementAt(CurrentTexture).Value;
                spriteBatch.DrawString(font, "Current Texture: ", new Vector2(970, 655), Color.Black);
                spriteBatch.Draw(tex, new Vector2(1152, 592), Color.White);

                spriteBatch.DrawString(font, "Depth: " + CurrentDepth, new Vector2(600, 655), Color.Black);

                spriteBatch.End();
            }

            
        }
    }
}
