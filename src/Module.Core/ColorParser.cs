namespace Module.Core;
using System;
using System.Collections.Generic;

public class ColorParser : IColorParser
{
    private readonly Dictionary<string, string> _colors = new()
    {
        { "#000000", "Black" }, { "#FFFFFF", "White" }, { "#FF0000", "Red" },
        { "#00FF00", "Green" }, { "#0000FF", "Blue" }, { "#FFFF00", "Yellow" },
        { "#00FFFF", "Cyan" }, { "#FF00FF", "Magenta" }
    };

    public string RgbToHex(int r, int g, int b)
    {
        if (r < 0 || r > 255 || g < 0 || g > 255 || b < 0 || b > 255)
            throw new ArgumentException("RGB values must be 0-255");
        return $"#{r:X2}{g:X2}{b:X2}";
    }

    public int[] HexToRgb(string hex)
    {
        if (string.IsNullOrEmpty(hex) || !hex.StartsWith("#") || hex.Length != 7)
            throw new ArgumentException("Invalid HEX format");
        return new int[]
        {
            Convert.ToInt32(hex.Substring(1, 2), 16),
            Convert.ToInt32(hex.Substring(3, 2), 16),
            Convert.ToInt32(hex.Substring(5, 2), 16)
        };
    }

    public string GetColorName(string hex)
    {
        return _colors.TryGetValue(hex?.ToUpper() ?? "", out var name) ? name : "Unknown";
    }
}