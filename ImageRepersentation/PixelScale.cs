using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageRepersentation
{
    /// <summary>
    /// Scaling method options.
    /// </summary>
    [Flags]
    public enum Scale { 
        DEFAULT = 1 << 0,
        FAST = 1 << 1,
        SMOOTH = 1 << 2,
    }
    /// <summary>
    /// Mapping of the scaling options to OpenCvSharp's Interpolation flags.
    /// </summary>
    public static class InterpolationMappings { 
        /// <summary>
        /// Method for converting the caling method option to OpenCvSharp's Interpoloation flag.
        /// </summary>
        /// <param name="scale">Scaling method option.</param>
        /// <returns>Appropriate OpenCvSharp's interpolation flag.</returns>
        public static OpenCvSharp.InterpolationFlags ToOpenCvFlag (Scale scale) {
            switch (scale) {
                case (Scale.DEFAULT): return OpenCvSharp.InterpolationFlags.Linear;
                case (Scale.FAST): return OpenCvSharp.InterpolationFlags.Cubic;
                case (Scale.SMOOTH): return OpenCvSharp.InterpolationFlags.Lanczos4;
                default: return OpenCvSharp.InterpolationFlags.Linear;
            }
        }
    }
    /// <summary>
    /// Class for holding the scaling configuration.
    /// </summary>
    public class PixelScale {
        /// <summary>
        /// Width of the scaled image.
        /// </summary>
        public int width { get; set; }
        /// <summary>
        /// Height of the scaled image.
        /// </summary>
        public int height { get; set; }
        /// <summary>
        /// Scaling method.
        /// </summary>
        public Scale scale { get; set; }

        public PixelScale() { }
        public PixelScale(int width, int height, Scale desiredScale) { 
            this.width = width;
            this.height = height;
            this.scale = desiredScale;
        }
    }
}
