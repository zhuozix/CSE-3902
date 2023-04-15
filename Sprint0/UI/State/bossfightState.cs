using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint0.Collision;
using Sprint0.Content;
using Sprint0.Controller;
using Sprint0.ObjectManager;
using Sprint0.Sounds;
using Sprint0.UI.Title;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.UI.State
{
    internal class bossfightState : IGameState
    {
        private readonly Game1 _game;

        public bossfightState(Game1 game)
        {
            _game = game;
        }

        public void Update(GameTime gameTime)
        {
            _game.keyboardController.UpdateInput();
            _game.MouseController.UpdateInput();


            _game.Collision.Update(gameTime);
            if (!_game.IsPaused)
            {
                _game.gameObjectManager.update(gameTime);
                _game.UI.Update(gameTime);
                _game.camera.MoveCamera(_game.mario);
            }

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _game.GraphicsDevice.Clear(Color.CornflowerBlue);
            Matrix combinedMatrix = Matrix.CreateTranslation(0, 100, 0) * _game.camera.Transform;
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, combinedMatrix);

            _game.gameObjectManager.Draw(spriteBatch, true);

            spriteBatch.End();
            _game.UI.Draw();
        }

        public void Enter()
        {
            Initialize();

            _game.camera = new Camera();

            //Load boss level
            LevelLoader.LevelLoader.loadLevel(_game.gameObjectManager, "bossLevel.xml", _game.spritesFactory, _game);

            SoundPlayer.loadSounds(_game);
            SoundPlayer.playBossTheme();

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
