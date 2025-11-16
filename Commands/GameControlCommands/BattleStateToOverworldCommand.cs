
using static AwesomeRPG.Util;

namespace AwesomeRPG.Commands
{
    /// <summary>
    /// I'm not entirely sure this command will be necessary, as I don't expect it will have any behavior other than running a single method in Game1
    ///     Nonetheless, better safe than sorry. --Lupine
    /// </summary>
    public class BattleStateToOverworldCommand : ICommand
    {

        /// <param name="content">ContentManager for RoomMapFromXML.</param>
        /// <param name="map">RoomMap for RoomMapFromXML.</param>
        /// <param name="direction">The dirction the room changes.</param>
        public BattleStateToOverworldCommand() {}

        public void Execute() 
        {
            Game1.TransitionToOverworldState();
        }
    }
}
