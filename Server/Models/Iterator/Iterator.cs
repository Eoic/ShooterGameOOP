namespace ConsoleApp1
{
    class Iterator : IAbstractIterator
    {
        private Collection collection;
        private int current = 0;

        public Iterator(Collection collection)
        {
            this.collection = collection;
        }

        public bool IsDone => current >= collection.Count;

        public dynamic GetCurrent()
        {
            return collection[current];
        }

        public dynamic First()
        {
            current = 0;
            return collection[current];
        }

        public dynamic Next()
        {
            current++;

            if (!IsDone)
                return collection[current];

            return null;

        }
    }
}