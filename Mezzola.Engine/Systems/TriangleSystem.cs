using Mezzola.Engine.Components;
using Mezzola.Engine.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mezzola.Engine.Systems
{
    public class TriangleSystem : System<TriangleNode>
    {
        public Camera3D Camera { get; set; }
        private BasicEffect effect;

        public TriangleSystem(Game game, Camera3D camera) : base(game)
        {
            Camera = camera;
        }

        protected override void LoadContent()
        {
            this.effect = new BasicEffect(GraphicsDevice);
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var node in Nodes)
            {
                this.effect.Projection = Camera.Projection;
                this.effect.View = Camera.View;
                this.effect.World = Camera.GetWorldMatrix(node.Position.Position);
                this.effect.VertexColorEnabled = true;

                foreach (var pass in this.effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, node.Triangle.Vertices, 0, 1);
                }
            }
        }
    }
}
