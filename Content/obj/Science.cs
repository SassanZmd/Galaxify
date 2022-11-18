using Microsoft.Xna.Framework;

namespace TestGame.Content.obj;

public class Science : Object2D
{
    public Science(float speed, float scale)
    {
        SetSpeed(speed);
        SetScale(scale);
    }

    public new Vector2 GetPosition()
    {
        return Vector2.One;
    }
}