using Artemis.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mezzola.Engine.Components
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
}