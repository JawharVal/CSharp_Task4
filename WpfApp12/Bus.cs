using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WpfApp12
{
    public class Bus
    {
        private static readonly Random random = new Random(); 
        public string Id { get; set; }  
        public event Action<Bus> BusFull;
        public event Action<Bus> Overcrowded;
        private readonly int capacity = 6; // Max capacity of the bus
        public bool IsMovingForward { get; set; } = true; 
        public bool IsFull => passengers.Count >= capacity;
        public int CurrentPassengerCount => passengers.Count;
        public int Capacity => capacity;
        private ObservableCollection<IPassenger> passengers = new ObservableCollection<IPassenger>();
        public ReadOnlyObservableCollection<IPassenger> Passengers { get; }
        public event PropertyChangedEventHandler PropertyChanged;
        
        public void CheckOvercrowded() {
            int overcrowdingThreshold = (int)(Capacity * 1.2);  // 20% more than the capacity
            bool isActuallyOvercrowded = Passengers.Count > Capacity;
            bool isCriticallyOvercrowded = Passengers.Count >= overcrowdingThreshold;

            Console.WriteLine($"Checking overcrowded status for Bus {Id}, Current count: {Passengers.Count}, Capacity: {Capacity}");
            if (isActuallyOvercrowded) {
                if (!IsOvercrowded) {
                    IsOvercrowded = true;
                    OnPropertyChanged(nameof(IsOvercrowded));
                    Console.WriteLine($"Overcrowded status changed for Bus {Id}: {IsOvercrowded}");
                    Overcrowded?.Invoke(this);
                }
            } else if (IsOvercrowded && !isCriticallyOvercrowded) {
                IsOvercrowded = false;
                OnPropertyChanged(nameof(IsOvercrowded));
                Console.WriteLine($"Overcrowded status changed for Bus {Id}: {IsOvercrowded}");
            }
            if (Passengers.Count == Capacity)
            {
                BusFull?.Invoke(this);
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
        
        public Bus(string id)
        {
            Id = id;
            passengers = new ObservableCollection<IPassenger>();
            Passengers = new ReadOnlyObservableCollection<IPassenger>(passengers);
        }
        
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool AddPassenger(IPassenger passenger)
        {
            int overcrowdingThreshold = (int)(Capacity * 1.5);  // 50% more than the capacity
            bool allowOvercrowding = random.Next(100) < 50;  // 50% chance to allow overcrowding
            
            Console.WriteLine($"Attempting to add passenger to Bus {Id}: Current count {Passengers.Count}, Capacity {Capacity}, Allow Overcrowding {allowOvercrowding}");

            if (Passengers.Count < Capacity || (allowOvercrowding && Passengers.Count < overcrowdingThreshold))
            {
                passengers.Add(passenger);
                OnPropertyChanged(nameof(Passengers));
                OnPropertyChanged(nameof(CurrentPassengerCount));
                CheckOvercrowded();
                Console.WriteLine($"Passenger added: {passenger} to Bus {Id}");
                return true;
            }
            else
            {
                Console.WriteLine($"Attempt to add passenger failed: Bus {Id} is full or overcrowding not allowed this time.");
                return false;
            }
        }
        
        public void RemovePassengerAt(int index)
        {
            if (index >= 0 && index < passengers.Count)
            {
                IPassenger passenger = passengers[index];
                passengers.RemoveAt(index);
                OnPropertyChanged(nameof(Passengers));
                OnPropertyChanged(nameof(CurrentPassengerCount));
                CheckOvercrowded();
                Console.WriteLine($"Passenger removed: {passenger.Name} from Bus {Id}");
            }
            else
            {
                Console.WriteLine($"Attempt to remove passenger failed: Invalid index {index}.");
            }
        }
    }
}