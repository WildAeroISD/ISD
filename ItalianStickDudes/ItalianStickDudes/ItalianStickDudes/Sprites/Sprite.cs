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
        public Texture2D SpriteTexture;
        protected Vector2 Position;
        protected float Rotation;
        protected float Depth;

        public Rectangle BoundingBox;
        public Sprite()
        {
            Position = new Vector2(0, 0);
            Rotation = 0.0f;
            Depth = 1.0f;

            BoundingBox = new Rectangle(0, 0, 0, 0);
        }

        public virtual void Initialize(Texture2D texture, Vector2 position, float depth)
        {
            SpriteTexture = texture;
            Position = position;
            Depth = depth;

            BoundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SpriteTexture, Position, null, Color.White, Rotation, new Vector2(0, 0), 1.0f, SpriteEffects.None, Depth);
        }

        public void SetPosition(Vector2 pos)
        {
            Position = pos;
        }

        public Vector2 GetPosition()
        {
            return Position;
        }

        public float GetDepth()
        {
            return Depth;
        }
    }
}
