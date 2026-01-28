using System;
using System.Collections;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LINQMethods
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // TODO: Add .NET version information to the region description available since (.NET 6+)

            // TODO: Add IEqualityComparer and IComparer custom examples

            // TODO: Move classes to separate .cs files

            // Filtering (Фільтрація)

            #region Where(); - filter elements in collection by specified condition;

            // 1. Where(Func<TSource, bool>)

            // Filter even numbers from the collection
            var numbersForWhere = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var resultForWhere = numbersForWhere.Where(n => n % 2 == 0);
            // Result: 2, 4, 6, 8, 10

            // Filter words with length greater than 4
            string[] wordsForWhere = { "apple", "banana", "pear", "plum" };
            var longWordsForWhere = wordsForWhere.Where(w => w.Length > 4);
            // Result: "apple", "banana"

            // Filter adults from a collection of people
            var peopleForWhere = new[]
            {
                new People("Ann", 17),
                new People("Bob", 18),
                new People("Carl", 30),
            };
            var adultsForWhere = peopleForWhere.Where(p => p.Age >= 18);
            // Result: Bob and Carl


            // 2. Where(Func<TSource, int, bool>) - with index

            string[] colorsForWhere = { "red", "green", "blue", "yellow" };
            var resultTwoForWhere = colorsForWhere.Where((color, index) => index % 2 == 0);
            // Result: "red", "blue" (elements at index 0 and 2)


            // Deferred Execution:
            var numbersForDefExe = new List<int> { 1, 2, 3 };

            var queryForDefExe = numbersForDefExe.Where(n => n > 1);

            // Modify the source collection after defining the query
            numbersForDefExe.Add(10);

            // Now, when we iterate over the query, it reflects the updated collection
            foreach (var n in queryForDefExe)
            {
                Console.WriteLine(n);
            }
            // Result: 2, 3, 10
            // WHY? Because LINQ uses deferred execution and evaluates the query during iteration.

            #endregion

            #region OfType(); - filter elements by specified type;

            // OfType<TResult>(IEnumerable)
            // <TResult> is the type to return from the collection

            // 1. Basic example with mixed collection
            object[] mixedColForOfType =
            {
                1,
                "hello",
                2,
                "world",
                3.14,
                42
            };

            var onlyIntegers = mixedColForOfType.OfType<int>();
            // Result: 1, 2, 42

            var onlyStrings = mixedColForOfType.OfType<string>();
            // Result: "hello", "world"


            // 2. OfType with inheritance
            var animals = new Animal[]
            {
                new Dog { Name = "Rex" },
                new Cat { Name = "Misty" }
            };

            var dogs = animals.OfType<Dog>();
            // Result: Dog ("Rex")

            #endregion

            // Projection (Проекція/перетворення)

            #region Select(); - project (transform) each element into a new form;

            // 1. Select(Func<TSource, TResult>)

            // Transform numbers (square)
            var numbersForSelect = new[] { 1, 2, 3, 4, 5 };
            var squaresForSelect = numbersForSelect.Select(n => n * n);
            // Result: 1, 4, 9, 16, 25

            // Convert strings to uppercase
            string[] wordsForSelect = { "apple", "banana", "pear" };
            var upperWordsForSelect = wordsForSelect.Select(w => w.ToUpper());
            // Result: "APPLE", "BANANA", "PEAR"

            // Select a property from objects
            var peopleForSelect = new[]
            {
                new Person("Ann", 17),
                new Person("Bob", 18),
                new Person("Carl", 30),
            };

            var namesForSelect = peopleForSelect.Select(p => p.Name);
            // Result: "Ann", "Bob", "Carl"
            // Before: IEnumerable<Person> a collection of Person objects 
            // After: IEnumerable<string> a collection of strings 


            // 2. Select(Func<TSource, int, TResult>) - with index

            string[] colorsForSelect = { "red", "green", "blue", "yellow" };

            var indexedColorsForSelect = colorsForSelect.Select((color, index) => $"{index}: {color}");
            // Result: "0: red", "1: green", "2: blue", "3: yellow"

            // 3. Select to anonymous type (very common in LINQ)

            // Projects each Person into a new anonymous object containing only
            var simplifiedPeopleForSelect = peopleForSelect.Select(p => new { p.Name, IsAdult = p.Age >= 18 });
            // Result: { Name = "Ann", IsAdult = false }, { Name = "Bob", IsAdult = true }, ...



            #endregion

            #region SelectMany(); - project (transform) each element of a collection to an IEnumerable<T> and flatten the resulting collections into one collection;

            var ordersForSelectMany = new List<OrderForSelectMany>
            {
                new OrderForSelectMany { Id = 1, Items = new() { "Apple", "Banana" } },
                new OrderForSelectMany { Id = 2, Items = new() { "Orange" } },
                new OrderForSelectMany { Id = 3, Items = new() { "Milk", "Bread", "Apple" } }
            };

            // 1) SelectMany(Func<TSource, IEnumerable<TResult>>)
            // Return: IEnumerable<string> with all items from all orders flattened into a single collection
            var itemsFlat_1 = ordersForSelectMany.SelectMany(o => o.Items);
            // Result: "Apple", "Banana", "Orange", "Milk", "Bread", "Apple"


            // 2) SelectMany(Func<TSource, int, IEnumerable<TResult>>) - with index
            var itemsFlat_2 = ordersForSelectMany.SelectMany((o, index) =>
            {
                // index is the position of the element in the source sequence
                return o.Items.Select(item => $"{index}:{item}");
            });
            // Result: "0:Apple", "0:Banana", "1:Orange", "2:Milk", "2:Bread", "2:Apple"


            // 3) SelectMany(collectionSelector, resultSelector)
            var itemsWithOrderInfo_3 = ordersForSelectMany.SelectMany(
                o => o.Items,
                (o, item) => new { OrderId = o.Id, Item = item }
            );
            // Result:
            // { OrderId = 1, Item = "Apple" }
            // { OrderId = 1, Item = "Banana" }
            // { OrderId = 2, Item = "Orange" }
            // { OrderId = 3, Item = "Milk" }
            // { OrderId = 3, Item = "Bread" }


            // 4) SelectMany(collectionSelector with index, resultSelector)
            var itemsWithOrderInfo_4 = ordersForSelectMany.SelectMany(
                (o, index) => // index count automatically like in for loop i
                {
                    return o.Items.Select(item => new { index, item }); // return IEnumerable of anonymous type with index and item
                },
                (o, x) => new { OrderIndex = x.index, OrderId = o.Id, Item = x.item } // x is the anonymous type from above
            );

            // Result:
            // { OrderIndex = 0, OrderId = 1, Item = "Apple" }
            // { OrderIndex = 0, OrderId = 1, Item = "Banana" }
            // { OrderIndex = 1, OrderId = 2, Item = "Orange" }
            // { OrderIndex = 2, OrderId = 3, Item = "Milk" }
            // { OrderIndex = 2, OrderId = 3, Item = "Bread" }
            // { OrderIndex = 2, OrderId = 3, Item = "Apple" }


            #endregion

            #region Zip(); - combines elements from sequences based on their index (position);


            var numbersForZip = new[] { 1, 2, 3 };
            var wordsForZip = new[] { "one", "two", "three" };

            // 1) Zip<TFirst, TSecond, TResult> with result selector
            var zippedWithSelector = numbersForZip.Zip(
                wordsForZip,
                (number, word) => $"{number} = {word}"
            );

            // Result:
            // "1 = one"
            // "2 = two"
            // "3 = three"


            // 2) Zip<TFirst, TSecond> without selector - returns tuples IEnumerable<(int First, string Second)>
            var zippedTuples = numbersForZip.Zip(wordsForZip);
            // Result:
            // (1, "one")
            // (2, "two")
            // (3, "three")


            // 3) Zip<TFirst, TSecond, TThird> without selector - returns tuples IEnumerable<(int First, string Second, int Third)>
            var agesForZip = new[] { 17, 25, 30 };

            var zippedThree = numbersForZip.Zip(
                wordsForZip,
                agesForZip
            );
            // Result:
            // (1, "one", 17)
            // (2, "two", 25)
            // (3, "three", 30)


            // Zip stops at the shortest sequence
            var shortArray = new[] { 10 };

            var zipStopsEarly = numbersForZip.Zip(shortArray);
            // Result:
            // (1, 10)

            #endregion

            #region Index(); - add index for each elements in collection;

            var items = new[] { "Apple", "Banana", "Orange" };

            var indexed = items.Index();
            // Return IEnumerable<(int Index, string Item)> valueTuple
            // (0, "Apple")
            // (1, "Banana")    
            // (2, "Orange")

            #endregion

            // Sorting (Сортування)

            #region Order(); - sort elements in ascending order (e.g. A–Z, 1–9);

            // 1. Order<T>(IEnumerable<T>)

            // Sort numbers in ascending order
            var numbersForOrder = new[] { 5, 1, 3, 2 };
            var numbersResultForOrder = numbersForOrder.Order();
            // Result: 1, 2, 3, 5

            // Sort words in ascending order
            var wordsForOrder = new[] { "banana", "apple", "cherry" };
            var wordsResultForOrder = wordsForOrder.Order();
            // Result: apple, banana, cherry


            // 2. Order<T>(IEnumerable<T>, IComparer<T>)

            // Sort words in ascending order with custom comparer (case-insensitive)
            var resultForOrder = wordsForOrder.Order(StringComparer.OrdinalIgnoreCase);
            // Result: apple, banana, cherry 

            // Or use custom comparer class that implements IComparer<T> interface

            // TODO: Example compare with custom comparer class


            #endregion

            #region OrderBy(); - sort elements in collection in ascending order by specified key, with or without comparer;

            var peopleForOderdBy = new[]
            {
                new Person { Name = "Bob", Age = 30 },
                new Person { Name = "Alice", Age = 25 },
                new Person { Name = "Charlie", Age = 30 }
            };

            // 1. OrderBy<TKey>(Func<TSource, TKey>) - sort by key selector
            var resultForOderdBy = peopleForOderdBy.OrderBy(p => p.Age);
            // Result:
            // (Alice, 25)
            // (Bob, 30)
            // (Charlie, 30)


            // 2. OrderBy<TKey>(Func<TSource, TKey>, IComparer<TKey>) - sort by key selector with custom comparer
            var resultWithComparer = peopleForOderdBy.OrderBy(
                p => p.Name,
                StringComparer.OrdinalIgnoreCase // case-insensitive comparer for string
            );
            // Result:
            // (Alice, 25)
            // (Bob, 30)
            // (Charlie, 30)

            // NONE: You also can create your own custom comparer that implements IComparer<TKey> interface

            #endregion

            #region OrderDescending(); - sort elements in descending order (e.g. Z–A, 9–1)

            // 1. OrderDescending<T>() - sort using default comparison

            var numbersForOrderDes = new[] { 5, 1, 3, 2 };
            var numbersResultForOrderDes = numbersForOrderDes.OrderDescending();
            // Result: 5, 3, 2, 1

            var wordsForOrderDes = new[] { "banana", "apple", "cherry" };
            var wordsResultForOrderDes = wordsForOrderDes.OrderDescending();
            // Result: cherry, banana, apple


            // 2. OrderDescending<T>(IComparer<T>) - sort using custom comparer

            var resultForOrderDes = wordsForOrderDes.OrderDescending(StringComparer.OrdinalIgnoreCase);
            // Result: cherry, banana, apple

            // TODO: Example with custom IComparer<T>

            #endregion

            #region OrderByDescending(); - sort elements in descending order by specified key (e.g. Z–A, 9–1), with or without comparer

            var peopleForOrderByDes = new[]
                        {
                new Person { Name = "Bob", Age = 30 },
                new Person { Name = "Alice", Age = 25 },
                new Person { Name = "Charlie", Age = 30 }
            };

            // 1. OrderByDescending<TSource, TKey>(Func<TSource, TKey>) - sort by key selector (descending)
            var resultForOrderByDes = peopleForOrderByDes.OrderByDescending(p => p.Age);
            // Result:
            // (Bob, 30)
            // (Charlie, 30)
            // (Alice, 25)


            // 2. OrderByDescending<TSource, TKey>(Func<TSource, TKey>, IComparer<TKey>) - sort by key selector with custom comparer
            var resultWithComparerForOrderByDes = peopleForOrderByDes.OrderByDescending(
                p => p.Name,
                StringComparer.OrdinalIgnoreCase // compare string case-insensitive
            );
            // Result:
            // (Charlie, 30)
            // (Bob, 30)
            // (Alice, 25)

            // NOTE: You can also create your own custom comparer that implements IComparer <TKey>

            #endregion

            #region ThenBy(); - secondary sort for elements with equal keys from OrderBy();

            var peopleForThenBy = new[]
            {
                new Person { Name = "Bob", Age = 30 },
                new Person { Name = "Charlie", Age = 30 },
                new Person { Name = "Alice", Age = 25 }
            };

            // 1. ThenBy<TSource, TKey>(Func<TSource, TKey>) - secondary sort ascending
            var resultForThenBy = peopleForThenBy
                .OrderBy(p => p.Age)
                .ThenBy(p => p.Name);
            // Result:
            // (Alice, 25)
            // (Bob, 30)
            // (Charlie, 30)

            // 2. ThenBy<TSource, TKey>(Func<TSource, TKey>, IComparer<TKey>) - secondary sort ascending with custom comparer
            var resultWithComparerForThenBy = peopleForThenBy
                .OrderBy(p => p.Age)
                .ThenBy(p => p.Name, StringComparer.OrdinalIgnoreCase);
            // Result:
            // (Alice, 25)
            // (Bob, 30)
            // (Charlie, 30)

            // NOTE: You can also create your own custom comparer that implements IComparer<TKey>

            #endregion

            #region ThenByDescending(); - secondary sort in descending order for elements with equal keys from OrderBy;

            var peopleForThenByDesc = new[]
            {
                new Person { Name = "Bob", Age = 30 },
                new Person { Name = "Charlie", Age = 30 },
                new Person { Name = "Alice", Age = 25 }
            };

            // 1. ThenByDescending<TSource, TKey>(Func<TSource, TKey>) - secondary sort descending
            var resultForThenByDesc = peopleForThenByDesc
                .OrderBy(p => p.Age)
                .ThenByDescending(p => p.Name);
            // Result:
            // (Alice, 25)
            // (Charlie, 30)
            // (Bob, 30)


            // 2. ThenByDescending<TSource, TKey>(Func<TSource, TKey>, IComparer<TKey>) - secondary sort descending with custom comparer
            var resultWithComparerForThenByDesc = peopleForThenByDesc
                .OrderBy(p => p.Age)
                .ThenByDescending(p => p.Name, StringComparer.OrdinalIgnoreCase);
            // Result:
            // (Alice, 25)
            // (Charlie, 30)
            // (Bob, 30)

            // NOTE: You can also create your own custom comparer that implements IComparer<TKey>

            #endregion

            #region Reverse(); - reverse the order of elements in a sequence (without sorting)

            // 1. Reverse<T>()

            // Reverse numbers
            var numbersForReverse = new[] { 1, 2, 3, 4 };
            var resultForReverse = numbersForReverse.Reverse();
            // Result: 4, 3, 2, 1

            // Reverse words
            var wordsForReverse = new[] { "apple", "banana", "cherry" };
            var wordsResultForReverse = wordsForReverse.Reverse();
            // Result: cherry, banana, apple


            #endregion

            #region Shuffle(); - randomly shuffle elements in collection;

            var numbersForShuffle = new[] { 1, 2, 3, 4, 5 };
            var resultForShuffle = numbersForShuffle.Shuffle();
            // Result: 3, 1, 4, 5, 2 (randomized)


            #endregion

            // Partitioning (Порції/пагінація)

            #region Skip(); - skip a specified number of elements from the start of a sequence

            // 1. Skip<TSource>(IEnumerable<TSource>, int)
            var numbersForSkip = new[] { 1, 2, 3, 4, 5 };
            var resultForSkip = numbersForSkip.Skip(2);
            // Result: 3, 4, 5

            // Real-world example

            int page = 2; // current page number
            int pageSize = 5; // number of items per page

            var data = new[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K" };

            var pageItems = data
                .Skip((page - 1) * pageSize) // skip all items from previous pages
                .Take(pageSize);
            // Page 1: Skip(0) -> "A", "B", "C", "D", "E"
            // Page 2: Skip(5) -> "F", "G", "H", "I", "J"
            // Page 3: Skip(10) -> "K"

            #endregion

            #region SkipLast(); - skip a specified number of elements from the end of a sequence

            // 1. SkipLast<TSource>(IEnumerable<TSource>, int)
            var numbersForSkipLast = new[] { 1, 2, 3, 4, 5 };
            var resultForSkipLast = numbersForSkipLast.SkipLast(2);
            // Result: 1, 2, 3

            // SkipLast method is useful when you want to exclude a certain number of elements from the end of a collection

            #endregion

            #region SkipWhile(); - skip elements in collection while condition is true

            var numbersForSkipWhile = new[] { 1, 2, 3, 4, 1, 2 };

            // 1. SkipWhile(Func<TSource, bool>) - skip while condition is true
            var resultForSkipWhile = numbersForSkipWhile.SkipWhile(n => n < 4);
            // Result: 4, 1, 2


            // 2. SkipWhile(Func<TSource, int, bool>) - with index
            var numbersForSkipWhileWithIndex = new[] { 1, 2, 3, 4, 5, 6 };
            var resultWithIndexForSkipWhile = numbersForSkipWhileWithIndex
                .SkipWhile((value, index) => value < 4 && index < 3);
            // Result: 4, 5, 6

            #endregion

            #region Take(); - take elements from the start of a sequence or by range

            var numbersForTake = new[] { 1, 2, 3, 4, 5, 6 };

            // 1. Take(int count) - take first N elements
            var resultForTake = numbersForTake.Take(3);
            // Result: 1, 2, 3


            // 2. Take(Range) - take elements by range (.NET 6+)
            var resultForTakeByRange = numbersForTake.Take(1..4);    // Result: 2, 3, 4 (elements from index 1 to index 3 inclusive)
            // var resultForTakeByRange = numbersForTake.Take(..3);  // Result: 1, 2, 3 (elements from start to index 2)
            // var resultForTakeByRange = numbersForTake.Take(3..);  // Result: 4, 5, 6 (elements from index 3 to end)
            // var resultForTakeByRange = numbersForTake.Take(^3..); // Result: 4, 5, 6 (elements from index -3 to end)

            #endregion

            #region TakeLast(); - take a specified number of elements from the end of a sequence

            // TakeLast<TSource>(int count)
            var numbersForTakeLast = new[] { 1, 2, 3, 4, 5 };
            var resultForTakeLast = numbersForTakeLast.TakeLast(2);
            // Result: 4, 5

            #endregion

            #region TakeWhile(); - take elements from collection while condition is true

            var numbersForTakeWhile = new[] { 1, 2, 3, 4, 5, 6 };

            // 1. TakeWhile(Func<TSource, bool>) - take while condition is true
            var resultForTakeWhile = numbersForTakeWhile.TakeWhile(n => n < 4);
            // Result: 1, 2, 3


            // 2. TakeWhile(Func<TSource, int, bool>) - with index
            var numbersForTakeWhileWithIndex = new[] { 1, 2, 3, 10, 4, 5 };
            var resultWithIndexForTakeWhile = numbersForTakeWhileWithIndex
                .TakeWhile((value, index) => value < 5 && index < 3); // take while value < 5 and index < 3
            // Result: 1, 2, 3

            #endregion

            #region Chunk(); - split collection into smaller collections of specified size;
            
            int[] array = { 1, 2, 3, 4, 5, 6, 7 };

            // Chunk(int) - split array into chunks of specified size
            var chunks = array.Chunk(3);
            // Chunk() contain:
            // chunks[0] -> [1, 2, 3]
            // chunks[1] -> [4, 5, 6]
            // chunks[2] -> [7]

            // Convert chunks to jagged array
            int[][] jaggedArray = array.Chunk(3).ToArray();
            // Chunk() contain:
            // chunks[0] -> [1, 2, 3]
            // chunks[1] -> [4, 5, 6]
            // chunks[2] -> [7]

            #endregion

            // Set operations (Множини)

            #region Distinct(); - remove duplicate elements from a sequence

            var numbersForDistinct = new[] { 1, 2, 2, 3, 4, 4, 5 };

            // 1. Distinct() - remove duplicates using default equality comparer
            var resultForDistinct = numbersForDistinct.Distinct();
            // Result: 1, 2, 3, 4, 5


            // 2. Distinct(IEqualityComparer<T>) - remove duplicates using custom comparer
            var wordsForDistinct = new[] { "Apple", "apple", "Banana", "BANANA" };

            var resultWithComparerForDistinct = wordsForDistinct.Distinct(StringComparer.OrdinalIgnoreCase);
            // Result: "Apple", "Banana"

            // NOTE: You can also create your own custom comparer that implements IEqualityComparer<T> interface

            #endregion

            #region DistinctBy(); - remove duplicate elements from a sequence by key (.NET 6+)

            var peopleForDistinctBy = new[]
            {
                new Person { Name = "Alice", Age = 25 },
                new Person { Name = "Bob", Age = 30 },
                new Person { Name = "Alice", Age = 40 },
                new Person { Name = "Charlie", Age = 30 },
                new Person { Name = "bob", Age = 30 }
            };

            // 1. DistinctBy(Func<TSource, TKey>) - remove duplicates by key selector
            var resultForDistinctBy = peopleForDistinctBy.DistinctBy(p => p.Name);
            // Result:
            // (Alice, 25)
            // (Bob, 30)
            // (Charlie, 30)
            // (bob, 30)


            // 2. DistinctBy(Func<TSource, TKey>, IEqualityComparer<TKey>) - remove duplicates by key with custom comparer
            var resultWithComparerForDistinctBy = peopleForDistinctBy
                .DistinctBy(p => p.Name, StringComparer.OrdinalIgnoreCase);
            // Result:
            // (Alice, 25)
            // (Bob, 30)
            // (Charlie, 30)
            // "bob" is considered duplicate of "Bob" due to case-insensitive comparison

            #endregion

            #region Union(); - produce the set union of two sequences and remove duplicates;

            var firstForUnion = new[] { 1, 2, 3, 4 };
            var secondForUnion = new[] { 3, 4, 5, 6 };

            // 1. Union(IEnumerable<TSource>) - combine two sequences and remove duplicates using default comparer
            var resultForUnion = firstForUnion.Union(secondForUnion);
            // Result: 1, 2, 3, 4, 5, 6


            // 2. Union(IEnumerable<TSource>, IEqualityComparer<TSource>) - combine sequences using custom comparer
            var wordsFirstForUnion = new[] { "Apple", "Banana" };
            var wordsSecondForUnion = new[] { "apple", "Orange" };

            var resultWithComparerForUnion = wordsFirstForUnion
                .Union(wordsSecondForUnion, StringComparer.OrdinalIgnoreCase);
            // Result: "Apple", "Banana", "Orange"

            // NOTE: You can also create your own custom comparer that implements IEqualityComparer<T> interface

            #endregion

            #region UnionBy(); - produce the set union of two sequences by key and remove duplicates (.NET 6+)

            var firstForUnionBy = new[]
            {
                new Person { Name = "Alice", Age = 25 },
                new Person { Name = "Bob", Age = 30 }
            };

            var secondForUnionBy = new[]
            {
                new Person { Name = "Alice", Age = 40 },   // duplicate by Name
                new Person { Name = "Charlie", Age = 35 }
            };

            // 1. UnionBy(IEnumerable<TSource>, Func<TSource, TKey>) - combine sequences and remove duplicates by key
            var resultForUnionBy = firstForUnionBy.UnionBy(secondForUnionBy, p => p.Name);
            // Result:
            // (Alice, 25)
            // (Bob, 30)
            // (Charlie, 35)


            // 2. UnionBy(IEnumerable<TSource>, Func<TSource, TKey>, IEqualityComparer<TKey>) - combine by key with custom comparer
            var wordsFirstForUnionBy = new[] { "Apple", "Banana" };
            var wordsSecondForUnionBy = new[] { "apple", "Orange" };

            var resultWithComparerForUnionBy = wordsFirstForUnionBy
                .UnionBy(wordsSecondForUnionBy, w => w, StringComparer.OrdinalIgnoreCase);
            // Result: "Apple", "Banana", "Orange"

            // NOTE: You can also create your own custom comparer that implements IEqualityComparer<TKey> interface

            #endregion

            #region Intersect(); - produce the set intersection of two sequences and remove duplicates

            var firstForIntersect = new[] { 1, 2, 3, 4, 5 };
            var secondForIntersect = new[] { 3, 4, 5, 6, 7 };

            // 1. Intersect(IEnumerable<TSource>) - return common elements using default comparer
            var resultForIntersect = firstForIntersect.Intersect(secondForIntersect);
            // Result: 3, 4, 5


            // 2. Intersect(IEnumerable<TSource>, IEqualityComparer<TSource>) - return common elements using custom comparer
            var wordsFirstForIntersect = new[] { "Apple", "Banana", "Orange" };
            var wordsSecondForIntersect = new[] { "apple", "KIWI", "orange" };

            var resultWithComparerForIntersect = wordsFirstForIntersect
                .Intersect(wordsSecondForIntersect, StringComparer.OrdinalIgnoreCase);
            // Result: "Apple", "Orange"

            // NOTE: You can also create your own custom comparer that implements IEqualityComparer<T> interface

            #endregion

            #region IntersectBy(); - get common elements from two collections by specified key;

            // 1. IntersectBy(IEnumerable<TKey>, Func<TSource, TKey>) - by key selector
            var usersForIntersect = new[]
            {
                new UserForIntersect { Id = 1, Name = "Ann" },
                new UserForIntersect { Id = 2, Name = "Bob" },
                new UserForIntersect { Id = 3, Name = "Carl" }
            };

            var ids = new[] { 2, 3 };

            var resultIntersectBy = usersForIntersect.IntersectBy(ids, u => u.Id);
            // Result:
            // { Id = 2, Name = "Bob" }
            // { Id = 3, Name = "Carl" }


            // 2. IntersectBy(IEnumerable<TKey>, Func<TSource, TKey>, IEqualityComparer<TKey>) - by key selector with custom comparer

            var words = new[] { "Apple", "Banana", "Orange" };
            var filter = new[] { "apple", "banana" };

            var resultForIntersectBy = wordsForWhere.IntersectBy(
                filter,
                w => w, // key selector
                StringComparer.OrdinalIgnoreCase
            );
            // Result: "Apple", "Banana"

            // NOTE: You can also create your own custom comparer that implements IEqualityComparer<TKey> interface

            #endregion

            #region Except(); - produce the set difference of two sequences and remove duplicates
            // What is in the first collection but not in the second

            var firstForExcept = new[] { 1, 2, 3, 4, 5 };
            var secondForExcept = new[] { 3, 4, 6 };

            // 1. Except(IEnumerable<TSource>) - return elements from first sequence that are not in second
            var resultForExcept = firstForExcept.Except(secondForExcept);
            // Result: 1, 2, 5
            // Number 6 is ignored because Except only returns elements from the first sequence that are not in the second


            // 2. Except(IEnumerable<TSource>, IEqualityComparer<TSource>) - return difference using custom comparer
            var wordsFirstForExcept = new[] { "Apple", "Banana", "Orange" };
            var wordsSecondForExcept = new[] { "apple", "KIWI" };

            var resultWithComparerForExcept = wordsFirstForExcept
                .Except(wordsSecondForExcept, StringComparer.OrdinalIgnoreCase);
            // Result: "Banana", "Orange"

            #endregion

            #region ExceptBy(); - produce the set difference of a sequence by key (.NET 6+)
            // What is in first collection but not in second (by key)

            var peopleForExceptBy = new[]
            {
                new Person { Name = "Alice", Age = 25 },
                new Person { Name = "Bob", Age = 30 },
                new Person { Name = "Charlie", Age = 35 }
            };

            var blockedNames = new[] { "Bob", "Eve" };


            // 1. ExceptBy(IEnumerable<TKey>, Func<TSource, TKey>) - return elements from first sequence whose keys are not in second
            var resultForExceptBy = peopleForExceptBy.ExceptBy(blockedNames, p => p.Name);
            // Result:
            // (Alice, 25)
            // (Charlie, 35)
            // Note: "Eve" is ignored because ExceptBy only returns elements from the first sequence


            // 2. ExceptBy(IEnumerable<TKey>, Func<TSource, TKey>, IEqualityComparer<TKey>) - return difference by key using custom comparer
            var wordsFirstForExceptBy = new[] { "Apple", "Banana", "Orange" };
            var wordsSecondForExceptBy = new[] { "apple", "KIWI" };

            var resultWithComparerForExceptBy = wordsFirstForExceptBy
                .ExceptBy(wordsSecondForExceptBy, w => w, StringComparer.OrdinalIgnoreCase);
            // Result: "Banana", "Orange"

            #endregion

            // Element operations (Вибір елемента)

            #region First(); - return the first element of a sequence, with or without a condition
            // Get the first element from a sequence or the first element that satisfies a condition

            var numbersForFirst = new[] { 1, 3, 4, 6 };


            // 1. First<TSource>() - return the first element of the sequence
            var firstForFirst = numbersForFirst.First();
            // Result: 1


            // 2. First<TSource>(Func<TSource, bool>) - return the first element that satisfies the condition
            var firstEvenForFirst = numbersForFirst.First(x => x % 2 == 0);
            // Result: 4 (because 4 is the first even number that satisfies the condition)


            // 3. Example with empty sequence, that causes First() to throw an exception
            var emptyForFirst = Array.Empty<int>();

            // var valueForFirst = emptyForFirst.First();
            // Result: throws InvalidOperationException


            // NOTICE: If the sequence is empty or no elements satisfy the condition, First() throws an InvalidOperationException.

            #endregion

            #region FirstOrDefault(); - get first element from collection or default value if collection is empty, with or without condition;

            int[] numbersForFirstOrDefault = { 1, 3, 4, 6 };

            // 1. FirstOrDefault<TSource>() - return the first element of the sequence or default(T) if the sequence is empty
            var firstForFirstOrDefault = numbersForFirstOrDefault.FirstOrDefault();
            // Result: 1


            // 2. FirstOrDefault<TSource>(Func<TSource, bool>) - return the first element that satisfies the condition or default(T)
            var firstEvenForFirstOrDefault = numbersForFirstOrDefault.FirstOrDefault(x => x % 2 == 0);
            // Result: 4 (because 4 is the first even number that satisfies the condition)


            // Example 1: If collection is empty return default value
            int firstOrDefaultEmpty = new int[] { }.FirstOrDefault();
            // Result: 0 because default for int is 0


            // Example 2: Overloads with default value
            int firstOrDefaultWithDefault = new int[] { }.FirstOrDefault(999);
            // Result: 999 (because this is your custom default value)
            // You can provide your own default value instead of default(T)


            // Example 3: FirstOrDefault with condition and custom default value
            int firstOrDefaultWithConditionAndDefault = numbersForFirstOrDefault.FirstOrDefault(x => x > 10, 999);
            // Result: 999 (because no elements satisfy the condition and we provided a default value of 999)

            #endregion

            #region Last(); - get last element from collection, with or without condition;

            int[] numbersForLast = { 1, 3, 4, 6 };


            // 1. Last<TSource>() - return the last element of the sequence
            var lastForLast = numbersForLast.Last();
            // Result: 6


            // 2. Last<TSource>(Func<TSource, bool>) - return the last element that satisfies the condition
            var lastEvenForLast = numbersForLast.Last(x => x % 2 == 0);
            // Result: 6 (because 6 is the last even number that satisfies the condition)


            // 3. Example with an empty sequence that causes Last() to throw an exception
            var emptyForLast = Array.Empty<int>();
            // var valueForLast = emptyForLast.Last();
            // Result: throws InvalidOperationException

            // NOTICE: If the sequence is empty or no elements satisfy the condition, Last() throws an InvalidOperationException.

            // NOTICE: Last() needs to iterate through the entire collection to find the last element,
            // which may have performance implications for large collections.

            #endregion

            #region LastOrDefault(); - get last element from sequence or default value if sequence is empty, with or without condition;

            int[] numbersForLastOrDefault = { 1, 3, 4, 6 };

            // 1. LastOrDefault<TSource>() - return the last element of the sequence or default(T) if the sequence is empty
            var lastForLastOrDefault = numbersForLastOrDefault.LastOrDefault();
            // Result: 6


            // 2. LastOrDefault<TSource>(TSource defaultValue) - return the last element or the provided default value if the sequence is empty
            int lastOrDefaultWithDefaultForLastOrDefault = Array.Empty<int>().LastOrDefault(999);
            // Result: 999 (custom default value)


            // 3. LastOrDefault<TSource>(Func<TSource, bool>) - return the last element that satisfies the condition or default(T)
            var lastEvenForLastOrDefault = numbersForLastOrDefault.LastOrDefault(x => x % 2 == 0);
            // Result: 6 (because 4 and 6 are even, and 6 is the last even number)


            // 4. LastOrDefault<TSource>(Func<TSource, bool>, TSource defaultValue) - return the last element that
            // satisfies the condition or the provided default value
            int lastOrDefaultWithConditionAndDefaultForLastOrDefault = numbersForLastOrDefault.LastOrDefault(x => x > 10, 999);
            // Result: 999 (no elements satisfy the condition, custom default value is returned)


            // 5. Example with empty sequence (returns default(T))
            int lastOrDefaultEmptyForLastOrDefault = Array.Empty<int>().LastOrDefault();
            // Result: 0 because default for int is 0


            // NOTICE: LastOrDefault() does NOT throw an exception.
            // It returns default(T) or the provided default value if no element is found.

            #endregion

            #region Single(); - get single element from collection, with or without condition;
            // Single means that the sequence must contain exactly one element (or exactly one matching element),
            // otherwise thrown an InvalidOperationException

            // 1. Single<TSource>() - return the only element of the sequence
            int[] singleItemForSingle = { 42};
            var singleForSingle = singleItemForSingle.Single();
            // Result: 42 (because 42 is the only element in the sequence)


            // Example 1: sequence is empty throws exception
            var emptyForSingle = Array.Empty<int>();
            // var valueEmptyForSingle = emptyForSingle.Single();
            // Result: throws InvalidOperationException


            // Example 2: sequence has more than one element throws exception
            int[] multipleItemsForSingle = { 10, 20 };
            // var valueMultipleForSingle = multipleItemsForSingle.Single();
            // Result: throws InvalidOperationException


            // 2. Single<TSource>(Func<TSource, bool>) - return the only element that satisfies the condition
            int[] oneEvenForSingle = { 1, 3, 4, 5 };

            var singleEvenForSingle = oneEvenForSingle.Single(x => x % 2 == 0);
            // Result: 4 (because 4 is the only even number)

            // NOTE: If no elements or more than one element satisfy the condition, Single() throws an InvalidOperationException

            #endregion

            #region SingleOrDefault(); - get single element from collection or default value if collection is empty, with or without condition;

            // 1. SingleOrDefault<TSource>() - return the only element of the sequence or default(T) if the sequence is empty
            int[] singleItemForSingleOrDefault = { 42 };
            var singleForSingleOrDefault = singleItemForSingleOrDefault.SingleOrDefault();
            // Result: 42


            // Example 1: sequence is empty (returns default(T))
            var emptyForSingleOrDefault = Array.Empty<int>();
            var valueEmptyForSingleOrDefault = emptyForSingleOrDefault.SingleOrDefault();
            // Result: 0 (because default for int is 0)


            // Example 2: sequence has more than one element (throws)
            int[] multipleItemsForSingleOrDefault = { 10, 20 };
            // var valueMultipleForSingleOrDefault = multipleItemsForSingleOrDefault.SingleOrDefault();
            // Result: throws InvalidOperationException


            // 2. SingleOrDefault<TSource>(TSource defaultValue) - return the only element or the provided default value if the sequence is empty
            int singleOrDefaultWithDefaultForSingleOrDefault = Array.Empty<int>().SingleOrDefault(999);
            // Result: 999 (custom default value)

            // NOTE: But if the sequence rerurn two or more elements, it will still throw InvalidOperationException


            // 3. SingleOrDefault<TSource>(Func<TSource, bool>) - return the only element that satisfies the condition or default(T)
            int[] oneEvenForSingleOrDefault = { 1, 3, 4, 5 };
            var singleEvenForSingleOrDefault = oneEvenForSingleOrDefault.SingleOrDefault(x => x % 2 == 0);
            // Result: 4 (because 4 is the only even number)


            // 4. SingleOrDefault<TSource>(Func<TSource, bool>, TSource defaultValue) - return the only element that satisfies the condition
            // or the provided default value if no elements match
            int singleOrDefaultWithConditionAndDefaultForSingleOrDefault =
                oneEvenForSingleOrDefault.SingleOrDefault(x => x > 10, 999);
            // Result: 999 (no elements satisfy the condition, custom default value is returned)


            // NOTE: It throws InvalidOperationException if more than one element exists or more than one element matches the condition.

            #endregion

            #region ElementAt(); - get element at specified index, with or without default value;

            int[] numbersForElementAt = { 10, 20, 30, 40 };

            // 1. ElementAt<TSource>(int index) - return the element at the specified index
            var elementAtIndexForElementAt = numbersForElementAt.ElementAt(2);
            // Result: 30 (because it's the element at index 2)


            // Example 1: index is out of range (throws)
            var shortSequenceForElementAt = new[] { 1, 2 };
            // var outOfRangeForElementAt = shortSequenceForElementAt.ElementAt(5);
            // Result: throws ArgumentOutOfRangeException


            // Example 2: negative index (throws)
            // var negativeIndexForElementAt = numbersForElementAt.ElementAt(-1);
            // Result: throws ArgumentOutOfRangeException


            // 2. ElementAt<TSource>(Index index) - return the element at the specified index (supports from end index)
            var lastElementForElementAt = numbersForElementAt.ElementAt(^1);
            // Result: 40 (last element, index from the end)


            // NOTICE: ElementAt() throws ArgumentOutOfRangeException if the index is less than 0
            // or greater than or equal to the number of elements in the sequence.

            #endregion

            #region ElementAtOrDefault(); - get element at specified index or default value if index is out of range;

            int[] numbersForElementAtOrDefault = { 10, 20, 30, 40 };

            // 1. ElementAtOrDefault<TSource>(int index) - return the element at the specified index or default(T) if the index is out of range
            var elementAtIndexForElementAtOrDefault = numbersForElementAtOrDefault.ElementAtOrDefault(2);
            // Result: 30 (because it's the element at index 2)


            // Example 1: index is out of range (returns default(T))
            var outOfRangeForElementAtOrDefault = numbersForElementAtOrDefault.ElementAtOrDefault(10);
            // Result: 0 (because default for int is 0)


            // Example 2: reference types return null by default
            string[] wordsForElementAtOrDefault = { "Apple", "Banana" };
            var outOfRangeWordForElementAtOrDefault = wordsForElementAtOrDefault.ElementAtOrDefault(5);
            // Result: null


            // 2. ElementAtOrDefault<TSource>(Index index) - return the element at the specified index or default(T) if the index is out of range
            var lastElementForElementAtOrDefault = numbersForElementAt.ElementAtOrDefault(^1);
            // Result: 40 (last element, index from the end)

            // NOTICE: ElementAtOrDefault() does NOT throw an exception when the index is out of range.
            // It returns default(T) instead (0 for int, null for reference types, etc.).

            #endregion

            // Quantifiers (Перевірки)

            #region All(); - check if all elements satisfy a condition;

            // 1. All<TSource>(Func<TSource, bool>) - return true if all elements satisfy the condition
            int[] numbersForAll = { 2, 4, 6, 8 };
            var allEvenForAll = numbersForAll.All(x => x % 2 == 0);
            // Result: true (all numbers are even)


            // Example 1: not all elements satisfy the condition
            int[] mixedNumbersForAll = { 2, 3, 4, 6 };
            var allEvenFalseForAll = mixedNumbersForAll.All(x => x % 2 == 0);
            // Result: false (3 is not even)


            // Example 2: empty sequence (returns true)
            var allEmptyForAll = Array.Empty<int>().All(x => x > 0);
            // Result: true (vacuously true, no elements violate the condition)


            // Example 3: reference types
            string[] wordsForAll = { "Apple", "Avocado", "Apricot" };
            var allStartWithAForAll = wordsForAll.All(w => w.StartsWith("A"));
            // Result: true


            // NOTICE: All() returns true for an empty sequence because there are no elements that violate the condition

            #endregion

            #region Any(); - check if any element satisfies a condition or if the sequence contains any elements;

            int[] numbersForAny = { 1, 3, 4, 7 };

            // 1. Any<TSource>() - return true if the sequence contains at least one element
            var anyForAny = numbersForAny.Any();
            // Result: true (the sequence is not empty)


            // Example 1: empty sequence (return false)
            var anyEmptyForAny = Array.Empty<int>().Any();
            // Result: false (the sequence contains no elements)


            // 2. Any<TSource>(Func<TSource, bool>) - return true if any element satisfies the condition
            var anyEvenForAny = numbersForAny.Any(x => x % 2 == 0);
            // Result: true (4 is even)

            // Example 1: no elements satisfy the condition
            int[] oddNumbersForAny = { 1, 3, 5, 7 };
            var anyEvenFalseForAny = oddNumbersForAny.Any(x => x % 2 == 0);
            // Result: false (no even numbers found)


            // Example 2: reference types
            string[] wordsForAny = { "Apple", "Banana", "Cherry" };
            var anyStartWithBForAny = wordsForAny.Any(w => w.StartsWith("B"));
            // Result: true ("Banana" starts with 'B')

            // Note: Any() returns false for an empty sequence because there are no elements to satisfy the condition

            #endregion

            #region Contains(); - check if collection contains specified element;

            string[] wordsForContains = { "Apple", "Banana", "Orange" };


            // 1. Contains<TSource>(TSource value) - return true if the sequence contains the specified element
            var containsBananaForContains = wordsForContains.Contains("Banana");
            // Result: true


            // Example 1: element is not found
            var containsKiwiForContains = wordsForContains.Contains("Kiwi");
            // Result: false


            // Example 2: with numbers
            int[] numbersForContains = { 1, 3, 4, 6 };
            var containsFourForContains = numbersForContains.Contains(4);
            // Result: true


            // 2. Contains<TSource>(TSource value, IEqualityComparer<TSource> comparer) - return true using a custom comparer
            var containsAppleIgnoreCaseForContains =
                wordsForContains.Contains("apple", StringComparer.OrdinalIgnoreCase);
            // Result: true

            // Example 1: reference types and null
            string[] wordsWithNullForContains = { "A", null, "B" };
            var containsNullForContains = wordsWithNullForContains.Contains(null);
            // Result: true

            // TODO: You can also create your own custom comparer that implements IEqualityComparer<T> interface

            #endregion

            // Aggregation (Агрегація)

            #region Count(); - count elements in collection, with or without condition;

            int[] numbersForCount = { 1, 3, 4, 6 };

            // 1. Count<TSource>() - return the total number of elements in the sequence
            var countForCount = numbersForCount.Count();
            // Result: 4


            // Example 1.1: empty sequence
            var countEmptyForCount = Array.Empty<int>().Count();
            // Result: 0


            // Example 2.2: reference types
            string[] wordsForCount = { "Apple", "Banana", "Cherry" };
            var countWordsForCount = wordsForCount.Count();
            // Result: 3


            // 2. Count<TSource>(Func<TSource, bool>) - return the number of elements that satisfy the condition
            var evenCountForCount = numbersForCount.Count(x => x % 2 == 0);
            // Result: 2 (4 and 6)


            // Example 2.1: counting with a condition on reference types
            var countStartWithBForCount = wordsForCount.Count(w => w.StartsWith("B"));
            // Result: 1 ("Banana")

            #endregion

            #region CountBy(); - count elements in collection by specified key, with or without condition;

            People[] peopleForCountBy =
            {
                new People("Alice", 25),
                new People("Bob", 30),
                new People("Charlie", 25)
            };


            // 1. CountBy<TSource, TKey>(Func<TSource, TKey>) - count elements by key
            var countByAgeForCountBy = peopleForCountBy.CountBy(p => p.Age);
            // Result:
            // (25, 2)
            // (30, 1)


            // Example 1: count people by age group (adult / under 30)
            var countByAgeGroupForCountBy =
                peopleForCountBy.CountBy(p => p.Age >= 30);
            // Result:
            // (false, 2) // under 30
            // (true, 1)  // 30 and above


            // Example 2: count people by first letter of name
            var countByFirstLetterForCountBy =
                peopleForCountBy.CountBy(p => p.Name[0]);
            // Result:
            // ('A', 1)
            // ('B', 1)
            // ('C', 1)

            #endregion

            #region LongCount(); - count elements in collection, with or without condition, returns long type;

            int[] numbersForLongCount = { 1, 3, 4, 6 };

            // 1. LongCount<TSource>() - return the total number of elements in the sequence as long
            var longCountForLongCount = numbersForLongCount.LongCount();
            // Result: 4L


            // 2. LongCount<TSource>(Func<TSource, bool>) - return the number of elements that satisfy the condition as long
            var evenLongCountForLongCount = numbersForLongCount.LongCount(x => x % 2 == 0);
            // Result: 2L (4 and 6)


            // Example 1: empty sequence
            var longCountEmptyForLongCount = Array.Empty<int>().LongCount();
            // Result: 0L


            // Example 2: reference types
            string[] wordsForLongCount = { "Apple", "Banana", "Cherry" };
            var longCountWordsForLongCount = wordsForLongCount.LongCount();
            // Result: 3L

            #endregion

            #region TryGetNonEnumeratedCount(); - try to get count of elements in collection without enumerating it;
            // TryGetNonEnumeratedCount() does NOT count elements. It checks whether the sequence already knows its exact count.

            int[] numbersForTryGetNonEnumeratedCount = { 1, 2, 3, 4 };

            // 1. TryGetNonEnumeratedCount(out int count) - try to get count without enumeration
            bool successForArray =
                numbersForTryGetNonEnumeratedCount.TryGetNonEnumeratedCount(out int countForArray);
            // Result:
            // successForArray = true
            // countForArray = 4


            // Example 1: List<T> also supports non-enumerated count
            List<string> wordsForTryGetNonEnumeratedCount = new() { "Apple", "Banana", "Cherry" };

            bool successForList =
                wordsForTryGetNonEnumeratedCount.TryGetNonEnumeratedCount(out int countForList);
            // Result:
            // successForList = true
            // countForList = 3


            // Example 2: IEnumerable<T> without known count
            IEnumerable<int> generatedSequenceForTryGetNonEnumeratedCount =
                Enumerable.Range(1, 10).Where(x => x > 5);

            bool successForEnumerable =
                generatedSequenceForTryGetNonEnumeratedCount.TryGetNonEnumeratedCount(out int countForEnumerable);
            // Result:
            // successForEnumerable = false
            // countForEnumerable = 0


            // Example 3: safe fallback to Count()
            int finalCountForTryGetNonEnumeratedCount =
                generatedSequenceForTryGetNonEnumeratedCount.TryGetNonEnumeratedCount(out int safeCount)
                    ? safeCount
                    : generatedSequenceForTryGetNonEnumeratedCount.Count();
            // Result: 5

            #endregion

            #region Max(); - get maximum value from numeric collection, with or without selector;

            int[] numbersForMax = { 1, 3, 4, 6 };


            // 1. Max<TSource>() - return the maximum element in the sequence
            var maxForMax = numbersForMax.Max();
            // Result: 6


            // Example 1: empty sequence (throws)
            var emptyForMax = Array.Empty<int>();
            // var valueEmptyForMax = emptyForMax.Max();
            // Result: throws InvalidOperationException


            // 2. Max<TSource>(Func<TSource, TResult>) - return the maximum value projected by the selector
            People[] peopleForMax =
            {
                new People("Alice", 25),
                new People("Bob", 30),
                new People("Charlie", 20)
            };

            var maxAgeForMax = peopleForMax.Max(p => p.Age);
            // Result: 30


            // Example 2: reference types with selector
            string[] wordsForMax = { "Apple", "Banana", "Cherry" };

            var maxLengthForMax = wordsForMax.Max(w => w.Length);
            // Result: 6 ("Banana" or "Cherry")


            // Example 3: nullable value types
            int?[] nullableNumbersForMax = { 1, null, 5, 3 };

            var maxNullableForMax = nullableNumbersForMax.Max();
            // Result: 5


            // Example 4: all values are null (returns null)
            int?[] allNullsForMax = { null, null };

            var maxAllNullsForMax = allNullsForMax.Max();
            // Result: null

            #endregion

            #region MaxBy(); - get element with maximum key value from collection;

            People[] peopleForMaxBy =
            {
                new People("Alice", 25),
                new People("Bob", 30),
                new People("Charlie", 20)
            };


            // 1. MaxBy<TSource, TKey>(Func<TSource, TKey>) - return the element with the maximum key
            var oldestPersonForMaxBy = peopleForMaxBy.MaxBy(p => p.Age);
            // Result: People { Name = "Bob", Age = 30 }


            // Example 1: multiple elements with the same max key
            People[] peopleWithSameMaxAgeForMaxBy =
            {
                new People("Alice", 30),
                new People("Bob", 30),
                new People("Charlie", 25)
            };

            var firstOldestForMaxBy = peopleWithSameMaxAgeForMaxBy.MaxBy(p => p.Age);
            // Result: People { Name = "Alice", Age = 30 }
            // Note: If multiple elements have the same maximum key, the FIRST one is returned.


            // Example 2: empty sequence
            var emptyForMaxBy = Array.Empty<People>();

            // var valueEmptyForMaxBy = emptyForMaxBy.MaxBy(p => p.Age);
            // Result: throws InvalidOperationException


            // Example 3: reference types with selector
            string[] wordsForMaxBy = { "Apple", "Banana", "Cherry" };

            var longestWordForMaxBy = wordsForMaxBy.MaxBy(w => w.Length);
            // Result: "Banana" (or "Cherry", but "Banana" appears first)

            #endregion

            #region Min(); - get minimum value from numeric collection, with or without selector;

            #endregion

            #region MinBy(); - get element with minimum key value from collection;

            #endregion

            #region Sum(); - calculate sum of numeric collection, with or without selector;

            #endregion

            #region Average(); - calculate average value of numeric collection;

            // Average() work with numeric types only(int, double, float, decimal, long, short, byte) and nullable as well(int?, double?, float?, decimal?, long?, short?, byte?)
            // returns double or decimal type depending on the input
            double average = new int[] { 1, 2, 3, 4, 5 }.Average(); // result is 3.0 because (1 + 2 + 3 + 4 + 5) / 5 = 3.0


            var people = new[] { new { Name = "Ann", Age = 20 }, new { Name = "Bob", Age = 30 }, new { Name = "Carl", Age = 40 } };
            // This Average with selector for collections of complex types
            var averageWithSelector = peopleForOderdBy.Average(p => p.Age); // result is 30.0 because (20 + 30 + 40) / 3 = 30.0

            #endregion 

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

            // Grouping (Групування)

            #region GroupBy(); - group elements in collection by specified key;

            var pet = new[]
            {
                new Pet("Ann",  "Berlin", 2),
                new Pet("Bob",  "Berlin", 3),
                new Pet("Carl", "Hamburg", 4),
                new Pet("Dina", "Hamburg", 2),
                new Pet("Eve",  "BERLIN", 3),
            };

            // GroupBy has multiple overloads, here are some examples:

            // 1. GroupBy(keySelector)
            var groupedByCity = pet.GroupBy(p => p.City);
            // Group pets by their City property like this
            // Berlin → [ (Ann, 2), (Bob, 3) ]
            // Hamburg → [ (Carl, 4), (Dina, 2) ]
            // BERLIN → [ (Eve, 3) ]  (note: "BERLIN" is treated as a separate group due to case sensitivity)

            // Example how to use the grouped result
            //foreach (var group in groupedByCity)
            //{
            //    Console.WriteLine($"City: {group.Key}");
            //    foreach (var petInGroup in group)
            //    {
            //        Console.WriteLine($" - Pet Name: {petInGroup.Name}, Age: {petInGroup.Age}");
            //    }
            //}


            // 2. GroupBy(keySelector, comparer)
            // Using StringComparer.OrdinalIgnoreCase to make the grouping case-insensitive
            // "Berlin" == "berlin" == "BERLIN"
            var groups = pet.GroupBy(p => p.City, StringComparer.OrdinalIgnoreCase);


            // 3. GroupBy(keySelector, elementSelector)
            // elementSelector - used to select what is stored in each group
            var groupsWithElementSelector = pet.GroupBy(
                p => p.City,
                p => p.Name // group by City but select only Name property for each pet in the group
                // p => new { p.Name, p.Age } // you can select multiple properties like this, more people use anonymous type
                // p => (p.Name, p.Age // or select tuple
                // p => new PetInfo { p.Name, p.Age } // or select custom type
                // p => p // or select the whole object
                );

            // 4. GroupBy(keySelector, elementSelector, comparer)
            var groupsWithElementSelectorAndComparer = pet.GroupBy(
                p => p.City, // keySelector
                p => p.Name, // elementSelector
                StringComparer.OrdinalIgnoreCase // comparer
            );

            // 5. GroupBy(keySelector,resultSelector)
            var summary = pet.GroupBy(
                p => p.City,
                (city, items) => new
                {
                    City = city, // sorted by city
                    Count = items.Count(), // number of pets in each city
                    AvgAge = items.Average(p => p.Age) // average age of pets in each city
                }
            );

            // result:
            // City: Berlin, Count: 2, AvgAge: 2.5
            // City: Hamburg, Count: 2, AvgAge: 3
            // City: BERLIN, Count: 1, AvgAge: 3

            // 6. GroupBy(keySelector, resultSelector, comparer)
            var summaryWithComparer = pet.GroupBy(
                p => p.City, // keySelector
                (city, items) => new { City = city, Count = items.Count() }, // resultSelector this mean return only city and count
                StringComparer.OrdinalIgnoreCase // comparer
            );

            // result:
            // City: Berlin, Count: 3
            // City: Hamburg, Count: 2
            // NOTE: "Berlin" and "BERLIN" are treated as the same group due to case insensitivity

            // 7. GroupBy(keySelector, elementSelector, resultSelector)

            var summaryWithElementSelector = pet.GroupBy(
                p => p.City, // keySelector
                p => p.Age, // elementSelector - select only Age property for each pet in the group
                (city, ages) => new// resultSelector
                {
                    City = city,
                    Min = ages.Min(),
                    Max = ages.Max()
                }
            );

            // result:
            // City: Berlin, Min: 2, Max: 3
            // City: Hamburg, Min: 2, Max: 4
            // City: BERLIN, Min: 3, Max: 3

            // 8. GroupBy(keySelector, elementSelector, resultSelector, comparer)
            var summaryWithElementSelectorAndComparer = pet.GroupBy(
                p => p.City, // keySelector
                p => p.Age, // elementSelector
                (city, ages) => new // resultSelector
                {
                    City = city,
                    Min = ages.Min(),
                    Max = ages.Max()
                },
                StringComparer.OrdinalIgnoreCase // comparer
            );

            #endregion

            #region ToLookup(); - convert collection to Lookup by specified key and element selectors, with or without comparer;

            #endregion

            // Joins (З’єднання)

            #region Join(); - correlate elements from two collections based on matching keys;

            // 1. Join(outer, inner, outerKeySelector, innerKeySelector, resultSelector) - without comparer
            int[] firstJoin = { 1, 2, 3 };
            int[] secondJoin = { 2, 3, 4 };

            var joinResult = firstJoin.Join(
                secondJoin,
                x => x, // outer key selector
                y => y, // inner key selector
                (x, y) => x // result selector
            ); // result: {2, 3} because 2 and 3 are common in both arrays

            // 2. Join(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer) - with comparer
            var joinResultWithComparer = firstJoin.Join(
                secondJoin,
                x => x,
                y => y,
                (x, y) => x,
                EqualityComparer<int>.Default // using default equality comparer for int
            );

            // result: {2, 3} because 2 and 3 are common in both arrays

            #endregion

            #region LeftJoin(); - perform left outer join between two collections;

            #endregion

            #region RightJoin(); - perform right outer join between two collections;

            #endregion

            #region GroupJoin(); - correlate elements from two collections based on matching keys and group the results;

            // 1. GroupJoin(outer, inner, outerKeySelector, innerKeySelector, resultSelector) - without comparer

            var customers = new[]
            {
                new Customer(1, "Ann"),
                new Customer(2, "Bob"),
            };

            var orders = new[]
            {
                new Order(1, "Pizza"),
                new Order(1, "Tea"),
                new Order(2, "Burger"),
            };

            var resultGroupJoin = customers. // outer collection
                GroupJoin(
                orders,                      // inner collection
                c => c.Id,                   // outer key
                o => o.CustomerId,           // inner key
                (c, os) => new               // result selector
                {
                    c.Name,                  // customer name
                    Items = os.Select(x => x.Item).ToList() // list of order items for the customer
                }
            );

            // foreach (var x in resultGroupJoin)
            // Console.WriteLine($"{x.Name}: {string.Join(", ", x.Items)}");

            // result
            // Ann -> [ "Pizza", "Tea" ]
            // Bob -> [ "Burger" ]


            // 2. GroupJoin(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer) - with comparer

            var users = new[] { new User("Ann@Mail.com") };
            var logins = new[] {
                new Login("ann@mail.com", DateTime.Now),
                new Login("bob@mail.com", DateTime.Now),
                new Login("CHARLIE@MAIL.COM", DateTime.Now),
                new Login("ANN@mail.com", DateTime.Now)
            };

            var resultGroupJoinWithComparer = users. // outer collection
                GroupJoin(
                  logins,       // inner collection
                  u => u.Email, // outer key selector
                  l => l.Email, // inner key selector
                  (u, ls) => new { u.Email, Count = ls.Count() }, // result selector
                  StringComparer.OrdinalIgnoreCase
            );

            // result:
            // Ann@Mail.com -> 2 (two logins matched ignoring case)

            #endregion

            // Generation (Генерація послідовностей)

            #region Empty(); - create an empty collection of specified type;

            // Using when you need return empty collection from method
            // Good for LINQ you can avoid null reference exceptions
            var emptyArray = Enumerable.Empty<int>(); // creates an empty IEnumerable<int>
            var emptyList = new List<string>(); // creates an empty List<string>

            #endregion

            #region Range(); - create a collection with a range of sequential numbers;

            #endregion

            #region Repeat(); - create a collection with repeated elements;

            #endregion

            #region InfiniteSequence(); - create an infinite collection by specified generator function;

            #endregion

            #region Sequence(); - generate a sequence of values based on a generator function;

            #endregion

            // Conversion / Materialization (Конвертація / матеріалізація)

            #region AsEnumerable(); - convert collection to IEnumerable type;

            // AsEnumerable() - used to convert a collection to IEnumerable<T> usually for LINQ operations that require IEnumerable<T> input.
            var asEnumerable = new int[] { 1, 2, 3 }.AsEnumerable().Where(x => x > 1); // Output: 2 3

            #endregion

            #region Cast(); - convert collection to specified type used on collection without generic type ArrayList, Hashtable, IEnumerable(old);

            ArrayList list = new ArrayList() { 1, 2, 3 };

            // Cast<T>() - used to convert a non-generic collection to a generic IEnumerable<T>.
            // In 99% of cases you don't need it
            IEnumerable<int> numbers = list.Cast<int>();

            //int sum1 = list.Sum();      // ❌ doesn't work
            int sum2 = numbersForDefExe.Sum();  // ✅ works

            #endregion

            #region ToArray(); - convert collection to array;

            #endregion

            #region ToDictionary(); - convert collection to dictionary by specified key and element selectors, with or without comparer;

            #endregion

            #region ToHashSet(); - convert collection to HashSet, with or without comparer;

            #endregion

            #region ToList(); - convert collection to List;

            #endregion

            // Concatenation (Склеювання)

            #region Concat(); - combine two collections in one

            // Concat(IEnumerable<TSource>)
            int[] a = { 1, 2, 3 };
            int[] b = { 4, 5, 6 };

            // Combine two collections into one
            var concat = a.Concat(b);   // {1,2,3,4,5,6}

            #endregion

            #region Append(); - add element to the end of collection;

            // Append(TSource)
            // Adds an element to the end of a sequence.
            var append = new int[] { 1, 2, 3 }.Append(4); // Output: 1 2 3 4

            #endregion

            #region Prepend(); - add element to the beginning of collection;

            #endregion

            // Equality / Compare sequences

            #region SequenceEqual(); - check if two collections are equal;

            #endregion

            // Default/Null-safety

            #region DefaultIfEmpty(); - provide default or custom value if collection is empty;

            var defaultIfEmpty = new int[] { }.DefaultIfEmpty(); // {0} because the default value for int is 0

            var defaultIfEmptyWithCustomDefault = new int[] { }.DefaultIfEmpty(42); // {42} because we provided a custom default value of 42

            // DefaultIfEmpty(); - usually used to avoid exceptions when performing operations on potentially empty collections.
            var avg = new int[] { }.DefaultIfEmpty().Average(); // 0 because default value for int is 0
            #endregion

        }
    }

    // Class for demonstration purposes

    #region OfType() with inheritance

    abstract class Animal
    {
        public string Name { get; set; }
    }

    class Dog : Animal { }
    class Cat : Animal { }

    #endregion

    class Pet
    {
        public string Name;
        public int Age;
        public string City;
        public Pet(string name, int age)
        {
            Name = name;
            Age = age;
        }
        public Pet(string name, string city, int age)
        {
            Name = name;
            City = city;
            Age = age;
        }

    }

    class UserForIntersect
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    record Customer(int Id, string Name);
    record Order(int CustomerId, string Item);
    record User(string Email);
    record Login(string Email, DateTime At);

    public class Person
    {
        public string? Name { get; set; } = null;
        public int? Age { get; set; } = null;

        public Person() { }

        public Person(string name)
        {
            Name = name;
        }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
    record People(string Name, int Age);

    class OrderForSelectMany
    {
        public int Id { get; set; }
        public List<string> Items { get; set; }
    }

}
