namespace SpaceWars
{
    public interface IPool<T>
    {
        public void Pop(T t);
        public T Pull();
    }
}