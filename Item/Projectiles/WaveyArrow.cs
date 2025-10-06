using System;
using Sprint0.Sprites;
using Microsoft.Xna.Framework;
using static Sprint0.Util;

namespace Sprint0;

/// <summary>
/// Arrow that moves in a pattern similar to a square wave.
/// </summary>
public class WaveyArrow : Projectile
{
    int damage;
    Vector2 linearPos;
    //hz
    float baseFreq;
    //seconds
    float baseAmplitude;

    /// <summary>
    /// Creates a new WaveyArrow from @position
    /// </summary>
    /// <param name="position"></param>
    /// <param name="direction"></param>
    /// <param name="baseFreq">How quickly this completes one complete up-down-up circuit in seconds</param>
    /// <param name="baseAmplitude">Half the total number of pixels this will travel "up" and "down"</param>
    public WaveyArrow(Vector2 position, Cardinal direction, float baseFreq = 0.8f, float baseAmplitude = 50)
    {
        this.linearPos = position;
        this.position = position;
        this.direction = direction;

        this.movementSpeed = 2;
        this.lifetime = 3;
        this.damage = 2;

        this.baseFreq = baseFreq;
        this.baseAmplitude = baseAmplitude;

        //Didn't work with arrow sprite, rework later
        sprite = ItemSpriteFactory.CreateArrowSprite(direction);
    }

    /// <summary>
    /// Utilizes the first odd harmonic of a sine wave to make motion similar to a square wave.
    /// Evaluates two or three sin functions per frame, which is certainly slow. These could be cached if necessary.
    /// </summary>
    protected override void Move()
    {
        linearPos += movementSpeed * Util.CardinalToUnitVector(direction);
        float x = (float)Math.Tau * baseFreq * age;

        //The specific waveform can be adjusted slightly for effect. Swap these lines for an example.
        float offset = (float)(baseAmplitude * (Math.Sin(x) + Math.Sin(3 * x) / 3));
        //float offset = (float) (baseAmplitude * (Math.Sin(x) + Math.Sin(3 * x) / 6 + Math.Sin(5 * x) / 10));

        Cardinal modCardinal = (Cardinal)((int)(direction + 1) % 4);
        Vector2 vectorOffset = Util.CardinalToUnitVector(modCardinal) * offset;
        position = linearPos + vectorOffset;
    }

    public override void Destroy()
    {
        Player.Instance.spawnedProjectiles.Remove(IEquipment.Projectiles.arrow);
    }
}