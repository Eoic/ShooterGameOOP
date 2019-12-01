using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Utilities
{
    public static class TimeConverter
    {
        public static int SecondsToTicks(int seconds) =>
            seconds * 1000 / (1000 / 60);
    }
}