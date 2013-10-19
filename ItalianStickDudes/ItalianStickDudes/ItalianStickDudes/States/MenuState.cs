using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace ItalianStickDudes
{
    class MenuState
    {
        Texture2D MenuImage;

        public bool ExitGame;
        public bool PlayGame;
        public bool GoEditor;

        public virtual void Initialize(Texture2D image)
        {
            ExitGame = false;
            PlayGame = false;
            GoEditor = false;

            MenuImage = image;
        }

        public virtual void Update(GameTime gameTime)
        {
            GamePadState playerOneState = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyboardState = Keyboard.GetState();

            if (playerOneState.Buttons.A == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Space))
                PlayGame = true;

            if (keyboardState.IsKeyDown(Keys.E))
                GoEditor = true;

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(MenuImage, new Vector2(0, 0), Color.White);
            spriteBatch.End();
        }

        public virtual void End()
        {

        }

    }
}
