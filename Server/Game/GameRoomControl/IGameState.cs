namespace Server.Game.GameRoomControl
{
    public interface IGameState
    {
        GameContext Context { get; }
        void Tick();
        void EndGame();
    }
}
