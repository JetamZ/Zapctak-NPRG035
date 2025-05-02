using BrightnessCalculation;
using ImageRepersentation;
using System.Collections;
using System.Runtime.ExceptionServices;
using System.Text;

namespace AsciiArtGeneration
{
    public class GeneratorConfig {
        public PixelScale scale { get; set; } = new PixelScale();
        public string[] symbols {get; set; }
        public IBrightnessCalculator brightnessCalculator { get; set; }
        public bool isReversedSymbol;
        public bool realTime;

        public string[] getSymbols() { 
            return this.isReversedSymbol ? reverseArray(symbols) : symbols; 
        }
        private string[] reverseArray(string[] array) {
            List<string> reversedArray = new List<string>();
            for (int i = array.Length - 1; i > -1; i--) { 
                reversedArray.Add(array[i]);
            }
            return reversedArray.ToArray(); 

        }
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
