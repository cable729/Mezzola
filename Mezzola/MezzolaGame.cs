using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mezzola
{
    public class MezzolaGame : Game
    {
        private VertexBuffer buffer;
        private BasicEffect effect;
        private VertexPositionColor[] vertices;
        private float rotationY;

        public MezzolaGame()
        {
            new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            this.vertices = new[]
            {
                new VertexPositionColor(new Vector3(0.0f, 1.0f, 0.0f), Color.Red),
                new VertexPositionColor(new Vector3(-1.0f, -1.0f, 0.0f), Color.Blue),
                new VertexPositionColor(new Vector3(1.0f, -1.0f, 0.0f), Color.Green)
            };
            this.effect = new BasicEffect(GraphicsDevice);
            this.buffer = new VertexBuffer(GraphicsDevice, VertexPositionColor.VertexDeclaration, 3,
                BufferUsage.WriteOnly);
            this.buffer.SetData(this.vertices);

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            rotationY += (float) gameTime.ElapsedGameTime.TotalSeconds;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            this.effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                GraphicsDevice.Viewport.AspectRatio, 0.001f, 1000.0f);
            this.effect.View = Matrix.CreateLookAt(new Vector3(0, 0, -5), Vector3.Forward, Vector3.Up);
            this.effect.World = Matrix.CreateRotationY(rotationY);
            this.effect.VertexColorEnabled = true;

            foreach (var pass in this.effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vertices, 0, 1);
            }

            base.Draw(gameTime);
        }
    }
}
