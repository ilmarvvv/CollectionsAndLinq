using System.Collections;

namespace LINQMethods
{
    internal class Program
    {
        static void Main(string[] args)
        {

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


            var people = new[] { new { Name = "Ann", Age = 20 }, new { Name = "Bob", Age = 30 }, new { Name = "Carl", Age = 40 } };
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
            int[] array = { 1, 2, 3, 4, 5, 6, 7 };


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

            #region Empty(); - create an empty collection of specified type;

            // Using when you need return empty collection from method
            // Good for LINQ you can avoid null reference exceptions
            var emptyArray = Enumerable.Empty<int>(); // creates an empty IEnumerable<int>
            var emptyList = new List<string>(); // creates an empty List<string>

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

            #region Intersect(); - get common elements from two collections;

            // 1. Intersect(IEnumerable<TSource>)
            var arrayForIntersect1 = new int[] { 1, 2, 3, 4 };
            var arrayForIntersect2 = new int[] { 3, 4, 5, 6 };

            var intersect = arrayForIntersect1.Intersect(arrayForIntersect2); // {3, 4} because 3 and 4 are common in both arrays

            // 2. Intersect(IEnumerable<TSource>, IEqualityComparer<TSource>) - with custom comparer for complex types
            // TODO: Add examples for Intersect() with custom comparer for complex types

            #endregion

            #region InterestBy(); - get common elements from two collections by specified key;
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

            var resultForIntersectBy = words.IntersectBy(
                filter,
                w => w, // key selector
                StringComparer.OrdinalIgnoreCase
            );



            #endregion

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

            #region Last(); - get last element from collection, with or without condition;

            #endregion

            #region LastOrDefault(); - get last element from collection or default value if collection is empty, with or without condition;

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

            #region Order(); - sort elements in collection in ascending order, with or without key selector and comparer;

            #endregion

            #region OrderBy(); - sort elements in collection in ascending order by specified key, with or without comparer;

            #endregion

            #region OrderByDescending(); - sort elements in collection in descending order by specified key, with or without comparer;

            #endregion

            #region OrderDescending(); - sort elements in collection in descending order, with or without key selector and comparer;

            #endregion

            #region Prepend(); - add element to the beginning of collection;

            #endregion

            #region Range(); - create a collection with a range of sequential numbers;

            #endregion

            #region Repeat(); - create a collection with repeated elements;

            #endregion

            #region Reverse(); - reverse the order of elements in collection;

            #endregion

            #region Select(); - project each element of a collection into a new form;

            #endregion

            #region SelectMany(); - project each element of a collection to an IEnumerable<T> and flatten the resulting collections into one collection;

            #endregion

            #region SequenceEqual(); - check if two collections are equal;

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
    class UserForIntersect
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    record Customer(int Id, string Name);
    record Order(int CustomerId, string Item);
    record User(string Email);
    record Login(string Email, DateTime At);
}
