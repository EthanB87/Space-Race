using Microsoft.Xna.Framework;
using SpaceRace.Source.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceRace.Source.GameComponents
{
    public abstract class Map
    {
        public abstract void Draw();

        public abstract List<Rectangle> GetCollisions(Rectangle bounds);


    }
}
