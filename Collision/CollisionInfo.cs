namespace AwesomeRPG.Collision
{
    public class CollisionInfo
    {
        public enum CollisionDirection { Left, Right, Top, Bottom, None }
        public CollisionObject CollisionObject1 { get; private set; }
        public CollisionObject CollisionObject2 { get; private set; }
        public CollisionDirection Direction { get; private set; }


        public CollisionInfo(CollisionObject collisionObject1, CollisionObject collisionObject2, CollisionDirection direction)
        {
            CollisionObject1 = collisionObject1;
            CollisionObject2 = collisionObject2;
            Direction = direction;
        }

    }
}
