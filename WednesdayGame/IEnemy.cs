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
    public interface IEnemy
    {
        static Random rand = new Random();
        Texture2D EnemySprite { get; set; }
        Vector2 Position { get; set; }
        int Speed { get; set; }
        int Radius { get; set; }
        bool OffScreen { get; set; }
        ContentManager ContManager { get; set; }
        public AnimatedSprite Anim { get; set; }
        GraphicsDeviceManager GDM { get; set; }
        public int Type { get; set; }
        void Update(GameTime gameTime);
    }
}
