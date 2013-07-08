using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using Mezzola.Engine.Components;
using Mezzola.Engine.Input;
using Mezzola.Engine.View;
using Microsoft.Xna.Framework;

namespace Mezzola.Engine.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class InputForMoveSystem : EntityProcessingSystem
    {
        private readonly InputManager input;
        private readonly Camera3D camera;

        public InputForMoveSystem()
            : base(Aspect.All(typeof (MovementComponent), typeof(PlayerInputComponent)))
        {
            input = BlackBoard.GetEntry<InputManager>("Input");
            camera = BlackBoard.GetEntry<Camera3D>("Camera");
        }

        public override void Process(Entity entity)
        {
            var movement = entity.GetComponent<MovementComponent>();

            if (input.RightMouseButtonJustPressed)
            {
                var clickCoords = new Vector2(input.CurrentMouseState.X, input.CurrentMouseState.Y);
                movement.IsUnderOrder = true;
                movement.Destination = camera.TransformCoordinates(clickCoords);
            }
        }
    }
}