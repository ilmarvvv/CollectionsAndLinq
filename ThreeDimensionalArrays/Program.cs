namespace ThreeDimensionalArrays
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ToDo: improve comments and check all

            Console.WriteLine("Three-Dimensional Arrays:    ");
            byte[,,] three1 = new byte[2, 2, 2];

            byte[,,] three2 = { { { 1, 2 }, { 3, 4 } }, { { 5, 6 }, { 7, 8 } } };

            byte[,,] three3 = new byte[2, 2, 2]
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

            three1[0, 0, 0] = 10;
            three1[0, 0, 1] = 20;
            three1[0, 1, 0] = 30;
            three1[0, 1, 1] = 40;
            three1[1, 0, 0] = 50;
            three1[1, 0, 1] = 60;
            three1[1, 1, 0] = 70;
            three1[1, 1, 1] = 80;

            for (int i = 0; i < three1.GetLength(0); i++)
            {
                for (int j = 0; j < three1.GetLength(1); j++)
                {
                    for (int k = 0; k < three1.GetLength(2); k++)
                    {
                        Console.Write(three1[i, j, k] + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}
