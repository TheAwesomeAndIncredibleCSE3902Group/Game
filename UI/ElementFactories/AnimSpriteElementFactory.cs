// Originally written by Eli L
// If you need help with working on UI, feel free to ask me!!!

using AwesomeRPG.Sprites;
using AwesomeRPG.UI.Events;
using Microsoft.Xna.Framework;

namespace AwesomeRPG.UI.ElementFactories;

public class AnimSpriteElementFactory : IElementFactory
{
    private RootElement RootElem { get; set; }

    public AnimSpriteElementFactory(RootElement rootElement)
    {
        RootElem = rootElement;
    }

    public Element CreateNew()
    {
        return CreateNew(null);
    }

    public Element CreateNew(AnimatableSprite animSprite)
    {
        var elem = new Element(RootElem);

        elem.Attributes["associated_anim_sprite"] = animSprite;

        elem.AddActionOnUIEvent(UIEvent.Draw, (e) =>
        {
            if (elem.Attributes.TryGetValue("associated_anim_sprite", out var spriteObj) && spriteObj is AnimatableSprite sprite)
            {
                sprite.Draw(e is DrawUIEventParams drawParams ? drawParams.GameTime : new GameTime(), elem.DerivedAbsolutePosition.ToVector2());
            }
        });

        if (animSprite != null)
        {
            elem.OffsetAndSize = new Rectangle(
                elem.OffsetAndSize.X,
                elem.OffsetAndSize.Y,
                animSprite.Width / (int)Util.GlobalScale,
                animSprite.Height / (int)Util.GlobalScale
            );
        }

        return elem;
    }
}