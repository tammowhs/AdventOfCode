using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._06Lanternfish
{
    public class Lanternfish : ICodePuzzle
    {
        public int EvaluatePartOne(string input)
        {
            //input = @"3,4,3,1,2";

            var fishes = input.Split(',').Select(t => new Fish(int.Parse(t))).ToList();

            for (int i = 0; i < 256; i++)
            {
                var breedingFishes = fishes.Where(f => f.Timer == 0).ToList();

                var count = breedingFishes.Count;
                
                fishes.ForEach(f => f.NewDay());

                var newFishes = Enumerable.Range(0, count).Select(e => new Fish());

                fishes.AddRange(newFishes);
            }

            return fishes.Count;
        }

        public int EvaluatePartTwo(string input)
        {
            throw new NotImplementedException();
            //input = @"3,4,3,1,2";

            //var fishes = input.Split(',').Select(t => int.Parse(t));

            //for (int i = 0; i < 80; i++)
            //{
            //    //Console.WriteLine(i);
            //    fishes = fishes.Select(f => f - 1);

            //    //Console.WriteLine("2: ", i); 
            //    var breedingFishes = fishes.Where(f => f < 0).Select(f => f = 6);

            //    //Console.WriteLine("3: ", i);
            //    var newFishes = breedingFishes.Select(e => 8);

            //    //Console.WriteLine("4: ", i);
            //    fishes = fishes.Concat(newFishes);
            //}

            //return fishes.ToList().Count;
        }
    }
}
