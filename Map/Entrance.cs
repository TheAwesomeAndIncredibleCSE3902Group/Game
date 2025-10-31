using Microsoft.Xna.Framework;
using AwesomeRPG.Collision;

namespace AwesomeRPG.Map
{
    public class Entrance : CollisionObject
    {
        public RoomAtlas roomAtlas;
        public Game1 myGame;

        public Entrance(Vector2 startPos, int width, int height)
        {
            Position = startPos;
            Collider = new CollisionRect(this, width, height);
            ObjectType = CollisionObjectType.Wall;
        }
    }
}
