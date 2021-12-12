using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._04GiantSquid
{
	public class GiantSquid : ICodePuzzle
	{
		public int EvaluatePartOne(string input)
        {
            //input = @"7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1

            //22 13 17 11  0
            // 8  2 23  4 24
            //21  9 14 16  7
            // 6 10  3 18  5
            // 1 12 20 15 19

            // 3 15  0  2 22
            // 9 18 13 17  5
            //19  8  7 25 23
            //20 11 10 24  4
            //14 21 16 12  6

            //14 21 17 24  4
            //10 16 15  9 19
            //18  8 23 26 20
            //22 11 13  6  5
            // 2  0 12  3  7";

            List<int> drawnNumbers;
            List<Block> bingoBlocks;
            DeserializeInput(input, out drawnNumbers, out bingoBlocks);

            return EvaluateBingo(drawnNumbers, bingoBlocks, (blocks) => true);
        }

        private void DeserializeInput(string input, out List<int> drawnNumbers, out List<Block> bingoBlocks)
        {
            var inputBlocks = input.Split("\r\n\r\n").ToList();

            drawnNumbers = inputBlocks[0].Split(',').Select(numString => int.Parse(numString)).ToList();
            inputBlocks.RemoveAt(0);

            bingoBlocks = inputBlocks.Select((iBString, blockIndex) => DeserializeBingoBlock(iBString, blockIndex)).ToList();
        }

        private bool CheckBingoBlockForWin(List<Field> bingoField)
        {
			var groupedByRows = bingoField.GroupBy(f => f.Row).ToList();

			var anyRowWin = groupedByRows.Any(group => group.All(f => f.Marked));

			var groupedByCols = bingoField.GroupBy(f => f.Col).ToList();

			var anyColWin = groupedByCols.Any(group => group.All(f => f.Marked));

			var anyWin = anyRowWin || anyColWin;
			
			return anyWin;
		}

		public Block DeserializeBingoBlock(string blockInput, int blockIndex)
		{
			var rowStrings = blockInput.Split("\r\n").ToList();

			var blockFields = rowStrings
				.SelectMany((rowString, rowIndex) => rowString
														.Split(' ')
														.Where(str => !string.IsNullOrWhiteSpace(str))
														.Select((numString, colIndex) => new Field(int.Parse(numString), rowIndex, colIndex, blockIndex))
				)
				.ToList();

			return new Block(blockIndex, blockFields);
		}

		public int EvaluatePartTwo(string input)
        {
            List<int> drawnNumbers;
            List<Block> bingoBlocks;
            DeserializeInput(input, out drawnNumbers, out bingoBlocks);

            Func<List<Block>, bool> endingCondition = (bingoBlocks) => bingoBlocks.All(bB => bB.IsBingo);

            return EvaluateBingo(drawnNumbers, bingoBlocks, endingCondition);
        }

        private int EvaluateBingo(List<int> drawnNumbers, List<Block> bingoBlocks, Func<List<Block>, bool> endingCondition)
        {
            foreach (var drawnNumber in drawnNumbers)
            {
                foreach (var bingoBlock in bingoBlocks)
                {
                    if (bingoBlock.IsBingo)
                        continue;

                    var field = bingoBlock.Fields.Find(f => f.Value == drawnNumber);

                    if (field is not null)
                    {
                        field.Marked = true;

                        if (CheckBingoBlockForWin(bingoBlock.Fields))
                        {
                            bingoBlock.IsBingo = true;

                            if (endingCondition(bingoBlocks))
                            {
                                var sumOfUnmarked = bingoBlock.Fields
                                    .Where(f => !f.Marked)
                                    .Sum(f => f.Value);

                                var bingoScore = sumOfUnmarked * drawnNumber;

                                return bingoScore;
                            }
                        }
                    }
                }
            }

            throw new Exception("No Bingo");
        }
    }
}
