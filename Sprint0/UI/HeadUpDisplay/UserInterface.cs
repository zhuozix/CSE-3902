using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0;
using Sprint0.Factory;
using Sprint0.Sprites;
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

    public UserInterface(Game1 game)
    {
        _game = game;
        _spritesFactory = game.spritesFactory;
        this.textList = new List<ISprite>();
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
        
        _game._uiSpriteBatch.End();
    }
    public void Update(GameTime time) 
    {
        foreach (ISprite obj in this.textList)
        {
            obj.Update(time);
        }
    }
}