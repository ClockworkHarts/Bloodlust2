﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Bloodlust2
{
    public class Sprite
    {
        public Vector2 position;
        public Vector2 offset;
        public Color colour;
        public Vector2 scale;

        Texture2D texture;

        List<AnimatedTexture> animations = new List<AnimatedTexture>();
        List<Vector2> animationOffsets = new List<Vector2>();

        int currentAnimation = 0;

        SpriteEffects effects = SpriteEffects.None;

        public Sprite()
        {
            position = Vector2.Zero;
            offset = Vector2.Zero;
            colour = Color.White;
            scale = new Vector2(1, 1);
        }

        public void Load(ContentManager content, string asset)
        {
            texture = content.Load<Texture2D>(asset);
        }

        public void Add(AnimatedTexture animation, int xOffset=0, int yOffset = 0)
        {
            animations.Add(animation);
            animationOffsets.Add(new Vector2(xOffset, yOffset));
        }

        public void Clear()
        {
            animations.Clear();
            animationOffsets.Clear();
        }

        public void Update(float deltaTime)
        {
            animations[currentAnimation].UpdateFrame(deltaTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            animations[currentAnimation].DrawFrame(spriteBatch, position + animationOffsets[currentAnimation], colour, effects);
        }

        public void SetFlipped(bool state)
        {
            if (state == true)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                effects = SpriteEffects.None;
            }
        }

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, (int)(animations[currentAnimation].FrameSize.X * scale.X), (int)(animations[currentAnimation].FrameSize.Y * scale.Y));
            }
        }

        public void Pause()
        {
            animations[currentAnimation].Pause();
        }

        public void Play()
        {
            animations[currentAnimation].Play();
        }


    }
}
