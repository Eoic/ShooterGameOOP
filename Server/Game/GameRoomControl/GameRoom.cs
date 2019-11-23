using System;
using System.Text;
using Server.Game.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Server.Game.Bonuses;

namespace Server.Game.GameRoomControl
{
    public class GameRoom : IGameContext
    {
        public IGameState State;
        public Guid RoomId { get; } = Guid.NewGuid();
        public int TimeTillRoomUpdate { get; private set; } = Constants.RoomUpdateInterval;
        public int TimeTillGameStart { get; private set; } = Constants.GameStartCountdown;
        public Dictionary<Guid, Player> Players { get; } = new Dictionary<Guid, Player>();
        public List<Bonus> Bonuses { get; private set; } = new List<Bonus>();

        public GameRoom() =>
            State = new GameStateWaiting(this);

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

                if (player == null)
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
      
                        otherPlayer.Value.TakeDamage(bullet.Damage);
                    }
                }
            }

            if (confirmedHits > 0)
                ForceUpdate();

            if (TimeTillRoomUpdate == 0)
                TimeTillRoomUpdate = Constants.RoomUpdateInterval;

            TimeTillRoomUpdate--;

            if (TimeTillGameStart > 0)
                TimeTillGameStart--;
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
        
        public SerializableGameRoom GetSerializable()
        {
            return new SerializableGameRoom(RoomId.ToString(), Players.Count, Constants.MaxPlayerCount);
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