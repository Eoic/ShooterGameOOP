using System;
using System.Dynamic;
using System.Collections.Generic;

namespace Server.Core
{
    class ServerEventArgs : EventArgs
    {
        public static readonly new ServerEventArgs Empty = new ServerEventArgs();
        public dynamic Data;

        public ServerEventArgs() { }

        public ServerEventArgs(ExpandoObject data) => Data = data;

        // Prints fields and values of Data object.
        public void Dump()
        {
            foreach (KeyValuePair<string, object> keyValuePair in Data)
            {
                Console.WriteLine($"'{keyValuePair.Key}': '{keyValuePair.Value}'");
            }
        }
    }
}