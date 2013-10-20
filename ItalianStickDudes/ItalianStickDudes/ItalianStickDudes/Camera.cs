using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace ItalianStickDudes
{
    class Camera
    {
        private float Zoom;
        private Matrix Transform;
        private Vector2 Position;
        private float Rotation;

        public Camera()
        {
            Zoom = 0.2f;
            Rotation = 0.0f;
            Position = Vector2.Zero;
            Transform = Matrix.Identity;
        }

        public void SetZoom(float zoom)
        {
            if (zoom < 0.1f)
                zoom = 0.1f;
            Zoom = zoom;
        }

        public float GetZoom()
        {
            return Zoom;
        }

        public void SetRotation(float rotation)
        {
            Rotation = rotation;
        }
        public float GetRotation()
        {
            return Rotation;
        }

        public void Move(Vector2 amount)
        {
            Position += amount;
        }

        public void SetPosition(Vector2 pos)
        {
            Position = pos;
        }

        public Vector2 GetPosition()
        {
            return Position;
        }

        public Matrix GetTransform()
        {
            Transform = Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                Matrix.CreateTranslation(new Vector3(1280.0f * 0.5f, 720.0f * 0.5f, 0));
            return Transform;
        }
    }
}
