using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace WednesdayGame
{
    public enum EnemyState
    {
        Fly,
        Beetle,
        Bee
    }
    public class Controller
    {
        public List<IEnemy> enemies = new List<IEnemy>();

        public List<Tongue> tongues = new List<Tongue>();
        Vector2 defTongue;
        public Texture2D tongueStartSprite;
        public Texture2D tongueEndSprite;
        float Rotation { get; set; }

        MouseState mState;
        Vector2 mPosition;
        //Vector2 mPostionOld;

        public double timer = 2D;
        public double maxTime = 2D;
        public int nextSpeed = 240;

        public bool inGame = false;
        public float totalTime = 0f;

        private GraphicsDeviceManager gdm;
        private ContentManager contrContent;
        //private EnemyState eState;
        private int eID;
        public Controller(GraphicsDeviceManager _graphics, ContentManager cont)
        {
            gdm = _graphics;
            contrContent = cont;
            defTongue = new Vector2(_graphics.PreferredBackBufferWidth/2 - 25, _graphics.PreferredBackBufferHeight/2 + 20);
            tongueStartSprite = cont.Load<Texture2D>("tstart");
            tongueEndSprite = cont.Load<Texture2D>("tend");
        }

        public void Update(GameTime gameTime)
        {


            mState = Mouse.GetState();
            mPosition = mState.Position.ToVector2();
            tongues.Clear();
            if (mState.LeftButton == ButtonState.Pressed || mState.RightButton == ButtonState.Pressed)
            {
                //find rotation
                Vector2 dPos = mPosition - defTongue;
                Rotation = (float)Math.Atan2(dPos.Y, dPos.X);
                Vector2 delta = mPosition - defTongue;
                float distance = delta.Length();
                //add + 1 to nerf int cast
                int numOfElems = (int)(distance / tongueEndSprite.Width) + 1;
                Vector2 direction = delta / distance;
                for (int i = 1; i <= numOfElems; i++)
                {
                    Vector2 pos = defTongue + direction * (distance / numOfElems * i);
                    if (i == numOfElems)
                    {
                        tongues.Add(new Tongue(pos, Rotation, tongueEndSprite, contrContent));
                    }
                    else
                    {
                        tongues.Add(new Tongue(pos, Rotation,tongueStartSprite, contrContent));
                    }
                    
                }
            }
            else if (mState.LeftButton == ButtonState.Released && mState.RightButton == ButtonState.Released)
            {
                tongues.Clear();
            }

            //mPostionOld = mPosition;
            /////////
            if (inGame)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
                totalTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                KeyboardState kState = Keyboard.GetState();
                if (kState.IsKeyDown(Keys.Enter))
                {
                    inGame = true;
                    totalTime = 0f;
                    timer = 2D;
                    maxTime = 2D;
                    nextSpeed = 240;
                }
            }

            if (timer <= 0)
            {
                eID = IEnemy.rand.Next(0, 3);
                switch (eID)
                {
                    case 0:
                        enemies.Add(new Fly(nextSpeed, gdm, contrContent));
                        break;
                    case 1:
                        enemies.Add(new Beetle(nextSpeed, gdm, contrContent));
                        break;
                    case 2:
                        enemies.Add(new Bee(nextSpeed, gdm, contrContent));
                        break;
                }
                //enemies.Add(new Fly(nextSpeed, gdm, contrContent));
                timer = maxTime;
                if (timer >= 0.5)
                {
                    maxTime -= 0.1D;
                }
                if (nextSpeed < 650)
                {
                    nextSpeed++;
                }

            }
        }
    }
}
