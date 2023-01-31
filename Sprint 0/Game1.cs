using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Net;

namespace Sprint_0
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Input _luigi;
        private Text _text;
        SpriteFont font;
        Vector2 position = new Vector2(200, 300);
        string text = "Credits\nProgram Made By:Zhuozi Xie\nSprites from:https://www.mariomayhem.com";

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var texture = Content.Load<Texture2D>("luigiAim");
            _luigi = new Input(texture);                          //load the picture of luigi
            font = Content.Load<SpriteFont>("font");
            _text = new Text(text, position, font);               // load the text
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Game1 game = this;
            _luigi.Update(game, gameTime);       // update every action from mouse and keyboard
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            _spriteBatch.Begin();
            _luigi.Draw(_spriteBatch);          //draw the luigi on the screen
            _text.Draw(_spriteBatch);           // draw the text on the screen
            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}