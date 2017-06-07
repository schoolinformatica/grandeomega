using System;

namespace Tree
{
    public class KdTree<T> : ITree<T>
    {
        public int Dimension { get; }
        public ITree<T> Left { get; }
        public ITree<T> Right { get; }
        public T Value { get; }
        public bool IsEmpty => true;

        protected KdTree(ITree<T> left, T value, ITree<T> right, int dimension)
        {
            Left = left;
            Value = value;
            Right = right;
            Dimension = dimension;
        }
    }
}