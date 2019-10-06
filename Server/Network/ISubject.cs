namespace Server.Network
{
    public interface ISubject<T>
    {
        void Attach(IObserver<T> observer);
        void Detach(IObserver<T> observer);
        void NotifyAllObservers(T data);
    }
}
