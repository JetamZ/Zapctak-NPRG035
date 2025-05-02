using ColorRepresentation;
using ImageRepersentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiArtGeneration
{
    public class AsciiArtGenerator {
        public static string generate(string imagePath, GeneratorConfig configuration) {
            try {
                ImageWrapper image = new ImageWrapper(imagePath);
                return generate(image, configuration);
            } catch (IOException ex) {
                Console.WriteLine("Error loading image from file");
                Console.WriteLine(ex.Message);
                return "";
            }
        }
        public static string generate(ImageWrapper image, GeneratorConfig configuration) {
            var symbols = configuration.getSymbols();
            int symbolGap = 255 / symbols.Length;

            StringBuilder asciiImage = new StringBuilder();
            ImageWrapper scaledImage = image.getScaledVersion(configuration.scale);
            scaledImage.pixels.applyToAllPixels(pixel => {
                PixelColor color = pixel.color;
                int red = color.redComponent.colorValue;
                int green = color.greenComponent.colorValue;
                int blue = color.blueComponent.colorValue;
                int brightness = configuration.brightnessCalculator.getPixelRepresentation(red, green, blue);

                string symbol = getSymbol(brightness, symbolGap,symbols);

                asciiImage.Append(symbol);
                if  (scaledImage.isHorizontalBorderPixel(pixel.x) && pixel.x != 0) {
                    asciiImage.Append('\n');
                }
            });
            return asciiImage.ToString();
        }
        private static string getSymbol(int brightness, int symbolGap, string[] symbols) {
            int symbolIndex = brightness / symbolGap;
            int symbolLastIndex = symbols.Length - 1;
            if (symbolIndex < 0) { symbolIndex = 0; }
            if (symbolIndex > symbolLastIndex) { symbolIndex = symbolLastIndex; }
            return symbols[symbolIndex];
        }
    }
}
