using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BrightnessCalculation
{
    /// <summary>
    /// 'Human eye' pixel brightness calculation algorithm.
    /// Implements the <see cref="IBrightnessCalculator"> interface.
    /// </summary>
    public class HumanEyeAlgorithm : IBrightnessCalculator {
        /// <summary>
        /// Pixel brightness calculation based on real-life color sensitivity of a humna eye.
        /// </summary>
        /// <param name="red">Red component of a pixel color.</param>
        /// <param name="green">Green component of a pixel color.</param>
        /// <param name="blue">Bluee component of a pixel color.</param>
        /// <returns>Calculated brightness of the pixel.</returns>
        public int getPixelRepresentation(int red, int green, int blue) {
            return Convert.ToInt32(Math.Round(red * 0.2126 + green * 0.7152 + blue * 0.0722));
        }
    }
}
