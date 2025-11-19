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
    private static SoundEffect characterTextPrintSoundEffect;
    private static SoundEffect overworldOceanAndWaterSoundEffect;
    private static SoundEffect roomDiscoverySoundEffect;
    private static SoundEffect roomOpeningSoundEffect;

    // Holders to grab the time duration of intro portions for better looping
    private static SoundEffect overWorldThemeIntroHolder;
    private static SoundEffect battleSceneThemeIntroHolder;
    private static SoundEffect bossBattleSceneThemeIntroHolder;

    private static SoundEffectInstance battleSceneThemeIntro;
    private static SoundEffectInstance battleSceneThemeMainLoop;
    private static SoundEffectInstance bossBattleSceneThemeIntro;
    private static SoundEffectInstance bossBattleSceneThemeMainLoop;
    private static SoundEffectInstance dungeonTheme;
    private static SoundEffectInstance overWorldThemeIntro;
    private static SoundEffectInstance overWorldThemeMainLoop;
    private static SoundEffectInstance startScreenTheme;

    public static void PlayOverworldMapTheme(GameTime gameTime)
    {
        ThemeController.PlayCurrentAreaTheme(gameTime, overWorldThemeIntro, overWorldThemeIntroHolder.Duration.TotalNanoseconds, overWorldThemeMainLoop);
    }

    public static void StopOverworldMapTheme()
    {
        ThemeController.StopCurrentAreaTheme(overWorldThemeIntro, overWorldThemeMainLoop);
    }

    public static void PlayBattleSceneTheme(GameTime gameTime)
    {
        ThemeController.PlayCurrentAreaTheme(gameTime, battleSceneThemeIntro, battleSceneThemeIntroHolder.Duration.TotalNanoseconds, battleSceneThemeMainLoop);
    }
    public static void StopBattleSceneTheme()
    {
        ThemeController.StopCurrentAreaTheme(battleSceneThemeIntro, battleSceneThemeMainLoop);
    }
    public static void PlayBossBattleSceneTheme(GameTime gameTime)
    {
        ThemeController.PlayCurrentAreaTheme(gameTime, bossBattleSceneThemeIntro, bossBattleSceneThemeIntroHolder.Duration.TotalNanoseconds, bossBattleSceneThemeMainLoop);
    }
    public static void StopBossBattleSceneTheme()
    {
        ThemeController.StopCurrentAreaTheme(bossBattleSceneThemeIntro, bossBattleSceneThemeMainLoop);
    }

    public static void PlayDungeonTheme()
    {
        ThemeController.PlayCurrentAreaTheme(dungeonTheme);
    }

    public static void StopDungeonTheme()
    {
        ThemeController.StopCurrentAreaTheme(dungeonTheme);
    }
    public static void PlayStartScreenTheme()
    {
        ThemeController.PlayCurrentAreaTheme(startScreenTheme);
    }

    public static void StopStartScreenTheme()
    {
        ThemeController.StopCurrentAreaTheme(startScreenTheme);
    }

    public static void PlayCharacterTextPrintSoundEffect()
    {
        characterTextPrintSoundEffect.Play();
    }

    public static void PlayOverworldOceanAndWaterSoundEffect()
    {
        overworldOceanAndWaterSoundEffect.Play();
    }

    public static void PlayRoomDiscoverySoundEffect()
    {
        roomDiscoverySoundEffect.Play();
    }

    public static void PlayRoomOpeningSoundEffect()
    {
        roomOpeningSoundEffect.Play();
    }

    public static void LoadAndSetUpAllThemes(ContentManager content)
    {
        ThemeController.IsCurrentThemePlaying = false;

        characterTextPrintSoundEffect = content.Load<SoundEffect>("BackgroundSoundEffects/CharacterTextPrintSoundEffect");
        overworldOceanAndWaterSoundEffect = content.Load<SoundEffect>("BackgroundSoundEffects/OverworldOceanAndWaterSoundEffect");
        roomDiscoverySoundEffect = content.Load<SoundEffect>("BackgroundSoundEffects/RoomDiscoverySoundEffect");
        roomOpeningSoundEffect = content.Load<SoundEffect>("BackgroundSoundEffects/RoomOpeningSoundEffect");

        battleSceneThemeIntroHolder = content.Load<SoundEffect>("GameThemes/BattleSceneThemeIntro");
        bossBattleSceneThemeIntroHolder = content.Load<SoundEffect>("GameThemes/BossBattleSceneThemeIntro");
        overWorldThemeIntroHolder = content.Load<SoundEffect>("GameThemes/OverworldMapThemeIntro");

        battleSceneThemeIntro = battleSceneThemeIntroHolder.CreateInstance();
        bossBattleSceneThemeIntro = bossBattleSceneThemeIntroHolder.CreateInstance();
        overWorldThemeIntro = overWorldThemeIntroHolder.CreateInstance();

        battleSceneThemeMainLoop = content.Load<SoundEffect>("GameThemes/BattleSceneThemeRepeat").CreateInstance();
        bossBattleSceneThemeMainLoop = content.Load<SoundEffect>("GameThemes/BossBattleSceneThemeRepeat").CreateInstance();
        overWorldThemeMainLoop = content.Load<SoundEffect>("GameThemes/OverworldMapThemeRepeat").CreateInstance();
        dungeonTheme = content.Load<SoundEffect>("GameThemes/DungeonTheme").CreateInstance();
        startScreenTheme = content.Load<SoundEffect>("GameThemes/StartScreenTheme").CreateInstance();

        overWorldThemeMainLoop.IsLooped = true;
        battleSceneThemeMainLoop.IsLooped = true;
        bossBattleSceneThemeMainLoop.IsLooped = true;
        dungeonTheme.IsLooped = true;
        startScreenTheme.IsLooped = true;
    }


}
