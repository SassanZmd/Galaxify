using System;
using Microsoft.Xna.Framework;

namespace TestGame.Content.obj;

public static class Config
{
    // Graphics config
    public const int ResolutionWidth = 720;
    public const int ResolutionHeight = 480;

    // Ball sprite config
    public static Vector2 BallPos { get; } = new((float)ResolutionWidth / 2, (float)ResolutionHeight / 2);
    public const float BallSpeed = 300f;
    public const float BallScale = 0.1f;
    public static float BallDiagonalSpeed { get; } = (float)Math.Sqrt(Math.Pow(BallSpeed, 2) * 2) / 2;
    public const float BallCollisionOffset = 10f;

    // Science sprite config
    public const float ScienceSpeed = 100f;
    public const float ScienceScale = 0.05f;
    public const float ScienceCollisionOffset = 10f;
    public const int ScienceDelayMs = 100;
}