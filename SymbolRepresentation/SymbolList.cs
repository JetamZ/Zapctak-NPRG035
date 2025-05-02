using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymbolRepresentation
{
    public class SymbolList {
        public Symbol[] sortedSymbols { get; private set; }
        private int acceptableSymbolAccuracy = 4;

        public SymbolList(int initialSize) {
            this.sortedSymbols = new Symbol[initialSize];
        }
        public SymbolList(int initialSize, int acceptableSymbolAccuracy) {
            this.sortedSymbols = new Symbol[initialSize];
            this.acceptableSymbolAccuracy = acceptableSymbolAccuracy;
        }

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

        public string[] toStringArray() {
            List<string> list = new List<string>();

            for (int i = 0; i < sortedSymbols.Length; i++) {
                if (sortedSymbols[i] == null) { continue; }
                list.Add(sortedSymbols[i].symbol);
            }
            return list.ToArray();
        }
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
