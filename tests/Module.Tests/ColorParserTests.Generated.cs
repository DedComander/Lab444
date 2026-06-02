// AUTO-GENERATED TESTS. DO NOT EDIT MANUALLY.
// Source: spec/colorparser.yaml
using System;
using NUnit.Framework;
using Module.Core;

namespace Module.Tests
{
    [TestFixture]
    public class ColorParserTestsGenerated
    {
        private ColorParser _sut;
        [SetUp] public void SetUp() => _sut = new ColorParser();
        [Test]
        [Description("Black")]
        public void Test_RgbToHex_Black()
        {
            // Pre: r >= 0 && r <= 255 && g >= 0 && g <= 255 && b >= 0 && b <= 255
            var result = _sut.RgbToHex(0, 0, 0);
            Assert.AreEqual("#000000", result);
        }
        [Test]
        [Description("White")]
        public void Test_RgbToHex_White()
        {
            // Pre: r >= 0 && r <= 255 && g >= 0 && g <= 255 && b >= 0 && b <= 255
            var result = _sut.RgbToHex(255, 255, 255);
            Assert.AreEqual("#FFFFFF", result);
        }
        [Test]
        [Description("Red")]
        public void Test_RgbToHex_Red()
        {
            // Pre: r >= 0 && r <= 255 && g >= 0 && g <= 255 && b >= 0 && b <= 255
            var result = _sut.RgbToHex(255, 0, 0);
            Assert.AreEqual("#FF0000", result);
        }
        [Test]
        [Description("Invalid negative")]
        public void Test_RgbToHex_Invalid_negative()
        {
            // Pre: r >= 0 && r <= 255 && g >= 0 && g <= 255 && b >= 0 && b <= 255
            Assert.Throws<ArgumentException>(() => _sut.RgbToHex(-1, 0, 0));
            // Exception expected
        }
        [Test]
        [Description("Invalid overflow")]
        public void Test_RgbToHex_Invalid_overflow()
        {
            // Pre: r >= 0 && r <= 255 && g >= 0 && g <= 255 && b >= 0 && b <= 255
            Assert.Throws<ArgumentException>(() => _sut.RgbToHex(0, 256, 0));
            // Exception expected
        }
        [Test]
        [Description("Red uppercase")]
        public void Test_HexToRgb_Red_uppercase()
        {
            // Pre: hex != null && hex matches valid HEX pattern
            var result = _sut.HexToRgb("#FF0000");
            Assert.AreEqual(new int[] { 255, 0, 0 }, result);
        }
        [Test]
        [Description("Black")]
        public void Test_HexToRgb_Black()
        {
            // Pre: hex != null && hex matches valid HEX pattern
            var result = _sut.HexToRgb("#000000");
            Assert.AreEqual(new int[] { 0, 0, 0 }, result);
        }
        [Test]
        [Description("Lowercase")]
        public void Test_HexToRgb_Lowercase()
        {
            // Pre: hex != null && hex matches valid HEX pattern
            var result = _sut.HexToRgb("#ffffff");
            Assert.AreEqual(new int[] { 255, 255, 255 }, result);
        }
        [Test]
        [Description("Null input")]
        public void Test_HexToRgb_Null_input()
        {
            // Pre: hex != null && hex matches valid HEX pattern
            Assert.Throws<ArgumentException>(() => _sut.HexToRgb(null));
            // Exception expected
        }
        [Test]
        [Description("Empty input")]
        public void Test_HexToRgb_Empty_input()
        {
            // Pre: hex != null && hex matches valid HEX pattern
            Assert.Throws<ArgumentException>(() => _sut.HexToRgb(""));
            // Exception expected
        }
        [Test]
        [Description("Invalid format")]
        public void Test_HexToRgb_Invalid_format()
        {
            // Pre: hex != null && hex matches valid HEX pattern
            Assert.Throws<ArgumentException>(() => _sut.HexToRgb("#12"));
            // Exception expected
        }
        [Test]
        [Description("Known color uppercase")]
        public void Test_GetColorName_Known_color_uppercase()
        {
            // Pre: hex != null && hex != empty
            var result = _sut.GetColorName("#FF0000");
            Assert.AreEqual("Red", result);
        }
        [Test]
        [Description("Known color lowercase")]
        public void Test_GetColorName_Known_color_lowercase()
        {
            // Pre: hex != null && hex != empty
            var result = _sut.GetColorName("#ff0000");
            Assert.AreEqual("Red", result);
        }
        [Test]
        [Description("Unknown color")]
        public void Test_GetColorName_Unknown_color()
        {
            // Pre: hex != null && hex != empty
            var result = _sut.GetColorName("#123456");
            Assert.AreEqual("Unknown", result);
        }
        [Test]
        [Description("Null input")]
        public void Test_GetColorName_Null_input()
        {
            // Pre: hex != null && hex != empty
            Assert.Throws<ArgumentException>(() => _sut.GetColorName(null));
            // Exception expected
        }
        [Test]
        [Description("Empty input")]
        public void Test_GetColorName_Empty_input()
        {
            // Pre: hex != null && hex != empty
            Assert.Throws<ArgumentException>(() => _sut.GetColorName(""));
            // Exception expected
        }

    }
}
