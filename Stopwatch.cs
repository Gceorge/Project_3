using System;

namespace Project_3
{
    public class Stopwatch
    {
        private DateTime? startTime;
        private DateTime? endTime;

        public void Start()
        {
            startTime = DateTime.Now;
            endTime = null; // Reset endTime
        }

        public void Stop()
        {
            if (startTime == null)
            {
                throw new InvalidOperationException("Stopwatch has not been started.");
            }

            endTime = DateTime.Now;
        }

        public double ElapsedMilliseconds
        {
            get
            {
                if (startTime == null)
                {
                    throw new InvalidOperationException("Stopwatch has not been started.");
                }

                var end = endTime ?? DateTime.Now;
                return (end - startTime.Value).TotalMilliseconds;
            }
        }

        public void Reset()
        {
            startTime = null;
            endTime = null;
        }

        // Add the StartNew method
        public static Stopwatch StartNew()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            return stopwatch;
        }
    }
}
