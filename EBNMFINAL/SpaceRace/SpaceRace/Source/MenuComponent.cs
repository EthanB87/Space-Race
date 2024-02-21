using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceRace.Source
{
    public class MenuComponent : DrawableGameComponent
    {
        SpriteFont regular, highlighted;
        SpriteBatch spriteBatch;
        Vector2 position;
        string[] items;
        Color regularColor, highlightedColor;
        public int selectedIndex = 0;
        KeyboardState oldState;

        public MenuComponent(Game game,
            SpriteBatch spriteBatch,
            SpriteFont regular, SpriteFont highlighted,
            Vector2 position, string[] items,
            Color regularColor, Color highlightedColor) :
            base(game)
        {
            this.spriteBatch = spriteBatch;
            this.regular = regular;
            this.highlighted = highlighted;
            this.position = position;
            this.items = items;
            this.regularColor = regularColor;
            this.highlightedColor = highlightedColor;
        }
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Down) &&
                oldState.IsKeyUp(Keys.Down))
            {

                selectedIndex = selectedIndex + 1 == items.Length ?
                    0 : selectedIndex + 1;

            }
            if (ks.IsKeyDown(Keys.Up) &&
                oldState.IsKeyUp(Keys.Up))
            {

                selectedIndex = selectedIndex == 0 ?
                    items.Length - 1 : selectedIndex - 1;
            }
            oldState = ks;
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            for (int i = 0; i < items.Length; i++)
            {
                if (i == selectedIndex)
                {
                    spriteBatch.DrawString(highlighted, items[i],
                    new Vector2(position.X,
                    position.Y + highlighted.LineSpacing * i),
                    highlightedColor);
                }
                else
                {
                    spriteBatch.DrawString(regular, items[i],
                    new Vector2(position.X,
                    position.Y + highlighted.LineSpacing * i),
                    regularColor);
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
