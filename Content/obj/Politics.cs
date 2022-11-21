namespace TestGame.Content.obj;
using static Config;

public class Politics : Object2D
{
    public Politics()
    {
        SetSpeed(PoliticsSpeed);
        SetScale(PoliticsScale);
        SetCollisionOffset(PoliticsCollisionOffset);
    }
}