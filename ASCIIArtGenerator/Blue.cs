using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorRepresentation
{
    public class Blue : ModifieableColor {
        public Blue(int colorValue) : base(colorValue) { }
        public override string ToString() {
            return "Blue { colorValue=" + colorValue + "}";
        }
    }
}
