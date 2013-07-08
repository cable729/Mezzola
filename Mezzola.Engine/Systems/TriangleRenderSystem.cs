using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using Mezzola.Engine.Components;
using Mezzola.Engine.View;
using Microsoft.Xna.Framework.Graphics;

namespace Mezzola.Engine.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Draw)]
    public class TriangleRenderSystem : EntityProcessingSystem
    {
        private readonly GraphicsDevice device;
        private readonly BasicEffect effect;

        public TriangleRenderSystem()
            : base(Aspect.All(typeof (TriangleComponent), typeof (PositionComponent)))
        {
            device = BlackBoard.GetEntry<GraphicsDevice>("GraphicsDevice");
            effect = new BasicEffect(device);
        }

        public override void Process(Entity entity)
        {
            var camera = BlackBoard.GetEntry<Camera3D>("Camera");
            var triangle = entity.GetComponent<TriangleComponent>();
            var position = entity.GetComponent<PositionComponent>();

            effect.Projection = camera.Projection;
            effect.View = camera.View;
            effect.World = camera.GetWorldMatrix(position.Position);
            effect.VertexColorEnabled = true;

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                device.DrawUserPrimitives(PrimitiveType.TriangleList, triangle.Vertices, 0, 1);
            }
        }
    }
}