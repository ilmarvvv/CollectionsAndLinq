namespace AnyDimensionalArrays
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ToDo: fill array with numbers and print biggest array

            // Example for four-dimensional array
            int[,,,] fourDimensional = new int[2, 2, 2, 2] { { { { 1, 2 }, { 3, 4 } }, { { 5, 6 }, { 7, 8 } } }, { { { 9, 10 }, { 11, 12 } }, { { 13, 14 }, { 15, 16 } } } };

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        for (int l = 0; l < 2; l++)
                        {
                            fourDimensional[i, j, k, l] = i + j + k + l;
                        }
                    }
                }
            }

            // Example for five-dimensional array
            int[,,,,] fiveDimensional = new int[2, 2, 2, 2, 2];

            // Example for six-dimensional array
            int[,,,,,] sixDimensional = new int[2, 2, 2, 2, 2, 2];

            // Example for seven-dimensional array
            int[,,,,,,] sevenDimensional = new int[2, 2, 2, 2, 2, 2, 2];

            Console.ReadKey();
        }
    }
}
