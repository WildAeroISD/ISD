﻿using System;
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

        public Sprite()
        {
            Position = new Vector2(0, 0);
            Rotation = 0.0f;
            Depth = 1.0f;
        }

        public virtual void Initialize(Texture2D texture, Vector2 position, float depth)
        {
            SpriteTexture = texture;
            Position = position;
            Depth = depth;
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
    }
}
