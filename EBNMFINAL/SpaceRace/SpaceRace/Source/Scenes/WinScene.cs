using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SpaceRace.Source.Scenes
{
    public class WinScene : GameScene
    {
        MenuComponent winMenu;
        private Texture2D texture;
        private SpriteFont spriteFont;
        private GameManager gameManager;
        public int getSelectedIndexWin()
        {
            return winMenu.selectedIndex;
        }

        public WinScene(Game game, GameManager gameManager) : base(game)
        {
            this.gameManager = gameManager;
            Globals.game = game;
            SpriteFont regular = game.Content.Load<SpriteFont>("File");
            SpriteFont highlighted = game.Content.Load<SpriteFont>("Highlight");
            winMenu = new MenuComponent(game, Globals.spriteBatch, regular, highlighted, new Vector2(100, 500), 
                new string[] {"Main Menu","Exit"}, Color.LightGray, Color.Red);
            this.Components.Add(winMenu);
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

            // Draw text to show win
            string controlsText = "Sorry Explorer\nThere are More Planets In Another Facility.... ";
            Vector2 textPosition = new Vector2(100, 100);
            Color textColor = Color.LightGray;

            TimeSpan currentScoreTime = gameManager.TotalGameTime;
            string currentScoreText = $" Your Time: {currentScoreTime.Hours:D2}:{currentScoreTime.Minutes:D2}:{currentScoreTime.Seconds:D2}";
            Vector2 currentScorePosition = new Vector2(100, 300);

            Globals.spriteBatch.DrawString(spriteFont, controlsText, textPosition, textColor);
            Globals.spriteBatch.DrawString(spriteFont, currentScoreText, currentScorePosition, textColor);

            Globals.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
