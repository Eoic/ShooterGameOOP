using System;
using System.Runtime.Serialization;

/**
 * Used to send data between client and server.
 */
namespace Server.Network
{
    [DataContract]
    public class Message
    {
        public Guid ClientId { get; set; }
        [DataMember] public int Type;
        [DataMember] public string Payload { get; private set; }

        public Message(string payload)
        {
            ClientId = Guid.Empty;
            Payload = payload;
        }

        public Message(int type, string payload)
        {
            ClientId = Guid.Empty;
            Type = type;
            Payload = payload;
        }

        public override string ToString() =>
            $"Type: {Type}\nPayload: {Payload}";
    }
}