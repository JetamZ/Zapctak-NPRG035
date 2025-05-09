using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightnessCalculation
{
    /// <summary>
    /// RGB algorithm for calculating the brightness of a pixel.
    /// Implements the <see cref="IBrightnessCalculator"> interface.
    /// </summary>
    public class RGBAlgorithm : IBrightnessCalculator {
        /// <summary>
        /// Method for calculating the brightness pixel based on the brightest
        /// RGB component.
        /// </summary>
        /// <param name="red">red component of a pixel color</param>
        /// <param name="green">green component of a pixel color</param>
        /// <param name="blue">blue component of a pixel color</param>
        /// <returns>Brightest component of RGB.</returns>
        public int getPixelRepresentation(int red, int green, int blue) {
            int max = red;
            if (green > max) { max = green; }
            if (blue > max) { max = blue; }
            return max;
        }
    }
}
