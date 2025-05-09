using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageRepersentation
{
    /// <summary>
    /// Class for holding internal representation of the source image.
    /// </summary>
    public class ImageWrapper {
        /// <summary>
        /// Internal representation of pixels of the source image.
        /// </summary>
        public PixelGroup pixels {
            get;
            init;
        }
        /// <summary>
        /// Width of the source image.
        /// </summary>
        public int width {
            get { return pixels.Width; }
        }
        /// <summary>
        /// Height of the source image
        /// </summary>
        public int height {
            get { return pixels.Height; }
        }
        /// <summary>
        /// Constructs the ImageWrapper around OpenCvSharp's Mat image representation. 
        /// </summary>
        /// <param name="image">Source image represented as OpenCvSharp's Mat.</param>
        public ImageWrapper(Mat image)
        {
            this.pixels = new PixelGroup(image);
        }
        /// <summary>
        /// Method for rescaling the source image to desired scale.
        /// </summary>
        /// <param name="scale">Object holding desired scale <see cref="PixelScale"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Method for determining whether a pixel is on border of an image or not.
        /// </summary>
        /// <param name="x">x coordiante of the pixel.</param>
        /// <param name="y">y coordinate of the pixel.</param>
        /// <returns>true if pixel is either horizontal or verticla border.</returns>
        public bool isBorderPixel(int x, int y) {
            return isHorizontalBorderPixel(x) || isVerticalBorderPixel(y);
        }
        /// <summary>
        /// Method for determining whether a pixel is on horizontal border of an image or not.
        /// </summary>
        /// <param name="x">x coordiante of a pixel.</param>
        /// <returns>true if pixel is on horizontal border of the image else false.</returns>
        public bool isHorizontalBorderPixel(int x) {
            return (x== 0 || x == width - 1);
        }
        /// <summary>
        /// Method for determining whether a pixel is on vertical border of an image.
        /// </summary>
        /// <param name="y">y coordinate of the pixel.</param>
        /// <returns>true if pixel is on vertival border of the image else false.</returns>
        public bool isVerticalBorderPixel(int y) {
            return (y == 0 || y == height - 1);
        }
        /// <summary>
        /// Constructs the ImageWrapper object from a provided source image path.
        /// </summary>
        /// <param name="imagePath">Path to the source image.</param>
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
