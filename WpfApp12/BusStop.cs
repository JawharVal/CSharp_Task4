using System.Collections.ObjectModel;
using WpfApp12;
public class BusStop
{
    public string StopId { get; private set; }
    public event Action<Bus> BusArrived;
    private readonly Random random = new Random();
    private PassengerFactory passengerFactory = new PassengerFactory();
    public ObservableCollection<Bus> Buses { get; private set; } = new ObservableCollection<Bus>();
    
    public void HandleBusArrival(Bus bus)
    {
        Buses.Add(bus);
        Console.WriteLine($"Bus {bus.Id} has arrived at {StopId}.");
        bus.BusFull += Bus_BusFull; // Subscribe to the BusFull event
        bus.CheckOvercrowded();
        BusArrived?.Invoke(bus);
    }
    
    public void BoardPassengers(Bus bus)
    {
        int passengersToBoard = random.Next(10, 20);
        for (int i = 0; i < passengersToBoard; i++)
        {
            IPassenger newPassenger = passengerFactory.CreateRandomPassenger();
            if (!bus.Passengers.Contains(newPassenger) && bus.AddPassenger(newPassenger))
            {
                bus.CheckOvercrowded();
            }
        }

        int passengersToDepart = random.Next(0, bus.CurrentPassengerCount);
        while (passengersToDepart-- > 0 && bus.CurrentPassengerCount > 0)
        {
            bus.RemovePassengerAt(random.Next(bus.CurrentPassengerCount));
        }
    }
    
    public void HandleBusDeparture(Bus bus)
    {
        if (Buses.Remove(bus))
        {
            Console.WriteLine($"Bus {bus.Id} has departed from {StopId}.");
            bus.BusFull -= Bus_BusFull; // Unsubscribe from the BusFull event
        }
        else {
            Console.WriteLine($"Failed to remove bus {bus.Id} from {StopId}.");
        }
    }
    
    private void Bus_BusFull(Bus bus)
    {
        Console.WriteLine($"The bus {bus.Id} at {StopId} is now full.");
    }

    public BusStop(string id)
    {
        StopId = id;
    }
}
