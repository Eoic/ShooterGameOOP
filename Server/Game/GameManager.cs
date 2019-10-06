using System.Collections.Generic;
using Server.Network;
using System.Diagnostics;
using System.Threading;
using Server.Game.Entities;
using Server.Utilities;

namespace Server.Game
{
    public class GameManager : IObserver<Message>
    {
        // Loop
        private readonly long _frameTime = 1_000_000_000 / Constants.Fps;
        private readonly Stopwatch _timer;
        private readonly Thread _runner;
        private long _updateTime;
        private bool _isRunning;
        private long _waitTime;
        private long _delta;
        private long _now;
        
        // Game session
        private List<GameRoom> _games = new List<GameRoom>();

        public GameManager()
        {
            _runner = new Thread(Tick);
            _timer = new Stopwatch();
        }

        /// <summary>
        /// Ends game loop
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
        /// Starts game loop
        /// </summary>
        public void Stop()
        {
            if (!_isRunning)
                return;

            _isRunning = false;
            _timer.Stop();
            _runner.Join();
        }

        /// <summary>
        /// Handles messages received from client
        /// </summary>
        /// <param name="data"> Message instance </param>
        public void Update(Message data)
        {
            switch (data.Type)
            {
                case EventType.ClientConnected:
                    break;
                case EventType.ClientDisconnected:
                    _games.ForEach(game => game.RemovePlayer(data.ClientId));
                    break;
                case EventType.ErrorOccured:
                    Debug.WriteLine(data.Payload);
                    break;
                case EventType.CreateGame:
                    // Creates new game and adds player itself
                    var gameRoom = new GameRoom();
                    var player = new Player(data.ClientId, gameRoom.RoomId);
                    gameRoom.AddPlayer(player);
                    _games.Add(gameRoom);
                    // Finally, send initial position (test)
                    var newPos = new Vector(0, 0);
                    var newPosString = JsonParser.Serialize(newPos);
                    var message = new Message(EventType.GameCreated, newPosString);
                    var messageStr = JsonParser.Serialize(message);

                    // Broadcast position update to all players in GameRoom
                    // TODO...

                    ConnectionsPool.GetInstance().GetClient(data.ClientId).Send(messageStr);
                    break;
                case EventType.DirectionUpdate:
                    _games.ForEach(game =>
                    {
                        var playerObj = game.GetPlayer(data.ClientId);
                        if (playerObj == null)
                            return;

                        var direction = JsonParser.Deserialize<Vector>(data.Payload);
                        game.GetPlayer(data.ClientId).Direction = direction;
                    });
                    break;
                default:
                    Debug.WriteLine("Received message with unknown type.");
                    Debug.WriteLine(data.ToString());
                    break;
            }
        }

        /// <summary>
        /// Runs continuously (e.g. 60 times per second) in a separate thread.
        /// Used for game logic calculations.
        /// </summary>
        private void Tick()
        {
            while (_isRunning)
            {
                _now = _timer.ElapsedMilliseconds * 1_000_000;

                // --- Game logic here ---
                _games.ForEach(game =>
                {
                    foreach (var keyValuePair in game.Players)
                    {
                        var player = keyValuePair.Value;

                        if (player == null)
                            continue;

                        player.Update(_delta);

                        if (player.TimeTillClientUpdate != 0) 
                            continue;
                        
                        var client = ConnectionsPool.GetInstance().GetClient(keyValuePair.Key);

                        if (client == null) 
                            continue;

                        var message = new Message(EventType.PositionUpdate, JsonParser.Serialize(player.Position));
                        client.Send(JsonParser.Serialize(message));
                    }
                });
                // -----------------------

                _updateTime = _timer.ElapsedMilliseconds * 1_000_000 - _now;
                _waitTime = (_frameTime - _updateTime) / 1_000_000;

                if (_waitTime < 0)
                    _waitTime = 0;

                Thread.Sleep((int)_waitTime);
                _delta = (_timer.ElapsedMilliseconds * 1_000_000 - _now) / 1_000_000;
            }
        }
    }
}