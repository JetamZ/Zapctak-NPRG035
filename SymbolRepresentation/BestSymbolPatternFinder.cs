using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ColorRepresentation;
using BrightnessCalculation;
using System.Security.Principal;
using OpenCvSharp;

namespace SymbolRepresentation
{
    /// <summary>
    /// Class which handles generating the symbol pool for ascii art as an instance of <see cref="SymbolList"/> class.
    /// </summary>
    public class BestSymbolPatternFinder {
        /// <summary>
        /// First overload of the findBestPattern() method, which simply finds
        /// best symbols from provided characters by calling second overload.
        /// </summary>
        /// <param name="characters">List of characters from among the symbol pool is to be chosen</param>
        /// <returns>Symbol pool.</returns>
        public static SymbolList findBestPattern(params string[] characters) {
            return findBestPattern(255, characters);
        }
        /// <summary>
        /// Second overload of the findBestPattern method.
        /// Finds maxSymbols best symbols from provided characters and forms a Symbol pool out of them.
        /// Calls the third overlad.
        /// </summary>
        /// <param name="maxSymbols">Maximum symbols to be included in the symbol pool.</param>
        /// <param name="characters">Possible candidates to the symbol pool</param>
        /// <returns>Symbol pool.</returns>
        public static SymbolList findBestPattern(int maxSymbols, params string[] characters) {
            return findBestPattern(4, maxSymbols, characters);
        }
        /// <summary>
        /// Third and final overload of the findBestPattern() method.
        /// Finds <maxSymbols> best symbols from provided characters in a way that one symbol covers
        /// <symbolAccuracy> brightness values
        /// </summary>
        /// <param name="symbolAccuracy">The amount of brightness values convered by a singular pixel</param>
        /// <param name="maxSymbols">Maximum symbols in the symbol pool.</param>
        /// <param name="characters">Candidates for symbol pool.</param>
        /// <returns>Symbol pool.</returns>
        public static SymbolList findBestPattern(int symbolAccuracy, int maxSymbols, params string[] characters) {
            SymbolList symbolList = new SymbolList(maxSymbols, symbolAccuracy);
            foreach (string character in characters) {
                if (symbolList.size() > maxSymbols) { break; }
                var averageBrightness = getCharacterBrightness(character);
                symbolList.AddSymbol(new Symbol(character, averageBrightness));
            }
            return symbolList;
        }
        /// <summary>
        /// Method responsible for determining the 'brightness' of a character.
        /// Does it in a following way:
        ///     1. Constructs a 50x50 bitmap with white background.
        ///     2. Writes a character on this bitmap using black color, Consolas font of size 50
        ///     3. Calcualtes the average pixel brightness of the resulting bitmap and uses that
        ///        as a brightness of a character.
        /// </summary>
        /// <param name="character">character of which brightness should be calculated</param>
        /// <returns>brightness of the character.</returns>
        private static decimal getCharacterBrightness(string character) {
            int width = 50;
            int height = 50;
            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp)) {
                g.Clear(Color.White);
                using (Font font = new Font("Consolas", 50))
                using (Brush brush = new SolidBrush(Color.Black)) {
                    g.DrawString(character, font, brush, new PointF(5, 5));
                }
            }

            decimal totalBrightness = 0;
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) { 
                    Color pixel = bmp.GetPixel(x, y);
                    IBrightnessCalculator algorithm = new HumanEyeAlgorithm();
                    int gray = (int)algorithm.getPixelRepresentation(pixel.R, pixel.G, pixel.B);
                    totalBrightness += gray;
                }
            }
            decimal averageBrightness = totalBrightness / (width * height);
            return averageBrightness;
        }
    }
}
