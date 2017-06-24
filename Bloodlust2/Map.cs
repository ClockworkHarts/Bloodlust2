using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Bloodlust2
{
    public class Map
    {
        Tile[,] tileMap;
        public int mapWidth = 100;
        public int mapHeight = 100;
        public Texture2D genericTile64;
        public Texture2D dirtTile;
        public Texture2D stoneTile;
        public Texture2D waterTile;

        public List<Tree> treeMap = new List<Tree>();
        public Texture2D genericTile32;
        public Texture2D pineTexture;
        public Texture2D oakTexture;
        public Texture2D spruceTexture;
        public Texture2D cedarTexture;



        public void Load(ContentManager Content)
        {
            genericTile64 = Content.Load<Texture2D>("Tile64");
            genericTile32 = Content.Load<Texture2D>("Tile32");

            GenerateMap();
        }
        public void GenerateMap()
        {
            //making the tile map
            tileMap = new Tile[mapWidth, mapHeight];
            SimplexNoise1 noise = new SimplexNoise1();
            noise.setSeed(01234567895);

            //making da trees
            SimplexNoise1 treeNoise = new SimplexNoise1();
            treeNoise.setSeed(515438);

            //setting da tile map
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    tileMap[x, y] = new Tile();
                    tileMap[x, y].position = new Vector2(x * 64, y * 64);
                    
                    float h = noise.getPerlinNoise(x, y, 100, 64);
                    if (h >= 58)
                    {
                        //is mountain
                        tileMap[x, y].type = TileType.Stone;
                    }
                    else if (h >= 35)
                    {

                        //makes dirt tile
                        tileMap[x, y].type = TileType.Dirt;

                        //for trees
                        float i = treeNoise.getPerlinNoise(x, y, 100, 32);
                        if (i < 50)
                            continue;
                        
                        Tree tree = new Tree();
                        tree.position = new Vector2(((x * 64) + 16), ((y * 64) + 16));

                        
                        if (i >= 85)
                        {
                            tree.type = TreeType.Cedar;
                        }
                        else if (i >= 70)
                        {
                            tree.type = TreeType.Oak;
                        }
                        else if (i >= 55)
                        {
                            tree.type = TreeType.Pine;
                        }
                        else if (i >= 50)
                        {
                            tree.type = TreeType.Spruce;
                        }

                        treeMap.Add(tree);
                        
                    }
                    else if (h >= 0)
                    {
                        //is water
                        tileMap[x, y].type = TileType.Water;
                    } 
                }
            }
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    switch (tileMap[x, y].type)
                    {
                        case TileType.Dirt:
                            spriteBatch.Draw(genericTile64, tileMap[x, y].position, Color.SaddleBrown);
                            break;

                        case TileType.Water:
                            spriteBatch.Draw(genericTile64, tileMap[x, y].position, Color.Blue);
                            break;

                        case TileType.Stone:
                            spriteBatch.Draw(genericTile64, tileMap[x, y].position, Color.Pink);
                            break;
          
                    }

                }
            }
            foreach(Tree tree in treeMap)
            {
                int x = (int)tree.position.X / 64;
                int y = (int)tree.position.Y / 64;
                switch (tree.type)
                {
                    case TreeType.Cedar:
                        spriteBatch.Draw(genericTile32, tree.position, Color.GreenYellow);
                        break;

                    case TreeType.Oak:
                        spriteBatch.Draw(genericTile32, tree.position, Color.LawnGreen);
                        break;

                    case TreeType.Pine:
                        spriteBatch.Draw(genericTile32, tree.position, Color.ForestGreen);
                        break;

                    case TreeType.Spruce:
                        spriteBatch.Draw(genericTile32, tree.position, Color.Green);
                        break;
                }
            }
            
        }

    }
}
