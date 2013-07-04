using System;
using Mezzola.Engine.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mezzola.Engine.View
{
    public class Camera3D : IEntity
    {
        private readonly GraphicsDevice device;
        private Vector3 position;
        private float farClip;
        public Matrix Projection { get; private set; }
        public Matrix View { get; private set; }

        public Vector3 Position
        {
            get { return position; }
            set
            {
                if (value.Z < 0) throw new ArgumentOutOfRangeException("value", "Z value must be greater than 0.");
                position = value;
                View = Matrix.CreateLookAt(Position, Vector3.Down, Vector3.Forward);
            }
        }

        public float FarClip
        {
            get { return this.farClip; }
            set
            {
                this.farClip = value;
                Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                    device.Viewport.AspectRatio, 0.001f, FarClip);
            }
        }

        public Camera3D(GraphicsDevice device, Vector3 position)
        {
            this.device = device;
            Position = position;
        }

        public Matrix GetWorldMatrix(IEntity entity)
        {
            return Matrix.CreateTranslation(entity.Position);
        }

        public Vector3 TransformCoordinates(Vector2 coordinate)
        {
            var coordinate3D = new Vector3(coordinate.X, coordinate.Y, 1.0f);
            var result = device.Viewport.Unproject(coordinate3D, Projection, View, Matrix.Identity);

            float scale = Position.Z / FarClip;
            return new Vector3(result.X, result.Y, 0) * scale;
        }
    }
}
