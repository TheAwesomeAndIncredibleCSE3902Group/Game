using System;
using AwesomeRPG.Characters;
using AwesomeRPG.Stats;
using AwesomeRPG.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AwesomeRPG;

public class Player
{
    public static Player Instance { get; private set; }
    public PlayerStats PlayerStats { get; init; }
    public PlayerOverworld PlayerOverworld { get; init; }

    public Player(PlayerStats pStats,PlayerOverworld pOverworld)
    {
        Instance = this;
        PlayerStats = pStats;
        PlayerOverworld = pOverworld;
    }
}