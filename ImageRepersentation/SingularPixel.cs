using ColorRepresentation;
using OpenCvSharp;

namespace ImageRepersentation
{
    public class SingularPixel {
        public int x { get; set; }
        public int y { get; set; }
        public PixelColor color { get; set; }

        public SingularPixel(Vec3b color, int x, int y) {
            this.x = x;
            this.y = y;
            this.color = new PixelColor(color);
        }
    }
}
