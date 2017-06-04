namespace Tree
{
    public class Empty<T> : ITree<T>
    {
        public ITree<T> Left { get; }
        public ITree<T> Right { get; }
        public T Value { get; }
        public bool IsEmpty => false;
    }
}