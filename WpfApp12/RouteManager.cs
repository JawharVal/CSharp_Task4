using System.Collections.ObjectModel;
using System.ComponentModel;
using WpfApp12;
using System.Windows;

public class RouteManager : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public List<BusStop> Stops { get; private set; } = new List<BusStop>();
    public ObservableCollection<Bus> Buses { get; private set; } = new ObservableCollection<Bus>();

    public RouteManager()
    {
        // Initialize stops
        for (int i = 1; i <= 5; i++)
        {
            Stops.Add(new BusStop($"Stop {i}"));
        }
    }
    
    public void MoveBuses()
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            if (!Buses.Any())
            {
                Console.WriteLine("No buses to move.");
                return;
            }

            foreach (var bus in new List<Bus>(Buses))  // Using a copy of the list to modify safely during iteration
            {
                int currentStopIndex = Stops.FindIndex(stop => stop.Buses.Contains(bus));
                if (currentStopIndex == -1)
                {
                    Console.WriteLine($"Bus {bus.Id} not found at any stop.");
                    continue;
                }

                // Handle passengers alighting and boarding
                Stops[currentStopIndex].BoardPassengers(bus);  // This method should handle both boarding and alighting

                int nextStopIndex = CalculateNextStopIndex(bus, currentStopIndex);

                Console.WriteLine($"Moving bus {bus.Id} from {Stops[currentStopIndex].StopId} to {Stops[nextStopIndex].StopId}");

                Stops[currentStopIndex].HandleBusDeparture(bus);
                Stops[nextStopIndex].HandleBusArrival(bus);
            }
        });
    }

    private int CalculateNextStopIndex(Bus bus, int currentStopIndex)
    {
        if (bus.IsMovingForward)
        {
            if (currentStopIndex + 1 < Stops.Count)
            {
                return currentStopIndex + 1;
            }
            else
            {
                bus.IsMovingForward = false;
                return currentStopIndex - 1;
            }
        }
        else
        {
            if (currentStopIndex - 1 >= 0)
            {
                return currentStopIndex - 1;
            }
            else
            {
                bus.IsMovingForward = true;
                return currentStopIndex + 1;
            }
        }
    }
    
    public void StartSimulation(CancellationToken token)
    {
        Task.Run(async () =>
        {
            try
            {
                Console.WriteLine("Task started.");
                if (!Buses.Any())
                {
                    Console.WriteLine("Adding initial buses to the simulation...");
                    AddInitialBuses();
                    Console.WriteLine("Initial buses added.");
                }

                while (!token.IsCancellationRequested)
                {
                    Console.WriteLine("Moving buses...");
                    MoveBuses();
                    Console.WriteLine("Buses moved, waiting...");
                    await Task.Delay(5000); // Ensures non-blocking delay
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during simulation: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Simulation stopping...");
            }
        }, token).ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Console.WriteLine($"Task faulted with exception: {task.Exception?.InnerException?.Message}");
            }
            if (task.IsCanceled)
            {
                Console.WriteLine("Task was canceled.");
            }
        });
    }
    public void UpdateRoutesWithNewStop(BusStop newStop)
    {
        Stops.Add(newStop); // Add the new stop to the list of stops
        OnPropertyChanged(nameof(Stops)); // Notify any observers that the list of stops has changed
        Console.WriteLine($"New stop added: {newStop.StopId}. Total stops: {Stops.Count}");
    }
    private void AddInitialBuses()
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            for (int i = 0; i < Stops.Count; i++)
            {
                Bus newBus = new Bus($"Bus {i + 1}");
                Stops[i].HandleBusArrival(newBus);
                Buses.Add(newBus);
                OnPropertyChanged(nameof(Stops)); 
            }
        });
    }
}
