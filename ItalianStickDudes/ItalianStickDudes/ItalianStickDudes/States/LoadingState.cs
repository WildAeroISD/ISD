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
            loadingTexture = Content.Load<Texture2D>("Loading");
        }

        public void Load(string mapName)
        {
            if (HasDrawn)
            {
                PlayState.font = Content.Load<SpriteFont>("DebugFont");

                Texture2D tex = Content.Load<Texture2D>("Player1");
                PlayState.AddNewTexture("Player1", tex);
                tex = Content.Load<Texture2D>("Player2");
                PlayState.AddNewTexture("Player2", tex);
                tex = Content.Load<Texture2D>("Player3");
                PlayState.AddNewTexture("Player3", tex);
                tex = Content.Load<Texture2D>("Player4");
                PlayState.AddNewTexture("Player4", tex);

                Stream stream = File.Open("Maps\\ScavengerMapOne.entm", FileMode.Open);
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
