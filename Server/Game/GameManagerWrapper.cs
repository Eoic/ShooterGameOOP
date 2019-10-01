namespace Server.Game
{
    public class GameManagerWrapper
    {
        private readonly GameManager _gameManager = new GameManager();
        private static GameManagerWrapper _instance;

        public GameManagerWrapper() =>
            _gameManager.Start();

        public static GameManagerWrapper GetInstance() => 
            _instance ?? (_instance = new GameManagerWrapper());

        public GameManager GetGameManager() =>
            _gameManager;
    }
}