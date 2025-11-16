// Originally written by Eli L
// If you need help with working on UI, feel free to ask me!!!

using AwesomeRPG.Sprites;
using AwesomeRPG.UI.Events;
using Microsoft.Xna.Framework;

namespace AwesomeRPG.UI.Elements;

class AnimSpriteElement : ElementBase
{
    private AnimatableSprite _associatedAnimSprite;
    public AnimatableSprite AssociatedAnimSprite { 
        get
        {
            return _associatedAnimSprite;
        } set
        {
            _associatedAnimSprite = value;
            _associatedAnimSprite.SetWidthNHeight(OffsetAndSize.Width, OffsetAndSize.Height);
        } 
    }
    public override void Draw(GameTime gameTime)
    {
        CalculateDerivedValuesFromAncestors();
        DispatchUIEvent(UIEvent.BeforeDraw, new DrawUIEventParams(this, gameTime));

        AssociatedAnimSprite.Draw(gameTime, DerivedAbsolutePosition.ToVector2());

        DrawChildren(gameTime);
        DispatchUIEvent(AwesomeRPG.UI.UIEvent.AfterDraw, new DrawUIEventParams(this, gameTime));

    }
    
    public AnimSpriteElement(RootElement rootElement, AnimatableSprite animSprite)
    {
        SetUpElement(rootElement);
        AssociatedAnimSprite = animSprite;
        OffsetAndSize = new Rectangle(
            OffsetAndSize.X,
            OffsetAndSize.Y,
            animSprite.Width / (int)Util.GlobalScale,
            animSprite.Height / (int)Util.GlobalScale
        );
        _associatedAnimSprite.SetWidthNHeight(OffsetAndSize.Width, OffsetAndSize.Height);
    }
}