using Microsoft.Xna.Framework;

namespace TestGame.Content.obj;

public class Ball : Object2D
{
    private float _diagonalSpeed;

    public Ball(Vector2 pos, float speed, float diagonalSpeed, float scale)
    {
        SetPosition(pos);
        SetSpeed(speed);
        SetDiagonalSpeed(diagonalSpeed);
        SetScale(scale);
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