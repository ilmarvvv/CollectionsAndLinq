namespace OneDimensionalArrays
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create an array of 4 elements
            int[] array1 = new int[4];

            // Add values to each element in the array
            array1[0] = 5; // First element always has index 0
            array1[1] = 6;
            array1[2] = 1;
            array1[3] = 4;

            // Create an array of 4 elements
            int[] array2 = new int[4];

            // Create an array of 4 elements and add values right away(long version)
            int[] array3 = new int[4] { 1, 2, 3, 4 };

            // Create an array of 4 elements without specifying the size
            int[] array4 = new int[] { 1, 2, 3, 4 };

            // Create an array of 4 elements, omitting the new keyword, and without specifying the size (short version)
            //    index: [0][1][2][3]
            int[] array5 = {1, 2, 3, 4};

            Console.WriteLine("One-Dimensional Arrays:    ");

            // Print each array using a foreach loop, foreach loop is used to iterate through each element in the array
            foreach (int num in array5)
            {
                Console.Write("{0} ", num);
            }

            Console.ReadKey();
        }
    }
}
