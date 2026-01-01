namespace LINQMethods
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // TODO: Demonstrate some LINQ methods here

            /* Aggregate Operator */
            #region Aggregate();

            // Aggregate(Func<TSource, TSource, TSource>)
            // Work similar like Sum, but can use all type(int, double, string), and all operations(+, -, *, /, % ...)
            int resaultAggregateMethod = new int[] { 1, 2, 3, 4 }.Aggregate((acc, x) => acc + x); // result is 10 because 1 + 2 + 3 + 4 = 10
            // acc - accumulator that stores the resat
            // x - current element from the array
            // acc + x - operation of accumulator: acc = acc + x; it can be any operation(+, -, *, /, %)

            // Aggregate(TAccumulate seed, Func<TAccumulate, TSource, TAccumulate>)
            // Similar to previous, but you can add starter value
            int resaultAggregateMethodWithSeed = new int[] { 1, 2, 3, 4 }.Aggregate(10,(acc, x) => acc + x); // result is 20 because 10 + 1 + 2 + 3 + 4 = 20
            // 0 - starter value of accumulator


            // Aggregate(TAccumulate seed, Func<TAccumulate, TSource, TAccumulate>, Func<TAccumulate, TResult>)

            string resaultAggregateMethodWithSeedAndConvertation = new int[] { 2, 4, 6 }.Aggregate(0, (acc, x) => acc + x, acc => "Sum = " + acc);
            // acc => "Sum = " + acc - result selector that converts the final accumulator value to a string

            // TODO: Demonstrate Aggregate operator
            // 1. Using Aggregate replace standard methods like Sum(), Count(), Min(), Max(), Average(), Any(), All(), ToList():
            // Sum()	(acc, x) => acc + x
            //   Count()(acc, x) => acc + 1
            //Max()(acc, x) => Math.Max(acc, x)
            //Min()(acc, x) => Math.Min(acc, x)
            //Any()   `(acc, x) => acc
            //All()(acc, x) => acc && condition(x)
            //ToList()    acc.Add(x)

            // 2. Research usage of Aggregate operator in real-world scenarios

            #endregion

            /* Condition */

            #region All();

            // All(Func<TSource, bool>)
            // Determines whether all elements of a sequence satisfy a condition.
            // Even if one element does not satisfy the condition, the result will be false.
            bool resaultAllMethod = new int[] { 1, 2, 3, 4 }.All(x => x > 0); // true because all elements are greater than 0


            #endregion

            #region Any();

            // All(Func<TSource, bool>)
            // Determines whether any element of a sequence satisfies a condition.
            // If at least one element satisfies the condition, the result will be true.
            bool resaultAnyMethod = new int[] { 1, 2, 3, 4 }.Any(x => x > 3); // true because there is at least one element greater than 3

            #endregion












            /* Aggregate Operator */

            // array.Sum();
            // array.Min();
            // array.Max();
            // array.Average();
            // array.Count();
            // array.Aggregate();

            /* Filtering Operator */
            // array.Where();

            /* Projection Operator */
            // array.Select();

            /* Sorting Operation */
            // array.OrderBy();
            // array.OrderByDescending();

            /* Set Operators */
            // array.Concat();
            // array.Union();
            // array.Intersect();
            // array.Except();

            /* Other */

            // array.First();
            // array.FirstOrDefault();
            // array.Last();
            // array.LastOrDefault();
            // array.Single();
            // array.SingleOrDefault();
            // array.Any();
            // array.All();



        }
    }
}
