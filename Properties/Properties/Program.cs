namespace Properties
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Core
            #region Length - returns the total number of elements in all dimensions of an array

            // One-dimensional array example
            int[] lengthArray = { 10, 20, 30 };
            int result1 = lengthArray.Length; // Result: 3 because there are 3 elements in the array

            // Example: iterate over array
            for (int i = 0; i < lengthArray.Length; i++)
            {
                // Console.WriteLine(lengthArray[i]);
            }

            // Example: check if array is empty
            if (lengthArray.Length == 0)
            {
                // Console.WriteLine("Array is empty");
            }
            else
            {
                // Console.WriteLine("Array is not empty");
            }

            int[,] length2DimArray = new int[3, 4];
            int result2 = length2DimArray.Length; // Result: 12 because 3 rows * 4 columns = 12 elements

            // NOTE: Length counts all elements in all dimensions and is not specific to any single dimension of a multi-dimensional array.
            // m.Length;        // 12  → total elements
            // m.GetLength(0);  // 3   → rows
            // m.GetLength(1);  // 4   → columns
            // GetLength(dimension) is method not property

            // NOTE: Only jagged arrays can have different lengths for each sub-array.

            int[][] jagged =
            {
                new[] { 1, 2, 3 },
                new[] { 4, 5 }
            };

            // Console.WriteLine(jagged.Length);     // 2
            // Console.WriteLine(jagged[0].Length);  // 3
            // Console.WriteLine(jagged[1].Length);  // 2



            #endregion

            #region LongLength - return the total number of elements in all dimensions of the array as a long

            #endregion

            #region Rank - gets the number of dimensions of the array

            #endregion

            // Limits

            #region MaxLength - gets the maximum number of elements an array can contain

            #endregion

            // Collection / Threading

            #region IsFixedSize - gets a value indicating whether the array has a fixed size

            #endregion

            #region IsReadOnly - gets a value indicating whether the array is read-only

            #endregion

            #region IsSynchronized - gets a value indicating whether access to the array is synchronized (thread safe)

            #endregion

            #region SyncRoot - gets an object that can be used to synchronize access to the array

            #endregion
        }
    }
}
