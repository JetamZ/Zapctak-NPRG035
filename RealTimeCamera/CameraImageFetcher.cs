using AsciiArtGeneration;
using ImageRepersentation;
using OpenCvSharp;

using Spectre.Console;

namespace RealTimeCamera
{
    /// <summary>
    /// Class for handling the real-tiem camera mode of the application.
    /// </summary>
    public class RealTime {
        /// <summary>
        /// Method for getting and resizing the image from specified camera.
        /// </summary>
        /// <param name="camera">Object of <see cref="OpenCvSharp.VideoCapture"/> class, camera from which the image is to
        /// be fetched.</param>
        /// <param name="width">Desired resizing width.</param>
        /// <param name="height">Desired resizing width.</param>
        /// <returns><see cref="ImageRepersentation.ImageWrapper"> object of the captured camera frame.</returns>
        private static ImageWrapper? getCameraImage(VideoCapture camera,int width, int height) {
            var capturedFrame = new Mat(); 
            camera.Read(capturedFrame);
            Cv2.Resize(capturedFrame, capturedFrame, new OpenCvSharp.Size(width, height));
            return new ImageWrapper(capturedFrame);
        }
        /// <summary>
        /// Main loop of the real-time mode.
        /// Tries to get the image from camera, then converts it to Ascii Art and prints it to console
        /// utilizing the Spectre Console's AnsiConsole Live Display widget.        
        /// </summary>
        /// <param name="configuration">Object of the <see cref="AsciiArtGeneration.GeneratorConfig"> holding ascii art generation configuration.</param>
        public static void work(GeneratorConfig configuration) {
            var cameraCapture = new VideoCapture(0);
            if (!cameraCapture.IsOpened()) {
                Console.WriteLine("Error: Camera not accessible.");
                return;
            }
            var initial = new Panel("Starting ...").Expand();
            AnsiConsole.Live(initial)
                .AutoClear(true)
                .Overflow(VerticalOverflow.Ellipsis)
                .Cropping(VerticalOverflowCropping.Top)
                .Start(ctx =>
                {
                    while (true)
                    {
                        var image = getCameraImage(cameraCapture, configuration.scale.width, configuration.scale.height);
                        string asciiArt = AsciiArtGenerator.generate(image, configuration);
                        var panel = new Panel(asciiArt).Header("Your beautiful face:").Border(BoxBorder.Rounded).Expand();
                        ctx.UpdateTarget(panel);
                        
                        Thread.Sleep(50);
                    }
                });
            
        }
    }
}
