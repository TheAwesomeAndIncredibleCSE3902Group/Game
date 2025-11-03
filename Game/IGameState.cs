
using Microsoft.Xna.Framework;

namespace AwesomeRPG;
public interface IGameState
{
    public void Update(GameTime gameTime);

    public void Draw(GameTime gameTime);

    public BattleState ToBattleState();
    public OverworldState ToOverworldState();
}