// Originally written by Eli L
// If you need help with working on UI, feel free to ask me!!!

using AwesomeRPG.UI.Events;
using Microsoft.Xna.Framework;


namespace AwesomeRPG.UI.Elements;

public class SelectionAnimationElement : ElementBase
{
    private Color _selectColor = Color.LightBlue;
    public override void Draw(GameTime gameTime)
    {
        CalculateDerivedValuesFromAncestors();
        DispatchUIEvent(UIEvent.BeforeDraw, new DrawUIEventParams(this, gameTime));

        if (DerivedAncestorIsSelected && IsVisible)
        {
            int animationFrame = (int)gameTime.TotalGameTime.TotalMicroseconds / 9000 % 100;
            // System.Console.WriteLine(animationFrame);
            var sizedRectangle1 = new Rectangle(DerivedAbsolutePosition, OffsetAndSize.Size);
            sizedRectangle1.Inflate(2, 2);

            RootElement.SpriteBatch.Draw(
                RootElement.RectangleTexture,
                sizedRectangle1,
                _selectColor * 1
            );
            for (int i = 0; i < 1; i++)
            {
                _selectColor = new Color(255, 255, 255);

                var sizedRectangle = new Rectangle(DerivedAbsolutePosition, OffsetAndSize.Size);
                sizedRectangle.Inflate(animationFrame / 8 + 2, animationFrame / 8 + 2);

                RootElement.SpriteBatch.Draw(
                    RootElement.RectangleTexture,
                    sizedRectangle,
                    _selectColor * ((99 - animationFrame) / 2 / 255.0f) * Opacity
                );
                animationFrame = (animationFrame + 50) % 100;
            }
        }

        DrawChildren(gameTime);
        DispatchUIEvent(UIEvent.AfterDraw, new DrawUIEventParams(this, gameTime));
    }

    public SelectionAnimationElement(RootElement rootElement)
    {
        SetUpElement(rootElement);
    }
}
