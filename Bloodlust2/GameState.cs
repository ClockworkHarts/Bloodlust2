using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended;


namespace Bloodlust2
{
    public class GameState : AIE.State
    {
        public static GameState current;

        //GameState variables
        bool isLoaded = false;
        SpriteFont font = null;

        //general
        Random random = new Random();

        //Textures
        Texture2D generic16;

        //Map
        public Map map = new Map();

        //player
        public Player player = new Player();

        //NPCs
        //NPC class is a group of enemies with the same home location
        //Batches list is a list of NPCS, or a list of groups of enemies
        List<NPC> Batches = new List<NPC>();     
       
        //public gamewide variables
        public static float tile = 64;
        public static float meter = tile;
        public static Vector2 maxVelocity = new Vector2(meter * 15, meter * 15);
        public static float acceleration = (maxVelocity.X * 2);
        public static float friction = (maxVelocity.X * 8);
        public float deltaTime;
        

        //debugging stuff
        Texture2D debugMap;
        private void LoadNPCs(int numberOfNPCs, int RectX, int RectY, int RectWidth, int RectHeight, Color colour)
        {
            NPC Batch = new NPC();
            Batch.batchNumber = numberOfNPCs;
            Batch.CreateNPCLocation(RectX, RectY, RectWidth, RectHeight);
            Batch.colour = colour;
            Batches.Add(Batch);

        }

        private void Load(ContentManager Content, GameTime gameTime)
        {
            current = this;

            //general
            font = Content.Load<SpriteFont>("Arial");
            generic16 = Content.Load<Texture2D>("Tile16");

            //player
            player.Load(Content);
            player.Position = new Vector2(10, 10);
            player.scale = new Vector2(1, 1);
            player.health = 100;
            player.maxHealth = 100;

            //NPCs
            LoadNPCs(5, 10, 10, 300, 300, Color.Blue);
            LoadNPCs(3, 400, 400, 300, 300, Color.Green);

            foreach (NPC B in Batches)
            {
                B.Load(Content);
            }

            //map
            map.Load(Content);

            //debugging
            debugMap = Content.Load<Texture2D>("maria");
        }


        private void UpdatePlayerNPCCollisions(float deltaTime)
        {
            foreach (NPC B in Batches)
            {
                foreach (Enemy E in B.NPCs)
                {
                    if(IsCollidingRectangle(E.Bounds, player.Bounds) == true)
                    {

                        player.direction = -player.direction;
                        player.velocity = player.direction * 200;

                        E.idleTimer = random.Next(2, 7);
                        E.hasTargetPosition = false;
                        E.velocity = Vector2.Zero;
                        E.targetRectangle = Rectangle.Empty;

                    }
                }
            }
        }

       

        private void UpdateCollisions(float deltaTime)
        {
            UpdatePlayerNPCCollisions(deltaTime);
        }
        

        public override void Update(ContentManager Content, GameTime gameTime)
        {

            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (isLoaded == false)
            {
                isLoaded = true;
                Load(Content, gameTime);
            }

            player.Update(deltaTime);
            foreach(NPC B in Batches)
            {
                B.Update(deltaTime);
            }

            UpdateCollisions(deltaTime);

            Game1.current.camera.Position = player.Position - new Vector2(Game1.current.ScreenWidth, Game1.current.ScreenHeight);


            //if (Keyboard.GetState().IsKeyDown(Keys.Enter) == true)
            //{
            //    AIE.StateManager.ChangeState("GAMEOVER");
            //}

        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            var t = Game1.current.camera.GetViewMatrix();
            
            spriteBatch.Begin(transformMatrix : t);
            //spriteBatch.DrawString(font, "Game State", new Vector2(200, 200), Color.White);
            spriteBatch.Draw(debugMap, new Vector2(0, 0), Color.White);

            map.Draw(spriteBatch);

            player.Draw(spriteBatch);

            foreach(NPC B in Batches)
            {
                B.Draw(spriteBatch);
            }


            spriteBatch.End();

            //GUI below
            spriteBatch.Begin();

            //player health bar
            spriteBatch.Draw(generic16, new Vector2(50, 50), null, null, Vector2.Zero, 0f, new Vector2((int)(player.maxHealth / 10), 1), Color.Black, SpriteEffects.None, 0f);
            spriteBatch.Draw(generic16, new Vector2(50, 50), null, null, Vector2.Zero, 0f, new Vector2((int)(player.health / 10), 1), Color.Red, SpriteEffects.None, 0f);
                    
            spriteBatch.End();
        }

        public override void CleanUp()
        {
            font = null;
            isLoaded = false;
        }

        public bool IsCollidingCircle(Vector2 position1, float radius1, Vector2 position2, float radius2)
        {
            Vector2 distance = position2 - position1;

            if (distance.Length() < radius1 + radius2)
            {
                return true;
            }
            return false;
        }

        public bool IsCollidingRectangle(Rectangle rect1, Rectangle rect2)
        {
            if (rect1.X + rect1.Width < rect2.X ||
                rect1.X > rect2.X + rect2.Width ||
                rect1.Y + rect1.Height < rect2.Y ||
                rect1.Y > rect2.Y + rect2.Height)
            {
                
                return false;
            }
            return true;
        }
    }
}
