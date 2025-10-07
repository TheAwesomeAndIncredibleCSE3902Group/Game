using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AwesomeRPG.Util;

namespace AwesomeRPG.Sprites
{
    public class PlayerSpriteFactory
    {
        private Cardinal currentDirection;
        private Texture2D linkSpriteSheet;
        private SpriteBatch linkSpriteBatch;

        private ISprite currentLinkSprite;

        private static Rectangle standingFrame;
        private static Rectangle itemFrame;
        private static Rectangle damageFrame;
        private static readonly int[] rectangleXPositions = {0, 30, 60, 90};
        private static int [,] walkingSpriteAtlas = { {0, 0, 15, 15} , {0, 30, 15, 15} };

        //Make 4 animatable sprites to have Link be able to walk
        public PlayerSpriteFactory()
        {
            // Link, on the legendofzelda_link_sheet.png, is 15 by 15 pixels in size
            damageFrame = new Rectangle(30, 150, 15, 15);
            standingFrame = new Rectangle(0, 0, 15, 15);
            itemFrame = new Rectangle(0, 60, 15, 15);
        }

        public void LoadPlayer(ContentManager content, SpriteBatch spriteBatch)
        {
            linkSpriteSheet = content.Load<Texture2D>("SpriteImages/legendofzelda_link_sheet");
            linkSpriteBatch = spriteBatch;
            currentLinkSprite = new AnimatableSprite(linkSpriteBatch, linkSpriteSheet, standingFrame);
        }
        public ISprite GetSprite()
        {
            return currentLinkSprite;
        }

        private void SetSpritesToFaceSameDirection()
        {
            standingFrame.X = walkingSpriteAtlas[0, 0];
            itemFrame.X = walkingSpriteAtlas[0, 0];
            walkingSpriteAtlas[1, 0] = walkingSpriteAtlas[0, 0];
        }

        public void ChangeDirection(Cardinal newDirection)
        {
            switch (newDirection)
            {
                case Cardinal.down:
                    walkingSpriteAtlas[0, 0] = rectangleXPositions[0];
                    break;

                case Cardinal.left:
                    walkingSpriteAtlas[0, 0] = rectangleXPositions[1];
                    break;

                case Cardinal.up:
                    walkingSpriteAtlas[0, 0] = rectangleXPositions[2];
                    break;

                case Cardinal.right:
                    walkingSpriteAtlas[0, 0] = rectangleXPositions[3];
                    break;
            }

            SetSpritesToFaceSameDirection();
            currentDirection = newDirection;
            ChangeSpriteWalking();
        }

        public void ChangeSpriteStanding()
        {
            currentLinkSprite = new AnimatableSprite(linkSpriteBatch, linkSpriteSheet, standingFrame);
        }

        public void ChangeSpriteWalking()
        {
            currentLinkSprite = new AnimatableSprite(linkSpriteBatch, linkSpriteSheet, walkingSpriteAtlas, 200);
        }

        public void ChangeSpriteItemUse()
        {
            currentLinkSprite = new AnimatableSprite(linkSpriteBatch, linkSpriteSheet, itemFrame);
        }
        public void ChangeSpriteDamaged()
        {
            currentLinkSprite = new AnimatableSprite(linkSpriteBatch, linkSpriteSheet, damageFrame);
        }
        
        public void Draw(GameTime gt, Vector2 position)
        {
            currentLinkSprite.Draw(gt, position);
        }
    }
}
