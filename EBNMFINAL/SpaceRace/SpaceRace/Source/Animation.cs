using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SpaceRace
{
    public class Animation
    {
        public Texture2D texture;
        public List<Rectangle> srcRectangles = new List<Rectangle>();
        private int frames;
        private int frame;
        private float frameTime;
        private float frameTimeLeft;
        private int frameWidth;
        private int frameHeight;
        private int frameCounter = 0;
        private int frameUpdateThreshold = 5;
        private bool active = true;

        public Animation(Texture2D texture, int frameWidth, int frameHeight, int frames, int frameTime)
        {
            this.texture = texture;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.frames = 6;
            this.frameTime = 33;
            this.frameTimeLeft = frameTime;

            for (int i = 0; i < frames; i++)
            {
                srcRectangles.Add(new Rectangle(i * frameWidth, 0, frameWidth, frameHeight));
            }
        }

        public Rectangle GetNextFrame()
        {
            frameCounter++;
            if (frameCounter >= frameUpdateThreshold)
            {
                frame++;
                if (frame >= frames)
                    frame = 0;

                frameCounter = 0;
            }

            int sourceX = frame * frameWidth;
            int sourceY = 0;

            Rectangle sourceRect = new Rectangle(sourceX, sourceY, frameWidth, frameHeight);
            return sourceRect;
        }

        public void Stop()
        {
            active = false;
        }

        public void Start()
        {
            active = true;
        }

        public void Reset()
        {
            frame = 0;
            frameTimeLeft = frameTime;
        }

        public void Update()
        {
            if (!active)
            {
                return;
            }

            frameTimeLeft -= Globals.time;

            if (frameTimeLeft <= 0)
            {
                frameTimeLeft += frameTime;
                frame = (frame + 1) % frames;
            }
        }

        public void Draw(Vector2 position)
        {
            Globals.spriteBatch.Draw(texture, position, srcRectangles[frame], Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 1);
        }
    }
}
