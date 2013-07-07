using Artemis;
using Artemis.Attributes;
using Artemis.Interface;
using Artemis.Manager;
using Artemis.System;
using Mezzola.Engine.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mezzola
{
    public class TriangleComponent : IComponent
    {
        public TriangleComponent(Color color)
        {
            // Declared in counter-clockwise format.
            Vertices = new[]
                {
                    new VertexPositionColor(new Vector3(1.0f, -1.0f, 0.0f), color),
                    new VertexPositionColor(new Vector3(-1.0f, -1.0f, 0.0f), color),
                    new VertexPositionColor(new Vector3(0.0f, 1.0f, 0.0f), color)
                };
        }

        public VertexPositionColor[] Vertices { get; set; }
    }

    public class PositionComponent : IComponent
    {
        public Vector3 Position { get; set; }
    }

    [ArtemisEntitySystem(GameLoopType = GameLoopType.Draw, Layer = 2)]
    public class TriangleRenderSystem : EntityProcessingSystem
    {
        private readonly GraphicsDevice device;
        private readonly BasicEffect effect;

        public TriangleRenderSystem() : base(Aspect.All(typeof (TriangleComponent)))
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

    public class MezzolaGame : Game
    {
        private Camera3D camera;
        private EntityWorld world;

        public MezzolaGame()
        {
            // ReSharper disable ObjectCreationAsStatement - Don't need to store, but must create it.
            new GraphicsDeviceManager(this);
            // ReSharper restore ObjectCreationAsStatement
            Content = new ContentManager(Services) {RootDirectory = "Content"};
        }

        protected override void Initialize()
        {
            world = new EntityWorld();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            camera = new Camera3D(GraphicsDevice, new Vector3(0, 0, 25)) {FarClip = 50f};
            EntitySystem.BlackBoard.SetEntry("Camera", camera);

            EntitySystem.BlackBoard.SetEntry("GraphicsDevice", GraphicsDevice);
            world.InitializeAll(true);

            Entity e = world.CreateEntity();
            e.AddComponent(new TriangleComponent(Color.Red));
            e.AddComponent(new PositionComponent {Position = new Vector3(0, 0, 0)});
            e.IsEnabled = true;

            base.LoadContent();
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

//        protected override void Initialize()
//        {
//            camera = new Camera3D(GraphicsDevice, new Vector3(0, 0, 25)) { FarClip = 50f };
//            GraphicsDevice.RasterizerState.CullMode = CullMode.None;

//            player = new DrawableTriangle(GraphicsDevice, camera, Color.Red) { Position = Vector3.Left * 5 };
//            creep = new TriangleCreep(GraphicsDevice, camera, Color.Blue)
//            {
//                Position = new Vector3(5, 10, 0),
//                MovementSpeed = 5f,
//                TargetMoveTowards = new Vector3(2, -10, 0)
//            };
//            drawableEntities = new List<DrawableEntity> { creep, player };

//            input = new InputManager(this);
//            Components.Add(input);

//            base.Initialize();
//        }

//        protected override void LoadContent()
//        {
//            font = Content.Load<SpriteFont>("SimpleFont");
//            effect = Content.Load<Effect>("StockEffects/BasicEffect");
//            batch = new SpriteBatch(GraphicsDevice);
//            base.LoadContent();
//        }

//        protected override void Update(GameTime gameTime)
//        {
//            if (input.RightMouseButtonJustPressed)
//            {
//                player.Position =
//                    camera.TransformCoordinates(new Vector2(input.CurrentMouseState.X, input.CurrentMouseState.Y));
//            }
//            creep.Move((float)gameTime.ElapsedGameTime.TotalSeconds);

//            base.Update(gameTime);
//        }

//        protected override void Draw(GameTime gameTime)
//        {
//            GraphicsDevice.Clear(Color.Black);

//            // Draw the triangle guys.
//            foreach (var d in drawableEntities) d.Draw();

//            base.Draw(gameTime);
//        }
    }
}