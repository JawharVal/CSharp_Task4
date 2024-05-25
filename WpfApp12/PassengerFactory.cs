namespace WpfApp12;

public class PassengerFactory
{
    private Random random = new Random();

    public IPassenger CreateRandomPassenger()
    {
        int type = random.Next(4);
        switch (type)
        {
            case 0: return new Student($"Student {random.Next(1000)}");
            case 1: return new Elderly($"Elderly {random.Next(1000)}");
            case 2: return new Commuter($"Commuter {random.Next(1000)}");
            case 3: return new Tourist($"Tourist {random.Next(1000)}");
            default: return new Student($"Student {random.Next(1000)}");
        }
    }
}
