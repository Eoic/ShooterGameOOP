using System;
using System.Dynamic;
using System.Collections.Generic;

namespace Server.Core
{
    internal class ServerEventArgs : EventArgs
    {
        public new static readonly ServerEventArgs Empty = new ServerEventArgs();
        public readonly dynamic Data;

        private ServerEventArgs() { }

        public ServerEventArgs(ExpandoObject data) => Data = data;

        // Prints all fields and values of Data object.
        public void Dump()
        {
            foreach (KeyValuePair<string, object> keyValuePair in Data)
            {
                Console.WriteLine($"'{keyValuePair.Key}': '{keyValuePair.Value}'");
            }
        }
    }
}