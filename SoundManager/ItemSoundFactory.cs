using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeRPG;

public static class ItemSoundFactory
{
    private static SoundEffect arrowAndReflectSoundEffect;
    private static SoundEffect bombBlastSoundEffect;
    private static SoundEffect bombPlacementSoundEffect;

    //Set up as a short sound effect to quickly repeat over and over while boomerang is in the air
    private static SoundEffect boomerangSoundEffect; 
    
    private static SoundEffect fireSoundEffect;
    private static SoundEffect fleeAndFluteSoundEffect;
    private static SoundEffect heartGetSoundEffect;
    private static SoundEffect heartRevealSoundEffect;
    private static SoundEffect itemRevealAndGetSoundEffect;
    private static SoundEffect magicBeamSoundEffect;
    private static SoundEffect rupeeGetSoundEffect;
    private static SoundEffect swordBeamSoundEffect;
    private static SoundEffect swordSlashSoundEffect;


    public static void PlayArrowAndReflectSoundEffect()
    {
        arrowAndReflectSoundEffect.Play();
    }

    public static void PlayBombBlastSoundEffect()
    {
        bombBlastSoundEffect.Play();
    }

    public static void PlayBombPlacementSoundEffect()
    {
        bombPlacementSoundEffect.Play();
    }

    public static void PlayBoomerangSoundEffect()
    {
        boomerangSoundEffect.Play();
    }

    public static void PlayFireSoundEffect()
    {
        fireSoundEffect.Play();
    }

    public static void PlayFleeAndFluteSoundEffect()
    {
        fleeAndFluteSoundEffect.Play();
    }

    public static void PlayHeartGetSoundEffect()
    {
        heartGetSoundEffect.Play();
    }

    public static void PlayHeartRevealSoundEffect()
    {
        heartRevealSoundEffect.Play();
    }
    public static void PlayItemRevealAndGetSoundEffect()
    {
        itemRevealAndGetSoundEffect.Play();
    }

    public static void PlayMagicBeamSoundEffect()
    {
        magicBeamSoundEffect.Play();
    }

    public static void PlayRupeeGetSoundEffect()
    {
        rupeeGetSoundEffect.Play();
    }

    public static void PlaySwordBeamSoundEffect()
    {
        swordBeamSoundEffect.Play();
    }

    public static void PlaySwordSlashSoundEffect()
    {
        swordSlashSoundEffect.Play();
    }

    public static void LoadAndSetUpAllItemSounds(ContentManager content)
    {
        arrowAndReflectSoundEffect = content.Load<SoundEffect>("ItemSoundEffects/ArrowAndReflectSoundEffect");
        bombBlastSoundEffect = content.Load<SoundEffect>("ItemSoundEffects/BombBlastSoundEffect");
        bombPlacementSoundEffect = content.Load<SoundEffect>("ItemSoundEffects/BombPlacementSoundEffect");
        boomerangSoundEffect = content.Load<SoundEffect>("ItemSoundEffects/BoomerangSoundEffect");
        fireSoundEffect = content.Load<SoundEffect>("ItemSoundEffects/FireSoundEffect");
        fleeAndFluteSoundEffect = content.Load<SoundEffect>("ItemSoundEffects/FleeAndFluteSoundEffect");
        heartGetSoundEffect = content.Load<SoundEffect>("ItemSoundEffects/HeartGetSoundEffect");
        heartRevealSoundEffect = content.Load<SoundEffect>("ItemSoundEffects/HeartRevealSoundEffect");
        itemRevealAndGetSoundEffect = content.Load<SoundEffect>("ItemSoundEffects/ItemRevealAndGetSoundEffect");
        magicBeamSoundEffect = content.Load<SoundEffect>("ItemSoundEffects/MagicBeamSoundEffect");
        rupeeGetSoundEffect = content.Load<SoundEffect>("ItemSoundEffects/RupeeGetSoundEffect");
        swordBeamSoundEffect = content.Load<SoundEffect>("ItemSoundEffects/SwordBeamSoundEffect");
        swordSlashSoundEffect = content.Load<SoundEffect>("ItemSoundEffects/SwordSlashSoundEffect");
    }
}
