using Mezzola.Engine.Base;

namespace Mezzola.Engine.Unit
{
    public class Unit : DrawableEntity, IHealth
    {
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; set; }

        public override void Draw()
        {
        }
    }
}
