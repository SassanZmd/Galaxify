using Microsoft.Xna.Framework;

namespace TestGame.Content.obj.Config;

public class Graphics
{
    private int _resolutionWidth;
    private int _resolutionHeight;
    private Color _backgroundColor;

    public Graphics(int resolutionWidth, int resolutionHeight, Color backgroundColor)
    {
        _resolutionWidth = resolutionWidth;
        _resolutionHeight = resolutionHeight;
        _backgroundColor = backgroundColor;
    }

    public (int width, int height) GetResolution()
    {
        return (_resolutionWidth, _resolutionHeight);
    }

    public Color GetColor()
    {
        return _backgroundColor;
    }
    
    public string GetColorName()
    {
        var color = System.Drawing.Color.FromArgb(_backgroundColor.A, _backgroundColor.R,
            _backgroundColor.G, _backgroundColor.B);
        return color.Name;
    }

    public void SetResolution(int width, int height)
    {
        _resolutionWidth = width;
        _resolutionHeight = height;
    }

    public void SetColor(Color color)
    {
        _backgroundColor = color;
    }
}