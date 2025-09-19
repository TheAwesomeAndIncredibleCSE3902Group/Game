using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Sprint0.Sprites;


public abstract class SpriteBase : ISprite
{
    protected Func<int> GetChosenSprite;
    protected class SourceRectangle
    {
        public SourceRectangle(int posX, int posY, int sizeX, int sizeY, int offsetX, int offsetY)
        {
            PositionAndSize = new Rectangle(posX, posY, sizeX, sizeY);
            Offset = new Point(offsetX, offsetY);
        }
        public Rectangle PositionAndSize { get; set; }
        public Point Offset { get; set; }
    }
    protected double _animationPosition;
    protected int _animationLength;
    protected Point _position = new();
    protected bool IsFlippedHorizontal { get; set; }
    private SpriteBatch _spriteBatch;
    private Texture2D _spriteTexture;
    private SourceRectangle[] _sourceRectangleArray;
    private Rectangle _calculatedPositionAndSize = new();
    public Point Position
    {
        get
        {
            return _position;
        }
        set
        {
            _position.X = value.X;
            _position.Y = value.Y;
        }
    }
    public Vector2 Scale { get; set; }
    public double AnimationSpeed { get; set; }
    public bool Enabled { get; set; }

    protected void SetUpSprite(SpriteBatch spriteBatch, Texture2D spriteTexture, Point initialPosition, Vector2 initialScale, SourceRectangle[] sourceRectangleArray, double animationSpeed)
    {
        _spriteBatch = spriteBatch;
        _spriteTexture = spriteTexture;
        Position = initialPosition;
        Scale = initialScale;
        _sourceRectangleArray = sourceRectangleArray;
        _animationPosition = 0;
        _animationLength = _sourceRectangleArray.Length;
        AnimationSpeed = animationSpeed;
    }
    // To be implemented in each sprite class
    public abstract void Update(GameTime gameTime);
    public void Draw(GameTime gameTime)
    {
        if (Enabled)
        {
            if (AnimationSpeed != 0)
            {
                _animationPosition += gameTime.ElapsedGameTime.TotalMilliseconds / AnimationSpeed;
            }
            _animationPosition %= _animationLength * 1000;
            int indexOfSourceRectangleArray = (int)(_animationPosition / 1000);

            // Console.WriteLine(_animationPosition);
            // Console.WriteLine(_animationLength);

            _calculatedPositionAndSize.X = Position.X + (int)(_sourceRectangleArray[indexOfSourceRectangleArray].Offset.X * Scale.X);
            _calculatedPositionAndSize.Y = Position.Y + (int)(_sourceRectangleArray[indexOfSourceRectangleArray].Offset.Y * Scale.Y);
            _calculatedPositionAndSize.Width = (int)(_sourceRectangleArray[indexOfSourceRectangleArray].PositionAndSize.Width * Scale.X);
            _calculatedPositionAndSize.Height = (int)(_sourceRectangleArray[indexOfSourceRectangleArray].PositionAndSize.Height * Scale.Y);

            _spriteBatch.Draw(
                _spriteTexture,
                _calculatedPositionAndSize,
                _sourceRectangleArray[indexOfSourceRectangleArray].PositionAndSize,
                Color.White,
                0,
                Vector2.Zero,
                IsFlippedHorizontal ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                1
            );
        }
    }
}
