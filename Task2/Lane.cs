using Interfaces;

namespace ConsoleApp {
    public class Lane : Interfaces.ILane {

        public string Name { get; private set; }
        public bool HasPriority { get; private set; }
        private DateTime _deactivatedAt;
        private DateTime _activatedAt;
        private IState _state;
        private bool _isActive;

        private QueueMock _queueMock;

        public Lane(IState state, string name, bool priority, int? queue = null) {
            Name = name;
            HasPriority = priority;
            _state = state;
            _isActive = false;
            _queueMock = new QueueMock(this, queue);
            state.AddLane(this);
        }

        public double Priority() {
            var waitTime = WaitTime();
            return Math.Pow(1.1, waitTime) + (HasPriority ? 3 : 0) + (IsActive() ? (ActiveTime() < 10 && HasQueue() ? 10 : 0) : 0);
        }
        
        public void Activate() {
            Console.WriteLine($"{Name,-25} activated   at {_deactivatedAt}, after {WaitTime(), -5} seconds");
            _isActive = true;
            _activatedAt = DateTime.Now;
        }

        public void Deactivate() {
            // Console.WriteLine($"{Name,-25} deactivated at {_deactivatedAt}");
            _isActive = false;
            _deactivatedAt = DateTime.Now;
        }

        public bool HasQueue() {
            return _queueMock.HasQueue();
        }
        
        public bool IsActive() {
            return _isActive;
        }

        public void Update() {
            _state.Update();
        }

        // Implementation specific
        private double WaitTime() {
            var waitTime = DateTime.Now.Subtract(_deactivatedAt).TotalSeconds;
            return IsActive() ? 0: waitTime;
        }

        public double ActiveTime() {
            var activeTime = DateTime.Now.Subtract(_activatedAt).TotalSeconds;
            return IsActive() ? activeTime : 0;
        }
    }
}
