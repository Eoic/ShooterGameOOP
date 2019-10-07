using System.Runtime.Serialization;

namespace Server.Game.Bonuses
{
    [DataContract]
    public class SerializableBonus
    {
        [DataMember] public int Lifespan { get; set; }
        [DataMember] public int BonusAmount { get; set; }
        [DataMember] public Vector Position { get; set; }
        [DataMember] public string Type { get; set; }

        public SerializableBonus(int lifespan, int bonusAmount, Vector position, string type)
        {
            Lifespan = lifespan;
            BonusAmount = bonusAmount;
            Position = position;
            Type = type;
        }
    }
}