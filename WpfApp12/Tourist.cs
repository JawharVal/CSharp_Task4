namespace WpfApp12
{
    public class Tourist : IPassenger
    {
        public string Name { get; }

        public Tourist(string name)
        {
            Name = name;
        }

        public void BoardBus(Bus bus)
        {
            Console.WriteLine($"{Name} is boarding {bus.Id}");
            bus.AddPassenger(this);
        }

        public override string ToString()
        {
            return $"Tourist {Name}";
        }
    }

}
