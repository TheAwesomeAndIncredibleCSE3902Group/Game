using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeRPG;

public static class GameSoundFactory
{
    private static double timeSinceIntroStart = 0;

    private static SoundEffect overWorldThemeIntroHolder;
    private static SoundEffect overWorldThemeMainLoopHolder;
    private static SoundEffect battleSceneThemeIntroHolder;
    private static SoundEffect battleSceneThemeMainLoopHolder;
    private static SoundEffect dungeonThemeHolder;

    private static SoundEffectInstance overWorldThemeIntro;
    private static SoundEffectInstance overWorldThemeMainLoop;
    private static SoundEffectInstance battleSceneThemeIntro;
    private static SoundEffectInstance battleSceneThemeMainLoop;
    private static SoundEffectInstance dungeonTheme;

    // To help pause and play
    public static bool IsCurrentThemePlaying { get; set; }

    /*
     * Pause and Play need to be placed into their own sound class as sounds need to be instances to be 
     * stopped and paused during gameplay
     */

    public static void PlayOverworldMapTheme(GameTime gameTime)
    {
        if (!IsCurrentThemePlaying)
        {
            overWorldThemeIntro.Play();
            IsCurrentThemePlaying = true;
        }

        // Once the intro is done, continue to the main loop
        if (timeSinceIntroStart > overWorldThemeIntroHolder.Duration.TotalNanoseconds)
        {
            overWorldThemeMainLoop.Play();
        }

        // Count the seconds that have played of the intro
        if (timeSinceIntroStart <= overWorldThemeIntroHolder.Duration.TotalNanoseconds)
        {
            timeSinceIntroStart += gameTime.ElapsedGameTime.TotalNanoseconds;
        }
    }

    public static void StopOverworldMapTheme()
    {
        timeSinceIntroStart = 0;
        overWorldThemeIntro.Stop();
        overWorldThemeMainLoop.Stop();
        IsCurrentThemePlaying = false;
    }

    public static void PlayBattleSceneTheme(GameTime gameTime)
    {
        if (!IsCurrentThemePlaying)
        {
            battleSceneThemeIntro.Play();
            IsCurrentThemePlaying = true;
        }

        if (timeSinceIntroStart > battleSceneThemeIntroHolder.Duration.TotalNanoseconds)
        {
            battleSceneThemeMainLoop.Play();
        }

        if (timeSinceIntroStart <= battleSceneThemeIntroHolder.Duration.TotalNanoseconds)
        {
            timeSinceIntroStart += gameTime.ElapsedGameTime.TotalNanoseconds;
        }
    }
    public static void StopBattleSceneTheme()
    {
        timeSinceIntroStart = 0;
        battleSceneThemeIntro.Stop();
        battleSceneThemeMainLoop.Stop();
        IsCurrentThemePlaying = false;
    }

    public static void LoadAllSongs(ContentManager content)
    {
        IsCurrentThemePlaying = false;

        overWorldThemeIntroHolder = content.Load<SoundEffect>("GameThemes/OverworldMapThemeIntro");
        overWorldThemeMainLoopHolder = content.Load<SoundEffect>("GameThemes/OverworldMapThemeRepeat");
        battleSceneThemeIntroHolder = content.Load<SoundEffect>("GameThemes/BattleSceneMusicIntro");
        battleSceneThemeMainLoopHolder = content.Load<SoundEffect>("GameThemes/BattleSceneMusicRepeat");
        dungeonThemeHolder = content.Load<SoundEffect>("GameThemes/DungeonTheme");

        overWorldThemeIntro = overWorldThemeIntroHolder.CreateInstance();
        overWorldThemeMainLoop = overWorldThemeMainLoopHolder.CreateInstance();
        battleSceneThemeIntro = battleSceneThemeMainLoopHolder.CreateInstance();
        battleSceneThemeMainLoop = battleSceneThemeMainLoopHolder.CreateInstance();
        dungeonTheme = dungeonThemeHolder.CreateInstance();

        overWorldThemeMainLoop.IsLooped = true;
        battleSceneThemeMainLoop.IsLooped = true;
        dungeonTheme.IsLooped = true;
    }


}
