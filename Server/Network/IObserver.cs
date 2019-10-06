namespace Server.Network
{
    public interface IObserver<in T>
    {
        void Update(T data);
    }
}
