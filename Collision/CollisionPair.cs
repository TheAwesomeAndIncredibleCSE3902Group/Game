using Microsoft.Xna.Framework;

namespace AwesomeRPG.Collision
{
    public readonly record struct CollisionPair
    {
        private CollisionObjectType A { get; init; }
        private CollisionObjectType B { get; init; }
        public CollisionPair(CollisionObjectType a,  CollisionObjectType b)
        {
            if(a <= b)
            {
                A = a;
                B = b;
            }
            else
            {
                B = a;
                A = b;
            }
        }
      
    }
}
