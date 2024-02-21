using Microsoft.Xna.Framework;
using SpaceRace.Source.GameComponents;
using SpaceRace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using SpaceRace.Source.Scenes;

namespace SpaceRace.Source.GameComponents
{
    public class Map3 : Map
    {

        public static int[,] map3 = {
 {2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
{2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2},
{ 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2},
{ 2, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2},
{ 2, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 1, 1, 0, 0, 0, 0, 2},
{ 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2},
{ 2, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2},
{ 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2},
{ 2, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2},
{ 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2},
{ 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2},
{ 2, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 2},
{ 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 2},
{ 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2},
{ 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 2},
{ 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2},
{ 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 2},
{ 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2},
{ 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 2},
{ 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2}};


        public static int tileSize = 50;

        public int GetTile(int row, int col)
        {
            // Check if the given row and column are within the map bounds
            if (row >= 0 && row < map3.GetLength(0) && col >= 0 && col < map3.GetLength(1))
            {
                return map3[row, col];
            }
            else
            {
                // Return a default value or handle out-of-bounds cases
                return -1;
            }
        }
        public Texture2D GetTileTexture(int tileValue)
        {
            switch (tileValue)
            {
                case 0:
                    return null;  // Return null for empty tiles
                case 1:
                    return Globals.content.Load<Texture2D>("scifi_platform");
                case 2:
                    return Globals.content.Load<Texture2D>("scifi_platform2");
                // Add cases for other tile values as needed
                default:
                    throw new ArgumentOutOfRangeException(nameof(tileValue), tileValue, null);
            }
        }
        public override List<Rectangle> GetCollisions(Rectangle bounds)
        {
            List<Rectangle> result = new List<Rectangle>();

            int leftTile = bounds.Left / tileSize;
            int rightTile = (bounds.Right - 1) / tileSize;
            int topTile = bounds.Top / tileSize;
            int bottomTile = (bounds.Bottom - 1) / tileSize;

            leftTile = MathHelper.Clamp(leftTile, 0, map3.GetLength(1) - 1);
            rightTile = MathHelper.Clamp(rightTile, 0, map3.GetLength(1) - 1);
            topTile = MathHelper.Clamp(topTile, 0, map3.GetLength(0) - 1);
            bottomTile = MathHelper.Clamp(bottomTile, 0, map3.GetLength(0) - 1);

            for (int x = topTile; x <= bottomTile; x++)
            {
                for (int y = leftTile; y <= rightTile; y++)
                {
                    if (map3[x, y] != 0)  // Assuming 0 represents an empty tile
                    {
                        // Adjust the rectangle position based on tileSize
                        Rectangle tileBounds = new Rectangle(y * tileSize, x * tileSize, tileSize, tileSize);

                        // Add only non-empty tiles to the result
                        if (bounds.Intersects(tileBounds))
                        {
                            result.Add(tileBounds);
                        }
                    }
                }
            }

            return result;
        }


        public override void Draw()
        {
            for (int i = 0; i < map3.GetLength(0); i++)
            {
                for (int j = 0; j < map3.GetLength(1); j++)
                {
                    int tileValue = map3[i, j];

                    if (tileValue != 0)  // Skip drawing for empty tiles (value 0)
                    {
                        Texture2D tileTexture = GetTileTexture(tileValue);
                        Globals.spriteBatch.Draw(tileTexture, new Vector2(j * tileSize, i * tileSize), Color.White);
                    }
                }
            }
        }
    }
}

