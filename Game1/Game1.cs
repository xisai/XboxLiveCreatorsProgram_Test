using Microsoft.Xbox.Services.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;

        XboxAccount account;
        string signInStatusStr;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            signInStatusStr = "";

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            font = this.Content.Load<SpriteFont>("Font");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                if (account == null)
                {
                    LoginXboxLiveAccount();
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            spriteBatch.DrawString(font, signInStatusStr, new Vector2(50, 50), Color.White);

            if (account != null && account.IsSignedIn())
            {
                spriteBatch.DrawString(font, account.Gamertag(), new Vector2(50, 100), Color.White);
            }
            else
            {
                spriteBatch.DrawString(font, "Press Enter", new Vector2(50, 100), Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void LoginXboxLiveAccount()
        {
            if (account == null)
            {
                account = new XboxAccount();
            }

            account.LoginAsync().ContinueWith(acc =>
            {
                if (acc.Result == SignInStatus.Success)
                {
                    signInStatusStr = "Success";
                }
                else if (acc.Result == SignInStatus.UserCancel)
                {
                    signInStatusStr = "UserCancel";
                }
                else if (acc.Result == SignInStatus.UserInteractionRequired)
                {
                    signInStatusStr = "UserInteractionRequired";
                }
            });
        }
    }
}