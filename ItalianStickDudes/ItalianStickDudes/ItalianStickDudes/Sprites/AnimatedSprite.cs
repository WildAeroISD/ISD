using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ItalianStickDudes
{
    class AnimatedSprite : ISprite
    {
        protected Vector2 Position;
        protected Texture2D Texture;

        protected int Rows;
        protected int Columns;
        protected int TotalFrames;

        protected int StartFrame;
        protected int CurrentFrame;
        protected int EndFrame;

        protected long TimeStep;
        protected Stopwatch AnimationTimer;

        public AnimatedSprite()
        {
            Rows = 0;
            Columns = 0;
            TotalFrames = 0;
            CurrentFrame = 0;
            EndFrame = 0;
            TimeStep = 0;
            AnimationTimer = new Stopwatch();
        }

        public virtual void Initialize(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
        }

        public virtual void InitializeAnimation(int rows, int cols)
        {
            Rows = rows;
            Columns = cols;
            TotalFrames = rows * cols;
        }

        public virtual void Update(GameTime gameTime)
        {

            if (AnimationTimer.ElapsedMilliseconds >= TimeStep)
            {
                CurrentFrame++;
                AnimationTimer.Restart();
            }

            if (CurrentFrame == EndFrame)
                CurrentFrame = StartFrame;

        }

        public virtual void PlayAnimation(int startFrame, int endFrame, long timeLength)
        {
            StartFrame = startFrame;
            CurrentFrame = StartFrame;
            EndFrame = endFrame;

            int numFrames = endFrame - startFrame;
            TimeStep = timeLength / numFrames;
            AnimationTimer.Restart();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)CurrentFrame / (float)Columns);
            int col = CurrentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * col, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, width, height);

            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
