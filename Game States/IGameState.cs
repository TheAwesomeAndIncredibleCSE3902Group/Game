
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AwesomeRPG;

/// <summary>
/// Implements the State Pattern (state by class). 
/// Make a new state conversion method (ex ToBattleState) for each new state implemented.
/// </summary>
public interface IGameState
{
    public void Update(GameTime gameTime);
    public void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    public Game1.GameState CurrentState { get; }

    public BattleState ToBattleState();
    public OverworldState ToOverworldState();
}