using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
