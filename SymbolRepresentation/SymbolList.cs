using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymbolRepresentation
{
    /// <summary>
    /// Class for holding the symbol pool for the Ascii Art along with some
    /// useful functionality.
    /// </summary>
    public class SymbolList {
        /// <summary>
        /// Symbol pool itself, holding symbols as objects of <see cref="Symbol"/> class.
        /// </summary>
        public Symbol[] sortedSymbols { get; private set; }
        /// <summary>
        /// Initial amount of brightness values covered by single symbol.
        /// </summary>
        private int acceptableSymbolAccuracy = 4;

        public SymbolList(int initialSize) {
            this.sortedSymbols = new Symbol[initialSize];
        }
        public SymbolList(int initialSize, int acceptableSymbolAccuracy) {
            this.sortedSymbols = new Symbol[initialSize];
            this.acceptableSymbolAccuracy = acceptableSymbolAccuracy;
        }
        /// <summary>
        /// Method for adding a symbol to a symbol pool on a correct index, based on the symbol's shade in
        /// attempt to keep the symbol list somewhat sorted.
        /// Also cares about not having duplicates in the list, so a symbol is added if and only if 
        /// it is not already in the list.
        /// Also if the symbol is to be inserted at an occupied index, it looks at a symbol that is
        /// at target index and compares it to new symbol. If new symbol fits better, the former is replaced
        /// by the new symbol.
        /// </summary>
        /// <param name="toAdd">Symbol to be added</param>
        public void AddSymbol(Symbol toAdd) {
            decimal newSymbolBrightness = toAdd.brightness;
            int symbolShade = (int)Math.Round(newSymbolBrightness / ((decimal)255 / sortedSymbols.Length)) - 1;
            if (sortedSymbols.Length < symbolShade) { return; }

            Symbol currentSymbol = sortedSymbols[symbolShade];
            if (currentSymbol == null) {
                this.sortedSymbols[symbolShade] = toAdd;
                return;
            }

            decimal currentBrightnessDelta = currentSymbol.brightness - symbolShade;
            decimal newBrightnessDelta = newSymbolBrightness - symbolShade;
            if (currentBrightnessDelta > newBrightnessDelta) {
                this.sortedSymbols[symbolShade] = toAdd;
            } else {
                for (int i = 0; i < symbolShade + this.acceptableSymbolAccuracy; i++) {
                    if (i > sortedSymbols.Length - 1) { break; }

                    if (sortedSymbols[i] == null) {
                        sortedSymbols[i] = toAdd;
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// Method for getting the symbol pool as an string array.
        /// </summary>
        /// <returns>Symbol pool as a string array</returns>
        public string[] toStringArray() {
            List<string> list = new List<string>();

            for (int i = 0; i < sortedSymbols.Length; i++) {
                if (sortedSymbols[i] == null) { continue; }
                list.Add(sortedSymbols[i].symbol);
            }
            return list.ToArray();
        }
        /// <summary>
        /// Method for getting the actual number of occupied slots in the symbol array. 
        /// </summary>
        /// <returns>Number of actual occupied slots in symbol list.</returns>
        public int size() { 
            int size = 0;
            for (int i = 0; i < sortedSymbols.Length; i++) {
                if (this.sortedSymbols[i] == null) { continue; }
                size++;
            }
            return size;   
        }
        public override string ToString() {
            return "SymbolList {" + " sortedSymbols=" + getSymbolsAsString() +" }";
        }
        /// <summary>
        /// Method for getting the symbol pool as string, for debugging purposes.
        /// </summary>
        /// <returns>String representation of the symbol pool.</returns>
        private string getSymbolsAsString() {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("{ ");
            foreach (Symbol symbol in sortedSymbols) { 
                if (symbol == null) { continue; }
                stringBuilder.Append('"');
                stringBuilder.Append(symbol.symbol);
                stringBuilder.Append('"');
                stringBuilder.Append(',');
            }
            return stringBuilder.ToString();
        }
    }
}
