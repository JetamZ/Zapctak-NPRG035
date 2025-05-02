using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorRepresentation
{
    public class Green : ModifieableColor {
        public Green(int colorValue) : base(colorValue) {  }
        public override string ToString() {
            return "Green { colorValue=" + this.colorValue + "}";
        }
    }
}
