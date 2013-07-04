using Microsoft.Xna.Framework;

namespace Mezzola.Engine.Base
{
    public interface IDrawable
    {
        void Draw();
    }

    public abstract class DrawableEntity : IEntity, IDrawable
    {
        public abstract void Draw();
        public Vector3 Position { get; set; }
    }
}
