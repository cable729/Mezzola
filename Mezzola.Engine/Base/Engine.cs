using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Mezzola.Engine.Base
{
    public class Engine : DrawableGameComponent
    {
        public Engine(Game game) : base(game)
        {
            Systems = new List<DrawableGameComponent>();
            Entities = new List<Entity>();
        }

        /// <summary>A list of systems sorted with lowest priority first.</summary>
        protected List<DrawableGameComponent> Systems { get; set; }
        protected List<Entity> Entities { get; set; }


        public void AddEntity(Entity e)
        {
            Entities.Add(e);
        }

        public void AddSystem(DrawableGameComponent system)
        {
            // TODO: Insert based on priority. Write tests to help write this method.
            Systems.Add(system);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var s in Systems) s.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var s in Systems) s.Draw(gameTime);
        }
    }
}
