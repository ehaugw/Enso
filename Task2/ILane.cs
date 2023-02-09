namespace Interfaces {
    public interface ILane {
        public void Activate();
        public void Deactivate();
        public double Priority();
        public bool HasQueue();
        public bool IsActive();
        public void Update();
    }
}
