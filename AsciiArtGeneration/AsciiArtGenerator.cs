using ColorRepresentation;
using ImageRepersentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiArtGeneration
{
    /// <summary>
    /// A class which is responsible for generating ascii art.
    /// Has only stsatuc methods.
    /// </summary>
    public class AsciiArtGenerator {
        /// <summary>
        /// Method for generating ascii art from provided path to source image.
        /// Constructs internal representation of the source image <see cref="ImageRepersentation.ImageWrapper"/> and passes it to 
        /// overload of this method <see cref="generate(ImageWrapper, GeneratorConfig)"/>
        /// </summary>
        /// <param name="imagePath">Path to the source image.</param>
        /// <param name="configuration">Generator configuration object</param>
        /// <returns>Ascii art as a string.</returns>
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
        /// <summary>
        /// Method for generating ascii art from provided internal image representation.
        /// Generates the ascii art string accoridng to instructions provided in configuration object
        /// <see cref="GeneratorConfig"/>.
        /// </summary>
        /// <param name="image">Internal representation of source image.</param>
        /// <param name="configuration">Generator configuration object.</param>
        /// <returns>Ascii art as a string.</returns>
        public static string generate(ImageWrapper image, GeneratorConfig configuration) {
            var symbols = configuration.getSymbols();
            int symbolGap = 255 / symbols.Length;

            StringBuilder asciiImage = new StringBuilder();
            ImageWrapper scaledImage = image.getScaledVersion(configuration.scale);
            // Iterate over each pixel of the internal image representation and 
            // Append appropriate symbol to the result string builder.
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
        /// <summary>
        /// Method for getting appropriate symbol from symbol pool based on target pixel's
        /// brightness
        /// </summary>
        /// <param name="brightness">Brightness of the pixel.</param>
        /// <param name="symbolGap">Amount of brightness values covered by a single symbol.</param>
        /// <param name="symbols">Symbol pool.</param>
        /// <returns></returns>
        private static string getSymbol(int brightness, int symbolGap, string[] symbols) {
            int symbolIndex = brightness / symbolGap;
            int symbolLastIndex = symbols.Length - 1;
            if (symbolIndex < 0) { symbolIndex = 0; }
            if (symbolIndex > symbolLastIndex) { symbolIndex = symbolLastIndex; }
            return symbols[symbolIndex];
        }
    }
}
