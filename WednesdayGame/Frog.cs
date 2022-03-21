using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace WednesdayGame
{
    public enum FrogState
    {
        Idle,
        BigScream,
        SmallScream
    }
    public class Frog
    {
        public AnimatedSprite anim;
        public Vector2 position;
        public FrogState frogState;

        Texture2D frogIdleSprite;
        Texture2D frogSmallSprite;
        Texture2D frogBigSprite;

        public Texture2D currentSprite;

        private SoundEffect bigSE;
        private SoundEffect smallSE;

        SoundEffectInstance smallInstance;
        SoundEffectInstance bigInstance;


        public Frog()
        {
            position = new Vector2(0, 0);
        }
        public Frog(Vector2 startPosition, ContentManager content)
        {
            position = startPosition;

            frogIdleSprite = content.Load<Texture2D>("frog_01");
            frogSmallSprite = content.Load<Texture2D>("frog_02");
            frogBigSprite = content.Load<Texture2D>("frog_03");
            currentSprite = frogIdleSprite;

            smallSE = content.Load<SoundEffect>("smol");
            bigSE = content.Load<SoundEffect>("big");
            smallInstance = smallSE.CreateInstance();
            bigInstance = bigSE.CreateInstance();
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (mouseState.LeftButton == ButtonState.Pressed && smallInstance.State != SoundState.Playing)
            {
                frogState = FrogState.SmallScream;
                smallInstance.Play();
                currentSprite = frogSmallSprite;
            }
            else if(mouseState.RightButton == ButtonState.Pressed && bigInstance.State != SoundState.Playing)
            {
                frogState = FrogState.BigScream;
                bigInstance.Play();
                currentSprite = frogBigSprite;
            }
            else 
            {
                if (mouseState.LeftButton == ButtonState.Released)
                {
                    smallInstance.Stop();
                    //frogState = FrogState.Idle;
                }
                if (mouseState.RightButton == ButtonState.Released)
                {
                    bigInstance.Stop();
                    //frogState = FrogState.Idle;
                }
                if (mouseState.LeftButton == ButtonState.Released && mouseState.RightButton == ButtonState.Released)
                {
                    frogState = FrogState.Idle;
                    currentSprite = frogIdleSprite;
                }

            }

        }
    }
}
