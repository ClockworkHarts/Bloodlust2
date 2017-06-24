using System;
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
    class Weapon
    {
        //NOTE to set the size of the weapons bounds, sprite.scale needs to be used


        Sprite sprite = new Sprite();

        //Vectors
        public Vector2 Position
        {
            get { return sprite.position; }
            set { sprite.position = value; }
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
            AnimatedTexture animation = new AnimatedTexture(Vector2.Zero, 0f, new Vector2(1, 1), 0f);
            switch (type)
            {
                case WeaponType.Dagger:
                    animation.Load(Content, "ADDTEXTURE", 1, 0);
                    break;

                case WeaponType.Sword:
                    animation.Load(Content, "ADDTEXTURE", 1, 0);
                    break;

                case WeaponType.Spear:
                    animation.Load(Content, "ADDTEXTURE", 1, 0);
                    break;
            }
            sprite.Add(animation, 0, 0);
            sprite.colour = Color.White;
            sprite.scale = new Vector2(1, 1);

        }

        public void Draw()
        {

        }
        
       
        


    }
}
