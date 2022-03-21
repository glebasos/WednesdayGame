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
    public class Fly : IEnemy
    {
        public Texture2D EnemySprite { get; set; }
        public int Speed { get; set; } = 220;
        public int Radius { get; set; }
        public bool OffScreen { get; set; }
        public ContentManager ContManager { get; set; }
        public AnimatedSprite Anim { get; set; }
        public Vector2 position;
        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }
        public int Type { get; set; } = 0;
        public GraphicsDeviceManager GDM { get; set; }
        //static Random rand = new Random();
        public Fly()
        {

        }

        public Fly(int newSpeed, GraphicsDeviceManager _graphics, ContentManager cont)
        {
            Speed = newSpeed;
            GDM = _graphics;
            Position = new Vector2(0 - Radius, IEnemy.rand.Next(0, _graphics.PreferredBackBufferHeight));
            ContManager = cont;
            EnemySprite = ContManager.Load<Texture2D>("flyfly");
            Radius = EnemySprite.Width / 2;
            Anim = new AnimatedSprite(EnemySprite, 3, 4);
        }

        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.X += Speed * dt;
            Anim.Update(gameTime);

            if (Position.X > (GDM.PreferredBackBufferWidth + Radius * 2))
            {
                OffScreen = true;
            }
        }
    }
}
