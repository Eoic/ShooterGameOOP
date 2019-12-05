using Server.Game;

namespace Server.Utilities
{
    public static class TimeConverter
    {
        public static int SecondsToTicks(int seconds) =>
            seconds * 1000 / Constants.AverageDeltaTime;

        public static int TicksToSeconds(int ticks) =>
            ticks * Constants.AverageDeltaTime / 1000;
    }
}