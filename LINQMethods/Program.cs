using System.Collections;


namespace LINQMethods
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // .NET 10 LINQ Methods examples with explanations

            #region Aggregate(); - Universal method can be used for any mathematic operation and concatenation with any type and more...

            // Aggregate(Func<TSource, TSource, TSource>)
            // Work similar like Sum, but can use all type(int, double, string), and all operations(+, -, *, /, % ...)
            int aggregate = new int[] { 1, 2, 3, 4 }.Aggregate((acc, x) => acc + x); // result is 10 because 1 + 2 + 3 + 4 = 10
            // acc - accumulator that stores the resat
            // x - current element from the array
            // acc + x - operation of accumulator: acc = acc + x; it can be any operation(+, -, *, /, %)

            // Aggregate(TAccumulate seed, Func<TAccumulate, TSource, TAccumulate>)
            // Similar to previous, but you can add starter value
            int aggregateWithSeed = new int[] { 1, 2, 3, 4 }.Aggregate(10, (acc, x) => acc + x); // result is 20 because 10 + 1 + 2 + 3 + 4 = 20
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

            #region AggregateBy(); - Universal method can be used for any mathematic operation and concatenation with any type and more... with key selector;

            #endregion

            #region All(); - check if all elements satisfy a condition;

            #endregion

            #region Any(); - check if any element satisfies a condition or if content any element;

            #endregion

            #region Append(); - add element to the end of collection;

            #endregion

            #region AsEnumerable(); - convert collection to IEnumerable type;

            #endregion

            #region Average(); - calculate average value of numeric collection;

            #endregion 

            #region Cast(); - convert collection to specified type used on collection without generic type ArrayList, Hashtable, IEnumerable(old);

            #endregion

            #region Chunk(); - split collection into smaller collections of specified size;

            #endregion

            #region Concat(); - combine two collections in one

            #endregion

            #region Count(); - count elements in collection, with or without condition;

            #endregion

            #region CountBy(); - count elements in collection by specified key, with or without condition;

            #endregion

            #region DefaultIfEmpty(); - provide default or custom value if collection is empty;

            #endregion

            #region Distinct(); - remove duplicate elements from collection;

            #endregion

            #region DistinctBy(); - remove duplicate elements from collection by specified key;

            #endregion

            #region ElementAt(); - get element at specified index, with or without default value;

            #endregion

            #region ElementAtOrDefault(); - get element at specified index or default value if index is out of range;

            #endregion

            #region Empty(); - create an empty collection of specified type;

            #endregion

            #region Except(); - get elements from first collection that are not in second collection;

            #endregion

            #region ExceptBy(); - get elements from first collection that are not in second collection by specified key;

            #endregion

            #region First(); - get first element from collection, with or without condition;

            #endregion

            #region FirstOrDefault(); - get first element from collection or default value if collection is empty, with or without condition;

            #endregion

            #region GroupBy(); - group elements in collection by specified key;

            #endregion

            #region GroupJoin(); - correlate elements from two collections based on matching keys and group the results;

            #endregion

            #region Index(); - get index of each element in collection;

            #endregion

            #region InfiniteSequence(); - create an infinite collection by specified generator function;

            #endregion

            #region Intersect(); - get common elements from two collections;

            #endregion

            #region IntersectBy(); - get common elements from two collections by specified key;

            #endregion

            #region Join(); - correlate elements from two collections based on matching keys;

            #endregion

            #region Last(); - get last element from collection, with or without condition;

            #endregion

            #region LastOrDefault(); - get last element from collection or default value if collection is empty, with or without condition;

            #endregion

            #region LeftJoin(); - perform left outer join between two collections;

            #endregion

            #region LongCount(); - count elements in collection, with or without condition, returns long type;

            #endregion

            #region Max(); - get maximum value from numeric collection, with or without selector;

            #endregion

            #region MaxBy(); - get element with maximum key value from collection;

            #endregion

            #region Min(); - get minimum value from numeric collection, with or without selector;

            #endregion

            #region MinBy(); - get element with minimum key value from collection;

            #endregion

            #region OfType(); - filter elements by specified type;

            #endregion

            #region Order(); - sort ascending using default comparer or provided comparer;

            #endregion

            #region OrderBy(); - sort elements in collection in ascending order by specified key, with or without comparer;

            #endregion

            #region OrderByDescending(); - sort elements in collection in descending order by specified key, with or without comparer;

            #endregion

            #region OrderDescending(); - sort elements in collection in descending order

            #endregion

            #region Prepend(); - add element to the beginning of collection;

            #endregion

            #region Range(); - create a collection with a range of sequential numbers;

            #endregion

            #region Repeat(); - create a collection with repeated elements;

            #endregion

            #region RightJoin(); - perform right outer join between two collections;

            #endregion

            #region Reverse(); - reverse the order of elements in collection;

            #endregion

            #region Select(); - project each element of a collection into a new form;

            #endregion

            #region SelectMany(); - project each element of a collection to an IEnumerable<T> and flatten the resulting collections into one collection;

            #endregion

            #region Sequence(); - generate a sequence of values based on a generator function;

            #endregion

            #region SequenceEqual(); - check if two collections are equal;

            #endregion

            #region Shuffle(); - randomly shuffle elements in collection;

            #endregion

            #region Single(); - get single element from collection, with or without condition;

            #endregion

            #region SingleOrDefault(); - get single element from collection or default value if collection is empty, with or without condition;

            #endregion

            #region Skip(); - skip specified number of elements in collection;

            #endregion

            #region SkipLast(); - skip specified number of elements from the end of collection;

            #endregion

            #region SkipWhile(); - skip elements in collection while condition is true;

            #endregion

            #region Sum(); - calculate sum of numeric collection, with or without selector;

            #endregion

            #region Take(); - take specified number of elements from collection;

            #endregion

            #region TakeLast(); - take specified number of elements from the end of collection;

            #endregion

            #region TakeWhile(); - take elements from collection while condition is true;

            #endregion

            #region ThenBy(); - perform a subsequent ordering of elements in collection in ascending order by specified key, with or without comparer;

            #endregion

            #region ThenByDescending(); - perform a subsequent ordering of elements in collection in descending order by specified key, with or without comparer;

            #endregion

            #region ToArray(); - convert collection to array;

            #endregion

            #region ToDictionary(); - convert collection to dictionary by specified key and element selectors, with or without comparer;

            #endregion

            #region ToHashSet(); - convert collection to HashSet, with or without comparer;

            #endregion

            #region ToList(); - convert collection to List;

            #endregion

            #region ToLookup(); - convert collection to Lookup by specified key and element selectors, with or without comparer;

            #endregion

            #region TryGetNonEnumeratedCount(); - try to get count of elements in collection without enumerating it;

            #endregion

            #region Union(); - get unique elements from two collections;

            #endregion

            #region UnionBy(); - get unique elements from two collections by specified key;

            #endregion

            #region Where(); - filter elements in collection by specified condition;

            #endregion

            #region Zip(); - merge two collections by combining corresponding elements;

            #endregion

        }
    }
}
