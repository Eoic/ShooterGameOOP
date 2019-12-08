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
    }
}
