using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SpaceRace.Managers;
using SpaceRace.Source.GameComponents;
using SpaceRace.Source.GameEntities;
using SpaceRace.Source.Scenes;

namespace SpaceRace
{
    public class GameManager
    {
        private LevelManager levelManager;
        private Astronaut astronaut;
        private Moon moon;
        private int collisionCount = 0;
        public bool hasWon = false;
        public bool winState = false;
        private List<TimeSpan> highScores;
        public List<TimeSpan> HighScores => highScores;
        private TimeSpan totalGameTime;
        public TimeSpan TotalGameTime => totalGameTime;

        public GameManager()
        {
            highScores = new List<TimeSpan>();
            totalGameTime = TimeSpan.Zero;
            
            // Create a dictionary of animations for the astronaut
            Dictionary<string, Texture2D> astronautAnimations = new Dictionary<string, Texture2D>
        {
            { "Idle", Globals.content.Load<Texture2D>("Astronaut_Idle") },
            { "Running", Globals.content.Load<Texture2D>("Astronaut_Run") },
            { "Jumping", Globals.content.Load<Texture2D>("Astronaut_Jump") },
        };
            levelManager = new LevelManager();
            astronaut = new Astronaut(astronautAnimations, new Vector2(100, 910));
            moon = new Moon(Globals.content.Load<Texture2D>("Moon"), new Vector2(503, 120));

            SetMoonPosition();
        }

        public void AddHighScore(TimeSpan score)
        {
            highScores.Add(score);
            // sort the high scores in ascending order
            highScores.Sort();
            // keep only the top 5 scores
            if (highScores.Count > 5)
            {
                highScores.RemoveAt(5);
            }
        }

        private void SetMoonPosition()
        {
            Map currentMap = LevelManager.GetCurrentMap();

            if(currentMap is Map2) 
            {
                moon.position = new Vector2(800, 120);
            }
            else if(currentMap is Map3)
            {
                moon.position = new Vector2(800, 120);
            }

        }

        public void Playing(GameTime gameTime)
        {

        }

        public void Update(GameTime gameTime)
        {

            if (!hasWon)
            {
                astronaut.Update();
                moon.CheckCollisionWithAstronaut(astronaut, moon);

                if(moon.hasCollided)
                {
                    collisionCount++;
                    moon.hasCollided = false;

                    if(collisionCount == 3)
                    {
                        hasWon = true;
                        winState = true;
                    }
                }
                totalGameTime += gameTime.ElapsedGameTime;
                
                Debug.WriteLine(totalGameTime);
                SetMoonPosition();
            }

            else
            {
                moon.position = new Vector2(1000, 1000);
                astronaut.position = new Vector2(1000, 1000);
                Debug.WriteLine("Got here");
                AddHighScore(totalGameTime);
                Debug.WriteLine($"GameManager Update: {totalGameTime}");
            }
        }

        public void Draw(GameTime gameTime)
        {
            if (!hasWon)
            {
                levelManager.Draw();
                moon.Draw();
                astronaut.Draw();
            }
        }
    }
}

