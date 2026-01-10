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

            int[] numbers = { 1, 2, 3 };
            long length1D = numbers.LongLength; // 3

            int[,] matrix = new int[2, 3];
            long length2D = matrix.LongLength; // 6

            // NOTE: Don't use it in loops or indexing because it's a long (Int64) and can cause performance issues.
            // NOTE: LongLength is the same as Length but returns a long (Int64)
            // NOTE: Use Length for indexing and loops, LongLength for large array checks

            #endregion

            #region Rank - gets the number of dimensions of the array

            // One-dimensional array
            int[] array1D = { 1, 2, 3 };
            int rank1 = array1D.Rank; // 1

            // Two-dimensional array
            int[,] array2D = new int[3, 4];
            int rank2 = array2D.Rank; // 2

            // Three-dimensional array
            int[,,] array3D = new int[2, 3, 4];
            int rank3 = array3D.Rank; // 3

            // Jagged array (array of arrays)
            int[][] jaggedArray = new int[3][];
            int rank4 = jaggedArray.Rank; // 1
            // NOTE: for jagged arrays, Rank is always 1 regardless of how many levels of arrays there are.

            // Example: checking ranking if you don't know the dimensions

            void Process(Array array)
            {
                if (array.Rank == 1)
                {
                    // логіка для 1D
                }
                else if (array.Rank == 2)
                {
                    // логіка для 2D
                }
            }

            #endregion

            // Limits

            #region MaxLength - gets the maximum number of elements an array can contain

            int requestedSize = 1000000000; // Example size
            if (requestedSize > Array.MaxLength)
                throw new ArgumentOutOfRangeException();


            #endregion

            // Collection / Threading

            #region IsFixedSize - gets a value indicating whether the array has a fixed size

            int[] array = { 1, 2, 3 };


            // Purpose: Determine if the array size can be changed if true then size is fixed and cannot be changed
            bool isFixedSize = array.IsFixedSize; // true

            // Array: true
            // List<T>: false
            // ReadOnlyCollection<T>: true
            // ArrayList: false

            // NOTE: Arrays always have a fixed size
            // NOTE: Elements can be modified, but the array size cannot be changed



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
