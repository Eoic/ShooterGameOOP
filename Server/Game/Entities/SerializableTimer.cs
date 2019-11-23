using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Server.Game.Entities
{
    [DataContract]
    public class SerializableTimer
    {
        [DataMember]
        public string Label { get; set; }

        [DataMember]
        public int Value { get; set; }

        public SerializableTimer(String label, int value)
        {
            Label = label;
            Value = value;
        }
    }
}