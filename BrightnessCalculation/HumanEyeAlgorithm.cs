using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BrightnessCalculation
{
    public class HumanEyeAlgorithm : IBrightnessCalculator {
        public int getPixelRepresentation(int red, int green, int blue) {
            return Convert.ToInt32(Math.Round(red * 0.2126 + green * 0.7152 + blue * 0.0722));
        }
    }
}
