using NUnit.Framework;
using Module.Core;
using System;

namespace Module.Tests;

[TestFixture]
public class ColorParserTests
{
    private IColorParser _parser = null!;

    [SetUp]
    public void Setup() => _parser = new ColorParser();

    // ---------- RgbToHex ----------
    [Test]
    public void RgbToHex_Red_ReturnsCorrectHex() =>
        Assert.AreEqual("#FF0000", _parser.RgbToHex(255, 0, 0));

    [Test]
    public void RgbToHex_Green_ReturnsCorrectHex() =>
        Assert.AreEqual("#00FF00", _parser.RgbToHex(0, 255, 0));

    [Test]
    public void RgbToHex_Blue_ReturnsCorrectHex() =>
        Assert.AreEqual("#0000FF", _parser.RgbToHex(0, 0, 255));

    [Test]
    public void RgbToHex_White_ReturnsCorrectHex() =>
        Assert.AreEqual("#FFFFFF", _parser.RgbToHex(255, 255, 255));

    [Test]
    public void RgbToHex_Black_ReturnsCorrectHex() =>
        Assert.AreEqual("#000000", _parser.RgbToHex(0, 0, 0));

    [TestCase(-1, 0, 0)]
    [TestCase(0, 256, 0)]
    [TestCase(0, 0, 300)]
    public void RgbToHex_OutOfRange_ThrowsArgumentException(int r, int g, int b) =>
        Assert.Throws<ArgumentException>(() => _parser.RgbToHex(r, g, b));

    // ---------- HexToRgb ----------
    [Test]
    public void HexToRgb_Red_ReturnsCorrectRgb()
    {
        var rgb = _parser.HexToRgb("#FF0000");
        Assert.AreEqual(new[] { 255, 0, 0 }, rgb);
    }

    [Test]
    public void HexToRgb_Black_ReturnsCorrectRgb()
    {
        var rgb = _parser.HexToRgb("#000000");
        Assert.AreEqual(new[] { 0, 0, 0 }, rgb);
    }

    [Test]
    public void HexToRgb_Null_ThrowsArgumentException() =>
        Assert.Throws<ArgumentException>(() => _parser.HexToRgb(null!));

    [Test]
    public void HexToRgb_Empty_ThrowsArgumentException() =>
        Assert.Throws<ArgumentException>(() => _parser.HexToRgb(""));

    [Test]
    public void HexToRgb_InvalidFormat_ThrowsArgumentException() =>
        Assert.Throws<ArgumentException>(() => _parser.HexToRgb("#12"));

    // ---------- GetColorName ----------
    [Test]
    public void GetColorName_RedUpperCase_ReturnsRed() =>
        Assert.AreEqual("Red", _parser.GetColorName("#FF0000"));

    [Test]
    public void GetColorName_RedLowerCase_ReturnsRed() =>
        Assert.AreEqual("Red", _parser.GetColorName("#ff0000"));

    [Test]
    public void GetColorName_Unknown_ReturnsUnknown() =>
        Assert.AreEqual("Unknown", _parser.GetColorName("#123456"));

    [Test]
    public void GetColorName_Null_ThrowsArgumentException() =>
        Assert.Throws<ArgumentException>(() => _parser.GetColorName(null!));

    [Test]
    public void GetColorName_Empty_ThrowsArgumentException() =>
        Assert.Throws<ArgumentException>(() => _parser.GetColorName(""));
}