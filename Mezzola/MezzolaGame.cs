using System.Collections.Generic;
using Mezzola.Engine.Base;
using Mezzola.Engine.Input;
using Mezzola.Engine.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mezzola
{
    class TempSprite : DrawableEntity
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
            effect.Projection = camera.Projection;
            effect.View = camera.View;
            effect.World = camera.GetWorldMatrix(this);
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
        private TempSprite player;
        private InputManager input;
        private Camera3D camera;

        public MezzolaGame()
        {
            new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            camera = new Camera3D(GraphicsDevice) { Position = new Vector3(0, 0, 50) };

            player = new TempSprite(GraphicsDevice, camera, Color.Red) { Position = Vector3.Left * 5 };
            drawableEntities = new List<DrawableEntity>
            {
                new TempSprite(GraphicsDevice, camera, Color.Blue) { Position = Vector3.Zero },
                player
            };
            GraphicsDevice.RasterizerState.CullMode = CullMode.None;
            
            input = new InputManager(this);
            Components.Add(input);

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            if (input.RightMouseButtonJustPressed)
            {
                var clickPosition = new Vector3(input.CurrentMouseState.X, input.CurrentMouseState.Y, 1);
                var moveTo = GraphicsDevice.Viewport.Unproject(clickPosition, camera.Projection, camera.View,
                    Matrix.Identity);
                player.Position = new Vector3(moveTo.X, moveTo.Y, 0);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            foreach (var d in drawableEntities) d.Draw();

            base.Draw(gameTime);
        }
    }
}
