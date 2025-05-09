namespace BrightnessCalculation
{
    /// <summary>
    /// A contract for brightness value calculation algorithms.
    /// </summary>
    public interface IBrightnessCalculator {
        public int getPixelRepresentation(int red, int green, int blue);
    }
}
