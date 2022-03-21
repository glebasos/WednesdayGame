using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WednesdayGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Controller gameController;
        Texture2D crosshairSprite;
        MouseState mState;
        Vector2 mPosition;
        public Frog zhaba;
        public Eye lEye;
        public Eye rEye;
        public int hpLeft = 10;
        int score = 0;
        int maxScore = 0;
        SpriteFont frogFont;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();
            zhaba = new Frog(new Vector2(_graphics.PreferredBackBufferWidth/2, _graphics.PreferredBackBufferHeight/2), Content);
            lEye = new Eye(new Vector2(_graphics.PreferredBackBufferWidth / 2 -40, _graphics.PreferredBackBufferHeight / 2 -15), Content, "e2");
            rEye = new Eye(new Vector2(_graphics.PreferredBackBufferWidth / 2 -10, _graphics.PreferredBackBufferHeight / 2 -15), Content, "e2");
            gameController = new Controller(_graphics, Content);
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            crosshairSprite = Content.Load<Texture2D>("crosshairs");
            frogFont = Content.Load<SpriteFont>("font");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            //get mouse position
            mState = Mouse.GetState();
            mPosition = mState.Position.ToVector2();
            //update controller
            gameController.Update(gameTime);
            //update frog logic
            zhaba.Update(gameTime);
            //update eyes
            lEye.Update();
            rEye.Update();
            /////////////////////////////////////////////////////////////////////////////
            //update every fly generated in controller; remove after not visible
            foreach (var f in gameController.enemies)
            {
                if (mState.LeftButton == ButtonState.Pressed)
                {
                    float mouseTargetDist = Vector2.Distance(f.Position, mPosition);
                    if (mouseTargetDist < f.Radius / 2)
                    {

                        f.OffScreen = true;
                        switch (f.Type)
                        {
                            case 0:
                                score++;
                                break;
                            case 1:
                                hpLeft--;
                                break;
                            case 2:
                                hpLeft -= 2;
                                break;
                            default:
                                break;
                        }
                    }
                }
                else if(mState.RightButton == ButtonState.Pressed)
                {
                    float mouseTargetDist = Vector2.Distance(f.Position, mPosition);
                    if (mouseTargetDist < f.Radius / 2)
                    {

                        f.OffScreen = true;
                        switch (f.Type)
                        {
                            case 0:
                                hpLeft--;
                                break;
                            case 1:
                                score++;
                                break;
                            case 2:
                                hpLeft -= 2;
                                break;
                            default:
                                break;
                        }
                    }
                }
                f.Update(gameTime);

            }
            gameController.enemies.RemoveAll(f => f.OffScreen);
            ///////////////////////////////////////////////////////////////////////
            //check hp
            if (hpLeft <= 0)
            {
                gameController.inGame = false;
                gameController.enemies.Clear();
                if (score > maxScore)
                {
                    maxScore = score;
                }
                score = 0;
                hpLeft = 10;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            
            switch (zhaba.frogState)
            {
                case FrogState.Idle:
                    GraphicsDevice.Clear(Color.MediumSpringGreen);
                    break;
                case FrogState.SmallScream:
                    GraphicsDevice.Clear(Color.Blue);
                    break;
                case FrogState.BigScream:
                    GraphicsDevice.Clear(Color.Red);
                    break;
            }


            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            
            _spriteBatch.Draw(zhaba.currentSprite, new Vector2(zhaba.position.X - zhaba.currentSprite.Width / 2, zhaba.position.Y - zhaba.currentSprite.Height / 2), Color.White);
            _spriteBatch.Draw(lEye.eyeSprite,
                    lEye.position,
                    null,
                    Color.White,
                    lEye.rotation,
                    new Vector2(lEye.eyeSprite.Width / 2, lEye.eyeSprite.Height / 2),
                    Vector2.One,
                    SpriteEffects.None,
                    0f);
            _spriteBatch.Draw(rEye.eyeSprite,
                    rEye.position,
                    null,
                    Color.White,
                    lEye.rotation,
                    new Vector2(rEye.eyeSprite.Width / 2, rEye.eyeSprite.Height / 2),
                    Vector2.One,
                    SpriteEffects.None,
                    0f);
            if (!gameController.inGame)
            {
                string message = "Press ENTER to begin...\nEat flies with LMB and beetles with RMB, but try not to catch bees!";
                Vector2 sizeOfText = frogFont.MeasureString(message);
                _spriteBatch.DrawString(frogFont, message, new Vector2(_graphics.PreferredBackBufferWidth / 2 - sizeOfText.X / 2, _graphics.PreferredBackBufferHeight / 2), Color.DeepPink);
            }
            _spriteBatch.End();
            foreach (var f in gameController.enemies)
            {
                Vector2 tempVec = f.Position;
                int tempRadius = f.Radius;
                f.Anim.Draw(_spriteBatch, f.Position);
            }
            
            _spriteBatch.Begin();
            foreach (var t in gameController.tongues)
            {
                _spriteBatch.Draw(t.tongueSprite,
                    t.mPosition,
                    null,
                    Color.White,
                    t.Rotation,
                    new Vector2(t.TongueWidth / 2, t.TongueWidth / 2),
                    Vector2.One,
                    SpriteEffects.None,
                    0f);
            }
            //I dunno why + lol
            _spriteBatch.Draw(crosshairSprite, new Vector2(mPosition.X - crosshairSprite.Width/2, mPosition.Y - crosshairSprite.Height/2), Color.White);
            _spriteBatch.DrawString(frogFont, "Health: " + hpLeft.ToString(), new Vector2(10, 10), Color.DeepPink);
            _spriteBatch.DrawString(frogFont, "Score: " + score.ToString(), new Vector2(10, 35), Color.DeepPink);
            string recordMessage = "Record: " + maxScore.ToString();
            Vector2 sizeOfText2 = frogFont.MeasureString(recordMessage);
            _spriteBatch.DrawString(frogFont, recordMessage, new Vector2(_graphics.PreferredBackBufferWidth - sizeOfText2.X - 10, 10), Color.DeepPink);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
