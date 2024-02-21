using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SharpDX.Direct3D9;
using SpaceRace.Source.GameEntities;

namespace SpaceRace
{

   // master class for sprites that all components inherit from
    public class Sprites
    {
        public Texture2D texture;
        public Vector2 position;
        public Sprites(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
        }

        public void Draw()
        {
            Globals.spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
