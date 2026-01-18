using System;
using System.Collections;

namespace LINQMethods
{
    internal class Program
    {
        static void Main(string[] args)
        {

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

            var people = new[]
            {
                new Person { Name = "Bob", Age = 30 },
                new Person { Name = "Alice", Age = 25 },
                new Person { Name = "Charlie", Age = 30 }
            };

            // 1. OrderBy<TKey>(Func<TSource, TKey>) - sort by key selector
            var result = people.OrderBy(p => p.Age);
            // Result:
            // (Alice, 25)
            // (Bob, 30)
            // (Charlie, 30)


            // 2. OrderBy<TKey>(Func<TSource, TKey>, IComparer<TKey>) - sort by key selector with custom comparer
            var resultWithComparer = people.OrderBy(
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

            #region OrderByDescending(); - sort elements in collection in descending order by specified key, with or without comparer;

            #endregion

            #region ThenBy(); - perform a subsequent ordering of elements in collection in ascending order by specified key, with or without comparer;

            #endregion

            #region ThenByDescending(); - perform a subsequent ordering of elements in collection in descending order by specified key, with or without comparer;

            #endregion

            #region Reverse(); - reverse the order of elements in collection;

            #endregion

            #region Shuffle(); - randomly shuffle elements in collection;

            #endregion

            // Partitioning (Порції/пагінація)

            #region Skip(); - skip specified number of elements in collection;

            #endregion

            #region SkipLast(); - skip specified number of elements from the end of collection;

            #endregion

            #region SkipWhile(); - skip elements in collection while condition is true;

            #endregion

            #region Take(); - take specified number of elements from collection;

            #endregion

            #region TakeLast(); - take specified number of elements from the end of collection;

            #endregion

            #region TakeWhile(); - take elements from collection while condition is true;

            #endregion

            #region Chunk(); - split collection into smaller collections of specified size;

            // Chunk(int)
            int[] array = { 1, 2, 3, 4, 5, 6, 7 };


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

            #region Union(); - get unique elements from two collections;

            #endregion

            #region UnionBy(); - get unique elements from two collections by specified key;

            #endregion

            #region Intersect(); - get common elements from two collections;

            // 1. Intersect(IEnumerable<TSource>)
            var arrayForIntersect1 = new int[] { 1, 2, 3, 4 };
            var arrayForIntersect2 = new int[] { 3, 4, 5, 6 };

            var intersect = arrayForIntersect1.Intersect(arrayForIntersect2); // {3, 4} because 3 and 4 are common in both arrays

            // 2. Intersect(IEnumerable<TSource>, IEqualityComparer<TSource>) - with custom comparer for complex types
            // TODO: Add examples for Intersect() with custom comparer for complex types

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

            var resultIntersectBy = usersForIntersect.IntersectBy(ids, u => u.Id); // contains only users with Id 2 and 3 (Bob and Carl)

            // 2. IntersectBy(IEnumerable<TKey>, Func<TSource, TKey>, IEqualityComparer<TKey>) - by key selector with custom comparer

            var words = new[] { "Apple", "Banana", "Orange" };
            var filter = new[] { "apple", "banana" };

            var resultForIntersectBy = wordsForWhere.IntersectBy(
                filter,
                w => w, // key selector
                StringComparer.OrdinalIgnoreCase
            );



            #endregion

            #region Except(); - get elements from first collection that are not in second collection;

            // compare first array with second and return only those elements that are in the first array but not in the second array
            // all result is unique
            int[] arrayForExcept1 = { 1, 1, 2, 3, 4 };
            int[] arrayForExcept2 = { 3, 4 }; // array that blocks elements from first array

            var result = arrayForExcept1.Except(arrayForExcept2); // {1, 2} because 1 and 2 are not in arrayForExcept2 in simple words : result = arrayForExcept1 - arrayForExcept2

            // TODO: Add example for Except() with custom comparer for complex types

            #endregion

            #region ExceptBy(); - get elements from first collection that are not in second collection by specified key;

            var peopleExcept = new[]
               {
                    new Pet("Ann", 3),
                    new Pet("Ann", 2),
                    new Pet("Bob", 3),
                    new Pet("Carl", 4)
                };

            var blockedNames = new[] { "Bob" }; // names to block

            var exceptByResult = peopleExcept.ExceptBy(blockedNames, p => p.Name); // contains only Pets with names "Ann" and "Carl", "Bob" is excluded

            // TODO: Add example for ExceptBy(keys, x => key, comparer) with custom comparer

            #endregion

            // Element operations (Вибір елемента)

            #region First(); - get first element from collection, with or without condition;

            int[] firstArray = { 1, 3, 4, 6 };

            int first = firstArray.First();   // 1

            // overload with condition
            int firstEven = firstArray.First(x => x % 2 == 0); // 4 because 4 is the first even number in the array that satisfies the condition

            // NOTICE: If the collection is empty or no elements satisfy the condition, First() will throw an InvalidOperationException.

            #endregion

            #region FirstOrDefault(); - get first element from collection or default value if collection is empty, with or without condition;

            int[] firstOrDefaultArray = { 1, 3, 4, 6 };

            int firstOrDefault = firstOrDefaultArray.FirstOrDefault();   // 1

            // overload with default value
            int firstOrDefaultWithDefault = new int[] { }.FirstOrDefault(999);   // 999

            // if collection is empty return default value
            int firstOrDefaultEmpty = new int[] { }.FirstOrDefault(); // 0 because the array is empty and default value for int is 0

            // overload with condition
            int firstOrDefaultWithCondition = firstOrDefaultArray.FirstOrDefault(x => x % 2 == 0); // 4 because 4 is the first even number in the array that satisfies the condition

            // 999 because no elements satisfy the condition and we provided a default value of 999
            int firstOrDefaultWithConditionAndDefault = firstOrDefaultArray.FirstOrDefault(x => x > 10, 999);

            #endregion

            #region Last(); - get last element from collection, with or without condition;

            #endregion

            #region LastOrDefault(); - get last element from collection or default value if collection is empty, with or without condition;

            #endregion

            #region Single(); - get single element from collection, with or without condition;

            #endregion

            #region SingleOrDefault(); - get single element from collection or default value if collection is empty, with or without condition;

            #endregion

            #region ElementAt(); - get element at specified index, with or without default value;

            var elementAtIndex = new int[] { 1, 2, 3, 4 }.ElementAt(2); // 3 because index 2 is the third element

            #endregion

            #region ElementAtOrDefault(); - get element at specified index or default value if index is out of range;

            var elementAtIndexWithDefault = new int[] { }.ElementAtOrDefault(0); // 0

            #endregion

            // Quantifiers (Перевірки)

            #region All(); - check if all elements satisfy a condition;

            // All(Func<TSource, bool>)
            // Determines whether all elements of a sequence satisfy a condition.
            // Even if one element does not satisfy the condition, the result will be false.
            bool all = new int[] { 1, 2, 3, 4 }.All(x => x > 0); // true because all elements are greater than 0


            #endregion

            #region Any(); - check if any element satisfies a condition or if the sequence contains any elements;

            // All(Func<TSource, bool>)
            // Determines whether any element of a sequence satisfies a condition.
            // If at least one element satisfies the condition, the result will be true.
            bool any = new int[] { 1, 2, 3, 4 }.Any(x => x > 3); // true because there is at least one element greater than 3

            // This Any(); used to check if the collection contains any elements.
            bool anyWithoutCondition = new int[] { 1, 2, 3, 4 }.Any(); // true because the array contains elements

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

            // Aggregation (Агрегація)

            #region Count(); - count elements in collection, with or without condition;

            int count = new int[] { 1, 2, 3, 4 }.Count(); // 4
            int countWithCondition = new int[] { 1, 2, 3, 4 }.Count(x => x > 2); // 2 because only 3 and 4 are greater than 2

            #endregion

            #region CountBy(); - count elements in collection by specified key, with or without condition;

            #endregion

            #region LongCount(); - count elements in collection, with or without condition, returns long type;

            #endregion

            #region TryGetNonEnumeratedCount(); - try to get count of elements in collection without enumerating it;

            #endregion

            #region Max(); - get maximum value from numeric collection, with or without selector;

            #endregion

            #region MaxBy(); - get element with maximum key value from collection;

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
            var averageWithSelector = people.Average(p => p.Age); // result is 30.0 because (20 + 30 + 40) / 3 = 30.0

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
