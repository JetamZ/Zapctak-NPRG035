using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorRepresentation
{
    public abstract class ModifieableColor {
        public int colorValue;
        public ModifieableColor(int colorValue) { 
            this.colorValue = colorValue;
            checkAndNormalizeColor();
        }
        private void checkAndNormalizeColor() {
            if (this.colorValue > 255) {
                this.colorValue = 255;
                return;
            }
            if (this.colorValue < 0) {
                this.colorValue = 0;
            }
        }
        public void Add(int toAdd) { 
            this.colorValue += toAdd;
            checkAndNormalizeColor();
        }
        public void Substract(int toSubtract) { 
            this.colorValue -= toSubtract;
            checkAndNormalizeColor();
        }
        public void Divide(int divideBy) {
            this.colorValue /= divideBy;
            checkAndNormalizeColor();
        }
        public void Multiply(int multiplyBy) {
            this.colorValue *= multiplyBy;
            checkAndNormalizeColor();
        }
        public override string ToString() {
            return "ModifiableColor{" + "colorValue=" + this.colorValue + "}";
        }
    }
}
