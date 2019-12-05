using System;
using System.Text;
using Server.Game.Entities;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Server.Game.Bonuses;
using Server.Utilities;
using Server.Network;
using System.Linq;
using System.Diagnostics;

namespace Server.Game.GameRoomControl
{
    public class GameRoom : GameContext
    {
        public Guid RoomId { get; } = Guid.NewGuid();
        public int TimeTillRoomUpdate { get; private set; } = Constants.RoomUpdateInterval;
        public List<Bonus> Bonuses { get; private set; } = new List<Bonus>();
        public string CurrentTimerLabel { get; private set; } = Constants.WaitingForPlayers;
        public bool PlayerCanJoin { get => !(typeof(GameStateRunning) == State.GetType()) && !(typeof(GameStateReady) == State.GetType()); }

        public GameRoom()
        {
            State = new GameStateWaiting(this);
            Players = new Dictionary<Guid, Player>();
            TimeTillStateChange = TimeConverter.SecondsToTicks(Constants.GameWaitTime);
            Debug.WriteLine("Game room initialized wait time: {0} ticks., {1} s.", TimeTillStateChange, Constants.GameWaitTime);
        }

        public void AddPlayer(Player player)
        {
            if(Players.Count < Constants.MaxPlayerCount)
                Players.Add(player.Id, player);
        }

        public Player GetPlayer(Guid id) =>
            Players.ContainsKey(id) ? Players[id] : null;

        public void RemovePlayer(Guid id) =>
            Players.Remove(id);

        public bool IsFull() =>
            Players.Count >= Constants.MaxPlayerCount;

        public void ForceUpdate() =>
            TimeTillRoomUpdate = 1;

        // Update all players.
        public void Update(long delta)
        {
            var confirmedHits = 0;

            foreach (var keyValuePair in Players.ToList())
            {
                var player = keyValuePair.Value;

                if (player == null || !player.IsAlive)
                    continue;

                player.Update(delta);

                foreach (var bullet in player.Bullets.Where(bullet => bullet.IsActive))
                {
                    foreach (var otherPlayer in Players.Where(otherPlayer => otherPlayer.Key != keyValuePair.Key))
                    {
                        if (!bullet.Collider.IsColliding(bullet, otherPlayer.Value))
                            continue;

                        bullet.IsActive = false;
                        confirmedHits++;

                        if (otherPlayer.Value.Team == player.Team)
                            continue;

                        // Take damage only when game is running.
                        if (typeof(GameStateRunning) == State.GetType())
                            otherPlayer.Value.TakeDamage(bullet.Damage);

                        // Send update to everyone in the room on player's death
                        if (!otherPlayer.Value.IsAlive)
                        {
                            var serializablePlayer = otherPlayer.Value.GetSerializable();
                            var deathEventMessage = JsonParser.Serialize(new Message(ResponseCode.PlayerDied, JsonParser.Serialize(serializablePlayer)));

                            foreach (var client in Players)
                                ConnectionsPool.GetInstance().GetClient(client.Key).Send(deathEventMessage);

                            // End game if at least one team has no more players
                            if (Players.Count((player) => player.Value.IsAlive && player.Value.Team == 0) == 0 ||
                                Players.Count((player) => player.Value.IsAlive && player.Value.Team == 1) == 0)
                                TimeTillStateChange = 0;
                        }
                    }
                }
            }

            if (confirmedHits > 0)
                ForceUpdate();

            if (TimeTillRoomUpdate == 0)
                TimeTillRoomUpdate = Constants.RoomUpdateInterval;

            TimeTillRoomUpdate--;
            State.Tick();
        }

        public void SetBonuses(List<Bonus> bonuses) => Bonuses = bonuses;

        public List<SerializableBonus> GetSerializableBonuses() => 
            Bonuses.Select(bonus => bonus.GetSerializable()).ToList();

        public override string ToString()
        {
            const int dividerWidth = 88;
            var index = 0;
            var builder = new StringBuilder();
            builder.AppendLine(new string('-', dividerWidth));
            builder.AppendFormat("| {0, -50} | {1, -31} |\n", "Room ID", "Size");
            builder.AppendLine(new string('-', dividerWidth));
            builder.AppendFormat("| {0, -50} | {1, -31} |\n", RoomId.ToString(), Players.Count + " / " + Constants.MaxPlayerCount);
            builder.AppendLine(new string('-', dividerWidth));
            builder.AppendFormat("| {0, -3} | {1, -44} | {2, -23} | {3, -5} |\n", "Nr.", "Player ID", "Position", "Team");
            builder.AppendLine(new string('-', dividerWidth));

            foreach (var keyValuePair in Players) {
                var player = keyValuePair.Value;
                builder.AppendLine(new string('-', dividerWidth));
                builder.AppendFormat("| {0, -3} | {1, -44} | {2, -23} | {3, -5} |\n", ++index, keyValuePair.Key, player.Position, player.Team);

                builder.AppendLine(new string('-', dividerWidth));
                builder.AppendFormat("| Bullets |\n");
                builder.AppendLine(new string('-', dividerWidth));

                keyValuePair.Value.Bullets.ForEach((bullet) => {
                    if (bullet.IsActive)
                        builder.AppendLine(bullet.ToString());
                });
            }
            
            builder.AppendLine(new string('-', dividerWidth));
            return builder.ToString();
        }
        
        public SerializableGameRoom GetSerializable() =>
            new SerializableGameRoom(RoomId.ToString(), Players.Count, Constants.MaxPlayerCount);

        public override void UpdateTimer(string label, int value)
        {
            Debug.WriteLine("Sending wait time to client: " + value);
            var timerMessage = new Message(ResponseCode.NewTimerValue, JsonParser.Serialize(new SerializableTimer(label, value)));
            var timerString = JsonParser.Serialize(timerMessage);

            foreach (var player in Players)
                ConnectionsPool.GetInstance().GetClient(player.Key).Send(timerString);

            CurrentTimerLabel = label;
        }

        public override void UpdateStateChangeTime() =>
            TimeTillStateChange--;

        public override void SetState(IGameState state)
        {
            if (state != null)
                State = state;
        }

        public override void SetPlayersReady()
        {
            var offset = 150;

            void PositionPlayers(int xOffset, int yOffset, int team)
            {
                var multiplier = 1;

                foreach (var player in Players.Where((player) => player.Value.Team == team))
                {
                    player.Value.Position = new Vector(xOffset, yOffset * multiplier);
                    multiplier++;
                }
            }

            PositionPlayers(offset, offset, 0);
            PositionPlayers(Constants.MapWidth * Constants.MapTileSize - offset * 2, offset, 1);
        }

        [DataContract]
        public class SerializableGameRoomId
        {
            [DataMember]
            public Guid RoomId { get; set; }
            [DataMember]
            public int Team { get; set; }

            public SerializableGameRoomId(string roomId, int team)
            {
                RoomId = Guid.Parse(roomId);
                Team = team;
            }
        }
    }
}