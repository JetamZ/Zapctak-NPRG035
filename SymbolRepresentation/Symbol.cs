namespace SymbolRepresentation
{
    /// <summary>
    /// Class for representing a symbol.
    /// Holds the symbol itself as a string and its calculated brightness.
    /// </summary>
    public class Symbol {
        /// <summary>
        /// Symbol itself.
        /// </summary>
        public string symbol { get; set; }
        /// <summary>
        /// Brightness of the symbol
        /// </summary>
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
