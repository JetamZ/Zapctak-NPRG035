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
    /// <summary>
    /// Class for building the configuration interactively through console.
    /// </summary>
    public class InteractiveConfigBuilder {
        /// <summary>
        /// Main method for configuration construction.
        /// Sequentially offers user options for individual configuartion parameters
        /// and asks them for their choice.
        /// </summary>
        /// <returns>Configuration object.</returns>
        public static GeneratorConfig makeConfiguration() { 
            GeneratorConfig config = new GeneratorConfig();
            config.realTime = askRealTime();
            config.brightnessCalculator = askBrightnessCalculator();
            config.symbols = askSymbols(config.realTime);
            config.scale.scale = askScale();
            config.isReversedSymbol = askReversed();
            config.scale.height = askHeight();
            config.scale.width = askWidth();
            askSave(config);
            return config;
        }
        /// <summary>
        /// Ask user whether they wish to start in real time mode.
        /// </summary>
        /// <returns>True or False based on user's choice.</returns>
        public static void askSave(GeneratorConfig conf) {
            Console.WriteLine("Would you like to save the configuration you created? [Y/n]");
            var response = Console.ReadLine().ToLower().Trim();
            if (response == "y"){
                Console.WriteLine("Please enter the path to where you would like to save the configuration:");
                var targetFile = Console.ReadLine();
                try {
                    using (StreamWriter sw = new StreamWriter(targetFile)) {
                        sw.WriteLine(conf.ToString());
                    }
                }
                catch (IOException e) {
                    Console.WriteLine("Error saving the configuration");
                }
            }
            else { 
                return; 
            }
        }
        /// <summary>
        /// Ask user whether they wish to start in real time mode.
        /// </summary>
        /// <returns>True or False based on user's choice.</returns>
        private static bool askRealTime() {
            Console.WriteLine("Would you like to start the application in real-time mode? [Y/n]");
            var response = Console.ReadLine().ToLower().Trim();
            while (response != "y" && response != "n") {
                Console.WriteLine("Please enter 'Y' if you wish to start in real-time mode, 'n' else");
                response = Console.ReadLine().ToLower().Trim();
            }
            return response == "y";
        }
        /// <summary>
        /// Offers user choice of brightness calculation algorithms and asks them for their choice.
        /// </summary>
        /// <returns>Brightness calculation algorithm based on user's choice.</returns>
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
        /// <summary>
        /// Asks user for symbol pool they wish to use in generating the image.
        /// Either user can type in their symbols, or can use the automatically generated
        /// UTF symbol pool.
        /// </summary>
        /// <returns>Symbol pool</returns>
        private static string[] askSymbols(bool realTime) { 
            Console.WriteLine("Please enter the symbols you would like to use as a single string or 'auto' to use automatically generated symbols: ");
            var response = Console.ReadLine();
            if (response.ToLower().Trim() == "auto") {
                Console.WriteLine("Please enter a number of symbols to be used (this corresponds to 'saturation' of resulting ascii art)");
                var number = Console.ReadLine();
                while (!int.TryParse(number, out _)) {
                    Console.WriteLine("Please enter an integer: ");
                    number = Console.ReadLine();
                }
                int num = int.Parse(number);
                if (realTime) { 
                    num = Math.Max(num, 0);
                    num = Math.Min(num, 30);
                }
                string[] chars = AsciiConfigBuilder.getUTFChars(32, 162);
                return BestSymbolPatternFinder.findBestPattern(1, num, chars).toStringArray();
            } else {
                string[] symbols = new string[response.Length];
                for (int i = 0; i < response.Length; i++) { 
                    symbols[i] = response[i].ToString();
                }
                return symbols;
            }
        }
        /// <summary>
        /// Asks user about scaling method of the resulting ascii art.
        /// </summary>
        /// <returns>Scaling method.</returns>
        private static Scale askScale() {
            Console.WriteLine("Please select from the below scaling options");
            Console.WriteLine("   1 - DEFAULT");
            Console.WriteLine("   2 - FAST");
            Console.WriteLine("   3 - SMOOTH");
            Console.WriteLine("For more information on these options please check the documentation.");
            var response = Console.ReadLine().ToLower().Trim();
            while (response != "1" && response != "2" && response != "3") {
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
        /// <summary>
        /// Ask user whether they wish to reverse the brightness meaning.
        /// </summary>
        /// <returns>true or false based on user's choice.</returns>
        private static bool askReversed() {
            Console.WriteLine("Would you like the brightness to be reversed (darkest pixel = brightest character)? [Y/n]");
            var response = Console.ReadLine().ToLower().Trim();
            while (response != "y" && response != "n")
            {
                Console.WriteLine("Please enter 'Y' if you wish to start in real-time mode, 'n' else");
                response = Console.ReadLine().ToLower().Trim();
            }
            return response == "y";
        }
        /// <summary>
        /// Ask user about the desired width of Ascii art, defaults to 200 characters.
        /// </summary>
        /// <returns>The width.</returns>
        private static int askWidth() {
            Console.WriteLine("Please enter width of the desired ascii art image (defaul 200):");
            var response = Console.ReadLine().ToLower().Trim();
            while (!int.TryParse(response, out _) && response != "") {
                Console.WriteLine("Please enter a number or nothing if you are okay with default value");
                response = Console.ReadLine().ToLower().Trim();
            }
            return int.TryParse(response, out _) ? int.Parse(response) : 200;
        }
        /// <summary>
        /// Ask user about the desired height of Ascii art, defults to 200 characters.
        /// </summary>
        /// <returns></returns>
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
