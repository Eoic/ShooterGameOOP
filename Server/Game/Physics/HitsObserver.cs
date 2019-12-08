using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Server.Game.Physics
{
    public class HitsObserver : Network.IObserver<string>
    {
        public void Update(string data)
        {
            Debug.WriteLine("Received: " + data);
        }
    }
}