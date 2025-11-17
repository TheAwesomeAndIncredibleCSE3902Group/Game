using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeRPG;

public static class EnemySoundFactory
{
    private static SoundEffect bossDyingSoundEffect;
    private static SoundEffect bossDamagedSoundEffect;
    private static SoundEffect bossRoarSoundEffect;

    private static SoundEffect enemyDyingSoundEffect;
    private static SoundEffect enemyRoarSoundEffect;
    private static SoundEffect enemyShrinkingSoundEffect;
    private static SoundEffect enemyZappedSoundEffect;


    public static void PlayBossDyingSoundEffect()
    {
        bossDyingSoundEffect.Play();
    }

    public static void PlayBossDamagedSoundEffect()
    {
        bossDamagedSoundEffect.Play();
    }
    
    public static void PlayBossRoarSoundEffect()
    {
        bossRoarSoundEffect.Play();
    }
    
    public static void PlayEnemyDyingSoundEffect()
    {
        enemyDyingSoundEffect.Play();
    }

    public static void PlayEnemyRoarSoundEffect()
    {
        enemyRoarSoundEffect.Play();
    }

    public static void PlayEnemyShrinkingSoundEffect()
    {
        enemyShrinkingSoundEffect.Play();
    }

    public static void PlayEnemyZappedSoundEffect()
    {
        enemyZappedSoundEffect.Play();
    }

    public static void LoadAndSetUpAllEnemySounds(ContentManager content)
    {
        bossDyingSoundEffect = content.Load<SoundEffect>("EnemySoundEffects/BossDeathSoundEffect");
        bossDamagedSoundEffect = content.Load<SoundEffect>("EnemySoundEffects/BossHurtSoundEffect");
        bossRoarSoundEffect = content.Load<SoundEffect>("EnemySoundEffects/BossRoarSoundEffect");

        enemyDyingSoundEffect = content.Load<SoundEffect>("EnemySoundEffects/EnemyKillSoundEffect");
        enemyRoarSoundEffect = content.Load<SoundEffect>("EnemySoundEffects/EnemyRoarSoundEffect");
        enemyShrinkingSoundEffect = content.Load<SoundEffect>("EnemySoundEffects/EnemyShrinkingSoundEffect");
        enemyZappedSoundEffect = content.Load<SoundEffect>("EnemySoundEffects/EnemyZappedSoundEffect");
    }
}
