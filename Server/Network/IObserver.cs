namespace Server.Network
{
    public interface IObserver<T>
    {
        void Update(T data);
    }
}
