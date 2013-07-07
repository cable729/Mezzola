using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Mezzola.Engine.Systems
{
    public abstract class System<T> : DrawableGameComponent where T : class
    {
        protected System(Game game) : base(game)
        {
            Nodes = new List<T>();
        }

        protected List<T> Nodes { get; set; }

        public void AddNode(T node)
        {
            Nodes.Add(node);
        }
    }
}
