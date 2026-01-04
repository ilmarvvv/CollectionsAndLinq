using System;
using System.Collections;

namespace LINQMethods
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // TODO: Demonstrate some LINQ methods here

            /* Aggregate Operator */
            #region Aggregate(); - Universal method can be used for any mathematic operation and concatenation with any type and more...

            // Aggregate(Func<TSource, TSource, TSource>)
            // Work similar like Sum, but can use all type(int, double, string), and all operations(+, -, *, /, % ...)
            int aggregate = new int[] { 1, 2, 3, 4 }.Aggregate((acc, x) => acc + x); // result is 10 because 1 + 2 + 3 + 4 = 10
            // acc - accumulator that stores the resat
            // x - current element from the array
            // acc + x - operation of accumulator: acc = acc + x; it can be any operation(+, -, *, /, %)

            // Aggregate(TAccumulate seed, Func<TAccumulate, TSource, TAccumulate>)
            // Similar to previous, but you can add starter value
            int aggregateWithSeed = new int[] { 1, 2, 3, 4 }.Aggregate(10,(acc, x) => acc + x); // result is 20 because 10 + 1 + 2 + 3 + 4 = 20
            // 0 - starter value of accumulator


            // Aggregate(TAccumulate seed, Func<TAccumulate, TSource, TAccumulate>, Func<TAccumulate, TResult>)

            string aggregateWithSeedAndConvertation = new int[] { 2, 4, 6 }.Aggregate(0, (acc, x) => acc + x, acc => "Sum = " + acc);
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

            #region All(); - check if all elements satisfy a condition;

            // All(Func<TSource, bool>)
            // Determines whether all elements of a sequence satisfy a condition.
            // Even if one element does not satisfy the condition, the result will be false.
            bool all = new int[] { 1, 2, 3, 4 }.All(x => x > 0); // true because all elements are greater than 0


            #endregion

            #region Any(); - check if any element satisfies a condition or if content any element;

            // All(Func<TSource, bool>)
            // Determines whether any element of a sequence satisfies a condition.
            // If at least one element satisfies the condition, the result will be true.
            bool any = new int[] { 1, 2, 3, 4 }.Any(x => x > 3); // true because there is at least one element greater than 3

            // This Any(); used to check if the collection contains any elements.
            bool anyWithoutCondition = new int[] { 1, 2, 3, 4 }.Any(); // true because the array contains elements

            #endregion


            /* Other Method*/

            #region Append(); - add element to the end of collection;

            // Append(TSource)
            // Adds an element to the end of a sequence.
            var append = new int[] { 1, 2, 3 }.Append(4); // Output: 1 2 3 4

            #endregion

            #region AsEnumerable(); - convert collection to IEnumerable type;

            // AsEnumerable() - used to convert a collection to IEnumerable<T> usually for LINQ operations that require IEnumerable<T> input.
            var asEnumerable = new int[] { 1, 2, 3 }.AsEnumerable().Where(x => x > 1); // Output: 2 3

            #endregion

            #region Average(); - calculate average value of numeric collection;

            // Average() work with numeric types only(int, double, float, decimal, long, short, byte) and nullable as well(int?, double?, float?, decimal?, long?, short?, byte?)
            // returns double or decimal type depending on the input
            double average = new int[] { 1, 2, 3, 4, 5 }.Average(); // result is 3.0 because (1 + 2 + 3 + 4 + 5) / 5 = 3.0


            var people = new[]{ new { Name = "Ann", Age = 20 }, new { Name = "Bob", Age = 30 }, new { Name = "Carl", Age = 40 }};
            // This Average with selector for collections of complex types
            var averageWithSelector = people.Average(p => p.Age); // result is 30.0 because (20 + 30 + 40) / 3 = 30.0

            #endregion 

            #region Cast(); - convert collection to specified type used on collection without generic type ArrayList, Hashtable, IEnumerable(old);

            ArrayList list = new ArrayList() { 1, 2, 3 };

            // Cast<T>() - used to convert a non-generic collection to a generic IEnumerable<T>.
            // In 99% of cases you don't need it
            IEnumerable<int> numbers = list.Cast<int>();

            //int sum1 = list.Sum();      // ❌ doesn't work
            int sum2 = numbers.Sum();  // ✅ works

            #endregion

            #region Chunk(); - split collection into smaller collections of specified size;

            // Chunk(int)
            int[] array = { 1, 2, 3, 4, 5, 6, 7};


            var chunks = array.Chunk(3);
            // Chunk() contain:
            // chunks[0] -> [1, 2, 3]
            // chunks[1] -> [4, 5, 6]
            // chunks[2] -> [7]

            // Convert chunks to jagged array
            int[][] JaggedArray = array.Chunk(3).ToArray();
            // Chunk() contain:
            // chunks[0] -> [1, 2, 3]
            // chunks[1] -> [4, 5, 6]
            // chunks[2] -> [7]

            #endregion

            #region Concat(); - combine two collections in one

            // Concat(IEnumerable<TSource>)
            int[] a = { 1, 2, 3 };
            int[] b = { 4, 5, 6 };

            // Combine two collections into one
            var concat = a.Concat(b);   // {1,2,3,4,5,6}

            #endregion

            #region Contains(); - check if collection contains specified element;

            bool contains = new int[] { 1, 2, 3, 4 }.Contains(3); // true because 3 is in the array
            bool containsNotFound = new string[] { "cat", "dog", "squirrel", "wolf" }.Contains("human"); // false because "human" is not in the array

            var nameList = new List<Person>
            {
                new Person { Name = "Alice" },
                new Person { Name = "Bob" }
            };

            bool containsDefault = nameList.Contains(new Person { Name = "Alice" }); // false because default equality check compares references, not values

            // Using Contains with custom comparer because Person is a complex type and default equality check won't work as expected
            bool containsWithCustomComparer = nameList.Contains(new Person { Name = "Alice" }, new PersonNameComparer()); // true because a person with the name "Alice" exists in the list
            // Important: You need create custom comparer that implements IEqualityComparer<Person> interface like PersonNameComparer class above

            #endregion

            #region Count(); - count elements in collection, with or without condition;

            int count = new int[] { 1, 2, 3, 4 }.Count(); // 4
            int countWithCondition = new int[] { 1, 2, 3, 4 }.Count(x => x > 2); // 2 because only 3 and 4 are greater than 2

            #endregion DefaultIfEmpty(); - provide default value if collection is empty;

            #region DefaultIfEmpty(); - provide default or custom value if collection is empty;

            var defaultIfEmpty = new int[] { }.DefaultIfEmpty(); // {0} because the default value for int is 0

            var defaultIfEmptyWithCustomDefault = new int[] { }.DefaultIfEmpty(42); // {42} because we provided a custom default value of 42

            // DefaultIfEmpty(); - usually used to avoid exceptions when performing operations on potentially empty collections.
            var avg = new int[] { }.DefaultIfEmpty().Average(); // 0 because default value for int is 0
            #endregion

            #region Distinct(); - remove duplicate elements from collection;

            var distinct = new int[] { 1, 2, 2, 3, 4, 4, 4, 5 }.Distinct(); // {1, 2, 3, 4, 5}

            // also can delete duplicates in complex types with custom comparer as Contains() method above
            var peopleWithDuplicates = new List<Person>
            {
                new Person { Name = "Alice" },
                new Person { Name = "Bob" },
                new Person { Name = "Alice" } // duplicate
            };

            var distinctPeople = peopleWithDuplicates.Distinct(new PersonNameComparer()); // contains only two unique "Alice" and "Bob"

            #endregion

            #region DistinctBy(); - remove duplicate elements from collection by specified key;

            var distinctByName = peopleWithDuplicates.DistinctBy(p => p.Name); // contains only two unique "Alice" and "Bob"

            // TODO: Add example for DistinctBy(x=> key, comparer) with custom comparer

            #endregion

            #region ElementAt(); - get element at specified index, with or without default value;

            var elementAtIndex = new int[] { 1, 2, 3, 4 }.ElementAt(2); // 3 becouse index 2 is the third element

            #endregion

            #region ElementAtOrDefault(); - get element at specified index or default value if index is out of range;

            var elementAtIndexWithDefault = new int[] { }.ElementAtOrDefault(0); // 0

            #endregion

            #region

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

    class Person
    {
        public string Name;
    }
    class PersonNameComparer : IEqualityComparer<Person>
    {
        public bool Equals(Person a, Person b)
        {
            return a.Name == b.Name;
        }

        public int GetHashCode(Person p)
        {
            return p.Name.GetHashCode();
        }
    }
}
