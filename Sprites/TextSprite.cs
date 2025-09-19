using System;
using System.Diagnostics.Metrics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.Sprites;

public class TextSprite : ISprite
{
    public Point Position { get
        {
            return _position.ToPoint();
        }
        set
        {
            _position = value.ToVector2();
            _positionOffset = value.ToVector2();
            Vector2 measurement = _spriteFont.MeasureString(_text);
            // _positionOffset.X -= measurement.Length() / 2;
        }
    }
    public Vector2 Scale { get; set; }
    public bool Enabled { get; set; }
    public double AnimationSpeed { get; set; }
    private Vector2 _position;
    private Vector2 _positionOffset;
    private SpriteBatch _spriteBatch;
    private SpriteFont _spriteFont;
    private String _text;
    private Color _color;
    public TextSprite(SpriteBatch spriteBatch, SpriteFont spriteFont, String text, int initPosX, int initPosY, Color color)
    {
        _spriteBatch = spriteBatch;
        _spriteFont = spriteFont;
        _text = text;
        Position = new Point(initPosX, initPosY);
        _color = color;
    }
    public void Update(GameTime gameTime) {}
    public void Draw(GameTime gameTime)
    {
        _spriteBatch.DrawString(_spriteFont, _text, _positionOffset, _color);
    }
}
