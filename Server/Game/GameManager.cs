using System;
using System.Collections.Generic;
using Server.Network;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Server.Game.Bonuses;
using Server.Game.Entities;
using Server.Utilities;
using Server.Game.Commands;
using Server.Game.Physics;
using Server.Models;

namespace Server.Game
{
    public class GameManager : Network.IObserver<Message>
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
                // New client connected
                case RequestCode.Connect:
                    Debug.WriteLine("New client connected.");
                    break;

                // Send all available games
                case RequestCode.FormGameList:
                    var gameList = JsonParser.Serialize((from game in _games select game.GetSerializable()).ToList());
                    var gameListMessage = new Message(ResponseCode.GameListFormed, gameList);
                    ConnectionsPool.GetInstance().GetClient(data.ClientId).Send(JsonParser.Serialize(gameListMessage));
                    break;

                // Connection with client lost
                case RequestCode.Disconnect:
                    Debug.WriteLine(data.ClientId);
                    var clientDisconnect = ConnectionsPool.GetInstance().GetClient(data.ClientId);

                    if (clientDisconnect?.RoomId != null)
                        new RemovePlayerCommand(data.ClientId, clientDisconnect.RoomId, 0, _games).Execute();

                    ConnectionsPool.GetInstance().RemoveClient(data.ClientId);
                    Debug.WriteLine("Client disconnected");
                    break;

                // Client left the game
                case RequestCode.QuitGame:
                    var roomIdQuit = ConnectionsPool.GetInstance().GetClient(data.ClientId).RoomId;
                    new RemovePlayerCommand(data.ClientId, roomIdQuit, 0, _games).Execute();
                    var message = new Message(RequestCode.QuitGame, "Game quit successfully.");
                    ConnectionsPool.GetInstance().GetClient(data.ClientId).Send(JsonParser.Serialize(message));
                    break;

                // An error occured
                case RequestCode.RaiseError:
                    var clientError = ConnectionsPool.GetInstance().GetClient(data.ClientId);

                    if (clientError?.RoomId != null)
                        new RemovePlayerCommand(data.ClientId, clientError.RoomId, 0, _games).Execute();

                    ConnectionsPool.GetInstance().RemoveClient(data.ClientId);
                    break;

                // Player is joining to existing game
                case RequestCode.JoinGame:
                    var joinInfo = JsonParser.Deserialize<GameRoom.SerializableGameRoomId>(data.Payload);
                    new AddPlayerCommand(data.ClientId, joinInfo.RoomId, joinInfo.Team, _games).Execute();

                    // Force room to update immediately
                    var roomToUpdate = _games.Find((room) => room.RoomId == joinInfo.RoomId);
                    roomToUpdate.ForceUpdate();
                    var bonusesMsg = new Message(ResponseCode.BonusesCreated, JsonParser.Serialize(roomToUpdate.GetSerializableBonuses()));

                    var clientConnection = ConnectionsPool.GetInstance().GetClient(data.ClientId);
                    clientConnection.RoomId = roomToUpdate.RoomId;

                    ConnectionsPool.GetInstance().GetClient(data.ClientId).Send(JsonParser.Serialize(bonusesMsg));
                    break;

                // Player creates new game
                case RequestCode.CreateGame:
                    // 1. Find and prepare client, create game room.
                    var client = ConnectionsPool.GetInstance().GetClient(data.ClientId);
                    var gameRoom = new GameRoom();
                    var team = int.Parse(data.Payload);
                    client.RoomId = gameRoom.RoomId;

                    // 2. Create new player instance and additional objects.
                    var initialPosition = new Vector(Map.CenterX, Map.CenterY);
                    var player = new Player(data.ClientId, gameRoom.RoomId);
                    var bonuses = BonusGenerator.Create();

                    // 3. Setup created game objects.
                    player.Position = initialPosition;
                    player.JoinTeam(team);
                    gameRoom.AddPlayer(player);
                    gameRoom.SetBonuses(bonuses);
                    _games.Add(gameRoom);

                    // 4. Notify event about created game and send data.
                    var serializablePlayer = new SerializablePlayer(initialPosition, new Vector(0, 0), 0, player.Id.ToString(), team, player.Health, new List<Bullet.SerializableBullet>());
                    var gameCreationMessage = new Message(ResponseCode.GameCreated, JsonParser.Serialize(serializablePlayer));
                    var bonusesMessage = new Message(ResponseCode.BonusesCreated, JsonParser.Serialize(gameRoom.GetSerializableBonuses()));
                    var gameCreationString = JsonParser.Serialize(gameCreationMessage);
                    var bonusesString = JsonParser.Serialize(bonusesMessage);
                    client.Send(gameCreationString);
                    client.Send(bonusesString);
                    break;

                // Player changed its movement direction
                case RequestCode.UpdateDirection:
                    _games.ForEach(game =>
                    {
                        var playerObj = game.GetPlayer(data.ClientId);

                        if (playerObj == null)
                            return;

                        var direction = JsonParser.Deserialize<Vector>(data.Payload);
                        game.GetPlayer(data.ClientId).Direction = direction;
                        game.ForceUpdate();
                    });
                    break;
                case RequestCode.Shoot:
                    _games.ForEach((game) =>
                    {
                        var playerObj = game.GetPlayer(data.ClientId);
                        var shotDirection = JsonParser.Deserialize<Vector>(data.Payload);

                        if (playerObj == null)
                            return;

                        playerObj.AddBullet(playerObj.Position, shotDirection, _hitsObserver);
                        game.ForceUpdate();
                    });
                    break;
                default:
                    Debug.WriteLine("Received message of unknown type.");
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
                foreach (var gameRoom in _games)
                {
                    // Update all room players.
                    gameRoom.Update(_delta);

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
                    
                    // Print game room info.
                    // Debug.WriteLine(gameRoom);

                    // Send broadcast.
                    foreach (var gameRoomPlayer in gameRoom.Players)
                    {
                        var client = ConnectionsPool.GetInstance().GetClient(gameRoomPlayer.Key);   // Target client

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