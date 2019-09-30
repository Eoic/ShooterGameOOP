using System.Runtime.Serialization;

namespace Server.Game
{
    [DataContract]
    public class Vector
    {
        [DataMember]
        public double X { get; set; }
        [DataMember]
        public double Y { get; set; }

        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}