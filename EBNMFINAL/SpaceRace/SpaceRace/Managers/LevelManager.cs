using Microsoft.Xna.Framework;
using SpaceRace.Source.GameComponents;
using SpaceRace.Source.GameEntities;
using SpaceRace.Source.Scenes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceRace.Managers
{
    public class LevelManager
    {
        private static Map currentMap;
        private static int levelCounter;
       /* private static WinScene winScene = new WinScene(Globals.game, );*/

        public LevelManager()
        {
            currentMap = new Map1();
            levelCounter = 0;
        }

        public static void SwitchMaps(Astronaut astronaut, Moon moon)
        {
            levelCounter++;
            Debug.WriteLine($"SwitchMaps called. levelCounter: {levelCounter}");
            switch (levelCounter)
            {
                case 1:
                    currentMap = new Map2();
                    astronaut.ResetPosition(new Vector2(100, 910));
                   
                    // Reset the screen size to fit the new level
                    Globals.screenSize = new(Map2.map2.GetLength(1) * Map2.tileSize, Map2.map2.GetLength(0) * Map2.tileSize);
                    Globals.graphics.PreferredBackBufferWidth = Globals.screenSize.X;
                    Globals.graphics.PreferredBackBufferHeight = Globals.screenSize.Y;
                    Globals.graphics.ApplyChanges();
                    break;
                case 2:
                    currentMap = new Map3();
                    astronaut.ResetPosition(new Vector2(100, 910));

                    // Reset the screen size to fit the new level
                    Globals.screenSize = new(Map3.map3.GetLength(1) * Map3.tileSize, Map3.map3.GetLength(0) * Map3.tileSize);
                    Globals.graphics.PreferredBackBufferWidth = Globals.screenSize.X;
                    Globals.graphics.PreferredBackBufferHeight = Globals.screenSize.Y;
                    Globals.graphics.ApplyChanges();
                    break;

                default:
                    break;
            }
        }

        // return current map 
        public static Map GetCurrentMap()
        {
            return currentMap;
        }

        public void Draw()
        {
            currentMap.Draw();
        }
    }
}
