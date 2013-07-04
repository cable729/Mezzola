using Mezzola.Engine.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mezzola.Engine.View
{
    public class Camera3D : IEntity
    {
        private Vector3 position;
        public Matrix Projection { get; private set; }
        public Matrix View { get; private set; }

        public Vector3 Position
        {
            get { return position; }
            set
            {
                position = value;
                View = Matrix.CreateLookAt(Position, Vector3.Down, Vector3.Forward);
            }
        }

        public Camera3D(GraphicsDevice device)
        {
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                device.Viewport.AspectRatio, 0.001f, 50.0f);
        }

        public Matrix GetWorldMatrix(IEntity entity)
        {
            return Matrix.CreateTranslation(entity.Position);
        }
    }
}
