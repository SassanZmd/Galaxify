using System;
using Microsoft.Xna.Framework;

namespace TestGame.Content.obj;

public static class Config
{
    // Graphics config
    public const int ResolutionWidth = 720;
    public const int ResolutionHeight = 480;
    public static Color BackgroundColor { get; } = Color.Tan;

    // Ball sprite config
    public const string BallTexture = "ball-pixel";
    public static Vector2 BallPosition { get; } = new((float)ResolutionWidth / 2, (float)ResolutionHeight / 2);
    public const float BallSpeed = 300f;
    public const float BallScale = 0.1f;
    public static float BallDiagonalSpeed { get; } = (float)Math.Sqrt(Math.Pow(BallSpeed, 2) * 2) / 2;
    public const float BallCollisionOffset = 10f;

    // Science sprite config
    public const string ScienceTexture = "science-pixel";
    public const float ScienceSpeed = 100f;
    public const float ScienceScale = 0.05f;
    public const float ScienceCollisionOffset = 10f;
    public const int ScienceDelayMs = 100;
    
    // Politics sprite config
    public const string PoliticsTexture = "politics-pixel";
    public const float PoliticsSpeed = 100f;
    public const float PoliticsScale = 0.05f;
    public const float PoliticsCollisionOffset = 10f;
    public const int PoliticsDelayMs = 5000;
}