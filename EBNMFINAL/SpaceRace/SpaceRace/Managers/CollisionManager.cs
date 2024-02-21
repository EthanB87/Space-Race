using Microsoft.Xna.Framework;
using SpaceRace.Source.GameEntities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SpaceRace.Source.GameEntities.Map1;

namespace SpaceRace.Managers
{
    public class CollisionManager
    {
        public enum CollisionDirection
        {
            None,
            Top,
            Bottom,
            Left,
            Right
        }
        public static CollisionDirection GetCollisionDirection(Rectangle rectA, Rectangle rectB)
        {
            if (rectA.Intersects(rectB))
            {
                float overlapWidth = Math.Min(rectA.Right, rectB.Right) - Math.Max(rectA.Left, rectB.Left);
                float overlapHeight = Math.Min(rectA.Bottom, rectB.Bottom) - Math.Max(rectA.Top, rectB.Top);

                if (overlapWidth < overlapHeight)
                {
                    // Collision is more likely in the horizontal direction
                    return rectA.Center.X < rectB.Center.X ? CollisionDirection.Right : CollisionDirection.Left;
                }
                else
                {
                    // Collision is more likely in the vertical direction
                    return rectA.Center.Y < rectB.Center.Y ? CollisionDirection.Bottom : CollisionDirection.Top;
                }
            }

            return CollisionDirection.None;
        }

    }
}
