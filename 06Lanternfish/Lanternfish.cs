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

            for (int i = 0; i < 80; i++)
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
            //input = @"3,4,3,1,2";

            var initialFishes = input.Split(',').Select(t => int.Parse(t)).ToArray();

            var days = 256;

            var newBirths = new long[days];

            foreach (var fish in initialFishes)
            {
                var birthdays = GetBirthDays(fish).Where(x => x < days);

                foreach (var birthDay in birthdays)
                {
                    newBirths[birthDay]++;
                }
            }

            for (int day = 1; day <= days; day++)
            {
                var newBorn = newBirths[day - 1];

                var birthdays = GetBirthDays(8).Where(d => d < days - day);

                foreach (int birthDay in birthdays)
                {
                    newBirths[day + birthDay] += newBorn;
                }

            }

            var y = newBirths.Sum() + initialFishes.Length;

            var x = PopulationAfter(days, initialFishes);

            return 0;
        }

        //a.e. fish with initial age 3 will give birth on day 4 (index 3), day 11 (i 10) and so on
        private IEnumerable<int> GetBirthDays(int initialAge)
        {
            return Enumerable.Range(0, 40)
                .Select(x => x * 7 + initialAge);
        }

        long PopulationAfter(int days, int[] initial)
        {
            long[] newbirths = new long[days];

            IEnumerable<int> births(int initialAge, int daysLeft) => Enumerable.Range(0, 50)
                .Select(x => x * 7 + initialAge)
                .Where(x => x < daysLeft);

            foreach (var age in initial)
            {
                foreach (var birthday in births(age, days))
                {
                    newbirths[birthday] += 1;
                }
            }

            for (int day = 0; day < days; day++)
            {
                var newBorn = newbirths[day];

                foreach (var birthday in births(8, days - day - 1))
                {
                    newbirths[day + 1 + birthday] += newBorn;
                }
            }

            return initial.Length + newbirths.Sum();
        }
    }
}
