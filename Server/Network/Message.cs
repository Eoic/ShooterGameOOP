using System.Runtime.Serialization;

/**
 * Used to send data between client and server.
 */
namespace Server.Network
{
    [DataContract]
    public class Message
    {
        [DataMember] public EventType Type;
        [DataMember] public string Payload { get; private set; }

        public Message(string payload)
        {
            Payload = payload;
        }

        public Message(EventType type, string payload)
        {
            Type = type;
            Payload = payload;
        }

        public override string ToString() =>
            $"Type: {Type}\nPayload: {Payload}";
    }
}