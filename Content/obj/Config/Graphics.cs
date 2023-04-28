using Microsoft.Xna.Framework;
using System.Drawing;
using System;
using Color = Microsoft.Xna.Framework.Color;
using Color2 = System.Drawing.Color;

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
        string defaultName = "Unknown";
        foreach (KnownColor kc in Enum.GetValues(typeof(KnownColor)))
        {
            var known = Color2.FromKnownColor(kc);
            if (_backgroundColor.A == known.A && _backgroundColor.R == known.R && _backgroundColor.B == known.B && _backgroundColor.G == known.G)
            {
                return known.Name;
            }
        }

        return defaultName;
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