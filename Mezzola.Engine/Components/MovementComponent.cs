using Artemis.Interface;
using Microsoft.Xna.Framework;

namespace Mezzola.Engine.Components
{
    public class MovementComponent : IComponent
    {
        public float MovementSpeed { get; set; }
        public bool IsUnderOrder { get; set; }
        public Vector3 Destination { get; set; }
    }
}
