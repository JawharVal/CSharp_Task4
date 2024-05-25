namespace WpfApp12
{
    public class Commuter : IPassenger
    {
        public string Name { get; private set; }

        public Commuter(string name)
        {
            Name = name;
        }

        public void BoardBus(Bus bus)
        {
            // Implementation for commuter passenger boarding logic
            Console.WriteLine($"{Name} is using a monthly pass to board {bus.Id}.");
            bus.AddPassenger(this);
        }
    }
}
