using System.Collections.Generic;
using Server.Network;
using System.Diagnostics;
using System.Threading;
using Server.Game.Entities;

// TODO: calculate delta (time between frames)

namespace Server.Game
{
    public class GameManager : IObserver<Message>
    {
        // Loop
        private readonly long _frameTime = 1_000_000_000 / Constants.Fps;
        private readonly Stopwatch _timer;
        private readonly Thread _runner;
        private long _updateTime;
        private bool _isRunning;
        private long _waitTime;
        private long delta;
        private long _now;
        
        // Game session
        private List<GameRoom> _games = new List<GameRoom>();

        public GameManager()
        {
            _runner = new Thread(Tick);
            _timer = new Stopwatch();
        }

        /// <summary>
        /// Ends game loop
        /// </summary>
        public void Start()
        {
            if (_isRunning)
                return;

            _isRunning = true;
            _timer.Start();
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
            _runner.Join();
        }

        /// <summary>
        /// Handles messages received from client
        /// </summary>
        /// <param name="data"> Message instance </param>
        public void Update(Message data)
        {
            switch (data.Type)
            {
                case EventType.ClientConnected:
                case EventType.ClientDisconnected:
                    break;
                case EventType.CreateGame:
                    // TODO: Create new game room and and player instance
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
                // Game logic here
                
                // ---------------
                _updateTime = _timer.ElapsedMilliseconds * 1_000_000 - _now;
                _waitTime = (_frameTime - _updateTime) / 1_000_000;

                if (_waitTime < 0)
                    _waitTime = 0;

                Thread.Sleep((int)_waitTime);
                delta = (_timer.ElapsedMilliseconds * 1_000_000 - _now) / 1_000_000;
            }
        }
    }
}