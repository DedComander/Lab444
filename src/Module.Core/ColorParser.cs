namespace Module.Core;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class ColorParser : IColorParser
{
    private static readonly Dictionary<string, string> ColorNames =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        ["#000000"] = "Black", ["#FFFFFF"] = "White", ["#FF0000"] = "Red",
        ["#00FF00"] = "Green", ["#0000FF"] = "Blue", ["#FFFF00"] = "Yellow",
        ["#FF00FF"] = "Magenta", ["#00FFFF"] = "Cyan", ["#808080"] = "Gray",
        ["#C0C0C0"] = "Silver", ["#800000"] = "Maroon", ["#808000"] = "Olive",
        ["#008000"] = "Dark Green", ["#800080"] = "Purple", ["#008080"] = "Teal",
        ["#000080"] = "Navy", ["#FFA500"] = "Orange", ["#FFC0CB"] = "Pink",
        ["#A52A2A"] = "Brown", ["#F0F8FF"] = "Alice Blue", ["#FAEBD7"] = "Antique White",
        ["#7FFFD4"] = "Aquamarine", ["#F0FFFF"] = "Azure", ["#F5F5DC"] = "Beige",
        ["#FFE4C4"] = "Bisque", ["#FFEBCD"] = "Blanched Almond", ["#8A2BE2"] = "Blue Violet",
        ["#DEB887"] = "Burly Wood"
    };

    public string RgbToHex(int r, int g, int b)
    {
        ValidateRgbValue(r, nameof(r));
        ValidateRgbValue(g, nameof(g));
        ValidateRgbValue(b, nameof(b));
        return $"#{r:X2}{g:X2}{b:X2}";
    }

    public int[] HexToRgb(string hex)
    {
        if (string.IsNullOrWhiteSpace(hex))
            throw new ArgumentException("HEX значение не может быть пустым.", nameof(hex));

        string cleanHex = hex.Trim();

        if (!IsValidHexFormat(cleanHex))
            throw new ArgumentException($"Некорректный формат HEX: {hex}. Ожидаемые форматы: #RGB, #RRGGBB, RGB, RRGGBB", nameof(hex));

        try
        {
            string hexWithoutHash = cleanHex.TrimStart('#');
            if (hexWithoutHash.Length == 3)
                hexWithoutHash = ExpandShortHex(hexWithoutHash);

            int r = Convert.ToInt32(hexWithoutHash.Substring(0, 2), 16);
            int g = Convert.ToInt32(hexWithoutHash.Substring(2, 2), 16);
            int b = Convert.ToInt32(hexWithoutHash.Substring(4, 2), 16);

            return new int[] { r, g, b };
        }
        catch (Exception ex)
        {
            throw new ArgumentException($"Ошибка при парсинге HEX значения: {hex}", nameof(hex), ex);
        }
    }

    public string GetColorName(string hex)
    {
        if (string.IsNullOrWhiteSpace(hex))
            throw new ArgumentException("HEX значение не может быть пустым.", nameof(hex));

        try
        {
            string normalizedHex = NormalizeHexToUpper(hex);
            if (ColorNames.TryGetValue(normalizedHex, out string colorName))
                return colorName;

            if (normalizedHex.Length == 4) // #RGB
            {
                string expandedHex = ExpandShortHex(normalizedHex.Substring(1));
                if (ColorNames.TryGetValue($"#{expandedHex}", out colorName))
                    return colorName;
            }

            return "Unknown";
        }
        catch (Exception ex)
        {
            throw new ArgumentException($"Ошибка при получении названия цвета для HEX: {hex}", nameof(hex), ex);
        }
    }

    private void ValidateRgbValue(int value, string paramName)
    {
        if (value < 0 || value > 255)
            throw new ArgumentException($"RGB компонента должна быть в диапазоне 0-255. Получено значение: {value}", paramName);
    }

    private bool IsValidHexFormat(string hex)
    {
        if (string.IsNullOrEmpty(hex)) return false;
        string pattern = @"^#?([0-9A-Fa-f]{3}|[0-9A-Fa-f]{6})$";
        return Regex.IsMatch(hex, pattern);
    }

    private string NormalizeHexToUpper(string hex)
    {
        string cleanHex = hex.Trim();
        if (!cleanHex.StartsWith("#"))
            cleanHex = "#" + cleanHex;
        return cleanHex.ToUpperInvariant();
    }

    private string ExpandShortHex(string shortHex)
    {
        if (shortHex.Length != 3) return shortHex;
        return $"{shortHex[0]}{shortHex[0]}{shortHex[1]}{shortHex[1]}{shortHex[2]}{shortHex[2]}";
    }
}