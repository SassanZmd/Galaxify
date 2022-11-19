using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestGame.Content.obj;

public class Object2D
{
    private Texture2D _texture;
    private Vector2 _position;
    private float _speed;
    private float _scale;
    private bool _destroyed;

    public void SetTexture(Texture2D texture)
    {
        _texture = texture;
    }

    public void SetPosition(Vector2 pos)
    {
        _position = pos;
    }

    protected void SetSpeed(float speed)
    {
        _speed = speed;
    }

    protected void SetScale(float scale)
    {
        _scale = scale;
    }

    protected void SelfDestruct()
    {
        _destroyed = true;
    }

    public Texture2D GetTexture()
    {
        return _texture;
    }
    
    public Vector2 GetPosition()
    {
        return _position;
    }

    public float GetSpeed()
    {
        return _speed;
    }

    public float GetScale()
    {
        return _scale;
    }
    
    public bool isDestroyed()
    {
        return _destroyed;
    }
}