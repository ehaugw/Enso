using System.Linq;
using Interfaces;

namespace ConsoleApp {
    public class State : Interfaces.IState {

        private List<ILane> lanes = new List<ILane>{};
        private IController _controller;
        public string Name { get; private set; }
        
        public State(IController controller, string name) {
            _controller = controller;
            controller.AddState(this);
            Name = name;
        }

        public void AddLanes(IEnumerable<ILane> lanes) {
            foreach (var lane in lanes) {
                AddLane(lane);
            }
        }

        public void AddLane(ILane lane) {
            lanes.Add(lane);
        }

        public double Priority() {
            return lanes.Select(lane => lane.Priority()).Sum();
        }

        public void Activate() {
            Console.WriteLine($"{"Activating state:", -25} {Name}");
            foreach (var lane in lanes) {
                lane.Activate();
            }
            Console.WriteLine("");
        }

        public void Deactivate() {
            // Console.WriteLine("Deactivating state");
            foreach (var lane in lanes) {
                lane.Deactivate();
            }
            // Console.WriteLine("");
        }

        public void Update() {
            _controller.Update();
        }
    }
}
