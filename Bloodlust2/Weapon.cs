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
    public class Weapon
    {
        //NOTE to set the size of the weapons bounds, sprite.scale needs to be used


        Sprite sprite = new Sprite();

        //Vectors
        public Vector2 origin = new Vector2(8,8);
        public Vector2 Position
        {
            get { return sprite.position; }
            set { sprite.position = value; }
        }

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
        public WeaponType oldType;

        //Textures
        


       public void Initialise()
        {
            //sprite = new Sprite();

            switch (type)
            {
                case WeaponType.Unarmed:
                    break;

                case WeaponType.Dagger:
                    Scale = GameState.current.daggerScale;
                    attackSpeed = GameState.current.daggerAttackSpeed;
                    damage = GameState.current.daggerDamage;
                    break;

                case WeaponType.Sword:
                    Scale = GameState.current.swordScale;
                    attackSpeed = GameState.current.swordAttackSpeed;
                    damage = GameState.current.swordDamage;
                    break;

                case WeaponType.Spear:
                    Scale = GameState.current.spearScale;
                    attackSpeed = GameState.current.spearAttackSpeed;
                    damage = GameState.current.spearDamage;
                    break;
            }
        }
        public void Load(ContentManager Content)
        {
            AnimatedTexture animation = new AnimatedTexture(origin, 0f, Scale, 0f);
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
            sprite.Clear();
            sprite.Add(animation, 0, 0);

        }

        public void Update(float deltaTime, ContentManager Content)
        {
            if (type != oldType)
            {
                Initialise();
                Load(Content);
                oldType = type;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
        }
        
       
        


    }
}
