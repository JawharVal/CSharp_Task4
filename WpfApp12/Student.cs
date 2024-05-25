namespace WpfApp12
{
    public class Student : IPassenger
    {
        public string Name { get; private set; }
        public bool HasBoarded { get; private set; }

        public Student(string name)
        {
            Name = name;
            HasBoarded = false;
        }

        public void BoardBus(Bus bus)
        {
            if (bus.CurrentPassengerCount < bus.Capacity)
            {
                HasBoarded = true;
                Console.WriteLine($"{Name} has boarded the bus.");
            }
            else
            {
                Console.WriteLine($"Cannot board: Bus is full. {Name} must wait for the next bus.");
            }
        }
    }
}
