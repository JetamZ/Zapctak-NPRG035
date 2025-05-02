using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageRepersentation
{
    [Flags]
    public enum Scale { 
        DEFAULT = 1 << 0,
        FAST = 1 << 1,
        SMOOTH = 1 << 2,
    }

    public static class InterpolationMappings { 
        public static OpenCvSharp.InterpolationFlags ToOpenCvFlag (Scale scale) {
            switch (scale) {
                case (Scale.DEFAULT): return OpenCvSharp.InterpolationFlags.Linear;
                case (Scale.FAST): return OpenCvSharp.InterpolationFlags.Cubic;
                case (Scale.SMOOTH): return OpenCvSharp.InterpolationFlags.Lanczos4;
                default: return OpenCvSharp.InterpolationFlags.Linear;
            }
        }
    }
    public class PixelScale {
        public int width { get; set; }
        public int height { get; set; }
        public Scale scale { get; set; }

        public PixelScale() { }
        public PixelScale(int width, int height, Scale desiredScale) { 
            this.width = width;
            this.height = height;
            this.scale = desiredScale;
        }
    }
}
