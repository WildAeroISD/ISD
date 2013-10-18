using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ItalianStickDudes
{
    interface ISprite
    {
        void Initialize(Texture2D texture, Vector2 position);
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
