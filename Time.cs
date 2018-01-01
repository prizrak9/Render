using System;
using System.Windows.Threading;

namespace render
{
    public static class Time
    {
        public static event EventHandler OnUpdate
        {
            add => timer.Tick += value;
            remove => timer.Tick -= value;
        }
        public static double DeltaTime { get; private set; }
        private static DispatcherTimer timer;


        public static void Start()
        {
            TimeSpan t = new TimeSpan(0, 0, 0, 0, 10);
            DeltaTime = t.TotalSeconds;
            timer = new DispatcherTimer();
            timer.Interval = t;
            timer.Start();
        }
    }
}
