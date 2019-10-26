using System.Collections.Generic;

namespace Server.Network
{
    public interface ISubject<T>
    {
        List<IObserver<T>> Observers { get; }
        void Attach(IObserver<T> observer);
        void Detach(IObserver<T> observer);
        void NotifyAllObservers(T data);
    }
}
