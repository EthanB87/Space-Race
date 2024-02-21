using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using SpaceRace.Managers;
using SpaceRace.Source.GameEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceRace.Source.GameComponents
{
    public class Moon : Sprites
    {
        public bool hasCollided = false;
        private SoundEffect moonCollisionSound = Globals.content.Load<SoundEffect>("item-pick-up-38258");
        public Moon(Texture2D texture, Vector2 position) : base(texture, position)
        {
            this.texture = texture;
            this.position = position;
        }

        public void CheckCollisionWithAstronaut(Astronaut astronaut, Moon moon)
        {
            Rectangle moonBounds = CalculateMoonBounds(position);
            Rectangle astronautBounds = astronaut.CalculateAstronautBounds(astronaut.position);

            if (moonBounds.Intersects(astronautBounds))
            {
                moonCollisionSound.Play();
                LevelManager.SwitchMaps(astronaut, moon);
                hasCollided = true;
            }
        }

        private Rectangle CalculateMoonBounds(Vector2 pos)
        {
            return new Rectangle((int)pos.X, (int)pos.Y, texture.Width, texture.Height);
        }

        public void Draw()
        {
            Globals.spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
