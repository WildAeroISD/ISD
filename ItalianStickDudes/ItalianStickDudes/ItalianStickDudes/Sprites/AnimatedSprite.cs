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
        protected struct AnimationInfo
        {
            public string animationName;
            public int startFrame, endFrame;
            public long timeStep;
        };

        public Vector2 Position;
        protected Texture2D SpriteTexture;
        protected float Rotation;
        protected float Depth;

        protected Dictionary<string, AnimationInfo> Animations;
        protected AnimationInfo CurrentAnimaion;

        protected int Rows;
        protected int Columns;
        protected int TotalFrames;
        protected int CurrentFrame;

        protected Stopwatch AnimationTimer;

        protected bool Flip;

        public AnimatedSprite()
        {
            Rows = 0;
            Columns = 0;
            AnimationTimer = new Stopwatch();
            Animations = new Dictionary<string, AnimationInfo>();
            CurrentAnimaion = new AnimationInfo();
            Rotation = 0.0f;
            Depth = 1.0f;

            Flip = false;
        }

        public virtual void Initialize(Texture2D texture, Vector2 position, float depth)
        {
            SpriteTexture = texture;
            Position = position;
            Depth = depth;
        }

        public virtual void InitializeAnimation(int rows, int cols)
        {
            Rows = rows;
            Columns = cols;
            TotalFrames = rows * cols;
        }

        public void AddAnimation(string name, int startFrame, int endFrame, long timeLength)
        {
            AnimationInfo info = new AnimationInfo();
            info.animationName = name;
            info.startFrame = startFrame;
            info.endFrame = endFrame;
            int numFrames = endFrame - startFrame;
            info.timeStep = timeLength / numFrames;
            Animations.Add(name, info);
        }

        public virtual void Update(GameTime gameTime)
        {

            if (AnimationTimer.ElapsedMilliseconds >= CurrentAnimaion.timeStep)
            {
                CurrentFrame++;
                AnimationTimer.Restart();
            }

            if (CurrentFrame == CurrentAnimaion.endFrame)
                CurrentFrame = CurrentAnimaion.startFrame;

        }

        public virtual void PlayAnimation(string name)
        {
            if (CurrentAnimaion.animationName != name)
            {
                if (Animations.ContainsKey(name))
                {
                    CurrentAnimaion = Animations[name];
                    CurrentFrame = CurrentAnimaion.startFrame;
                    AnimationTimer.Restart();
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            int width = SpriteTexture.Width / Columns;
            int height = SpriteTexture.Height / Rows;
            int row = (int)((float)CurrentFrame / (float)Columns);
            int col = CurrentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * col, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, width, height);

            spriteBatch.Draw(SpriteTexture, destinationRectangle, sourceRectangle, Color.White, Rotation, new Vector2(0, 0), SpriteEffects.None, Depth);
        }
    }
}
