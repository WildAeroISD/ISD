using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ItalianStickDudes
{
    class SpriteManager
    {
        private List<ISprite> SpriteList;
        
        public SpriteManager()
        {
            SpriteList = new List<ISprite>();
        }

        public void AddNewSprite(Texture2D texture, Vector2 position)
        {
            
           
        }

        public void UpdateSprites(GameTime gameTime)
        {
            for (int index = 0; index < SpriteList.Count; index++)
            {
                SpriteList[index].Update(gameTime);
            }
        }

        public void DrawSprites(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            for (int index = 0; index < SpriteList.Count; index++)
            {
                SpriteList[index].Draw(spriteBatch);  
            }

            spriteBatch.End();
        }
    }
}
