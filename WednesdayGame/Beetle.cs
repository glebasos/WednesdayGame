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
    class Beetle : IEnemy
    {
        public Texture2D EnemySprite { get; set; }
        public int Speed { get; set; } = 180;
        public int Radius { get; set; }
        public bool OffScreen { get; set; }
        public ContentManager ContManager { get; set; }
        public AnimatedSprite Anim { get; set; }
        public GraphicsDeviceManager GDM { get; set; }
        public int Type { get; set; } = 1;
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
        //static Random rand = new Random();
        public Beetle()
        {

        }

        public Beetle(int newSpeed, GraphicsDeviceManager _graphics, ContentManager cont)
        {
            Speed = newSpeed;
            GDM = _graphics;
            Position = new Vector2(0 - Radius, IEnemy.rand.Next(0, _graphics.PreferredBackBufferHeight));
            ContManager = cont;
            EnemySprite = ContManager.Load<Texture2D>("beetle");
            Radius = EnemySprite.Width / 2;
            Anim = new AnimatedSprite(EnemySprite, 1, 4);
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
