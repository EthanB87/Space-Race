using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace SpaceRace
{
    public class Globals
    {
        public static ContentManager content;
        public static SpriteBatch spriteBatch;
        public static GraphicsDevice graphicsDevice;
        public static GraphicsDeviceManager graphics;
        public static Game game;
        public static Point screenSize;
        public static float time;

        public static void Update(GameTime gameTime)
        {
            time = (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}