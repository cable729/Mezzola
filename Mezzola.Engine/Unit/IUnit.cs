using Microsoft.Xna.Framework;

namespace Mezzola.Engine.Unit
{
    public interface IUnit
    {
        float MovementSpeed { get; set; }
        Vector3 TargetMoveTowards { get; set; }

        void Move(float delta);
    }
}
