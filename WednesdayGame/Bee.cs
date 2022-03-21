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
    class Bee : IEnemy
    {
        public Texture2D EnemySprite { get; set; }
        public int Speed { get; set; } = 180;
        public int Radius { get; set; }
        public bool OffScreen { get; set; }
        public ContentManager ContManager { get; set; }
        public AnimatedSprite Anim { get; set; }
        public GraphicsDeviceManager GDM { get; set; }
        public int Type { get; set; } = 2;
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
        public Bee()
        {

        }

        public Bee(int newSpeed, GraphicsDeviceManager _graphics, ContentManager cont)
        {
            Speed = newSpeed;
            GDM = _graphics;
            Position = new Vector2(_graphics.PreferredBackBufferWidth + Radius, IEnemy.rand.Next(0, _graphics.PreferredBackBufferHeight));
            ContManager = cont;
            EnemySprite = ContManager.Load<Texture2D>("bee");
            Radius = EnemySprite.Width / 2;
            Anim = new AnimatedSprite(EnemySprite, 1, 12);
        }

        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.X -= Speed * dt;
            Anim.Update(gameTime);

            if (Position.X < (0 - Radius * 2))
            {
                OffScreen = true;
            }
        }
    }
}
