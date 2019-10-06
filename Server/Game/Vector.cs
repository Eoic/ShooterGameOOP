﻿using System.Runtime.Serialization;

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

        public static Vector operator *(Vector vector, double scalar) =>
            new Vector(vector.X * scalar, vector.Y * scalar);

        public override string ToString()
        {
            return $"(X: {X}, Y: {Y})";
        }
    }
}