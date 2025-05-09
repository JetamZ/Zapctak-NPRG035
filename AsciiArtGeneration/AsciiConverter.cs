using BrightnessCalculation;
using ImageRepersentation;
using SymbolRepresentation;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace AsciiArtGeneration
{
    /// <summary>
    /// Class for building the ascii generation configuration object.
    /// Contains only static fields and static methods.
    /// </summary>
    public class AsciiConfigBuilder {
        static GeneratorConfig configuration = new GeneratorConfig();
        /// <summary>
        /// A method for parsing a configuration file and building the <see cref="GeneratorConfig"> object from it.
        /// </summary>
        /// <param name="filePath">Path to the configuration file.</param>
        /// <returns>Configuration object.</returns>
        public static GeneratorConfig readConfiguration(string filePath) {
            using StreamReader sr = new StreamReader(filePath);
            string? line;
            while ((line = sr.ReadLine()) != null) {
                string[] configLine = line.Split(':');
                switch (configLine[0]) {
                    case "Real Time":
                        configuration.realTime = configLine[1] == "T" ? true : false;
                        break;
                    case "Brightness Calculation":
                        if (configLine[1] == "1") {
                            configuration.brightnessCalculator = new HumanEyeAlgorithm();
                        }
                        else if (configLine[1] == "2") {
                            configuration.brightnessCalculator = new RGBAlgorithm();
                        }
                        break;
                    case "Symbols":
                        if (configLine[1].Contains("Auto")) {
                            string[] parts = configLine[1].Split(';');

                            string[] chars = getUTFChars(32, 162);

                            configuration.symbols = BestSymbolPatternFinder.findBestPattern(int.Parse(parts[2]), int.Parse(parts[1]), chars).toStringArray();
                        } else {
                            string[] symbols = new string[configLine[1].Length];
                            for (int i = 0; i < configLine[1].Length; i++) {
                                symbols[i] = configLine[1][i].ToString();
                            }
                            
                            configuration.symbols = BestSymbolPatternFinder.findBestPattern(4, configLine[1].Length, symbols).toStringArray();
                        }
                        break;
                    case "Reversed":
                        configuration.isReversedSymbol = false;
                        if (configLine[1] == "T") {
                            configuration.isReversedSymbol = true;
                        }
                        break;
                    case "Scale":
                        configuration.scale = new PixelScale();
                        switch (configLine[1]) {
                            case "DEFAULT":
                                configuration.scale.scale = Scale.DEFAULT;
                                break;
                            case "FAST":
                                configuration.scale.scale = Scale.FAST;
                                break;
                            case "SMOOTH":
                                configuration.scale.scale = Scale.SMOOTH;
                                break;
                            default:
                                configuration.scale.scale = Scale.DEFAULT;
                                break;
                        }
                        break;
                    case "Width":
                        configuration.scale.width = int.Parse(configLine[1]);
                        break;
                    case "Height":
                        configuration.scale.height = int.Parse(configLine[1]);
                        break;
                }
            }
            return configuration;
        }
        /// <summary>
        /// Method for getting UTF characters of given range.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="finish"></param>
        /// <returns>List of UTF characters.</returns>
        public static string[] getUTFChars(int start, int finish) {
            finish++;
            string[] utfChars = new string[finish - start];
            for (int i = start; i < finish; i++) {
                utfChars[i - start] = ((char)i).ToString();
            }
            return utfChars;
        }
    }
}
