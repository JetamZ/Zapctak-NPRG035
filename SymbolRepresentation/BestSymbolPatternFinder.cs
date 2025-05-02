using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ColorRepresentation;
using BrightnessCalculation;
using System.Security.Principal;

namespace SymbolRepresentation
{
    public class BestSymbolPatternFinder {
        public static SymbolList findBestPattern(params string[] characters) {
            return findBestPattern(255, characters);
        }
        public static SymbolList findBestPattern(int maxSymbols, params string[] characters) {
            return findBestPattern(10, maxSymbols, characters);
        }
        public static SymbolList findBestPattern(int symbolAccuracy, int maxSymbols, params string[] characters) {
            SymbolList symbolList = new SymbolList(maxSymbols, symbolAccuracy);
            foreach (string character in characters) {
                if (symbolList.size() > maxSymbols) { break; }
                var averageBrightness = getCharacterBrightness(character);
                symbolList.AddSymbol(new Symbol(character, averageBrightness));
            }
            return symbolList;
        }
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
