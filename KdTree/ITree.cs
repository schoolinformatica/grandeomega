namespace Tree
{
    public interface ITree<T>
    {
        ITree<T> Left { get; }
        ITree<T> Right { get; }
        T Value { get; }
        bool IsEmpty { get; }
    }
}