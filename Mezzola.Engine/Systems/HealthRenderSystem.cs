using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using Mezzola.Engine.Components;
using Mezzola.Engine.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mezzola.Engine.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Draw)]
    public class HealthRenderSystem : EntityProcessingSystem
    {
        private SpriteBatch batch;
        private SpriteFont font;
        private GraphicsDevice device;

        public HealthRenderSystem()
            : base(Aspect.All(typeof (PositionComponent), typeof(HealthComponent)))
        {
            device = BlackBoard.GetEntry<GraphicsDevice>("GraphicsDevice");
            batch = BlackBoard.GetEntry<SpriteBatch>("SpriteBatch");
            font = BlackBoard.GetEntry<SpriteFont>("SimpleFont");
        }

        public override void Process(Entity entity)
        {
            var health = entity.GetComponent<HealthComponent>();
            var position = entity.GetComponent<PositionComponent>();

            batch.DrawString(font, string.Format("Health: {0}/{1}", health.CurrentHealth, health.MaxHealth), new Vector2(2,2), Color.White);
        }

        protected override void Begin()
        {
            batch.Begin();
            base.Begin();
        }

        protected override void End()
        {
            batch.End();
            device.BlendState = BlendState.Opaque;
            device.DepthStencilState = DepthStencilState.Default;
            base.End();
        }
    }
}