namespace Interfaces {
    public interface IState {
        public void AddLanes(IEnumerable<ILane> lanes);
        public void AddLane(ILane lane);
        public double Priority();
        public void Activate();
        public void Deactivate();
        public void Update();
    }
}
