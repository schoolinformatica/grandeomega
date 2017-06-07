namespace Tree
{
    public interface ITree<T>
    {
        int Dimension { get; }
        ITree<T> Left { get; }
        ITree<T> Right { get; }
        T Value { get; }
        bool IsEmpty { get; }
    }
}