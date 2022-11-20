using Microsoft.Xna.Framework;

namespace TestGame.Content.obj;

public class Ball : Object2D
{
    private float _diagonalSpeed;

    public Ball(Vector2 pos, float speed, float diagonalSpeed, float scale, float collOffset)
    {
        SetPosition(pos);
        SetSpeed(speed);
        SetDiagonalSpeed(diagonalSpeed);
        SetScale(scale);
        SetCollisionOffset(collOffset);
    }

    private void SetDiagonalSpeed(float diagonalSpeed)
    {
        _diagonalSpeed = diagonalSpeed;
    }

    public float GetDiagonalSpeed()
    {
        return _diagonalSpeed;
    }
}