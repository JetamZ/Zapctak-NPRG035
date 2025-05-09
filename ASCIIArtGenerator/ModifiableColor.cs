using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorRepresentation
{
    /// <summary>
    /// Abstract class for internal representation of RGB components.
    /// </summary>
    public abstract class ModifieableColor {
        public int colorValue;
        public ModifieableColor(int colorValue) { 
            this.colorValue = colorValue;
            checkAndNormalizeColor();
        }
        /// <summary>
        /// Method for normalizing the color value to interval of 0-255.
        /// </summary>
        private void checkAndNormalizeColor() {
            if (this.colorValue > 255) {
                this.colorValue = 255;
                return;
            }
            if (this.colorValue < 0) {
                this.colorValue = 0;
            }
        }
        /// <summary>
        /// Method for adding to a color component.
        /// </summary>
        /// <param name="toAdd">Value to add.</param>
        public void Add(int toAdd) { 
            this.colorValue += toAdd;
            checkAndNormalizeColor();
        }
        /// <summary>
        /// Method for substracting from a color component.
        /// </summary>
        /// <param name="toSubstract">Value to substract.</param>
        public void Substract(int toSubstract) { 
            this.colorValue -= toSubstract;
            checkAndNormalizeColor();
        }
        /// <summary>
        /// Method for dividing a color component.
        /// </summary>
        /// <param name="divideBy">Value to divide by.</param>
        public void Divide(int divideBy) {
            this.colorValue /= divideBy;
            checkAndNormalizeColor();
        }
        /// <summary>
        /// Method for multiplying a color component.
        /// </summary>
        /// <param name="multiplyBy">Value to multiply by.</param>
        public void Multiply(int multiplyBy) {
            this.colorValue *= multiplyBy;
            checkAndNormalizeColor();
        }
        public override string ToString() {
            return "ModifiableColor{" + "colorValue=" + this.colorValue + "}";
        }
    }
}
