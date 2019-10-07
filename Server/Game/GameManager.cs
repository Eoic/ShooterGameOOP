using System;
using System.Collections.Generic;
using Server.Network;
using System.Diagnostics;
using System.Threading;
using Server.Game.Bonuses;
using Server.Game.Entities;
using Server.Utilities;

namespace Server.Game
{
    public class GameManager : Network.IObserver<Message>
    {
        private List<GameRoom> _games = new List<GameRoom>();
        private readonly BonusFactory _bonusFactory;
        private readonly long _frameTime = 1_000_000_000 / Constants.Fps;
        private readonly Stopwatch _timer;
        private readonly Thread _runner;
        private long _updateTime;
        private bool _isRunning;
        private long _waitTime;
        private long _delta;
        private long _now;
        
        public GameManager()
        {
            _bonusFactory = new BonusFactory();
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
                    // Create new game room and add player.
                    var initialPosition = new Vector(Map.CenterX, Map.CenterY);
                    var bonuses = CreateBonuses();
                    var gameRoom = new GameRoom();
                    var player = new Player(data.ClientId, gameRoom.RoomId);
                    player.Position = initialPosition;
                    gameRoom.AddPlayer(player);
                    _games.Add(gameRoom);

                    // Finally, send initial position (test)
                    var gameCreationMessage = new Message(EventType.GameCreated, JsonParser.Serialize(initialPosition));
                    var bonusesMessage = new Message(EventType.InstantiateBonuses, JsonParser.Serialize(bonuses));
                    var gameCreationString = JsonParser.Serialize(gameCreationMessage);
                    var bonusesString = JsonParser.Serialize(bonusesMessage);

                    // TODO: Broadcast position update to all players in the game room.

                    // Send update to player.
                    var client = ConnectionsPool.GetInstance().GetClient(data.ClientId);
                    client.Send(gameCreationString);
                    client.Send(bonusesString);
                    break;
                case EventType.DirectionUpdate:
                    _games.ForEach(game =>
                    {
                        // Get player and update direction.
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

        // Generates random amount of bonuses.
        private List<SerializableBonus> CreateBonuses()
        {
            var randomGen = new Random(DateTime.Now.Millisecond);
            var iterations = randomGen.Next(Constants.MinBonusCount, Constants.MaxBonusCount);
            var bonusList = new List<Bonus>();
            var serializables = new List<SerializableBonus>();

            for (var i = 0; i < iterations; i++)
            {
                var bonusType = string.Empty;
                var bonusAmount = randomGen.Next(Constants.BonusMinAmount, Constants.BonusMaxAmount + 1);
                var bonusLifespan = randomGen.Next(Constants.BonusMinLifespan, Constants.BonusMaxLifespan + 1);
                
                switch (randomGen.Next(Constants.BonusTypeCount))
                {
                    case 0:
                        bonusType = BonusType.Health;
                        break;
                    case 1:
                        bonusType = BonusType.Ammo;
                        break;
                    case 2:
                        bonusType = BonusType.Speed;
                        break;
                }

                if (string.IsNullOrEmpty(bonusType))
                    continue;

                var bonus = _bonusFactory.GetBonus(bonusType, bonusAmount, bonusLifespan);
                bonus.Position = new Vector(randomGen.Next(0, Map.Width - Constants.MapBoundOffset), randomGen.Next(0, Map.Height - Constants.MapBoundOffset));
                serializables.Add(bonus.GetSerializable(bonusType));
                bonusList.Add(bonus);
            }

            return serializables;
        }
    }
}