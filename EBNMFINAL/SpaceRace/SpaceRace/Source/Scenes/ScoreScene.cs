using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceRace.Source.Scenes
{
    public class ScoreScene : GameScene
    {
        private SpriteFont spriteFont;
        private Texture2D texture;
        private GameManager gameManager;

        public ScoreScene(Game1 game, GameManager gameManager) : base(game)
        {
            this.gameManager = gameManager;
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

            //Drawing the content of the score page
            List<TimeSpan> highScores = gameManager.HighScores;
            Vector2 highScorePosition = new Vector2(100, 300);
            Vector2 textPosition = new Vector2(100, 100);
            Color textColor = Color.LightGray;

            for (int i = 0; i < highScores.Count; i++)
            {
                string highScoreText = $" High Scores: {highScores[i].Hours:D2}:{highScores[i].Minutes:D2}:{highScores[i].Seconds:D2}";
                spriteBatch.DrawString(spriteFont, highScoreText, textPosition, textColor);
                
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
