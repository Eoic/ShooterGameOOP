using Server.Game.Bonuses;
using Server.Game.Commands;
using Server.Game.Entities;
using Server.Game.GameRoomControl;
using Server.Models;
using Server.Network;
using Server.Utilities;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Server.Game
{
    public static class RequestsHandler
    {
        // When new client connected
        public static void OnConnect(Message data)
        {
            Debug.WriteLine("New client connected");
        }

        // When conection with client was lost
        public static void OnDisconnect(Message data, List<GameRoom> games)
        {
            var client = ConnectionsPool.GetInstance().GetClient(data.ClientId);

            if (client?.RoomId != null)
            {
                var game = games.Find(game => game.Players.ContainsKey(data.ClientId));
                var message = new Message(ResponseCode.GameQuit, data.ClientId.ToString());

                // Send update to remaing room players
                foreach (var player in game.Players)
                    ConnectionsPool.GetInstance().GetClient(player.Key).Send(JsonParser.Serialize(message));

                new RemovePlayerCommand(data.ClientId, client.RoomId, 0, games).Execute();
            }

            ConnectionsPool.GetInstance().RemoveClient(data.ClientId);
        }

        // When an error occours
        public static void OnError(Message data, List<GameRoom> games)
        {
            var client = ConnectionsPool.GetInstance().GetClient(data.ClientId);

            if (client?.RoomId != null)
                new RemovePlayerCommand(data.ClientId, client.RoomId, 0, games).Execute();

            ConnectionsPool.GetInstance().RemoveClient(data.ClientId);
        }

        // When player wants to join the game
        public static void OnGameJoin(Message data, List<GameRoom> games)
        {
            // Add player to game room
            var joinInfo = JsonParser.Deserialize<GameRoom.SerializableGameRoomId>(data.Payload);
            new AddPlayerCommand(data.ClientId, joinInfo.RoomId, joinInfo.Team, games).Execute();

            // Force room to update immediately
            var roomToUpdate = games.Find((room) => room.RoomId == joinInfo.RoomId);
            roomToUpdate.ForceUpdate();

            // Get other game room info and sent to client
            var bonusesMsg = new Message(ResponseCode.BonusesCreated, JsonParser.Serialize(roomToUpdate.GetSerializableBonuses()));
            var clientConnection = ConnectionsPool.GetInstance().GetClient(data.ClientId);
            clientConnection.RoomId = roomToUpdate.RoomId;
            ConnectionsPool.GetInstance().GetClient(data.ClientId).Send(JsonParser.Serialize(bonusesMsg));
        }

        // When player wants to create new game
        public static void OnGameCreate(Message data, List<GameRoom> games)
        {
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
            games.Add(gameRoom);

            // 4. Notify event about created game and send data.
            var serializablePlayer = new SerializablePlayer(initialPosition, new Vector(0, 0), 0, player.Id.ToString(), team, player.Health, new List<Bullet.SerializableBullet>());
            var gameCreationMessage = new Message(ResponseCode.GameCreated, JsonParser.Serialize(serializablePlayer));
            var bonusesMessage = new Message(ResponseCode.BonusesCreated, JsonParser.Serialize(gameRoom.GetSerializableBonuses()));
            var gameCreationString = JsonParser.Serialize(gameCreationMessage);
            var bonusesString = JsonParser.Serialize(bonusesMessage);
            client.Send(gameCreationString);
            client.Send(bonusesString);
        }

        // When player quits the game
        public static void OnGameQuit(Message data, List<GameRoom> games)
        {
            var game = games.Find(game => game.Players.ContainsKey(data.ClientId));
            var message = new Message(ResponseCode.GameQuit, data.ClientId.ToString());
            
            // Send update to remainng room players
            foreach (var player in game.Players)
                ConnectionsPool.GetInstance().GetClient(player.Key).Send(JsonParser.Serialize(message));

            new RemovePlayerCommand(data.ClientId, game.RoomId, 0, games).Execute();
        }

        // Forms and sends list of available games
        public static void OnGetGameList(Message payload, List<GameRoom> games)
        {
            var gameList = JsonParser.Serialize((from game in games select game.GetSerializable()).ToList());
            var gameListMessage = new Message(ResponseCode.GameListFormed, gameList);
            ConnectionsPool.GetInstance().GetClient(payload.ClientId).Send(JsonParser.Serialize(gameListMessage));
        }

        // When player chages its movement direction
        public static void OnDirectionUpdate(Message data, List<GameRoom> games)
        {
            var direction = JsonParser.Deserialize<Vector>(data.Payload);
            var game = games.Find(game => game.Players.ContainsKey(data.ClientId));
            var player = game.GetPlayer(data.ClientId);

            if (player == null)
                return;

            player.Direction = direction;
            game.ForceUpdate();
        }

        // When player makes a shot
        public static void OnShot(Message data, List<GameRoom> games, Network.IObserver<string> hitsObserver)
        {
            games.ForEach((game) =>
            {
                var player = game.GetPlayer(data.ClientId);
                var shotDirection = JsonParser.Deserialize<Vector>(data.Payload);

                if (player == null)
                    return;

                player.AddBullet(player.Position, shotDirection, hitsObserver);
                game.ForceUpdate();
            });
        }

        // When type of received request is unknown
        public static void OnUnknown(Message data)
        {
            Debug.WriteLine("Received message of unknown type.");
            Debug.WriteLine(data.ToString());
        }
    }
}