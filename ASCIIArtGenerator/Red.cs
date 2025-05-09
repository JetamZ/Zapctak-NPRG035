using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorRepresentation
{
    /// <summary>
    /// Red RGB component class.
    /// </summary>
    public class Red : ModifieableColor {
        public Red(int colorValue) : base(colorValue) {  }
        public override string ToString() {
            return "Red { colorValue=" + colorValue + "}";
        }
    }
}
