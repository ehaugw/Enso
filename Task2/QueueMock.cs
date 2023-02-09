using Interfaces;

namespace ConsoleApp {
    public class QueueMock {

        private Lane _lane;
        private int _queue;
        Random random = new Random();

        public QueueMock(Lane lane, int? queue) {
            _queue = queue ?? random.Next(0,10);
            _lane = lane;

            Task.Run(() => DriveOut());
            Task.Run(() => DriveIn());
        }

        public bool HasQueue() {
            return true;
        }

        private Task DriveOut() {
            while (true) {
                Task.Delay(random.Next(400, 600)).Wait();
                if (_lane.ActiveTime() > 1) {
                    if (_queue > 0) {
                        _queue --;
                        Console.WriteLine($"    Departed from {_lane.Name, -25}{_queue} remaining.");
                        _lane.Update();
                    }
                }
            }
        }

        private Task DriveIn() {
            while (true) {
                if (_lane.HasPriority) {
                    Task.Delay(random.Next(60000, 300000)).Wait();
                } else {
                    Task.Delay(random.Next(1000, 5000)).Wait();
                }
                // Console.WriteLine($"    Arrived at {_lane.Name, -28}{_queue} remaining.");
                _queue ++;
                _lane.Update();
            }
        }
    }
}
