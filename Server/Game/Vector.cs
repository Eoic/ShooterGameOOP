using System.Runtime.Serialization;

namespace Server.Game
{
    [DataContract]
    public class Vector
    {
        [DataMember]
        public int X { get; set; }
        [DataMember]
        public int Y { get; set; }

        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}