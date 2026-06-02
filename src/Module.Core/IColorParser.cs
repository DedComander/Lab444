namespace Module.Core;

public interface IColorParser
{
    string RgbToHex(int r, int g, int b);
    int[] HexToRgb(string hex);
    string GetColorName(string hex);
}