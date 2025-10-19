using Microsoft.Xna.Framework;

namespace AwesomeRPG.Collision
{
    public class CollisionRect
    {

        CollisionObject _gameObject;
        int _width;
        int _height;


        public CollisionRect(CollisionObject gameObject, int width, int height) 
        { 
            _gameObject = gameObject;
            _width = width;
            _height = height;
        }

        /// <summary>
        /// Returns a rectangle based off the height and width at the position of the gameObject
        /// </summary>
        /// <returns></returns>
        public Rectangle GetCollisionRect()
        {
            return new Rectangle((int)_gameObject.Position.X, (int)_gameObject.Position.Y, _width, _height);
        }
    }
}
