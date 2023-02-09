using Interfaces;

namespace ConsoleApp {
    public class Controller : Interfaces.IController {

        private List<IState> _states = new List<IState>{};
        private IState? _activeState = null;
        private bool _isUpdating = false;

        public Controller() {
            Init();
            Update();
            OnUpdate();
            Console.WriteLine("OnUpdate has returned");
        }

        public void AddStates(IEnumerable<IState> states) {
            foreach (var state in states) {
                AddState(state);
            }
        }

        public void AddState(IState state) {
            _states.Add(state);
        }

        private void OnUpdate() {
            while (true) {
                Task.Delay(1000).Wait();
            }
        }

        public void Update() {
            while (_isUpdating) {
                Task.Delay(1);
            }

            _isUpdating = true;
            if (_activeState != null) {
                if (HighestPriority() != _activeState) {
                    Deactivate();
                }
            }

            if (_activeState == null) {
                Activate(HighestPriority());
            }
            _isUpdating = false;
        }

        // Implementation specific
        private void Activate(IState state) {
            _activeState = state;
            _activeState.Activate();
        }

        private IState HighestPriority() {
            return _states.Aggregate((state1, state2) => state1.Priority() >= state2.Priority() ? state1 : state2);
        }

        private void Deactivate() {
            _activeState!.Deactivate();
            _activeState = null;
        }

        private void Init() {
            var straightHorizontalState = new State(this, "Horizontal Straight");
            var straightVerticalState = new State(this, "Vertical Straight");
            var turnHorizontalState = new State(this, "Horizontal Turn");
            var turnVerticalState = new State(this, "Vertical Turn");
            var pedestrianState = new State(this, "Pedestrian");

            foreach (var tup in new Tuple<IState, string, bool>[] {
                    new Tuple<IState, string, bool>(straightHorizontalState, "West -> East", false),
                    new Tuple<IState, string, bool>(straightHorizontalState, "West -> East (Buss)", true),
                    new Tuple<IState, string, bool>(straightHorizontalState, "East -> West", false),
                    new Tuple<IState, string, bool>(straightHorizontalState, "East -> West (Buss)", true),
                    new Tuple<IState, string, bool>(straightHorizontalState, "West -> South", false),
                    new Tuple<IState, string, bool>(straightHorizontalState, "East -> North", false),

                    new Tuple<IState, string, bool>(straightVerticalState, "North -> South", false),
                    new Tuple<IState, string, bool>(straightVerticalState, "South -> North", false),
                    new Tuple<IState, string, bool>(straightVerticalState, "North -> West", false),
                    new Tuple<IState, string, bool>(straightVerticalState, "South -> East", false),

                    new Tuple<IState, string, bool>(turnHorizontalState, "West -> North", false),
                    new Tuple<IState, string, bool>(turnHorizontalState, "East -> South", false),

                    new Tuple<IState, string, bool>(turnVerticalState, "North -> East", false),
                    new Tuple<IState, string, bool>(turnVerticalState, "South -> West", false),

                    new Tuple<IState, string, bool>(pedestrianState, "Walk Omnidirectional", false)
            }) {
                new Lane(tup.Item1, tup.Item2, tup.Item3);
            }

            foreach (var state in _states) {
                state.Deactivate();
            }

            Console.WriteLine("Initiation complete\n");
        }
    }
}
