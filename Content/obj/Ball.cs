using static TestGame.Content.obj.Config;

namespace TestGame.Content.obj;

public class Ball : Object2D
{
    private float _diagonalSpeed;

    public Ball()
    {
        SetPosition(BallPosition);
        SetSpeed(BallSpeed);
        SetDiagonalSpeed(BallDiagonalSpeed);
        SetScale(BallScale);
        SetCollisionOffset(BallCollisionOffset);
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