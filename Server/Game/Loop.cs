using Server.Network;
using System.Diagnostics;
using System.Threading;

// TODO: calculate delta (time between frames)

namespace Server.Game
{
    public class Loop : IObserver<Message>
    {
        private readonly long frameTime = 1_000_000_000 / Constants.FPS;
        private readonly Stopwatch timer = new Stopwatch();
        private bool isRunning = false;
        private Thread runner;
        private long updateTime;
        private long lastTime;
        private long waitTime;
        private long now;
        private long delta;

        // Start game loop
        public void Start()
        {
            if (isRunning)
                return;

            timer.Start();
            isRunning = true;
            runner = new Thread(new ThreadStart(Tick));
            runner.Start();
        }

        // Stop game loop
        public void Stop()
        {
            if (!isRunning)
                return;

            isRunning = false;
            timer.Stop();
        }

        // Called once subject (publisher) notifies its subscribers.
        // Probably needs queueing...
        public void Update(Message data)
        {
            // Debug.WriteLine("Message received:");
            Debug.WriteLine(data.Payload);
        }

        // Runs continously (e.g. 60 times per second) in a separate thread.
        // Used for game logic calculations.
        private void Tick()
        {
            while (isRunning)
            {
                now = timer.ElapsedMilliseconds * 1_000_000;
                // Execute game logic here
                updateTime = timer.ElapsedMilliseconds * 1_000_000 - now;
                waitTime = (frameTime - updateTime) / 1_000_000;
                Thread.Sleep((int)waitTime);
            }
        }
    }
}