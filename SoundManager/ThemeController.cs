using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeRPG;

public static class ThemeController
{
    private static double timeSinceIntroStart = 0;

    // To help pause and play
    public static bool IsCurrentThemePlaying { get; set; }

    /*
     * Pause and Play need to be placed into their own sound class as sounds need to be instances to be 
     * stopped and paused during gameplay
     */

    public static void PlayCurrentAreaTheme(SoundEffectInstance currentTheme)
    {
        if (!IsCurrentThemePlaying)
        {
            currentTheme.Play();
            IsCurrentThemePlaying = true;
        }
    }

    public static void PlayCurrentAreaTheme(GameTime gameTime, SoundEffectInstance currentThemeIntro, double currentIntroThemeTimeDuration, SoundEffectInstance currentThemeLoop)
    {
        if (!IsCurrentThemePlaying)
        {
            currentThemeIntro.Play();
            IsCurrentThemePlaying = true;
        }

        // Once the intro is done, continue to the main loop
        if (timeSinceIntroStart > currentIntroThemeTimeDuration)
        {
            currentThemeLoop.Play();
        }

        // Count the seconds that have played of the intro
        if (timeSinceIntroStart <= currentIntroThemeTimeDuration)
        {
            timeSinceIntroStart += gameTime.ElapsedGameTime.TotalNanoseconds;
        }
    }

    public static void StopCurrentAreaTheme(SoundEffectInstance currentTheme)
    {
        timeSinceIntroStart = 0;
        currentTheme.Stop();
        IsCurrentThemePlaying = false;
    }

    public static void StopCurrentAreaTheme(SoundEffectInstance currentThemeIntro, SoundEffectInstance currentThemeLoop)
    {
        timeSinceIntroStart = 0;
        currentThemeIntro.Stop();
        currentThemeLoop.Stop();
        IsCurrentThemePlaying = false;
    }
}
