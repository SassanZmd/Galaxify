using System;
using Microsoft.Xna.Framework;

namespace TestGame.Content.obj;

public class Science : Object2D
{
    public Science(float speed, float scale)
    {
        SetSpeed(speed);
        SetScale(scale);
    }

    public void SetPosition(int resWidth, int resHeight)
    {
        // random position within a defined space
        var scale = GetScale();
        var texture = GetTexture();
        var width = texture.Width;
        var height = texture.Height;
        
        var rnd = new Random();
        var x = rnd.Next(width * (int)scale / 5, resWidth - width * (int)scale / 5);
        var y = rnd.Next(height * (int)scale / 5 + 5, resHeight - height * (int)scale / 5 + 5);
        var pos = new Vector2(x, y);
        SetPosition(pos);
    }
}