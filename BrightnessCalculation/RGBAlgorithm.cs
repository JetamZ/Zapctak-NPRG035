using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightnessCalculation
{
    public class RGBAlgorithm : IBrightnessCalculator {
        public int getPixelRepresentation(int red, int green, int blue) {
            int max = red;
            if (green > max) { max = green; }
            if (blue > max) { max = blue; }
            return max;
        }
    }
}
