using BrightnessCalculation;
using ImageRepersentation;
using SymbolRepresentation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiArtGeneration
{
    public class InteractiveConfigBuilder {
        public static GeneratorConfig makeConfiguration() { 
            GeneratorConfig config = new GeneratorConfig();
            config.realTime = askRealTime();
            config.brightnessCalculator = askBrightnessCalculator();
            config.symbols = askSymbols();
            config.scale.scale = askScale();
            config.isReversedSymbol = askReversed();
            config.scale.height = askHeight();
            config.scale.width = askWidth();
            return config;
        }
        private static bool askRealTime() {
            Console.WriteLine("Would you like to start the application in real-time mode? [Y/n]");
            var response = Console.ReadLine().ToLower().Trim();
            while (response != "y" && response != "n") {
                Console.WriteLine("Please enter 'Y' if you wish to start in real-time mode, 'n' else");
                response = Console.ReadLine().ToLower().Trim();
            }
            return response == "y";
        }
        private static IBrightnessCalculator askBrightnessCalculator() {
            IBrightnessCalculator bc;
            Console.WriteLine("Please select the pixel (group) brightness calculation method: ");
            Console.WriteLine("    1 - Human eye method");
            Console.WriteLine("    2 - Most significant RGB component method");
            var response = Console.ReadLine().ToLower().Trim();
            while (response != "1" && response != "2") { 
                Console.WriteLine("Please enter either 1 (Human eye) or 2 (Most significant RGB component)");
            }
            bc = (response == "1") ? new HumanEyeAlgorithm() : new RGBAlgorithm();
            return bc;
        }
        private static string[] askSymbols() { 
            Console.WriteLine("Please enter the symbols you would lie to use as a single string or 'auto' to use automatically generated symbols: ");
            var response = Console.ReadLine();
            if (response.ToLower().Trim() == "auto") {
                Console.WriteLine("Please enter a number of symbols to be used (this corresponds to 'saturation' of resulting ascii art)");
                var number = Console.ReadLine();
                while (!int.TryParse(number, out _)) {
                    Console.WriteLine("Please enter an integer: ");
                    number = Console.ReadLine();
                }
                int num = int.Parse(number);
                string[] chars = AsciiConverter.getUTFChars(32, 162);
                return BestSymbolPatternFinder.findBestPattern(1, num, chars).toStringArray();
            } else {
                string[] symbols = new string[response.Length];
                for (int i = 0; i < response.Length; i++) { 
                    symbols[i] = response[i].ToString();
                }
                return symbols;
            }
        }
        private static Scale askScale() {
            Console.WriteLine("Please select from the below scaling options");
            Console.WriteLine("   1 - DEFAULT");
            Console.WriteLine("   2 - FAST");
            Console.WriteLine("   3 - SMOOTH");
            Console.WriteLine("   4 - REPLICATE");
            Console.WriteLine("   5 - AVERAGE_PIXEL");
            Console.WriteLine("For more information on these options please check the documentation.");
            var response = Console.ReadLine().ToLower().Trim();
            while (response != "1" && response != "2" && response != "3" && response != "4" && response != "5") {
                Console.WriteLine("Please choose one of above mentioned options.");
            }
            switch (response) {
                case "1":
                    return Scale.DEFAULT;
                case "2":
                    return Scale.FAST;
                case "3":
                    return Scale.SMOOTH;
                default:
                    return Scale.DEFAULT;
            }
        }
        private static bool askReversed() {
            Console.WriteLine("Would you like the brightness to be reversed (darkest pixel = brightest character)? [Y/n]");
            var response = Console.ReadLine();
            while (response != "y" && response != "n")
            {
                Console.WriteLine("Please enter 'Y' if you wish to start in real-time mode, 'n' else");
                response = Console.ReadLine().ToLower().Trim();
            }
            return response == "y";
        }
        private static int askWidth() {
            Console.WriteLine("Please enter width of the desired ascii art image (defaul 200):");
            var response = Console.ReadLine().ToLower().Trim();
            while (!int.TryParse(response, out _) && response != "") {
                Console.WriteLine("Please enter a number or nothing if you are okay with default value");
                response = Console.ReadLine().ToLower().Trim();
            }
            return int.TryParse(response, out _) ? int.Parse(response) : 200;
        }
        private static int askHeight() {
            Console.WriteLine("Please enter height of the desired ascii art image (default 100):");
            var response = Console.ReadLine().ToLower().Trim();
            while (!int.TryParse(response, out _) && response != "") {
                Console.WriteLine("Please enter a number or nothing if you are okay with default value");
                response = Console.ReadLine().ToLower().Trim();
            }
            return int.TryParse(response, out _) ? int.Parse(response) : 100;
        }
    }
}
