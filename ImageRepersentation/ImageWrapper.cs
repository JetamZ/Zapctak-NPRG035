using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageRepersentation
{
    public class ImageWrapper {
        public PixelGroup pixels {
            get;
            init;
        }
        public int width {
            get { return pixels.Width; }
        }
        public int height {
            get { return pixels.Height; }
        }

        public ImageWrapper(Mat image)
        {
            this.pixels = new PixelGroup(image);
        }
        public ImageWrapper getScaledVersion(PixelScale scale) {
            Mat scaled = new Mat();
            Cv2.Resize(
                src: pixels.internalImage,
                dst: scaled,
                dsize: new Size(scale.width, scale.height),
                fx: 0, fy: 0,
                interpolation: InterpolationMappings.ToOpenCvFlag(scale.scale));
            return new ImageWrapper(scaled);
        }
        public bool isBorderPixel(int x, int y) {
            return isHorizontalBorderPixel(x) || isVerticalBorderPixel(y);
        }
        public bool isHorizontalBorderPixel(int x) {
            return (x== 0 || x == width - 1);
        }
        public bool isVerticalBorderPixel(int y) {
            return (y == 0 || y == height - 1);
        } 
        public ImageWrapper(string imagePath) {
            try
            {
                this.pixels = new PixelGroup(Cv2.ImRead(imagePath));
            }
            catch (Exception ex) { 
                Console.WriteLine("Error loading image");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
