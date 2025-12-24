namespace JaggedArrays
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ToDO: Improve comments and check all


            Console.WriteLine("Jagged Arrays:");

            // Initialize the jagged array
            int[][] jagged = InitializeJaggedArray();

            // Display the jagged array
            DisplayJaggedArray(jagged);

            // Perform some operations
            Console.WriteLine("\nSum of all elements:");
            Console.WriteLine(SumJaggedArray(jagged));


            int[][] InitializeJaggedArray()
            {
                int[][] jagged = new int[3][];
                jagged[0] = new int[] { 1, 2 };
                jagged[1] = new int[] { 1, 2, 3, 4, 5 };
                jagged[2] = new int[] { 1, 2, 3 };
                return jagged;
            }

            void DisplayJaggedArray(int[][] jagged)
            {
                for (int i = 0; i < jagged.Length; ++i)
                {
                    for (int j = 0; j < jagged[i].Length; ++j)
                    {
                        Console.Write("{0} ", jagged[i][j]);
                    }
                    Console.WriteLine();
                }
            }

            // Method to sum all elements in a jagged array
            int SumJaggedArray(int[][] jagged)
            {
                int sum = 0;
                for (int i = 0; i < jagged.Length; ++i)
                {
                    for (int j = 0; j < jagged[i].Length; ++j)
                    {
                        sum += jagged[i][j];
                    }
                }
                return sum;
            }
        }
    }
}
