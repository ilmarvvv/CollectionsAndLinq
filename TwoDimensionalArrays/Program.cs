namespace TwoDimensionalArrays
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Two-Dimensional Arrays:");

            // Declaring and initializing two-dimensional arrays
            int[,] array1 = new int[3, 3];

            // Filling the array with values
            int[,] array2 = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

            array2[0, 0] = 1;
            array2[0, 1] = 2;
            array2[0, 2] = 3;

            array2[1, 0] = 4;
            array2[1, 1] = 5;
            array2[1, 2] = 6;

            array2[2, 0] = 7;
            array2[2, 1] = 8;
            array2[2, 2] = 9;


            // Accessing elements in the array
            for (int i = 0; i < array2.GetLength(0); i++)
            {
                for (int j = 0; j < array2.GetLength(1); j++)
                {
                    Console.Write(array2[i, j] + "  ");
                }
                Console.Write("\n");
            }

            Console.WriteLine("Using foreach for Two-Dimensional arrays");

            // Using foreach to iterate through the array
            foreach (int num in array2)
            {
                Console.Write("{0} ", num);
            }

            Console.ReadKey();
        }
    }
}
