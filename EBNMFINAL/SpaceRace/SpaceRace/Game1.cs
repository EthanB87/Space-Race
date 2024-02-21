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
using SharpDX.XInput;
using SpaceRace.Source.GameComponents;
using SpaceRace.Source.GameEntities;
using SpaceRace.Source.Scenes;

namespace SpaceRace
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        private GameManager _gameManager;
        private SoundEffect jumpSound;
        private SoundEffect moonCollisionSound;
        private Song backgroundMusic;
        StartScene start;
        HelpScene help;
        WinScene win;
        AboutScene about;
        ScoreScene score;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Globals.graphics = _graphics;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        public void HideAllScenes()
        {
            foreach (GameScene scene in this.Components)
            {
                scene.hide();
            }
        }

        protected override void Initialize()
        {
            //change the screen size to fit the dimensions of the map being loaded
            Globals.screenSize = new(Map1.map.GetLength(1) * Map1.tileSize, Map1.map.GetLength(0) * Map1.tileSize);
            Globals.graphics.PreferredBackBufferWidth = Globals.screenSize.X;
            Globals.graphics.PreferredBackBufferHeight = Globals.screenSize.Y;
            Globals.graphics.ApplyChanges();

            Globals.content = Content;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _gameManager = new GameManager();
            Globals.spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.graphicsDevice = GraphicsDevice;
            jumpSound = Globals.content.Load<SoundEffect>("cartoon-jump-6462");
            moonCollisionSound = Globals.content.Load<SoundEffect>("item-pick-up-38258");
            backgroundMusic = Globals.content.Load<Song>("(Level 3) broken-bitz-170656");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(backgroundMusic);
            start = new StartScene(this);
            start.LoadContentForScene(); // loads the background image for fake menu lmao
            this.Components.Add(start);
            help = new HelpScene(this);
            help.LoadContentForScene();
            this.Components.Add(help);
            win = new WinScene(this, _gameManager);
            win.LoadContentForScene();
            this.Components.Add(win);
            about = new AboutScene(this);
            about.LoadContentForScene();
            this.Components.Add(about);
            score = new ScoreScene(this, _gameManager);
            score.LoadContentForScene();
            this.Components.Add(score);


             start.show();
        }

        protected override void Update(GameTime gameTime)
        {
            if(_gameManager.winState == true)
            {
                win.show();
                _gameManager.winState = false;
            }
            // Handling shifting between scenes
            if (start.Visible)
            {
                KeyboardState ks = Keyboard.GetState();
                if (ks.IsKeyDown(Keys.Enter))
                {
                    int selectedScene = start.
                        getSelectedIndex();
                    HideAllScenes();
                    switch (selectedScene)
                    {
                        case 1: help.show(); break;
                        case 2: about.show(); break;
                        case 3: score.show(); break;
                        case 4: Exit(); break;
                    }
                }
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Back))
                {
                    HideAllScenes();
                    start.show();
                }
            }

            // menu controls for win menu navigation
            if (win.Visible)
            {
                KeyboardState ks = Keyboard.GetState();
                if (ks.IsKeyDown(Keys.Enter))
                {
                    int selectedSceneWin = win.getSelectedIndexWin();
                    HideAllScenes();
                    switch (selectedSceneWin)
                    {
                        case 0: start.show();
                            break;
                        case 1: Exit(); 
                            break;
                    }
                }
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Globals.Update(gameTime);
            _gameManager.Update(gameTime);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            Globals.spriteBatch.Begin();
            _gameManager.Draw(gameTime);
            Globals.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}