namespace _353503_Martinovich_Lab4
{
    internal class MyCustomComparer<T> : IComparer<T> where T : Automobile
    {
        public int Compare(T? x, T? y)
        {
            if (x == null || y == null)
            {
                throw new ArgumentNullException();
            }

            return string.Compare(x.Name, y.Name);
        }
    }
}
