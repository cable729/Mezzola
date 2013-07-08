using Artemis.Interface;

namespace Mezzola.Engine.Components
{
    public class HealthComponent : IComponent
    {
        public float MaxHealth { get; set; }
        public float CurrentHealth { get; set; }
        public bool IsAlive { get { return CurrentHealth > 0; } }
    }
}
