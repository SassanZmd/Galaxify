using System;
using System.IO;
using System.Text.Json.Nodes;
using System.Threading;
using Microsoft.Xna.Framework;
using static TestGame.Content.obj.Config.Config;

namespace TestGame.Content.obj;

public class Science : Object2D
{
    private Ball _tempBall;
    private readonly (int width, int height) _resolution;
    private int _delayMs;

    public Science(string textureName, float speed, float scale, float collisionOffset, int delayMs, 
        int resolutionWidth, int resolutionHeight)
    {
        SetTextureName(textureName);
        SetSpeed(speed);
        SetScale(scale);
        SetCollisionOffset(collisionOffset);

        _delayMs = delayMs;
        _resolution = (resolutionWidth, resolutionHeight);
    }

    private void SetPosition(int resWidth, int resHeight)
    {
        // random position within a defined space
        var scale = GetScale();
        var texture = GetTexture();
        var width = texture.Width;
        var height = texture.Height;
        
        var rnd = new Random();
        var x = rnd.Next((int)(width * scale / 2), (int)(resWidth - width * scale / 2));
        var y = rnd.Next((int)(height * scale / 2), (int)(resHeight - height * scale / 2));
        var pos = new Vector2(x, y);
        SetPosition(pos);
    }

    public void Spawn(Ball ball)
    {
        ResetDestruction();
        SetPosition(_resolution.width, _resolution.height);
        
        while (Collision(ball))
        {
            SetPosition(_resolution.width, _resolution.height);
        }
    }

    public void DelayedSpawn(Ball ball)
    {
        ResetDestruction();
        SetPosition(new Vector2(-100, -100));
        _tempBall = ball;
        var thread = new Thread(SpawnThread);
        thread.Start();
    }

    private void SpawnThread()
    {
        Thread.Sleep(_delayMs);
        Spawn(_tempBall);
    }

    public void SetDelayMs(int delayMs)
    {
        _delayMs = delayMs;
    }
    
    public int GetDelayMs()
    {
        return _delayMs;
    }
}