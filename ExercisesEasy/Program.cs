namespace ExercisesEasy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] intArray = { 1, 2, 3, 4, 5 };

            // Using LINQ to sum the elements of the array
            int sumArray1 = intArray.Sum();

            Console.WriteLine("Sum of array elements: " + sumArray1);

            // Using foreach loop to sum the elements of the array
            int sumArray2 = 0;

            foreach (int num in intArray)
            {
                sumArray2 += num;
            }

            Console.WriteLine("Sum of array elements using foreach: " + sumArray2);

            // Using for loop to sum the elements of the array
            int sumArray3 = 0;

            for(int i = 0; i < intArray.Length; i++)
            {
                sumArray3 += intArray[i];
            }

            Console.WriteLine("Sum of array elements using for loop: " + sumArray3);
        }
    }
}
