using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WednesdayGame
{
    public class Eye
    {
        public Vector2 position;
        private Vector2 dPos;
        public Texture2D eyeSprite;
        MouseState mState;
        Vector2 mPosition;
        public float rotation = 0;
        public Eye()
        {

        }

        public Eye(Vector2 startPosition, ContentManager content, string spritename)
        {
            position = startPosition;
            eyeSprite = content.Load<Texture2D>(spritename);
        }

        public void Update()
        {
            mState = Mouse.GetState();
            mPosition = mState.Position.ToVector2();
            dPos = position - mPosition;
            rotation = (float)Math.Atan2(dPos.Y, dPos.X);
        }
    }
}
