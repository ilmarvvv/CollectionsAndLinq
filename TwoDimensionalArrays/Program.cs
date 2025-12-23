namespace TwoDimensionalArrays
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ToDO: improve comments
            Console.WriteLine("Two-Dimensional Arrays:");

            int[,] array1 = new int[3, 3];
            int[,] array2 = { { 1, 4, 2 }, { 3, 6, 8 }, { 7, 8, 9 } };

            array2[0, 0] = 1;
            array2[0, 1] = 2;
            array2[0, 2] = 3;

            array2[1, 0] = 4;
            array2[1, 1] = 5;
            array2[1, 2] = 6;


            for (int i = 0; i < array2.GetLength(0); i++)
            {
                for (int j = 0; j < array2.GetLength(1); j++)
                {
                    Console.Write(array2[i, j] + "  ");
                }
                Console.Write("\n");
            }

            Console.WriteLine("Using foreach for Two-Dimensional arrays");

            foreach(int num in array2)
            {
                Console.Write("{0} ", num);
            }

            Console.ReadKey();
        }
    }
}
