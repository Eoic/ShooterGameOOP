using System;
using System.Runtime.Serialization;
using System.Text;

namespace Server.Game
{
    [DataContract]
    public class Vector
    {
        [DataMember] public double X { get; set; }
        [DataMember] public double Y { get; set; }

        public Vector() { }

        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }

        public void Add(Vector vector)
        {
            X += vector.X;
            Y += vector.Y;
        }

        public void Add(double scalar)
        {
            X += scalar;
            Y += scalar;
        }

        public static Vector operator+(Vector left, Vector right) =>
            new Vector(left.X + right.X, left.Y + right.Y);

        public static Vector operator *(Vector vector, double scalar) =>
            new Vector(vector.X * scalar, vector.Y * scalar);

        public Vector Normalized()
        {
            var magnitude = Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
            return new Vector(X / magnitude, Y / magnitude);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendFormat("X: {0:0.00}, Y: {0:0.00}", X, Y);
            return builder.ToString();
        }
    }
}