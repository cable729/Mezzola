using System;
using Mezzola.Engine.Components;

namespace Mezzola.Engine.Nodes
{
    public class TriangleNode : Node
    {
        private readonly Type[] components = new[]
        {
            typeof (TriangleComponent),
            typeof (PositionComponent)
        };
        public override Type[] ComponentRequirements
        {
            get { return components; }
        }

        public TriangleComponent Triangle { get; set; }
        public PositionComponent Position { get; set; }
    }
}