
using System;
using System.Collections.Generic;
using System.Collections;
using ColorRepresentation;
using OpenCvSharp;

namespace ImageRepersentation
{
    /// <summary>
    /// Class for internally reperesenting pixels of an image.
    /// Implements the <see cref="IEnumerable"/> interface looking at each individual
    /// pixel of internalImage as instance of <see cref="SingularPixel"/> class.
    /// </summary>
    public class PixelGroup : IEnumerable<SingularPixel> {
        /// <summary>
        /// Image represented as OpenCvSharp's Mat object.
        /// </summary>
        public Mat internalImage { private set;  get; }
        /// <summary>
        /// Width of the internal image.
        /// </summary>
        public int Width { 
            get { return internalImage.Width; }
        }
        /// <summary>
        /// Height of the internal image.
        /// </summary>
        public int Height {
            get { return internalImage.Height; }
        }
        /// <summary>
        /// The size as pixel count of the image.
        /// </summary>
        public int size { 
            get { return this.internalImage.Width * this.internalImage.Height; } 
        }
        public PixelGroup(Mat image) {
            this.internalImage = image;
        }
        /// <summary>
        /// Method for applying function to each pixel of an image.
        /// Iterates over each pixel in the Mat representation,
        /// constructs an <see cref="SingularPixel"> object from it
        /// and applies the action to that object.
        /// </summary>
        /// <param name="action">Action to be applied to pixels.</param>
        public void applyToAllPixels(Action<SingularPixel> action) {
            int width = internalImage.Width;
            int height = internalImage.Height;
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) { 
                    Vec3b bgrColor = this.internalImage.At<Vec3b>(y, x);
                    Vec3b rgbColor = new Vec3b();
                    rgbColor[0] = bgrColor[2];
                    rgbColor[1] = bgrColor[1];
                    rgbColor[2] = bgrColor[0];

                    var pixel = new SingularPixel(rgbColor, x, y);
                    action(pixel);
                }
            }
        }
        /// <summary>
        /// Enumerator getter.
        /// Iterate over pixels of internalImage and yield <see cref="SingularPixel"/> objects.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<SingularPixel> GetEnumerator() {
            int width = internalImage.Width;
            int height = internalImage.Height;
            for (int y = 0; y < height; y++) {
                for (int x = 0; x <= width; x++) { 
                    Vec3b bgr = internalImage.At<Vec3b>(x,y);
                    Vec3b rgb = new Vec3b();
                    rgb[0] = bgr[2];
                    rgb[1] = bgr[1];
                    rgb[2] = bgr[0];
                    var pixel = new SingularPixel(rgb, x, y);
                    yield return pixel;
                }
            }
        }
        /// <summary>
        /// Enumerator getter enforced by implementation of the
        /// IEnumerable contract.
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator() { 
            return GetEnumerator();
        }
    }
}
