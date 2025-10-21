using Microsoft.Xna.Framework;
using AwesomeRPG.Collision;

namespace AwesomeRPG.Map
{
    public class Wall : CollisionObject
    {
        public Wall(Vector2 startPos, int width, int height)
        {
            Position = startPos;
            Collider = new CollisionRect(this, width, height);
            ObjectType = CollisionObjectType.Wall;
        }
    }
}
