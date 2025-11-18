using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeRPG;

public static class PlayerSoundFactory
{
    private static SoundEffect bossBattleVictorySoundEffect;
    private static SoundEffect linkEnterRoomSoundEffect;
    private static SoundEffect linkHurtSoundEffect;
    private static SoundEffect linkItemGetSoundEffect;
    private static SoundEffect playerUICursorMoveAndLinkDyingSoundEffect;
    private static SoundEffect victoryFanfareSoundEffect;
    
    public static void PlayBossBattleVictorySoundEffect()
    {
        bossBattleVictorySoundEffect.Play();
    }

    public static void PlayLinkEnterRoomSoundEffect()
    {
        linkEnterRoomSoundEffect.Play();
    }
    
    public static void PlayLinkHurtSoundEffect()
    {
        //This was causing a NullReferenceException
        //linkHurtSoundEffect.Play();
    }
    
    public static void PlayLinkItemGetSoundEffect()
    {
        linkItemGetSoundEffect.Play();
    }

    public static void PlayUICursorMoveAndLinkDyingSoundEffect()
    {
        playerUICursorMoveAndLinkDyingSoundEffect.Play();
    }

    public static void PlayVictoryFanfare()
    {
        victoryFanfareSoundEffect.Play();
    }

    public static void LoadAndSetUpAllPlayerSounds(ContentManager content)
    {
        bossBattleVictorySoundEffect = content.Load<SoundEffect>("LinkSoundEffects/BossBattleVictorySoundEffect");
        linkEnterRoomSoundEffect = content.Load<SoundEffect>("LinkSoundEffects/LinkEnterRoomSoundEffect");
        linkHurtSoundEffect = content.Load<SoundEffect>("LinkSoundEffects/LinkHurtPlacementSoundEffect");
        linkItemGetSoundEffect = content.Load<SoundEffect>("LinkSoundEffects/LinkItemGetSoundEffect");
        playerUICursorMoveAndLinkDyingSoundEffect = content.Load<SoundEffect>("LinkSoundEffects/UICursorMoveAndLinkDyingSoundEffect");
        victoryFanfareSoundEffect = content.Load<SoundEffect>("LinkSoundEffects/VictoryFanfare");
    }
}
