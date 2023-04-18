using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0;
using Sprint0.Factory;
using Sprint0.Sprites;
using Sprint0.UI.Text;
using System.Collections.Generic;

public class UserInterface
{
    private Game1 _game;
    private SpritesFactory _spritesFactory;

    private ISprite coinText;
    private ISprite lifeText;
    private ISprite timeText;
    public List<ISprite> textList;
    private Texture2D _blackBar;
    //boss fight only
    private ISprite moonJumpText;
    private ISprite MGunText;
    private ISprite bossHpText;
    public List<ISprite> bossTextList;

    public UserInterface(Game1 game)
    {
        _game = game;
        _spritesFactory = game.spritesFactory;
        this.textList = new List<ISprite>();
        this.bossTextList = new List<ISprite>();
        getAllText();
        LoadContent();
    }

    public void getAllText()
    {
        coinText = _spritesFactory.getCoinFontSprite();
        lifeText = _spritesFactory.getLifeFontSprite();
        timeText = _spritesFactory.getTimeFontSprite();
        this.textList.Add(coinText);
        this.textList.Add(lifeText);
        this.textList.Add(timeText);
        //boss fight level
        moonJumpText = _spritesFactory.getMoonJumpFontSprite();
        MGunText = _spritesFactory.getGunFontSprite();
        this.bossTextList.Add(moonJumpText);
        this.bossTextList.Add(MGunText);
        //during boss fight only
        bossHpText = _spritesFactory.getBossFontSprite();
    }

    private void LoadContent()
    {

        _blackBar = new Texture2D(_game.GraphicsDevice, 1, 1);
        _blackBar.SetData(new[] { Color.Black });
    }

    public void Draw()
    {
        _game._uiSpriteBatch.Begin();
        _game._uiSpriteBatch.Draw(_blackBar, new Rectangle(0, 0, 800, 100), Color.Black);
        foreach (ISprite obj in this.textList)
        {
            obj.Draw(_game._uiSpriteBatch, false);
        }
        //boss level only
        if (_game.gameObjectManager.isBossFight)
        {
            foreach (ISprite obj in this.bossTextList)
            {
              obj.Draw(_game._uiSpriteBatch, false);
            }
            //during boss fight
            if (_game.bowser.activated && _game.bossHP > 0)
            {
                bossHpText.Draw(_game._uiSpriteBatch, false);
            }
        }
        

        _game._uiSpriteBatch.End();
    }
    public void Update(GameTime time) 
    {
        foreach (ISprite obj in this.textList)
        {
            obj.Update(time);
        }
        if (_game.gameObjectManager.isBossFight)
        {
            foreach (ISprite obj in this.bossTextList)
            {
                obj.Update(time);
            }
            if (_game.bowser.activated && _game.bossHP > 0)
            {
                bossHpText.Update(time);
            }
        }
        
    }
}