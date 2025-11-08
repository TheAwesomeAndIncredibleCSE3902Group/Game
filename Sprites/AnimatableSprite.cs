
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AwesomeRPG.Sprites;

public class AnimatableSprite : ISprite
{
    // For GameTime, there are 10 ticks per microsecond.
    // So, there are 10000 ticks in a millisecond.
    private const ulong TICKS_IN_ONE_MILLISECOND = 10000ul;

    private readonly float _globalScale;

    private readonly Texture2D _texture;
    private readonly Rectangle[] _sourceList;
    private readonly Vector2[] _offsetList;
    private readonly uint _numberOfFrames;
    private ulong _elapsedTicksOnCurrentFrame = 0;

    private readonly SpriteBatch _spriteBatch;
    private ulong _ticksBetweenFrames;
    private uint _currentFrame = 0;

    public int Width { get; private set; }
    public int Height { get; private set; }

    public uint CurrentFrame
    {
        get
        {
            return _currentFrame;
        }
        set
        {
            _currentFrame = value % _numberOfFrames;
        }
    }

    public ulong MillisecondsBetweenFrames
    {
        get
        {
            return _ticksBetweenFrames / TICKS_IN_ONE_MILLISECOND;
        }
        set
        {
            _ticksBetweenFrames = value * TICKS_IN_ONE_MILLISECOND;
        }
    }

    

    /// <summary>
    /// Creates a static (non-moving) sprite. AnimationSpeed has no effect.
    /// </summary>
    /// <param name="spriteBatch">The SpriteBatch to draw the sprite to</param>
    /// <param name="texture">The Texture2D that should be used for the sprite</param>
    /// <param name="spriteSheetStaticSource">The position and size of the frame on the source texture.</param>
    public AnimatableSprite(SpriteBatch spriteBatch, Texture2D texture, Rectangle spriteSheetStaticSource)
    {
        _spriteBatch = spriteBatch;
        _texture = texture;
        _sourceList = [spriteSheetStaticSource];
        _offsetList = new Vector2[1]; // offset is set to 0.0f, 0.0f
        _numberOfFrames = 1;
        MillisecondsBetweenFrames = 0;
        _globalScale = Util.GlobalScale;

        SetWidthNHeight(spriteSheetStaticSource.Width, spriteSheetStaticSource.Height);
    }

    /// <summary>
    /// Constructs an AnimatableSprite using a rectangle as a starting point and a number of frames, (for sprite sheets).
    /// </summary>
    /// <param name="spriteBatch">The SpriteBatch to draw the sprite to.</param>
    /// <param name="texture">The Texture2D that should be used for the sprite.</param>
    /// <param name="spriteSheetInitialFrameSource">The position and size of the initial frame on the source texture.</param>
    /// <param name="numberOfFrames">Number of frames that should be used from the sprite sheet.</param>
    /// <param name="msBetweenFrames">The number of milliseconds between each frame (<= 0 will pause animation)</param>
    /// <param name="gapSize">Optional: gap X and gap Y between each frame on the sprite sheet.</param>
    public AnimatableSprite(SpriteBatch spriteBatch, Texture2D texture, Rectangle spriteSheetInitialFrameSource, uint numberOfFrames, ulong msBetweenFrames, Point? gapSize)
    {
        _spriteBatch = spriteBatch;
        _texture = texture;
        MillisecondsBetweenFrames = msBetweenFrames;
        _numberOfFrames = numberOfFrames;
        _globalScale = Util.GlobalScale;

        _sourceList = new Rectangle[numberOfFrames];
        _offsetList = new Vector2[numberOfFrames];
        Point currentPositionOnSpriteSheet = spriteSheetInitialFrameSource.Location;

        int widthOfTexture = texture.Width;

        for (int i = 0; i < numberOfFrames; i++)
        {
            _sourceList[i] = new Rectangle(currentPositionOnSpriteSheet, spriteSheetInitialFrameSource.Size);

            currentPositionOnSpriteSheet.X += spriteSheetInitialFrameSource.Width;
            if (gapSize.HasValue)
            {
                currentPositionOnSpriteSheet += gapSize.Value;
            }
            if (currentPositionOnSpriteSheet.X >= widthOfTexture)
            {
                currentPositionOnSpriteSheet.X %= widthOfTexture;
                currentPositionOnSpriteSheet.Y += spriteSheetInitialFrameSource.Height;
            }
        }

        SetWidthNHeight(spriteSheetInitialFrameSource.Width, spriteSheetInitialFrameSource.Height);
        //Console.WriteLine(currentPositionOnSpriteSheet);
    }

    /// <summary>
    /// Constructs an AnimatableSprite using a 2D array, (for sprite atlases).<br/><br/>
    /// NOTE: The spriteAtlasSource 2D array must either have 6 columns (X,Y,W,H,offsetX,offsetY) or 4 columns (X,Y,W,H).
    /// </summary>
    /// <param name="spriteBatch">The SpriteBatch to draw the sprite to</param>
    /// <param name="texture">The Texture2D that should be used for the sprite</param>
    /// <param name="spriteAtlasSource">
    /// A 2D array that contains the positions, sizes, and the offset (optional) of each frame on the source texture.<br/><br/>
    /// Each row represents a frame of animation.<br/>
    /// Each column represents a "parameter".<br/><br/>
    /// The array must either have 6 columns (X,Y,W,H,offsetX,offsetY) or 4 columns (X,Y,W,H).<br/><br/>
    /// The offset parameters can be useful in case you need certain frames to be shifted by a certain number of pixels.
    /// </param>
    /// <param name="msBetweenFrames">The number of milliseconds between each frame (<= 0 will pause animation)</param>
    /// <exception cref="System.Exception"></exception>
    public AnimatableSprite(SpriteBatch spriteBatch, Texture2D texture, int[,] spriteAtlasSource, ulong msBetweenFrames)
    {
        _spriteBatch = spriteBatch;
        _texture = texture;
        MillisecondsBetweenFrames = msBetweenFrames;

        int numberOfFrames = spriteAtlasSource.GetLength(0);
        int numberOfParams = spriteAtlasSource.GetLength(1);

        _numberOfFrames = (uint)numberOfFrames;
        _globalScale = Util.GlobalScale;

        _sourceList = new Rectangle[numberOfFrames]; // all source rectangles set to 0,0,0,0
        _offsetList = new Vector2[numberOfFrames];  // all offsets set to 0.0f, 0.0f

        if (numberOfParams == 4 || numberOfParams == 6)
        {
            // Add each position and size of source
            for (int i = 0; i < numberOfFrames; i++)
            {
                _sourceList[i] = new Rectangle(spriteAtlasSource[i, 0], spriteAtlasSource[i, 1], spriteAtlasSource[i, 2], spriteAtlasSource[i, 3]);
            }
            if (numberOfParams == 6)
            {
                // Add each offset
                for (int i = 0; i < numberOfFrames; i++)
                {
                    _offsetList[i] = new Vector2(spriteAtlasSource[i, 4], spriteAtlasSource[i, 5]);
                }
            }
            return;
        }

        //Assumes that the size of each sprite in the atlas is the same
        int width = spriteAtlasSource[0, 2];
        int height = spriteAtlasSource[0, 3];
        SetWidthNHeight(width, height);

        throw new System.Exception("Invalid spriteAtlasSource 2D array! Must either have 6 columns (X,Y,W,H,offsetX,offsetY) or 4 columns (X,Y,W,H)");
    }

    private void updateAnimationFrameAndOffset(GameTime gameTime, ref Vector2 position, float frameScale)
    {
        _elapsedTicksOnCurrentFrame += (ulong)gameTime.ElapsedGameTime.Ticks;
        if (_elapsedTicksOnCurrentFrame >= _ticksBetweenFrames)
        {
            if (_ticksBetweenFrames != 0)
            {
                _elapsedTicksOnCurrentFrame %= _ticksBetweenFrames;
                _currentFrame += 1;
                _currentFrame %= _numberOfFrames;
            }
        }
        position += _offsetList[_currentFrame] * frameScale;
    }

    private void DoDraw(GameTime gameTime, Vector2 position)
    {
        updateAnimationFrameAndOffset(gameTime, ref position, _globalScale);
        _spriteBatch.Draw(_texture, position, _sourceList[_currentFrame], Color.White, 0.0f, Vector2.Zero, _globalScale, SpriteEffects.None, 0.0f);
    }

    /// <summary>
    /// Draw the sprite to its SpriteBatch. 
    /// </summary>
    /// <param name="gameTime">The gameTime inherited from the Game's Draw() method.</param>
    /// <param name="position">The X,Y position to draw the sprite at.</param>
    public void Draw(GameTime gameTime, Vector2 position)
    {
        DoDraw(gameTime, position);
    }

    private void SetWidthNHeight(int width, int height)
    {
        Width = width * (int)Util.GlobalScale;
        Height = height * (int)Util.GlobalScale;
    }

}