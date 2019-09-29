namespace Server.Network
{
    public class Message
    {
        public MessageType Type { get; private set; }
        public string Payload { get; private set; }

        public Message(MessageType type, string payload)
        {
            Type = type;
            Payload = payload;
        }
    }
}