using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Mezzola.Engine.Input
{
    public class InputManager : GameComponent
    {
        public InputManager(Game game) : base(game)
        {
        }

        public MouseState CurrentMouseState { get; private set; }
        public MouseState PreviousMouseState { get; private set; }

        public bool RightMouseButtonJustPressed
        {
            get
            {
                return PreviousMouseState.RightButton == ButtonState.Released &&
                       CurrentMouseState.RightButton == ButtonState.Pressed;
            }
        }

        public override void Update(GameTime gameTime)
        {
            PreviousMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();
        }
    }
}
