namespace SymbolRepresentation
{
    public class Symbol {
        public string symbol { get; set; }
        public decimal brightness { get; set; }

        public Symbol(string symbol, decimal brightness) { 
            this.symbol = symbol;
            this.brightness = brightness;
        }
        public override string ToString() {
            return "Symbol { " + "symbol='" + this.symbol + "'" + " brightness=" + this.brightness + "}"; 
        }
    }
}
