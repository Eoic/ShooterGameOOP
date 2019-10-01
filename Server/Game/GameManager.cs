using Server.Network;
using System.Diagnostics;
using System.Threading;

// TODO: calculate delta (time between frames)

namespace Server.Game
{
    public class GameManager : IObserver<Message>
    {
        private readonly long _frameTime = 1_000_000_000 / Constants.Fps;
        private readonly Stopwatch _timer = new Stopwatch();
        private long _updateTime;
        private bool _isRunning;
        private Thread _runner;
        private long _waitTime;
        private long _now;
        // private long lastTime;
        // private long delta;

        /// <summary>
        /// Ends game loop
        /// </summary>
        public void Start()
        {
            if (_isRunning)
                return;

            _timer.Start();
            _isRunning = true;
            _runner = new Thread(Tick);
            _runner.Start();
        }

        /// <summary>
        /// Starts game loop
        /// </summary>
        public void Stop()
        {
            if (!_isRunning)
                return;

            _isRunning = false;
            _timer.Stop();
        }

        /// <summary>
        /// Handles messages received from client
        /// </summary>
        /// <param name="data"> Message instance </param>
        public void Update(Message data)
        {
            switch (data.Type)
            {
                case EventType.CreateGame:
                    break;
                case EventType.StartGame:
                    break;
                case EventType.EndGame:
                    break;
                default:
                    Debug.WriteLine("Received message with unknown type.");
                    Debug.WriteLine(data.ToString());
                    break;
            }
        }

        /// <summary>
        /// Runs continuously (e.g. 60 times per second) in a separate thread.
        /// Used for game logic calculations.
        /// </summary>
        private void Tick()
        {
            while (_isRunning)
            {
                _now = _timer.ElapsedMilliseconds * 1_000_000;
                // Execute game logic here
                _updateTime = _timer.ElapsedMilliseconds * 1_000_000 - _now;
                _waitTime = (_frameTime - _updateTime) / 1_000_000;
                Thread.Sleep((int)_waitTime);
            }
        }
    }
}