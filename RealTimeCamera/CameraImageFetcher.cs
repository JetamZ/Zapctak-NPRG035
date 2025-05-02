using AsciiArtGeneration;
using ImageRepersentation;
using OpenCvSharp;

using Spectre.Console;

namespace RealTimeCamera
{
    public class RealTime {
        private static ImageWrapper? getCameraImage(VideoCapture camera,int width, int height) {
            var capturedFrame = new Mat(); 
            camera.Read(capturedFrame);
            Cv2.Resize(capturedFrame, capturedFrame, new OpenCvSharp.Size(width, height));
            return new ImageWrapper(capturedFrame);
        }
        public static void work(GeneratorConfig configuration) {
            var cameraCapture = new VideoCapture(0);
            if (!cameraCapture.IsOpened()) {
                Console.WriteLine("Error: Camera not accessible.");
                return;
            }
            var initial = new Panel("Starting ...").Expand();
            AnsiConsole.Live(initial)
                .AutoClear(false)
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
