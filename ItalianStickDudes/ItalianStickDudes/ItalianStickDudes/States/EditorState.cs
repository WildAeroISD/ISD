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

        private bool BuildHoriz;
        private int BuildAmount;

        private int SelectingIndex = -1;
        private bool Hide = false;

        public EditorState()
        {
            ChosenFile = false;
            ChosenMethod = false;
            LoadFile = false;
            GoMenu = false;
            FileLoadError = false;

            BuildHoriz = true;
            BuildAmount = 1;

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

            Texture2D tex = Content.Load<Texture2D>("Background_Plain");
            tex.Name = "Background_Plain";
            AvailableTextures.Add("Background_Plain", tex);

            tex = Content.Load<Texture2D>("Background_Windowed");
            tex.Name = "Background_Windowed";
            AvailableTextures.Add("Background_Windowed", tex);

            tex = Content.Load<Texture2D>("Foreground_Arch_Large");
            tex.Name = "Foreground_Arch_Large";
            AvailableTextures.Add("Foreground_Arch_Large", tex);

            tex = Content.Load<Texture2D>("Foreground_Arch_Small");
            tex.Name = "Foreground_Arch_Small";
            AvailableTextures.Add("Foreground_Arch_Small", tex);

            tex = Content.Load<Texture2D>("Foreground_Gap_Left");
            tex.Name = "Foreground_Gap_Left";
            AvailableTextures.Add("Foreground_Gap_Left", tex);

            tex = Content.Load<Texture2D>("Foreground_Gap_Right");
            tex.Name = "Foreground_Gap_Right";
            AvailableTextures.Add("Foreground_Gap_Right", tex);

            tex = Content.Load<Texture2D>("Foreground_Railling");
            tex.Name = "Foreground_Railling";
            AvailableTextures.Add("Foreground_Railling", tex);

            tex = Content.Load<Texture2D>("Foreground_Wall");
            tex.Name = "Foreground_Wall";
            AvailableTextures.Add("Foreground_Wall", tex);

            tex = Content.Load<Texture2D>("Foreground_Wall_Gap_Left");
            tex.Name = "Foreground_Wall_Gap_Left";
            AvailableTextures.Add("Foreground_Wall_Gap_Left", tex);

            tex = Content.Load<Texture2D>("Foreground_Wall_Gap_Right");
            tex.Name = "Foreground_Wall_Gap_Right";
            AvailableTextures.Add("Foreground_Wall_Gap_Right", tex);

            tex = Content.Load<Texture2D>("Platform_Wooden_Ledge_Left");
            tex.Name = "Platform_Wooden_Ledge_Left";
            AvailableTextures.Add("Platform_Wooden_Ledge_Left", tex);

            tex = Content.Load<Texture2D>("Platform_Wooden_Ledge_Right");
            tex.Name = "Platform_Wooden_Ledge_Right";
            AvailableTextures.Add("Platform_Wooden_Ledge_Right", tex);

            tex = Content.Load<Texture2D>("Platform_Wooden_Pole");
            tex.Name = "Platform_Wooden_Pole";
            AvailableTextures.Add("Platform_Wooden_Pole", tex);

            tex = Content.Load<Texture2D>("Props_Barrel");
            tex.Name = "Props_Barrel";
            AvailableTextures.Add("Props_Barrel", tex);

            tex = Content.Load<Texture2D>("Props_Crate");
            tex.Name = "Props_Crate";
            AvailableTextures.Add("Props_Crate", tex);

            tex = Content.Load<Texture2D>("Top_Full");
            tex.Name = "Top_Full";
            AvailableTextures.Add("Top_Full", tex);

            tex = Content.Load<Texture2D>("Top_Gap_Left");
            tex.Name = "Top_Gap_Left";
            AvailableTextures.Add("Top_Gap_Left", tex);

            tex = Content.Load<Texture2D>("Background_GreenWall");
            tex.Name = "Background_GreenWall";
            AvailableTextures.Add("Background_GreenWall", tex);

            tex = Content.Load<Texture2D>("Background_LargeAwning");
            tex.Name = "Background_LargeAwning";
            AvailableTextures.Add("Background_LargeAwning", tex);

            tex = Content.Load<Texture2D>("Background_LargeWoodenShutter");
            tex.Name = "Background_LargeWoodenShutter";
            AvailableTextures.Add("Background_LargeWoodenShutter", tex);

            tex = Content.Load<Texture2D>("Background_SmallAwning");
            tex.Name = "Background_SmallAwning";
            AvailableTextures.Add("Background_SmallAwning", tex);

            tex = Content.Load<Texture2D>("Background_SmallWoodenShutter");
            tex.Name = "Background_SmallWoodenShutter";
            AvailableTextures.Add("Background_SmallWoodenShutter", tex);

            tex = Content.Load<Texture2D>("Top_Gap_Right");
            tex.Name = "Top_Gap_Right";
            AvailableTextures.Add("Top_Gap_Right", tex);

            tex = Content.Load<Texture2D>("Background_YellowWall");
            tex.Name = "Background_YellowWall";
            AvailableTextures.Add("Background_YellowWall", tex);

            tex = Content.Load<Texture2D>("Foreground_Metal_Railing");
            tex.Name = "Foreground_Metal_Railing";
            AvailableTextures.Add("Foreground_Metal_Railing", tex);

            tex = Content.Load<Texture2D>("Foreground_Stone_Wall");
            tex.Name = "Foreground_Stone_Wall";
            AvailableTextures.Add("Foreground_Stone_Wall", tex);

            tex = Content.Load<Texture2D>("Foreground_Lamp");
            tex.Name = "Foreground_Lamp";
            AvailableTextures.Add("Foreground_Lamp", tex);

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
                            Stream stream = File.Open("Maps\\" + InputString + ".entm", FileMode.Open);
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

                if(Input.IsNewKeyDown(Keys.PageUp))
                {
                    if (CurrentTexture != AvailableTextures.Keys.Count - 1)
                    {
                        CurrentTexture++;
                    }
                }
                else if (Input.IsNewKeyDown(Keys.PageDown))
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

                    if (CurrentDepth < 0.1f)
                        CurrentDepth = 0.1f;
                }

                if(Input.IsNewKeyDown(Keys.Z))
                {
                    if (MapTiles.Count > 0)
                    {
  
                            MapTiles.RemoveAt(MapTiles.Count - 1);
                    }
                }

                if (Input.IsNewKeyDown(Keys.Multiply))
                {
                    BuildAmount++;
                }
                else if (Input.IsNewKeyDown(Keys.Divide))
                {
                    BuildAmount--;

                    if (BuildAmount < 0)
                        BuildAmount = 0;
                }

                if (Input.IsNewKeyDown(Keys.B))
                    BuildHoriz = BuildHoriz ? false : true;

                if (Input.IsNewKeyDown(Keys.H))
                    Hide = Hide ? false : true;

                if (Input.GetCurrentMouseState().RightButton == ButtonState.Pressed)
                {
                    if (SelectingIndex == -1)
                    {
                        //Get mouse pos
                        Vector2 mousePos = new Vector2(Input.GetCurrentMouseState().X, Input.GetCurrentMouseState().Y);
                        mousePos = Vector2.Transform(mousePos, Matrix.Invert(camera.GetTransform()));

                        for (int v = 0; v < MapTiles.Count; v++)
                        {
                            Sprite tile = MapTiles[v];
                            if (tile.GetDepth() == CurrentDepth)
                            {
                                //Transform position to same coordinates as mouse
                                Vector2 pos = tile.GetPosition();
                                if (mousePos.X >= pos.X && mousePos.X <= (pos.X + tile.SpriteTexture.Width))
                                {
                                    if (mousePos.Y >= pos.Y && mousePos.Y <= (pos.Y + tile.SpriteTexture.Height))
                                    {
                                        SelectingIndex = v;
                                        break;
                                    }
                                }

                            }
                        }
                    }
                    else
                    {
                        Sprite st = MapTiles[SelectingIndex];
                        float texWidth = (st.SpriteTexture.Width / 2) * camera.GetZoom();
                        float texHeight = (st.SpriteTexture.Height / 2) * camera.GetZoom();

                        Vector2 loc = new Vector2(Input.GetCurrentMouseState().X - texWidth, Input.GetCurrentMouseState().Y - texHeight);
                        loc = Vector2.Transform(loc, Matrix.Invert(camera.GetTransform()));
                        st.SetPosition(loc);

                        if (Input.IsNewKeyDown(Keys.Delete))
                        {
                            MapTiles.RemoveAt(SelectingIndex);
                            SelectingIndex = -1;
                        }
                    }
                }
                else if(Input.GetCurrentMouseState().RightButton == ButtonState.Released)
                {
                    SelectingIndex = -1;
                }
 

                if (Input.IsNewLeftMouseDown() && !Hide)
                {
                    for (int x = 0; x < BuildAmount; x++)
                    {
                        Sprite ns = new Sprite();
                        ns.Initialize(AvailableTextures.ElementAt(CurrentTexture).Value, new Vector2(0, 0), CurrentDepth);

                        float texWidth = (ns.SpriteTexture.Width / 2) * camera.GetZoom();
                        float texHeight = (ns.SpriteTexture.Height / 2) * camera.GetZoom();

                        Vector2 loc = new Vector2(Input.GetCurrentMouseState().X - texWidth, Input.GetCurrentMouseState().Y - texHeight);
                        if (BuildHoriz)
                        {
                            loc.X = loc.X + (texWidth * x);
                        }
                        else
                        {
                            loc.Y += texHeight * x;
                        }

                        loc = Vector2.Transform(loc, Matrix.Invert(camera.GetTransform()));
                        ns.SetPosition(loc);
                        MapTiles.Add(ns);
                    }
                }

                if (Input.IsNewKeyDown(Keys.J))
                {
                    Map buildMap = new Map();
                    buildMap.NameOfMap = InputString;
                    
                    for(int z = 0; z < MapTiles.Count; z++)
                    {
                        MapTile tile = new MapTile();
                        tile.Position = MapTiles[z].GetPosition();
                        tile.TextureName = MapTiles[z].SpriteTexture.Name.ToString();
                        tile.Depth = MapTiles[z].GetDepth();

                        buildMap.MapTiles.Add(tile);
                    }

                    Stream stream = File.Open("Maps\\" + InputString + ".entm", FileMode.Create);
                    BinaryFormatter bFormatter = new BinaryFormatter();
                    bFormatter.Serialize(stream, buildMap);
                    stream.Close();
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

                if (!Hide)
                {
                    float texWidth = (AvailableTextures.ElementAt(CurrentTexture).Value.Width / 2) * camera.GetZoom();
                    float texHeight = (AvailableTextures.ElementAt(CurrentTexture).Value.Height / 2) * camera.GetZoom();

                    Vector2 loc = new Vector2(Input.GetCurrentMouseState().X - texWidth, Input.GetCurrentMouseState().Y - texHeight);
                    loc = Vector2.Transform(loc, Matrix.Invert(camera.GetTransform()));
                    spriteBatch.Draw(AvailableTextures.ElementAt(CurrentTexture).Value, loc, null, Color.White, 0.0f, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.0f);
                }

                for (int x = 0; x < MapTiles.Count; x++)
                {
                    MapTiles[x].Draw(spriteBatch);
                }

                spriteBatch.End();


                Vector2 mouseLoc = new Vector2(Input.GetCurrentMouseState().X, Input.GetCurrentMouseState().Y);
                mouseLoc = Vector2.Transform(mouseLoc, Matrix.Invert(camera.GetTransform()));

                spriteBatch.Begin();

                

                spriteBatch.DrawString(font, "X: " + mouseLoc.X, new Vector2(0, 0), Color.Black);
                spriteBatch.DrawString(font, "Y: " + mouseLoc.Y, new Vector2(0, 20), Color.Black);

                spriteBatch.DrawString(font, "Depth: " + CurrentDepth, new Vector2(1100, 600), Color.Black);
                spriteBatch.DrawString(font, "Hide: " + Hide, new Vector2(1100, 660), Color.Black);
                spriteBatch.DrawString(font, "Build Amount: " + BuildAmount, new Vector2(1100, 640), Color.Black);
                spriteBatch.DrawString(font, "Build Horizontally: " + BuildHoriz, new Vector2(1000, 620), Color.Black);
                
                

                spriteBatch.End();
            }

            
        }
    }
}
