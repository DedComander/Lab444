namespace Module.Core;
using System;
using System.Collections.Generic;
using System.Globalization;

public class ColorParser : IColorParser
{
    public string RgbToHex(int r, int g, int b)
    {
        if (r < 0 || r > 255)
            throw new ArgumentException("Red component must be in range 0-255.", nameof(r));
        if (g < 0 || g > 255)
            throw new ArgumentException("Green component must be in range 0-255.", nameof(g));
        if (b < 0 || b > 255)
            throw new ArgumentException("Blue component must be in range 0-255.", nameof(b));
        return $"#{r:X2}{g:X2}{b:X2}";
    }

    public int[] HexToRgb(string hex)
    {
        if (string.IsNullOrEmpty(hex))
            throw new ArgumentException("HEX string cannot be null or empty.", nameof(hex));

        string cleanHex = hex.StartsWith("#") ? hex.Substring(1) : hex;

        if (cleanHex.Length != 3 && cleanHex.Length != 6)
            throw new ArgumentException("HEX string must be 3 or 6 characters long (without #).", nameof(hex));

        if (cleanHex.Length == 3)
        {
            cleanHex = $"{cleanHex[0]}{cleanHex[0]}{cleanHex[1]}{cleanHex[1]}{cleanHex[2]}{cleanHex[2]}";
        }

        foreach (char c in cleanHex)
        {
            if (!Uri.IsHexDigit(c))
                throw new ArgumentException($"Invalid HEX character: {c}.", nameof(hex));
        }

        int r = int.Parse(cleanHex.Substring(0, 2), NumberStyles.HexNumber);
        int g = int.Parse(cleanHex.Substring(2, 2), NumberStyles.HexNumber);
        int b = int.Parse(cleanHex.Substring(4, 2), NumberStyles.HexNumber);

        return new int[] { r, g, b };
    }

    public string GetColorName(string hex)
    {
        if (string.IsNullOrEmpty(hex))
            throw new ArgumentException("HEX string cannot be null or empty.", nameof(hex));

        string cleanHex = hex.StartsWith("#") ? hex.Substring(1) : hex;
        if (cleanHex.Length == 3)
        {
            cleanHex = $"{cleanHex[0]}{cleanHex[0]}{cleanHex[1]}{cleanHex[1]}{cleanHex[2]}{cleanHex[2]}";
        }

        var colorMap = new Dictionary<string, string>
        {
            { "000000", "Black" }, { "FFFFFF", "White" }, { "FF0000", "Red" },
            { "00FF00", "Green" }, { "0000FF", "Blue" }, { "FFFF00", "Yellow" },
            { "00FFFF", "Cyan" }, { "FF00FF", "Magenta" }
        };

        return colorMap.ContainsKey(cleanHex) ? colorMap[cleanHex] : "Unknown";
    }
}