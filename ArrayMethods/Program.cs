using System.Runtime.Intrinsics.X86;

namespace ArrayMethods
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int[] array = { 5, 4, 6, 8, 0, 3, 7, 7 };

            PrintArray("Original: ", array);

            // Sort the array in ascending
            var sortArray = array.Clone() as int[];
            Array.Sort(sortArray);
            PrintArray("Sorted asc: ", sortArray);

            // But if you want to sort by descending, use Array.Sort + Array.Reverse
            var reverseSortedArray = sortArray.Clone() as int[];
            Array.Reverse(reverseSortedArray);
            PrintArray("Sorted desc: ", reverseSortedArray);

            // Find the first index of 7 in the array, but only the first that can be found
            PrintArray("Original again: ", array);
            Console.WriteLine("Index of 7 is " + Array.IndexOf(array, 7));

            // The last index of 7 in the array
            Console.WriteLine("Last index of 7 is " + Array.LastIndexOf(array, 7));

            // This is fast way to check if array satisfies a condition
            bool hasBigNumber = Array.Exists(array, x => x > 10);
            Console.WriteLine("Array has number bigger than 10: " + hasBigNumber);

            // TODO: More methods to demonstrate
            // Array.Find(array, predicate);
            // Array.FindAll(array, predicate);
            // Array.TrueForAll(array, predicate);
            // Array.ConvertAll(array, converter);
            // Array.ForEach(array, action);
            // Array.Clear(array, index, length);
            // Array.Copy(sourceArray, destinationArray, length);
        }

        static void PrintArray(string explanation, int[] array)
        {
            foreach (int i in array)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
        }
    }
}
