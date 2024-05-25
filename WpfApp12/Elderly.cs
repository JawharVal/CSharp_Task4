namespace WpfApp12
{
    public class Elderly : IPassenger
    {
        public string Name { get; private set; }

        public Elderly(string name)
        {
            Name = name;
        }

        public void BoardBus(Bus bus)
        {
            // Implementation for elderly passenger boarding logic
            Console.WriteLine($"{Name} is boarding {bus.Id} slowly.");
            bus.AddPassenger(this);
        }
    }
}
