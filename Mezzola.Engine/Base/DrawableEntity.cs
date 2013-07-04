namespace Mezzola.Engine.Base
{
    public interface IDrawable
    {
        void Draw();
    }

    public abstract class DrawableEntity : Entity, IDrawable
    {
        public abstract void Draw();
    }
}
