namespace Interfaces {
    public interface IController {
        public void AddStates(IEnumerable<IState> states);
        public void AddState(IState state);
        public void Update();
    }
}
