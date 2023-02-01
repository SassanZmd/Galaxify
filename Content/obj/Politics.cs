namespace TestGame.Content.obj;

public class Politics : Object2D
{
    private int _delayMs;
    
    public Politics(string textureName, float speed, float scale, float collisionOffset, int delayMs)
    {
        SetTextureName(textureName);
        SetSpeed(speed);
        SetScale(scale);
        SetCollisionOffset(collisionOffset);

        _delayMs = delayMs;
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