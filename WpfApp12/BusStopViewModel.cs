using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;

namespace WpfApp12
{
    public class BusStopViewModel : ViewModelBase
    {
        private CancellationTokenSource _simulationCts;
        private RouteManager _routeManager;
        private Random random = new Random();
        public ObservableCollection<BusViewModel> Buses { get; private set; }
        public ObservableCollection<BusStop> Stops { get; private set; }
        public ICommand StartSimulationCommand { get; private set; }
        public ICommand StopSimulationCommand { get; private set; }
        public ICommand AddBusStopCommand { get; private set; }
        public ICommand AddBusCommand { get; private set; }
        public BusStopViewModel()
        {
            InitializeCommands();
            _routeManager = new RouteManager();
            Stops = new ObservableCollection<BusStop>(_routeManager.Stops);
            Buses = new ObservableCollection<BusViewModel>(_routeManager.Buses.Select(b => new BusViewModel(b)));
            // Subscribe to BusArrived event for each BusStop
            foreach (var stop in Stops)
            {
                stop.BusArrived += OnBusArrived;
            }
        }

        private void InitializeCommands()
        {
            StartSimulationCommand = new RelayCommand(_ => StartSimulation(), _ => _simulationCts == null);
            StopSimulationCommand = new RelayCommand(_ => StopSimulation(), _ => _simulationCts != null);
            AddBusStopCommand = new RelayCommand(AddBusStop);
            AddBusCommand = new RelayCommand(AddBus);
        }

        public void StartSimulation()
        {
            if (_simulationCts != null)
            {
                Console.WriteLine("Simulation already running.");
                return;
            }
            Console.WriteLine("Starting new simulation...");
            _simulationCts = new CancellationTokenSource();
            var cancellationToken = _simulationCts.Token;

            // Start the simulation after a delay on a new background thread
            Task.Delay(2000).ContinueWith(_ =>
            {
                Task.Run(() =>
                {
                    try
                    {
                        _routeManager.StartSimulation(cancellationToken);
                    }
                    catch (OperationCanceledException)
                    {
                        Console.WriteLine("Simulation was canceled.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred during the simulation: {ex.Message}");
                        throw;
                    }
                }, cancellationToken);
            }, cancellationToken);
        }
        
        private void StopSimulation()
        {
            _simulationCts?.Cancel();
            _simulationCts?.Dispose();
            _simulationCts = null;
        }
        
        private void OnBusArrived(Bus bus)
        {
            Application.Current.Dispatcher.Invoke(() => {
                var busVm = new BusViewModel(bus);
                Buses.Add(busVm);
            });
        }
        
        private void AddBusStop(object parameter)
        {
            var newStopId = $"Stop {Stops.Count + 1}";
            var newStop = new BusStop(newStopId);
            Stops.Add(newStop);
            OnPropertyChanged(nameof(Stops));
            
            _routeManager.UpdateRoutesWithNewStop(newStop);
        }

        private void AddBus(object parameter)
        {
            if (Stops.Count == 0)
            {
                MessageBox.Show("Please add a bus stop first.");
                return;
            }
            var newBus = new Bus($"Bus {Buses.Count + 1}");
            Stops[0].HandleBusArrival(newBus);
            Buses.Add(new BusViewModel(newBus));
            _routeManager.Buses.Add(newBus); 
            OnPropertyChanged(nameof(Buses));
            
        }
    }
}
