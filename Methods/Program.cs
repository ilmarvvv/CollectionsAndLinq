namespace Methods
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // TODO: What is difference between static and instance methods in Array class?


            // 1. Creation & Resizing

            #region CreateInstance() - creates a new instance of the specified array type
            // Static
            #endregion

            #region CreateInstanceFromArrayType() - creates a new instance of the specified array type using an existing array to specify the type
            // Static
            #endregion

            #region Empty() - returns an empty array of the specified type T
            // Static
            #endregion

            #region Resize() - changes the number of elements of a one-dimensional array to the specified new size
            // Static
            #endregion

            // 2. Copying & Cloning

            #region Clone() - creates a shallow copy of the array
            // Instance
            #endregion

            #region ConstrainedCopy() -  copies a range of elements from an array and pastes them to another array
            // Static
            #endregion

            #region Copy() - copies a range of elements from an array starting at the first element and pastes them into another array starting at the first element
            // Static
            #endregion

            #region CopyTo() - copies all the elements of the current one-dimensional array to the specified one-dimensional array starting at the specified destination array index
            // Instance
            #endregion

            // 3. Searching(Index/Predicate)

            #region BinarySearch() - searches a one-dimensional sorted array for a value and returns the index of the value
            // Static
            #endregion

            #region Exists() - determines whether the array contains elements that match the conditions defined by the specified predicate
            // Static
            #endregion

            #region Find() - searches for an element that matches the conditions defined by the specified predicate, and returns the first occurrence within the entire array
            // Static
            #endregion

            #region FindAll() - retrieves all the elements that match the conditions defined by the specified predicate
            // Static
            #endregion

            #region FindIndex() - searches for an element that matches the conditions defined by the specified predicate, and returns the index of the first occurrence within the entire array
            // Static
            #endregion

            #region FindLast() - searches for an element that matches the conditions defined by the specified predicate, and returns the last occurrence within the entire array
            // Static
            #endregion

            #region FindLastIndex() - searches for an element that matches the conditions defined by the specified predicate, and returns the index of the last occurrence within the entire array
            // Static
            #endregion

            #region IndexOf() - searches for the specified object and returns the index of its first occurrence in a one-dimensional array
            // Static
            #endregion

            #region LastIndexOf() - searches for the specified object and returns the index of its last occurrence in a one-dimensional array
            // Static
            #endregion

            #region TrueForAll() - determines whether every element in the array matches the conditions defined by the specified predicate
            // Static
            #endregion

            // 4. Ordering

            #region Reverse() - reverses the sequence of the elements in the entire one-dimensional array or in a portion of the array
            // Static
            #endregion

            #region Sort() - sorts the elements in a one-dimensional array
            // Static
            #endregion

            // 5. Mutating / Filling

            #region Clear() - sets a range of elements in the array to the default value of each element type
            // Static
            #endregion

            #region Fill() - assigns the specified value to each element of the specified array
            // Static
            #endregion

            #region SetValue() - sets a value to the element at the specified position in the array
            // Instance
            #endregion

            // 6. Reading / Multi-dimensional helpers

            #region GetEnumerator() - returns an enumerator that iterates through the array
            // Instance
            #endregion

            #region GetLength() - gets the number of elements in the specified dimension of the array
            // Instance
            #endregion

            #region GetLongLength() - gets the number of elements in the specified dimension of the array as a long
            // Instance
            #endregion

            #region GetLowerBound() - gets the lower bound of the specified dimension of the array
            // Instance
            #endregion

            #region GetUpperBound() - gets the upper bound of the specified dimension of the array
            // Instance
            #endregion

            #region GetValue() - retrieves the value at the specified position in the array
            // Instance
            #endregion

            // 7. Projecting / Transform

            #region ConvertAll() - converts an array of one type to an array of another type using a specified converter
            // Static
            #endregion

            #region ForEach() - performs the specified action on each element of the array
            // Static
            #endregion

            // 8. Read-only wrappers

            #region AsReadOnly() - returns a read-only wrapper for the current array
            // Static
            #endregion

            // 9. Object methods

            #region Equals() - determines whether the specified object is equal to the current array
            // Instance
            #endregion

            #region GetHashCode() - returns a hash code for the current array
            // Instance
            #endregion

            #region GetType() - gets the Type of the current instance
            // Instance
            #endregion

            #region ToString() - returns a string that represents the current array
            // Instance
            #endregion


            #region MemberwiseClone() - protected Object method used internally by Array.Clone()

            #endregion

            // 10. Rare / Legacy methods

            #region Initialize() - initializes every element of the specified array
            // Instance
            #endregion

        }
    }
}
