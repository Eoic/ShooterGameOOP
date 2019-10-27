using Server.Game.Entities;
using Server.Network;
using Server.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Server.Game
{
    public class PlayerManager
    {
        public static void AddPlayer(List<GameRoom> games, Guid clientId)
        {
            for (int i = 0; i < games.Count; i++)
            {
                if (!games[i].IsFull())
                {
                    if (games[i].Players.ContainsKey(clientId))
                    {
                        Debug.WriteLine("Player is already in the game. Bailing out.");
                        break;
                    }

                    // 1. Find room and prepare client.
                    var room = games[i];
                    var joiningPlayer = new Player(clientId, room.RoomId);
                    var initPos = new Vector(Map.CenterX, Map.CenterY);
                    joiningPlayer.Position = initPos;
                    games[i].AddPlayer(joiningPlayer);

                    // 2. TODO: get bonuses from game room

                    // 3. Notify about successful join
                    var gameJoinString = new Message(ResponseCode.GameJoined, JsonParser.Serialize(initPos));
                    ConnectionsPool.GetInstance().GetClient(clientId).Send(JsonParser.Serialize(gameJoinString));
                    break;
                }
            }
        }

        public static void RemovePlayer(List<GameRoom> games, Guid clientId)
        {
            int? emptyRoomIndex = null;

            for (var i = 0; i < games.Count; i++)
            {
                var playerInThisRoom = games[i].Players.ContainsKey(clientId);

                if (!playerInThisRoom)
                    continue;

                games[i].Players.Remove(clientId);

                if (games[i].Players.Count == 0)
                {
                    Debug.WriteLine("Room has no more players...");
                    emptyRoomIndex = i;
                }

                break;
            }

            if (emptyRoomIndex.HasValue)
                games.RemoveAt(emptyRoomIndex.Value);
        }
    }
}