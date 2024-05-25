using System.Collections.ObjectModel;
using System.Windows.Input;

namespace WpfApp12
{
    public class BusViewModel : ViewModelBase
    {
        public ICommand CheckPassengerCountCommand { get; private set; }
        private Bus _bus;
        private ObservableCollection<IPassenger> _passengers;
        public int CurrentPassengerCount => _bus.Passengers.Count;
        public string Id => _bus.Id;

        public string Status => _bus.IsFull ? "Full" : "Not Full";
        public ObservableCollection<IPassenger> Passengers
        {
            get => _passengers;
            set
            {
                _passengers = value;
                OnPropertyChanged(nameof(Passengers));
            }
        }
        
        public void ExecuteCheckPassengerCount(object parameter)
        {
            Console.WriteLine($"Current passenger count (via command): {CurrentPassengerCount}");
        }
        public BusViewModel(Bus bus)
        {
            CheckPassengerCountCommand = new RelayCommand(ExecuteCheckPassengerCount);
            _bus = bus;
            Passengers = new ObservableCollection<IPassenger>(_bus.Passengers);
            _bus.BusFull += OnBusFull;
            Console.WriteLine($"BusViewModel created for Bus {bus.Id} with {CurrentPassengerCount} passengers initially.");
            Console.WriteLine($"BusViewModel created for Bus {bus.Id} with capacity: {bus.Capacity}");
            _bus.Overcrowded += OnOvercrowded;
            
        }
        
        private bool _overcrowdedMessageShown = false; // Flag to track whether the message has been shown
        private void OnOvercrowded(Bus bus)
        {
            {
                Console.WriteLine($"Bus {bus.Id} is overcrowded. Displayed message to the user.");
            }
        }
        
        private bool _isOvercrowded;
        public bool IsOvercrowded
        {
            get => _isOvercrowded;
            set
            {
                if (_isOvercrowded != value)
                {
                    _isOvercrowded = value;
                    OnPropertyChanged(nameof(IsOvercrowded));
                    Console.WriteLine($"Overcrowded status changed for Bus {Id}: {IsOvercrowded}");
                }
            }
        }

        private void OnBusFull(Bus bus)
        {
            OnPropertyChanged(nameof(Status));
        }
    }
}