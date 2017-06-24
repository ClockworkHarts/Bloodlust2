﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Bloodlust2
{
    public class Weapon
    {
        //NOTE to set the size of the weapons bounds, sprite.scale needs to be used


        Sprite sprite = new Sprite();

        //Vectors
        public Vector2 Position
        {
            get { return sprite.position; }
            set { sprite.position = value; }
        }


        //once a good size for the different weapon types is determined
        //this set function needs to be changed to a switch function
        //that loads a scale based on the weapon's type
        //as all of them will be the same 
        public Vector2 Scale
        {
            get { return sprite.scale; }
            set { sprite.scale = value; }
        }

        //Ints

        //Floats
        public float attackSpeed;
        public float damage;

        //Rectangles
        public Rectangle Bounds
        {
            get { return sprite.Bounds; }
        }

        //General
        public WeaponType type;

        //Textures
        
        public void Load(ContentManager Content)
        {
            AnimatedTexture animation = new AnimatedTexture(Vector2.Zero, 0f, Scale, 0f);
            switch (type)
            {
                case WeaponType.Dagger:
                    animation.Load(Content, "Tile16", 1, 0);
                    sprite.colour = Color.Cyan;
                    break;

                case WeaponType.Sword:
                    animation.Load(Content, "Tile16", 1, 0);
                    sprite.colour = Color.DarkViolet;
                    break;

                case WeaponType.Spear:
                    animation.Load(Content, "Tile16", 1, 0);
                    sprite.colour = Color.DeepPink;
                    break;

                case WeaponType.Unarmed:
                    animation.Load(Content, "Tile16", 1, 0);
                    sprite.colour = Color.Transparent;
                    break;
            }
            sprite.Add(animation, 0, 0);

        }

        public void Update(float deltaTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
        }
        
       
        


    }
}
