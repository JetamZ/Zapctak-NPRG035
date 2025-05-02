using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
namespace ColorRepresentation
{
    public class PixelColor {
        private Vec3b pixelColor;
        public Red redComponent { get; init; }
        public Green greenComponent { get; init; }
        public Blue blueComponent { get; init; }

        public PixelColor(Vec3b color) {
            this.pixelColor = color;
            this.redComponent = new Red(pixelColor[2]);
            this.blueComponent = new Blue(pixelColor[0]);
            this.greenComponent = new Green(pixelColor[1]);
        }

        public override string ToString() {
            return "PixelColor {\n" + "    redCompnent=" + this.redComponent + '\n'
                   + "    greenComponent=" + this.greenComponent + "\n"
                   + "    blueComponent=" + this.blueComponent + "\n}";
        }
        
    }
}
