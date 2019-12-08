namespace ConsoleApp1
{
    interface IAbstractIterator
    {
        dynamic First();
        dynamic Next();
        bool IsDone { get; }
        dynamic GetCurrent();
    }
}
