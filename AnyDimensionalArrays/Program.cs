namespace AnyDimensionalArrays
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ToDo: fill array with numbers and print biggest array

            // Example for four-dimensional array
            int[,,,] fourDim = new int[2, 2, 2, 2];

            // Example for five-dimensional array
            int[,,,,] fiveDim = new int[2, 2, 2, 2, 2];

            // Example for six-dimensional array
            int[,,,,,] sixDim = new int[2, 2, 2, 2, 2, 2];

            // Example for seven-dimensional array
            int[,,,,,,] sevenDim = new int[2, 2, 2, 2, 2, 2, 2];

            Console.ReadKey();
        }
    }
}
