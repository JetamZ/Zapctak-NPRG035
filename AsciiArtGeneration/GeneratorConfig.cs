using BrightnessCalculation;
using ImageRepersentation;
using System.Collections;
using System.Runtime.ExceptionServices;
using System.Text;

namespace AsciiArtGeneration
{
    /// <summary>
    /// A class for holding a configuration of ascii art generation process.
    /// </summary>
    public class GeneratorConfig {
        /// <summary>
        /// Scale of the resulting ascii art
        /// <see cref="PixelScale">
        /// </summary>
        public PixelScale scale { get; set; } = new PixelScale();
        /// <summary>
        /// Symbol pool for ascii art.
        /// </summary>
        public string[] symbols {get; set; }
        /// <summary>
        /// Algorithom for calculating brightness of a pixel.
        /// </summary>
        public IBrightnessCalculator brightnessCalculator { get; set; }
        /// <summary>
        /// Reversed flag - birghtest symbol -> darkest pixel.
        /// </summary>
        public bool isReversedSymbol;
        /// <summary>
        /// Real Time flag.
        /// </summary>
        public bool realTime;
        /// <summary>
        /// Method for getting symbol pool.
        /// </summary>
        /// <returns>Based on the isReversedSymbol flag, returns the symbol pool or reversed symbol pool</returns>
        public string[] getSymbols() { 
            return this.isReversedSymbol ? reverseArray(symbols) : symbols; 
        }
        /// <summary>
        /// Helper method for reversing an array.
        /// </summary>
        /// <param name="array">Array to be reversed.</param>
        /// <returns>Reversed array.</returns>
        private string[] reverseArray(string[] array) {
            List<string> reversedArray = new List<string>();
            for (int i = array.Length - 1; i > -1; i--) { 
                reversedArray.Add(array[i]);
            }
            return reversedArray.ToArray(); 

        }
        /// <summary>
        /// ToString override, for serialization of configuration object into
        /// a configuration file.
        /// </summary>
        /// <returns>String representation of configuration object compatible with configuration file
        /// format.</returns>
        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            var rt = realTime ? "T" : "F";
            sb.AppendLine($"Real Time:{rt}");
            var bc = brightnessCalculator.GetType().Name == "HumanEyeAlgorithm" ? "1" : "2";
            sb.AppendLine($"Brightness Calculation:{bc}");
            var usedSymbols = "";
            foreach (var symbol in symbols) { 
                usedSymbols += symbol.ToString();
            }
            sb.AppendLine($"Symbols:{usedSymbols}");
            var rs = isReversedSymbol ? "T" : "F";
            sb.AppendLine($"Reversed:{rs}");
            sb.AppendLine($"Scale:{scale.scale.ToString()}");
            sb.AppendLine($"Height:{scale.height}");
            sb.AppendLine($"Width:{scale.width}");

            return sb.ToString();
        }
    }
}
