using Microsoft.Xna.Framework;

namespace TestGame.Content.obj;

public class Ball : Object2D
{
    private float _diagonalSpeed;

    public Ball(string textureName, Vector2 position, float speed, float scale, float diagonalSpeed, 
        float ballCollOffset)
    {
        SetTextureName(textureName);
        SetPosition(position);
        SetSpeed(speed);
        SetDiagonalSpeed(diagonalSpeed);
        SetScale(scale);
        SetCollisionOffset(ballCollOffset);
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