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
    private float _collisionOffset;

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

    protected void SetCollisionOffset(float collOffset)
    {
        _collisionOffset = collOffset;
    }

    public void SelfDestruct()
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

    public float GetCollisionOffset()
    {
        return _collisionOffset;
    }
    
    public bool IsDestroyed()
    {
        return _destroyed;
    }

    public bool Collision(Object2D obj)
    {
        var pos = obj.GetPosition();
        var texture = obj.GetTexture();
        var width = texture.Width;
        var height = texture.Height;
        var collOffset = obj.GetCollisionOffset();

        var leftPoint = _position.X - (float)_texture.Width / 2 * _scale + _collisionOffset;
        var rightPoint = _position.X + (float)_texture.Width / 2 * _scale - _collisionOffset;
        var topPoint = _position.Y - (float)_texture.Height / 2 * _scale + _collisionOffset;
        var bottomPoint = _position.Y + (float)_texture.Height / 2 * _scale - _collisionOffset;
        var objLeftPoint = pos.X - (float)width / 2 * obj.GetScale() + collOffset;
        var objRightPoint = pos.X + (float)width / 2 * obj.GetScale() - collOffset;
        var objTopPoint = pos.Y - (float)height / 2 * obj.GetScale() + collOffset;
        var objBottomPoint = pos.Y + (float)height / 2 * obj.GetScale() - collOffset;

        return !(leftPoint > objRightPoint || rightPoint < objLeftPoint) 
               && !(topPoint > objBottomPoint || bottomPoint < objTopPoint);
    }

    protected void ResetDestruction()
    {
        _destroyed = false;
    }
}