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

        public Rectangle GetCollisionRect()
        {
            return new Rectangle((int)_gameObject.Position.X, (int)_gameObject.Position.Y, _width, _height);
        }
    }
}
