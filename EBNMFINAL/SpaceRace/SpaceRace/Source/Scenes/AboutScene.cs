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
    public class AboutScene : GameScene
    {
        private SpriteFont spriteFont;
        private Texture2D texture;

        public AboutScene(Game1 game) : base(game)
        {
            Globals.game = game;
            spriteFont = game.Content.Load<SpriteFont>("File");
        }

        public void LoadContentForScene()
        {
            texture = Game.Content.Load<Texture2D>("spacebackground1");
        }

        public override void Draw(GameTime gameTime)
        {
            Globals.spriteBatch.Begin();
            Globals.spriteBatch.Draw(texture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 2.7f, SpriteEffects.None, 0f);
            Globals.spriteBatch.End();

            SpriteBatch spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteBatch.Begin();

            //Drawing the content of the about page
            string aboutText = "Developer Information:\n\nEthan Brockman\n8830509\nEbrockman@conestogac.on.ca\n\nNoah McCracken\n8761842\nNmccracken@conestogac.on.ca";
            Vector2 textPosition = new Vector2(100, 100);
            Color textColor = Color.LightGray;

            spriteBatch.DrawString(spriteFont, aboutText, textPosition, textColor);

            spriteBatch.End();

            base.Draw(gameTime);
        }



    }
}
