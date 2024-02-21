using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceRace.Source.Scenes
{
    public abstract class GameScene : DrawableGameComponent
    {
        public List<GameComponent> Components { get; set; }

        public virtual void hide()
        {
            this.Visible = false;
            this.Enabled = false;
        }
        public virtual void show()
        {
            this.Visible = true;
            this.Enabled = true;
        }
        public GameScene(Game game) : base(game)
        {
            Globals.game = game;
            Components = new List<GameComponent>();
            hide();
        }
        public override void Draw(GameTime gameTime)
        {
            foreach (DrawableGameComponent component
                in Components)
            {
                if (component.Visible)
                {
                    component.Draw(gameTime);
                }
            }
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent component in Components)
            {
                if (component.Enabled)
                {
                    component.Update(gameTime);
                }
            }
            base.Update(gameTime);
        }
    }
}
