using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeRPG;

public static class UISoundFactory
{
    private static SoundEffect cantSelectSoundEffect;
    private static SoundEffect grazeSoundEffect;
    private static SoundEffect selectSoundEffect;
    private static SoundEffect textSoundEffect;

    public static void PlayCantSelectSoundEffect()
    {
        cantSelectSoundEffect.Play();
    }

    public static void PlayGrazeSoundEffect()
    {
        grazeSoundEffect.Play();
    }

    public static void PlaySelectSoundEffect()
    {
        selectSoundEffect.Play();
    }

    public static void PlayTextSoundEffect()
    {
        textSoundEffect.Play();
    }

    public static void LoadAndSetUpAllUISounds(ContentManager content)
    {
        cantSelectSoundEffect = content.Load<SoundEffect>("UISoundEffects/snd_cantselect");
        grazeSoundEffect = content.Load<SoundEffect>("UISoundEffects/snd_graze");
        selectSoundEffect = content.Load<SoundEffect>("UISoundEffects/snd_select");
        textSoundEffect = content.Load<SoundEffect>("UISoundEffects/snd_text");
    }
}
