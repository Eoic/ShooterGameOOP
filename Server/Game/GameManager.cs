using System.Collections.Generic;
using Server.Network;
using System.Diagnostics;
using System.Threading;
using Server.Game.Entities;
using Server.Utilities;
using Server.Game.Physics;

namespace Server.Game
{
    public class GameManager : IObserver<Message>
    {
        private readonly long _frameTime = 1_000_000_000 / Constants.Fps;
        private readonly HitsObserver _hitsObserver = new HitsObserver();
        private readonly List<GameRoom> _games = new List<GameRoom>();
        private readonly Stopwatch _timer;
        private readonly Thread _runner;
        private long _updateTime;
        private bool _isRunning;
        private long _waitTime;
        private long _delta;
        private long _now;
        
        public GameManager()
        {
            _runner = new Thread(Tick);
            _timer = new Stopwatch();
        }

        /// <summary>
        /// Starts game loop
        /// </summary>
        public void Start()
        {
            if (_isRunning)
                return;

            _isRunning = true;
            _timer.Start();
            _runner.Start();
        }

        /// <summary>
        /// Handles messages received from client
        /// </summary>
        /// <param name="data"> Message instance </param>
        public void Update(Message data)
        {
            switch (data.Type)
            {
                case RequestCode.Connect:
                    RequestsHandler.OnConnect(data);
                    break;
                case RequestCode.FormGameList:
                    RequestsHandler.OnGetGameList(data, _games);
                    break;
                case RequestCode.Disconnect:
                    RequestsHandler.OnDisconnect(data, _games);
                    break;
                case RequestCode.QuitGame:
                    RequestsHandler.OnGameQuit(data, _games);
                    break;
                case RequestCode.RaiseError:
                    RequestsHandler.OnError(data, _games);
                    break;
                case RequestCode.JoinGame:
                    RequestsHandler.OnGameJoin(data, _games);
                    break;
                case RequestCode.CreateGame:
                    RequestsHandler.OnGameCreate(data, _games);
                    break;
                case RequestCode.UpdateDirection:
                    RequestsHandler.OnDirectionUpdate(data, _games);
                    break;
                case RequestCode.Shoot:
                    RequestsHandler.OnShot(data, _games, _hitsObserver);
                    break;
                default:
                    RequestsHandler.OnUnknown(data);
                    break;
            }
        }

        /// <summary>
        /// Runs continuously (e.g. 60 times per second) in a separate thread.
        /// </summary>
        private void Tick()
        {
            while (_isRunning)
            {
                _now = _timer.ElapsedMilliseconds * 1_000_000;
                UpdateGameState(_delta);
                _updateTime = _timer.ElapsedMilliseconds * 1_000_000 - _now;
                _waitTime = (_frameTime - _updateTime) / 1_000_000;

                if (_waitTime < 0)
                    _waitTime = 0;

                Thread.Sleep((int)_waitTime);
                _delta = (_timer.ElapsedMilliseconds * 1_000_000 - _now) / 1_000_000;
            }
        }

        /// <summary>
        /// Updates game state and sends updates to clients
        /// </summary>
        /// <param name="delta"></param>
        private void UpdateGameState(long delta)
        {
            foreach (var gameRoom in _games)
            {
                // Update all room players.
                gameRoom.Update(delta);

                // If clients still don't need update sent, skip this iteration.
                if (gameRoom.TimeTillRoomUpdate != 0)
                    continue;

                // Create list of serializable players.
                var playerSerializes = new List<SerializablePlayer>();

                foreach (var gameRoomPlayer in gameRoom.Players)
                {
                    var player = gameRoomPlayer.Value;
                    var playerSerialize = new SerializablePlayer(player.Position, player.Direction, 0, gameRoomPlayer.Value.Id.ToString(), player.Team, player.Health, player.GetBullets());
                    playerSerializes.Add(playerSerialize);
                }

                // Send broadcast.
                foreach (var gameRoomPlayer in gameRoom.Players)
                {
                    var client = ConnectionsPool.GetInstance().GetClient(gameRoomPlayer.Key);

                    foreach (var player in playerSerializes)
                    {
                        // Set host player
                        if (player.PlayerId == gameRoomPlayer.Key.ToString())
                        {
                            player.Type = 10;
                            continue;
                        }

                        player.Type = 0;
                    }

                    var playerSerializesString = JsonParser.Serialize(playerSerializes);
                    var message = new Message(ResponseCode.PositionUpdated, playerSerializesString);
                    client?.Send(JsonParser.Serialize(message));
                }
            }
        }
    }
}