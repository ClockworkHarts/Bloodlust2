using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Bloodlust2
{
    public class NPC
    {

        public List<Enemy> NPCs = new List<Enemy>();
        public int batchNumber;                       // will be used to specify how many NPCs are spawned in this location batch
        public Color colour;
        Random random = new Random();
        Rectangle NPCLocation = new Rectangle();
        
        


        //will create a NPC Location rectangle in which NPCs reside when not patrolling
        public void CreateNPCLocation(int OriginX, int OriginY, int Width, int Height)
        {

            //much better code can be written here once the map is working
            //which will set the npc rectangle  
            Rectangle rect = new Rectangle(OriginX, OriginY, Width, Height);
            NPCLocation = rect;
        }

        //return a random location within the NPC Location rectangle
        private Vector2 NPCPosition()
        {
            int PositionX = random.Next(NPCLocation.X, NPCLocation.X + NPCLocation.Width);
            int PositionY = random.Next(NPCLocation.Y, NPCLocation.Y + NPCLocation.Height);

            return new Vector2(PositionX, PositionY);
        }

        //Spawns an enemy and adds to list of enemies call NPCs
        private void SpawnEnemy(ContentManager Content)
        {
            Enemy enemy = new Enemy();
            enemy.Load(Content, "Tile32", 1, 0);
            enemy.Position = NPCPosition();
            enemy.sprite.colour = colour;
            enemy.speed = random.Next(30, 80);
            enemy.state = EnemyState.Idle;
            NPCs.Add(enemy);
        }
      

        public void Load(ContentManager Content)
        {
            
            for (int NPCBatchIdx = 0; NPCBatchIdx < batchNumber; NPCBatchIdx++)
            {
                SpawnEnemy(Content);
            }

        }


        private void IdleTimer(float deltaTime, Enemy NPC)
        {
                if (NPC.idleTimer > 0)
                {
                    NPC.idleTimer -= deltaTime;
                }
                
                if (NPC.idleTimer < 0)
                {
                    NPC.idleTimer = 0;
                }
        }

        private bool CheckCollisionsIdle(Rectangle target, Rectangle bounds)
        {
            if (GameState.current.IsCollidingRectangle(target, bounds) == true)
            {
                return true;
            }

            return false;
        }


        public void UpdateIdle(float deltaTime, Enemy NPC) 
        {
                if (NPC.hasTargetPosition == false && NPC.idleTimer == 0)
                {
                    NPC.targetPosition = new Vector2((random.Next((NPCLocation.X), (NPCLocation.X + NPCLocation.Width))), (random.Next((NPCLocation.Y), (NPCLocation.Y + NPCLocation.Height))));
                    NPC.hasTargetPosition = true;
                }

                if(NPC.hasTargetPosition == true)
                {
                    NPC.targetRectangle = new Rectangle((int)NPC.targetPosition.X - 16, (int)NPC.targetPosition.Y - 16, 32, 32);  //minus 16 for offset
                    NPC.velocity = NPC.targetPosition - NPC.Position;
                    NPC.velocity.Normalize();
                    NPC.Position += NPC.velocity * NPC.speed * deltaTime;


                    if (CheckCollisionsIdle(NPC.targetRectangle, NPC.Bounds) == true)
                    {
                        NPC.idleTimer = random.Next(2, 7);
                        NPC.hasTargetPosition = false;
                        NPC.velocity = Vector2.Zero;
                        NPC.targetRectangle = Rectangle.Empty;
                    }
                }
        }

        public void UpdatePatrol()
        {

        }

        public void UpdateAttack()
        {

        }

        public void UpdateSleep()
        {

        }

        public void Update(float deltaTime)
        {

            foreach (Enemy NPC in NPCs)
            {
                if (NPC.state == EnemyState.Idle)
                {
                    UpdateIdle(deltaTime, NPC);
                    IdleTimer(deltaTime, NPC);
                }
            }
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy NPC in NPCs)
            {
                NPC.Draw(spriteBatch);
            }
        }
    }
}
