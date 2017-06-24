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
    public class Player
    {
        Sprite sprite = new Sprite();

        //debugging dem buggos
        public bool isPressed = false;

        //Vectors
        public Vector2 velocity = Vector2.Zero;
        public Vector2 direction = Vector2.Zero;
        public Vector2 scale = new Vector2(1, 1);
        public Vector2 Position
        {
            set { sprite.position = value; }
            get { return sprite.position; }
        }

        //Floats
        public float health;
        public float maxHealth;
        public float Radius()
        {
            float radius = Math.Min(Bounds.Height, Bounds.Width);
            return radius;
        }

        //Bools
        public bool isAttacking = false;


        //Rectangles
        public Rectangle Bounds
        {
            get { return sprite.Bounds; }
        }

        //General
        public PlayerWeapon equipType = PlayerWeapon.Unarmed;

        

        public void Load(ContentManager Content)
        {
            AnimatedTexture animation = new AnimatedTexture(Vector2.Zero, 0, scale, 1);
            animation.Load(Content, "tile32", 1, 1);
            sprite.Add(animation, 0, 0);
            sprite.colour = Color.Red;
        }

        

        public void Update(float deltaTime)
        {
            UpdateInput(deltaTime);
            sprite.Update(deltaTime);
        }

        private void UpdateCombat(float deltaTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) == true && isPressed == false)
            {
                isPressed = true;
                health -= 10;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Enter) == true)
            {
                isPressed = false;
            }
        }

        private void UpdateMotion(float deltaTime)
        {
            bool wasMovingRight = velocity.X > 0;
            bool wasMovingLeft = velocity.X < 0;
            bool wasMovingUp = velocity.Y < 0;
            bool wasMovingDown = velocity.Y > 0;

            Vector2 acceleration = Vector2.Zero;

            if (Keyboard.GetState().IsKeyDown(Keys.W) == true)
            {
                acceleration.Y = -GameState.acceleration;
                //add in some code to animated texture and sprite to allow for vertical flipping
                direction.Y = -1;

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S) == true)
            {
                acceleration.Y = GameState.acceleration;
                // add in some code for vertical flipping
                direction.Y = 1;
            }
            else if (wasMovingUp == true)
            {
                acceleration.Y = GameState.friction;
                direction.Y = 0;
            }
            else if (wasMovingDown == true)
            {
                acceleration.Y = -GameState.friction;
                direction.Y = 0;
            }


            if (Keyboard.GetState().IsKeyDown(Keys.A) == true)
            {
                acceleration.X = -GameState.acceleration;
                sprite.SetFlipped(true);
                direction.X = -1;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D) == true)
            {
                acceleration.X = GameState.acceleration;
                sprite.SetFlipped(false);
                direction.X = 1;
            }
            else if (wasMovingLeft == true)
            {
                acceleration.X = GameState.friction;
                direction.X = 0;
            }
            else if (wasMovingRight == true)
            {
                acceleration.X = -GameState.friction;
                direction.X = 0;
            }

            velocity += acceleration * deltaTime;

            velocity.X = MathHelper.Clamp(velocity.X, -GameState.maxVelocity.X, GameState.maxVelocity.X);
            velocity.Y = MathHelper.Clamp(velocity.Y, -GameState.maxVelocity.Y, GameState.maxVelocity.Y);

            Position += velocity * deltaTime;

            if ((wasMovingLeft && (velocity.X > 0)) || (wasMovingRight && (velocity.X < 0)))
            {
                velocity.X = 0;
            }

            if ((wasMovingUp && (velocity.Y > 0)) || (wasMovingDown && (velocity.Y < 0)))
            {
                velocity.Y = 0;
            }
        }

        private void UpdateInput(float deltaTime)
        {
            UpdateMotion(deltaTime);
            UpdateCombat(deltaTime);
        }

       public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
        }
    
     }     
    

}

    

