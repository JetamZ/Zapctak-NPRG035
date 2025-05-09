using ColorRepresentation;
using OpenCvSharp;

namespace ImageRepersentation
{
    /// <summary>
    /// A class for representing a singular pixel.
    /// </summary>
    public class SingularPixel {
        /// <summary>
        /// x coordinate within the image.
        /// </summary>
        public int x { get; set; }
        /// <summary>
        /// y coordinate within the image.
        /// </summary>
        public int y { get; set; }
        /// <summary>
        /// Color of the pixel specification.
        /// </summary>
        public PixelColor color { get; set; }

        public SingularPixel(Vec3b color, int x, int y) {
            this.x = x;
            this.y = y;
            this.color = new PixelColor(color);
        }
    }
}
