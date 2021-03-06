﻿using Artemis;
using Artemis.Manager;
using Artemis.System;
using Mezzola.Engine.Components;
using Mezzola.Engine.Input;
using Mezzola.Engine.Systems;
using Mezzola.Engine.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mezzola
{
    public class MezzolaGame : Game
    {
        private Camera3D camera;
        private readonly EntityWorld world;
        private readonly InputManager input;
        private SpriteFont simpleFont;
        private SpriteBatch batch;

        public MezzolaGame()
        {
            // ReSharper disable ObjectCreationAsStatement - Don't need to store, but must create it.
            new GraphicsDeviceManager(this);
            // ReSharper restore ObjectCreationAsStatement
            Content = new ContentManager(Services) { RootDirectory = "Content" };

            world = new EntityWorld();
            input = new InputManager(this);
        }

        protected override void Initialize()
        {
            Components.Add(input);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            camera = new Camera3D(GraphicsDevice, new Vector3(0, 0, 25)) {FarClip = 50f};
            simpleFont = Content.Load<SpriteFont>("SimpleFont");
            batch = new SpriteBatch(GraphicsDevice);

            EntitySystem.BlackBoard.SetEntry("Camera", camera);
            EntitySystem.BlackBoard.SetEntry("SimpleFont", simpleFont);
            EntitySystem.BlackBoard.SetEntry("SpriteBatch", batch);
            EntitySystem.BlackBoard.SetEntry("GraphicsDevice", GraphicsDevice);
            EntitySystem.BlackBoard.SetEntry("Input", input);
            
            SetupComponents();

            base.LoadContent();
        }

        private void SetupComponents()
        {
            world.InitializeAll(true);

            var player = world.CreateEntity();
            player.AddComponent(new TriangleComponent(Color.Red));
            player.AddComponent(new PositionComponent { Position = new Vector3(0, 0, 0) });
            player.AddComponent(new MovementComponent { MovementSpeed = 6f });
            player.AddComponent(new PlayerInputComponent());
            player.AddComponent(new HealthComponent{CurrentHealth=50,MaxHealth=50});

            var computer = world.CreateEntity();
            computer.AddComponent(new TriangleComponent(Color.Blue));
            computer.AddComponent(new PositionComponent { Position = new Vector3(4, -4, 0) });

            var computer2 = world.CreateEntity();
            computer2.AddComponent(new TriangleComponent(Color.Green));
            computer2.AddComponent(new PositionComponent { Position = new Vector3(4, 4, 0) });
        }

        protected override void Update(GameTime gameTime)
        {
            world.Update(gameTime.ElapsedGameTime.Ticks);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            world.Draw();
            base.Draw(gameTime);
        }
    }
}