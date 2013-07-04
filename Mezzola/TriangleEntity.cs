using System;
using Mezzola.Engine.Base;
using Mezzola.Engine.Unit;
using Mezzola.Engine.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mezzola
{
    public class TriangleCreep : TriangleEntity, IHealth, IUnit
    {
        public TriangleCreep(GraphicsDevice device, Camera3D camera, Color color) : base(device, camera, color)
        {
        }

        public int CurrentHealth { get; set; }
        public int MaxHealth { get; set; }
        public float MovementSpeed { get; set; }
        public Vector3 TargetMoveTowards { get; set; }

        public void Move(float delta)
        {
            const float epsilon = 0.01f;

            var distance = (TargetMoveTowards - Position).Length();
            if (distance < MovementSpeed * delta && distance > epsilon)
            {
                Position = TargetMoveTowards;
            }
            else if (distance > epsilon)
            {
                var movementDirection = TargetMoveTowards - Position;
                movementDirection.Normalize();
                Position += movementDirection * MovementSpeed * delta;
            }
        }
    }

    public class TriangleEntity : DrawableEntity
    {
        private readonly VertexBuffer buffer;
        private readonly Camera3D camera;
        private readonly GraphicsDevice device;
        private readonly BasicEffect effect;
        private readonly VertexPositionColor[] vertices;

        public TriangleEntity(GraphicsDevice device, Camera3D camera, Color color)
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
            this.effect.Projection = this.camera.Projection;
            this.effect.View = this.camera.View;
            this.effect.World = this.camera.GetWorldMatrix(this);
            this.effect.VertexColorEnabled = true;

            foreach (var pass in this.effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                this.device.DrawUserPrimitives(PrimitiveType.TriangleList, this.vertices, 0, 1);
            }
        }
    }
}
