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
    public class Tongue
    {
        MouseState mState;
        public Vector2 mPosition;
        public Texture2D tongueSprite;
        public int TongueWidth {get; set;}
        public float Rotation { get; set; }
        public Tongue(Vector2 pos, float rot, Texture2D tonguesprite, ContentManager cont)
        {
            mPosition = pos;
            tongueSprite = tonguesprite;
            TongueWidth = tongueSprite.Width;
            Rotation = rot;
        }

        void Update(GameTime gametime)
        {
            mState = Mouse.GetState();
            mPosition = mState.Position.ToVector2();
        }
    }
}
