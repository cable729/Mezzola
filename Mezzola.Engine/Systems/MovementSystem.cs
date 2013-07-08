using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using Mezzola.Engine.Components;
using Mezzola.Engine.Extensions;

namespace Mezzola.Engine.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class MovementSystem : EntityProcessingSystem
    {
        public MovementSystem()
            : base(Aspect.All(typeof (MovementComponent), typeof (PositionComponent)))
        {
        }

        public override void Process(Entity entity)
        {
            var position = entity.GetComponent<PositionComponent>();
            var movement = entity.GetComponent<MovementComponent>();

            if (!movement.IsUnderOrder) return;

            const float epsilon = 0.01f;

            var distance = (movement.Destination - position.Position).Length();
            if (distance < movement.MovementSpeed * EntityWorld.DeltaToSeconds() && distance > epsilon)
            {
                position.Position = movement.Destination;
                movement.IsUnderOrder = false;
            }
            else if (distance > epsilon)
            {
                var movementDirection = movement.Destination - position.Position;
                movementDirection.Normalize();
                position.Position += movementDirection * movement.MovementSpeed * EntityWorld.DeltaToSeconds();
            }
        }
    }
}