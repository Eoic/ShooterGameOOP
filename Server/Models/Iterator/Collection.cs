using System.Collections;

namespace ConsoleApp1
{
    class Collection : IAbstractCollection
    {
        private ArrayList items = new ArrayList();

        public Iterator CreateIterator()
        {
            return new Iterator(this);
        }

        public object this[int index]
        {
            get { return items[index]; }
            set { items.Add(value); }
        }

        public int Count
        {
            get{ return items.Count; }
        }

        public void AddToCollection(dynamic item)
        {
            items.Add(item);
        }

        public void AddRangeToCollection(dynamic list)
        {
            foreach (var item in list)
            {
                items.Add(item);
            }
        }
    }
}
