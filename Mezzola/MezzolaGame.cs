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
        private TempSprite player;
        private InputManager input;
        private Camera3D camera;

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

            player = new TempSprite(GraphicsDevice, camera, Color.Red) { Position = Vector3.Left * 5 };
            drawableEntities = new List<DrawableEntity>
            {
                new TempSprite(GraphicsDevice, camera, Color.Blue) { Position = Vector3.Zero },
                player
            };
            
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
