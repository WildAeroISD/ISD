using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ItalianStickDudes
{
    class Sprite : ISprite
    {
        private Texture2D SpriteTexture;
        private Vector2 Position;

        public virtual void Initialize(Texture2D texture, Vector2 position)
        {
            SpriteTexture = texture;
            Position = position;
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SpriteTexture, Position, Color.White);
        }
    }
}
