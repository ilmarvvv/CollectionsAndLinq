namespace ThreeDimensionalArrays
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ToDo: improve comments and check all

            Console.WriteLine("Three-Dimensional Arrays:    ");

            // Declaring and initializing three-dimensional arrays
            byte[,,] Array1 = new byte[2, 2, 2];

            byte[,,] Array2 = { { { 1, 2 }, { 3, 4 } }, { { 5, 6 }, { 7, 8 } } };

            byte[,,] Array3 = new byte[2, 2, 2]
            {
                {
                    { 1, 2 },
                    { 3, 4 }
                },
                {
                    { 5, 6 },
                    { 7, 8 }
                }
            };

            // Filling the array with values
            Array1[0, 0, 0] = 1;
            Array1[0, 0, 1] = 2;
            Array1[0, 1, 0] = 3;
            Array1[0, 1, 1] = 4;
            Array1[1, 0, 0] = 5;
            Array1[1, 0, 1] = 6;
            Array1[1, 1, 0] = 7;
            Array1[1, 1, 1] = 8;

            // Accessing elements in the array
            for (int i = 0; i < Array1.GetLength(0); i++)
            {
                for (int j = 0; j < Array1.GetLength(1); j++)
                {
                    for (int k = 0; k < Array1.GetLength(2); k++)
                    {
                        Console.Write(Array1[i, j, k] + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }

            foreach (byte b in Array1)
            {
                Console.Write("{0} ", b);
            }

            Console.ReadKey();
        }
    }
}
