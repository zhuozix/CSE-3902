using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.Content;
using Sprint0.Controller;
using Sprint0.MarioPlayer;
using Sprint0.ObjectManager;
using Sprint0.UI.Title;
using Sprint0.Factory;
using Sprint0.Sounds;
using Sprint0.Collision;

namespace Sprint0.UI.State
{
    internal class PlayState: IGameState
    {
        private readonly Game1 _game;

        public PlayState(Game1 game)
        {
            _game = game;
        }

        public void Update(GameTime gameTime)
        {
            _game.keyboardController.UpdateInput();
            _game.MouseController.UpdateInput();

            _game.Collision.Update(gameTime);

            _game.gameObjectManager.update(gameTime);

            _game.camera.MoveCamera(_game.mario);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _game.GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(transformMatrix: _game.camera.Transform);

            _game.gameObjectManager.Draw(spriteBatch, true);
            spriteBatch.End();
        }

        public void Enter()
        {
            Initialize();

            _game.camera = new Camera();

            //Load level 1
            LevelLoader.LevelLoader.loadLevel(_game.gameObjectManager, "level1-1.xml", _game.spritesFactory, _game);

            SoundPlayer.loadSounds(_game);
            SoundPlayer.playMainTheme();

            _game.gameObjectManager.addObject(_game.mario, "player");
            //Load commands to controller
            _game.keyboardController.loadCommonCommand();
            _game.MouseController.loadCommonCommand();
        }

        public void Initialize()
        {
            //controllers
            _game.keyboardController = new KeyboardController(_game);
            _game.MouseController = new MouseController(_game);

            //Game object manager
            _game.gameObjectManager = new GameObjectManager(_game);

            _game.Collision = new Collide(_game);
        }

        public void Exit()
        {
            SoundPlayer.stopMusic();
        }
    }
}
