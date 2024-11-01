namespace Control_Tower_System_DAL
{
    /// <summary>
    /// Interface for ListManager handling different list operations
    /// </summary>
    public interface IListManager<T>
    {
        /// <summary>
        /// Adds item to the list
        /// </summary>
        /// <param name="item"></param>
        void Add(T item);

        /// <summary>
        /// Removes and items at a certain index
        /// </summary>
        /// <param name="index"></param>
        void RemoveAt(int index);

        /// <summary>
        /// Replaces an item at a certain index
        /// </summary>
        /// <param name="item"></param>
        /// <param name="index"></param>
        void ReplaceAt(T item, int index);

        /// <summary>
        /// Gets an item at a certain index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        T Get(int index);

        /// <summary>
        /// Returns all items in the list to an array of strings
        /// </summary>
        /// <returns></returns>
        string[] ToStringArray();

        /// <summary>
        /// returns the count of the list
        /// </summary>
        /// <returns></returns>
        int Count { get; }
    }
}
