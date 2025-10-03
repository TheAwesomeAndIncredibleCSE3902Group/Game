using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sprint0.Util;

namespace Sprint0.Sprites
{
    public class PlayerSpriteFactory
    {
        private Cardinal currentDirection;
        private Texture2D linkSpriteSheet;
        private SpriteBatch linkSpriteBatch;

        private ISprite currentLinkSprite;
        private ISprite standingLinkSprite;
        private ISprite walkingLinkSprite;
        private ISprite itemUseLinkSprite;

        private static Rectangle standingFrame;
        private static Rectangle itemFrame;
        private static int[] rectangleXPositions;
        private static int [,] spriteAtlas;

        //Make 4 animatable sprites to have Link be able to walk
        public PlayerSpriteFactory()
        {
            // Link, on the legendofzelda_link_sheet.png, is 15 by 15 pixels in size
            standingFrame = new Rectangle(0, 0, 15, 15);
            itemFrame = new Rectangle(0, 60, 15, 15);
            int [] rectangleXPositions = {0, 30, 60, 90};
            int [,] spriteAtlas = { {0, 0, 15, 15} , {0, 30, 15, 15} };
        }

        public void LoadPlayer(ContentManager content, SpriteBatch spriteBatch)
        {
            linkSpriteSheet = content.Load<Texture2D>("SpriteImages/legendofzelda_link_sheet");
            linkSpriteBatch = spriteBatch;
            standingLinkSprite = new AnimatableSprite(linkSpriteBatch, linkSpriteSheet, standingFrame);
            walkingLinkSprite = new AnimatableSprite(linkSpriteBatch, linkSpriteSheet, spriteAtlas, 500);
            itemUseLinkSprite = new AnimatableSprite(linkSpriteBatch, linkSpriteSheet, itemFrame);
            currentLinkSprite = standingLinkSprite;
        }
        public ISprite GetSprite()
        {
            return currentLinkSprite;
        }

        // Sets x values of each rectangle to be the same so that each frame correlates with the same direction
        private void SetXValuesEqual()
        {
            standingFrame.X = spriteAtlas[0, 0];
            itemFrame.X = standingFrame.X;
            spriteAtlas[1, 0] = spriteAtlas[0, 0];
        }

        public void ChangeDirection(Cardinal newDirection)
        {
            switch (newDirection)
            {
                case Cardinal.down:
                    spriteAtlas[0, 0] = rectangleXPositions[0];
                    break;

                case Cardinal.left:
                    spriteAtlas[0, 0] = rectangleXPositions[1];
                    break;

                case Cardinal.up:
                    spriteAtlas[0, 0] = rectangleXPositions[2];
                    break;

                case Cardinal.right:
                    spriteAtlas[0, 0] = rectangleXPositions[3];
                    break;
            }

            SetXValuesEqual();
            currentDirection = newDirection;
        }

        public void ChangeSpriteStanding()
        {
            spriteAtlas[0,1] = 0;
            spriteAtlas[0,2] = 15;
            spriteAtlas[0,3] = 15;
            SetXValuesEqual();

            currentLinkSprite = standingLinkSprite;
        }

        public void ChangeSpriteWalking()
        {
            currentLinkSprite = walkingLinkSprite;
        }

        public void ChangeSpriteItemUse()
        {
            currentLinkSprite = itemUseLinkSprite;
        }
        public void ChangeSpriteDamaged()
        {
            // decorator type stuff goes here to 'damage' Link
        }
        
        public void Draw(GameTime gt, Vector2 position)
        {
            currentLinkSprite.Draw(gt, position);
        }
    }
}
