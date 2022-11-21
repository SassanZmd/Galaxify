using System;
using System.Threading;
using Microsoft.Xna.Framework;
using static TestGame.Content.obj.Config;

namespace TestGame.Content.obj;

public class Science : Object2D
{
    private Ball _tempBall;
    
    public Science()
    {
        SetSpeed(ScienceSpeed);
        SetScale(ScienceScale);
        SetCollisionOffset(ScienceCollisionOffset);
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
        SetPosition(ResolutionWidth, ResolutionHeight);
        
        while (Collision(ball))
        {
            SetPosition(ResolutionWidth, ResolutionHeight);
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
        Thread.Sleep(ScienceDelayMs);
        Spawn(_tempBall);
    }
}