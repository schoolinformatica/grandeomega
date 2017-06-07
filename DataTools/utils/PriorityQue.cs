using System;
using System.Collections.Generic;
using System.Linq;

namespace DataTools.utils
{
    // Optimization could be done by using a BinaryTree
    public class PriorityQue<T>
    {
        private List<Tuple<double, T>> _entries;

        public int Count => _entries.Count;
        public bool IsEmpty => Count == 0;

        public PriorityQue(IEnumerable<Tuple<double, T>> entry)
        {
            _entries = entry.OrderByDescending(x => x.Item1).ToList();
        }
        
        public PriorityQue()
        {
            _entries = new List<Tuple<double, T>>();
        }

        public void Insert(double priority, T queItem)
        {
            _entries.Add(new Tuple<double, T>(priority, queItem));
            _entries = _entries.OrderByDescending(x => x.Item1).ToList();
        }

        public QueItem<T> Peek()
        {
            return new QueItem<T>(_entries.First());
        }

        public QueItem<T> Pop()
        {
            var first = Peek();
            _entries.RemoveAt(0);
            return first;
        }

        public List<Tuple<double, T>> ToList()
        {
            return _entries;
        }

    }

    public class QueItem<T>
    {
        private readonly Tuple<double, T> _queItem;

        public double Priority => _queItem.Item1;
        public T Item => _queItem.Item2;

        public QueItem(Tuple<double, T> queItem)
        {
            _queItem = queItem;
        }        
    }
}