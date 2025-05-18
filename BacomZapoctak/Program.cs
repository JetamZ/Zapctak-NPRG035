using AsciiArtGeneration;
using RealTimeCamera;
using System.IO;
using Spectre.Console;
namespace BacomZapoctak
{
    /// <summary>
    /// Main class of the application.
    /// Contains entry-point.
    /// </summary>
    class Program {
        /// <summary>
        /// Method for handling interactive run, when no path to configuration file is provided.
        /// </summary>
        private static void runInteractive() { 
            var config = InteractiveConfigBuilder.makeConfiguration();
            if (config.realTime) {
                RealTime.work(config);
                return;
            } else {
                Console.WriteLine("Please enter the path to the image you wish to convert (default: images/lion.jpg): ");
                var response = Console.ReadLine();
                var imagePath = (response == "") ? "images/lion.png" : response;
                try {
                    var ASCIIart = AsciiArtGenerator.generate(imagePath, config);
                    Console.WriteLine("Would you like to save the ascii art ? If yes enter destination path please:");
                    var target = Console.ReadLine();
                    
                    if (target == "" || target==null) {
                        Console.WriteLine(ASCIIart);
                    }
                    try {
                        File.WriteAllText(target, ASCIIart);
                    }
                    catch (IOException e){
                        Console.WriteLine("Error saving the ascii art.");
                    }
                }
                catch (IOException e) {
                    Console.WriteLine($"Error reading {imagePath}");
                }
                

            }
        }
        /// <summary>
        /// Entry point of the application.
        /// Based on the command-line arguments decide how to run th eapplication.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args) {
            GeneratorConfig config = new GeneratorConfig();
            if (args.Length == 0) { 
                runInteractive();
            }
            else {
                config = AsciiConfigBuilder.readConfiguration(args[0]);
                if (config.realTime) {
                    RealTime.work(config);
                    return;
                }
                if (args.Length == 2)
                {
                    var sourcePath = args[1];
                    Console.WriteLine(AsciiArtGenerator.generate(sourcePath, config));
                }
                else if (args.Length == 3)
                {
                    var sourcePath = args[1];
                    var outputPath = args[2];
                    using (var sr = new StreamWriter(outputPath))
                    {
                        sr.WriteLine(AsciiArtGenerator.generate(sourcePath, config));
                    }
                }
                else { 
                    Console.WriteLine("Please see documentation for running options.");
                    return;
                }
            }
        }
    }
}
