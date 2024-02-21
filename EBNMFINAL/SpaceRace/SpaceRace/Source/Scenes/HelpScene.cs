using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceRace.Source.Scenes
{
    public class HelpScene : GameScene
    {
        private SpriteFont spriteFont;
        private Texture2D texture;

        public HelpScene(Game game) : base(game)
        {
            Globals.game = game;
            spriteFont = game.Content.Load<SpriteFont>("File");
        }

        public void LoadContentForScene()
        {
            // Load the background texture
            texture = Game.Content.Load<Texture2D>("spacebackground1");

        }

        public override void Draw(GameTime gameTime)
        {

            Globals.spriteBatch.Begin();
            Globals.spriteBatch.Draw(texture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 2.7f, SpriteEffects.None, 0f);
            Globals.spriteBatch.End();

            SpriteBatch spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteBatch.Begin();

            // Draw text to explain controls
            string controlsText = "Controls:\n\nA - Move Left\nD - Move Right\nSpace - Jump\nSpaceX2 - Double Jump";
            Vector2 textPosition = new Vector2(50, 50);
            Color textColor = Color.LightGray;

            spriteBatch.DrawString(spriteFont, controlsText, textPosition, textColor);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
