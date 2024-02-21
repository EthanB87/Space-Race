using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Media;

namespace SpaceRace.Source.Scenes
{
    public class StartScene : GameScene
    {
        MenuComponent menu;
        Texture2D texture;
        public int getSelectedIndex()
        {
            return menu.selectedIndex;
        }
        public StartScene(Game game) : base(game)
        {
            Globals.game = game;
            SpriteFont regular = game.Content.Load<SpriteFont>("File");
            SpriteFont highlighted = game.Content.Load<SpriteFont>("Highlight");
            menu = new MenuComponent(game,Globals.spriteBatch, regular, highlighted, new Vector2(100, 100), new string[] {"Start",
                "Help","Controls","High Score", "Exit"}, Color.LightGray, Color.Red);
            this.Components.Add(menu);

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

            base.Draw(gameTime);
        }


    }
}
