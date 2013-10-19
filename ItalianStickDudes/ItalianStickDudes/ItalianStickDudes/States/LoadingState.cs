using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ItalianStickDudes
{
    class LoadingState
    {
        private Texture2D loadingTexture;
        private bool HasDrawn = false;
        public bool DoneLoading = false;

        private ContentManager Content;
        private PlayingState PlayState;

        public void Initialize(PlayingState play, ContentManager content)
        {
            Content = content;
            PlayState = play;
            loadingTexture = Content.Load<Texture2D>("LoadingPlaceHolder");
        }

        public void Load(string mapName)
        {
            if (HasDrawn)
            {
                PlayState.font = Content.Load<SpriteFont>("DebugFont");

                Texture2D tex = Content.Load<Texture2D>("Player");
                if (!PlayState.AddNewTexture("Player", tex))
                    tex.Dispose();

                Stream stream = File.Open("Maps\\Earlytest.entm", FileMode.Open);
                BinaryFormatter bFormatter = new BinaryFormatter();
                Map map = (Map)bFormatter.Deserialize(stream);
                stream.Close();
                //Build list...
                for (int x = 0; x < map.MapTiles.Count; x++)
                {
                    Sprite sp = new Sprite();
                    Texture2D t = Content.Load<Texture2D>(map.MapTiles[x].TextureName);
                    PlayState.AddNewTexture(map.MapTiles[x].TextureName, t);
                    sp.Initialize(t, map.MapTiles[x].Position, map.MapTiles[x].Depth);
                    PlayState.MapTiles.Add(sp);
                }

                DoneLoading = true;
            }
        }

        public void DrawScreen(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(loadingTexture, new Vector2(0, 0), Color.White);
            spriteBatch.End();

            HasDrawn = true;
        }
    }
}
