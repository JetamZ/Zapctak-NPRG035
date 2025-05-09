using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
namespace ColorRepresentation
{
    /// <summary>
    /// Class for holding an information about pixel color.
    /// </summary>
    public class PixelColor {
        /// <summary>
        /// Color represented as a triplet utilizing OpenCvSharp's Vec3b.
        /// </summary>
        private Vec3b pixelColor;
        /// <summary>
        /// Individual red component.
        /// </summary>
        public Red redComponent { get; init; }
        /// <summary>
        /// Individual green component.
        /// </summary>
        public Green greenComponent { get; init; }
        /// <summary>
        /// Individual blue component.
        /// </summary>
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
