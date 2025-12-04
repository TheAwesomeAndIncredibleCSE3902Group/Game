using System;
using System.Collections.Generic;
using System.Diagnostics;
using AwesomeRPG.BattleMechanics;
using AwesomeRPG.Characters;
using AwesomeRPG.Controllers;
using AwesomeRPG.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AwesomeRPG;

/// <summary>
/// Will be made every time the player switches to Battle State
/// </summary>
public class BattleState : IGameState
{
    //This could still be used in case we want different text scrolling times or etc
    //public float TimeScale { get; private set; }

    //Root of the UI. 
    //Currently the UI is initialized at the beginning in Game1 and safely stored in OverworldState.
    private RootElement RootUIElement { get; set; }

    //Caches the last OverworldState. This makes returning to the overworld much easier
    private OverworldState overworldState;
    private Game1 game;
    //Ideally enemies and enemySprites would be combined into a BattleEnemy
    private CharacterEnemyBase[] enemies;
    private CharacterBattleSprite[] enemySprites;

    private string enemyType;
    public GameState CurrentState { get => GameState.battle; }

    //BattleState can only be made from an OverworldState
    public BattleState(OverworldState overState, Game1 game, CharacterEnemyBase[] enemies)
    {
        this.game = game;
        this.overworldState = overState;
        this.RootUIElement = game.RootUIElement;
        this.enemies = enemies;
        enemyType = enemies[0].Name;
        InitializeBattle();
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        RootUIElement.Draw(gameTime);
        foreach (CharacterBattleSprite enemy in enemySprites)
            enemy.Draw(gameTime);
    }

    public void Update(GameTime gameTime)
    {
        GameSoundFactory.PlayBattleSceneTheme(gameTime);
        RootUIElement.Update(gameTime);
    }

    public void ChangeToBattleState(CharacterEnemyBase[] enemies) { }
    public void ChangeToStartState() { }
    public void ChangeToGameOverState() { }
    public void ChangeToWinState() { }

    public void ChangeToOverworldState()
    {
        //throw new System.NotImplementedException();
        //This will have to convert all relevant data to Overworld delta
        //Use that delta to modify the Overworld state
        //And then return to that Overworld state
        GameSoundFactory.StopBattleSceneTheme();
        //PlayerSoundFactory.PlayVictoryFanfare();
        foreach (CharacterEnemyBase enemy in enemies)
        {
            enemy.TryDestroy();
        }

        //TODO: do changes to player, NPCs (ie health), and enemies
        game.SetStateClass(overworldState);
    }

    public bool TransitionAllowedTo(GameState state)
    {
        return state switch
        {
            GameState.battle => true,
            GameState.overworld => true,
            _ => false
        };
    }

    private void InitializeBattle()
    {
        BattleScene.Instance.InitializeBattleSequence(true, new InitializeSampleBattle().SetUpEnemies(), new InitializeSampleBattle().SetUpAllies());
        BuildBattlePanel();
    }
    
    //Little test method to test displaying multiple enemies
    private void TESTDupeEnemies()
    {
        const int num = 3;

        CharacterEnemyBase[] oldEnemies = enemies;
        enemies = new CharacterEnemyBase[num];

        for (int i = 0; i < num; i++)
        {
            enemies[i] = oldEnemies[0];
        }
        enemies[1] = new CharacterEnemyMoblin(Vector2.Zero, Util.Cardinal.down);
    }

    private void BuildBattlePanel()
    {
        TESTDupeEnemies();

        //TODO: background

        Vector2[] enemyOffsets = FindEnemyOffsets(enemies.Length);
        enemySprites = new CharacterBattleSprite[enemies.Length];

        for (int i = 0; i < enemies.Length; i++)
            enemySprites[i] = new CharacterBattleSprite(enemies[i].Type, enemyOffsets[i]);

        enemySprites[1].Hurt = true;
    }
    

    /// <summary>
    /// This is a simple and very hacky aligner for up to three enemies
    ///     If you need more than that then we'll have to make a proper solution
    /// </summary>
    /// <param name="enemies"></param>
    /// <returns></returns>
    private static Vector2[] FindEnemyOffsets(int enemies)
    {
        const int enemyWidth = 15;
        const int verticalPadding = 50;
        const int horizontalPadding = 50;

        if (enemies > 3)
            Debug.WriteLine("Warning! FindEnemyOffsets does not work for more than 3 enemies!");

        Rectangle screenRect = Util.ScreenRect;
        Vector2[] offset = new Vector2[enemies];

        offset[0] = new Vector2(horizontalPadding, verticalPadding);
        int thirdHorizontal = screenRect.Width - horizontalPadding - enemyWidth * Util.BattleScale;
        int secondHorizontal = horizontalPadding + (int)((thirdHorizontal - horizontalPadding) / 2f);

        if (enemies == 2)
            offset[1] = new Vector2(thirdHorizontal, verticalPadding);
        else if (enemies == 3)
        {
            offset[1] = new Vector2(secondHorizontal, verticalPadding);
            offset[2] = new Vector2(thirdHorizontal, verticalPadding);
        }

        return offset;
    }

}