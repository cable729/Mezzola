using Mezzola.Engine.Base;
using Mezzola.Engine.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mezzola
{
    public class TempSprite : DrawableEntity
    {
        private readonly GraphicsDevice device;
        private readonly Camera3D camera;
        private readonly VertexPositionColor[] vertices;
        private readonly BasicEffect effect;
        private readonly VertexBuffer buffer;

        public TempSprite(GraphicsDevice device, Camera3D camera, Color color)
        {
            this.device = device;
            this.camera = camera;

            // Declared in counter-clockwise format.
            this.vertices = new[]
            {
                new VertexPositionColor(new Vector3(1.0f, -1.0f, 0.0f), color),
                new VertexPositionColor(new Vector3(-1.0f, -1.0f, 0.0f), color),
                new VertexPositionColor(new Vector3(0.0f, 1.0f, 0.0f), color)
            };

            this.effect = new BasicEffect(device);

            this.buffer = new VertexBuffer(device, VertexPositionColor.VertexDeclaration, 3,
                BufferUsage.WriteOnly);
            this.buffer.SetData(this.vertices);
        }

        public override void Draw()
        {
            this.effect.Projection = this.camera.Projection;
            this.effect.View = this.camera.View;
            this.effect.World = this.camera.GetWorldMatrix(this);
            this.effect.VertexColorEnabled = true;

            foreach (var pass in this.effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                this.device.DrawUserPrimitives(PrimitiveType.TriangleList, this.vertices, 0, 1);
            }
        }
    }
}