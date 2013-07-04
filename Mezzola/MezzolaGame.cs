using System.Collections.Generic;
using Mezzola.Engine.Base;
using Mezzola.Engine.Unit;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mezzola
{
    class TempSprite : DrawableEntity
    {
        private readonly GraphicsDevice device;
        private readonly VertexPositionColor[] vertices;
        private readonly BasicEffect effect;
        private readonly VertexBuffer buffer;

        public TempSprite(GraphicsDevice device, Color color)
        {
            this.device = device;
            // Declared in clockwise format.
            this.vertices = new[]
            {
                new VertexPositionColor(new Vector3(0.0f, 1.0f, 0.0f), color),
                new VertexPositionColor(new Vector3(-1.0f, -1.0f, 0.0f), color),
                new VertexPositionColor(new Vector3(1.0f, -1.0f, 0.0f), color)
            };
            this.effect = new BasicEffect(device);
            this.buffer = new VertexBuffer(device, VertexPositionColor.VertexDeclaration, 3,
                BufferUsage.WriteOnly);
            this.buffer.SetData(this.vertices);
        }

        public override void Draw()
        {
            // Projection defines how far to the left, right, close, and far to render.
            this.effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                device.Viewport.AspectRatio, 0.001f, 1000.0f);
            // View defines which way is forward, left, right, up, down, etc.
            this.effect.View = Matrix.CreateLookAt(new Vector3(0, 0, -25), Vector3.Down, Vector3.Forward);
            // World shifts the actual object.
            this.effect.World = Matrix.CreateTranslation(Position);
            this.effect.VertexColorEnabled = true;

            foreach (var pass in this.effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                device.DrawUserPrimitives(PrimitiveType.TriangleList, vertices, 0, 1);
            }
        }
    }

    public class MezzolaGame : Game
    {
        private List<DrawableEntity> drawableEntities;

        public MezzolaGame()
        {
            new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            drawableEntities = new List<DrawableEntity>
            {
                new TempSprite(GraphicsDevice, Color.Blue) { Position = Vector3.Zero },
                new TempSprite(GraphicsDevice, Color.Red) { Position = Vector3.Left * 5 }
            };

            base.Initialize();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            foreach (var d in drawableEntities)
            {
                d.Draw();
            }

            base.Draw(gameTime);
        }
    }
}
