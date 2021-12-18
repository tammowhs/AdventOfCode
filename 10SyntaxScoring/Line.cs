using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._10SyntaxScoring
{
    public class Line
    {
        private readonly string value;
        private readonly Symbol[] symbols;
        private readonly Symbol? corruptingSymbol;

        private static readonly Symbol[] openingSymbols = { Symbol.RoundOpen, Symbol.SquareOpen, Symbol.CurlyOpen, Symbol.AngleOpen };

        public string Value => value;
        public Symbol[] Symbols => symbols;
        public Symbol? CorruptingSymbol => corruptingSymbol;

        public Line(string value)
        {
            this.value = value;
            symbols = value.ToCharArray().Select(c => MapToSymbol(c)).ToArray();
            corruptingSymbol = Evaluate();
        }

        public Symbol? Evaluate()
        {
            var lastOccuringOpeningSymbols = new List<Symbol>();

            //var balanceRound = 0;
            //var balanceSquare = 0;
            //var balanceCurly = 0;
            //var balanceAngle = 0;

            foreach (var symbol in symbols)
            {
                var isOpeningSymbol = openingSymbols.Contains(symbol);
                
                if (isOpeningSymbol)
                {
                    lastOccuringOpeningSymbols.Add(symbol);
                }
                else
                {
                    if(!lastOccuringOpeningSymbols.Any())
                    {
                        return symbol;
                    }
                    var last = lastOccuringOpeningSymbols.Last();

                    var isRoundPair = last == Symbol.RoundOpen && symbol == Symbol.RoundClose;
                    var isSquarePair = last == Symbol.SquareOpen && symbol == Symbol.SquareClose;
                    var isCurlyPair = last == Symbol.CurlyOpen && symbol == Symbol.CurlyClose;
                    var isAnglePair = last == Symbol.AngleOpen && symbol == Symbol.AngleClose;

                    if(isRoundPair || isSquarePair || isCurlyPair || isAnglePair)
                    {
                        lastOccuringOpeningSymbols.RemoveAt(lastOccuringOpeningSymbols.Count - 1);
                    }
                    else
                    {
                        return symbol;
                    }
                }

                //switch (symbol)
                //{
                //    case Symbol.RoundOpen:
                //        balanceRound++;
                //        break;
                //    case Symbol.RoundClose:
                //        balanceRound--;
                //        break;
                //    case Symbol.SquareOpen:
                //        balanceSquare++;
                //        break;
                //    case Symbol.SquareClose:
                //        balanceSquare--;
                //        break;
                //    case Symbol.CurlyOpen:
                //        balanceCurly++;
                //        break;
                //    case Symbol.CurlyClose:
                //        balanceCurly--;
                //        break;
                //    case Symbol.AngleOpen:
                //        balanceAngle++;
                //        break;
                //    case Symbol.AngleClose:
                //        balanceAngle--;
                //        break;
                //}
            }

            return null;
        }

        private Symbol MapToSymbol(char sign) => sign switch
        {
            '(' => Symbol.RoundOpen,
            ')' => Symbol.RoundClose,
            '[' => Symbol.SquareOpen,
            ']' => Symbol.SquareClose,
            '{' => Symbol.CurlyOpen,
            '}' => Symbol.CurlyClose,
            '<' => Symbol.AngleOpen,
            '>' => Symbol.AngleClose,
            _ => throw new ArgumentException()
        };
    }

    // https://de.wikipedia.org/wiki/Klammer_(Zeichen)
    public enum Symbol
    {
        RoundOpen,
        RoundClose,
        SquareOpen,
        SquareClose,
        CurlyOpen,
        CurlyClose,
        AngleOpen,
        AngleClose,
    }
}
