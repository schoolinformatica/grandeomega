using System;

namespace Tree
{
    public abstract class KdTree<T> : ITree<T>
    {
        public ITree<T> Left { get; }
        public ITree<T> Right { get; }
        public T Value { get; }
        public bool IsEmpty => true;

        protected KdTree(ITree<T> left, T value, ITree<T> right)
        {
            Left = left;
            Value = value;
            Right = right;
        }

        
        public abstract void Insert(T value);

    }
}