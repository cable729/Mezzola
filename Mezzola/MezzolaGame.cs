using System.Collections.Generic;
using Mezzola.Engine.Base;
using Mezzola.Engine.Input;
using Mezzola.Engine.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mezzola
{
    public class MezzolaGame : Game
    {
        private List<DrawableEntity> drawableEntities;
        private TriangleEntity player;
        private InputManager input;
        private Camera3D camera;
        private TriangleCreep creep;

        public MezzolaGame()
        {
// ReSharper disable ObjectCreationAsStatement - Don't need to store, but must create it.
            new GraphicsDeviceManager(this);
// ReSharper restore ObjectCreationAsStatement
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            camera = new Camera3D(GraphicsDevice, new Vector3(0, 0, 25)) { FarClip = 50f };
            GraphicsDevice.RasterizerState.CullMode = CullMode.None;

            player = new TriangleEntity(GraphicsDevice, camera, Color.Red) { Position = Vector3.Left * 5 };
            creep = new TriangleCreep(GraphicsDevice, camera, Color.Blue)
            {
                Position = new Vector3(5, 10, 0),
                MovementSpeed = 5f,
                TargetMoveTowards = new Vector3(2, -10, 0)
            };
            drawableEntities = new List<DrawableEntity> { creep, player };
            
            input = new InputManager(this);
            Components.Add(input);

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            if (input.RightMouseButtonJustPressed)
            {
                player.Position =
                    camera.TransformCoordinates(new Vector2(input.CurrentMouseState.X, input.CurrentMouseState.Y));
            }
            creep.Move((float)gameTime.ElapsedGameTime.TotalSeconds);

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
