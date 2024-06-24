namespace Lab8;

public class CarDealer : ICarDealer
{
    public Inventory Inventory { get; set; }
    public CurrentAccount CurrentAccount { get; private set; }
    private const decimal MarkupPercentage = 0.10m;

    public CarDealer(decimal initialBalance)
    {
        Inventory = new Inventory();
        CurrentAccount = new CurrentAccount(initialBalance);
    }

    public void BuyCar(Car car)
    {
        Inventory.AddCar(car);
        CurrentAccount.Debit(car.Price);
    }

    public void SellCar(Car car)
    {
        var sellingPrice = car.Price * (1 + MarkupPercentage);
        Inventory.RemoveCar(car);
        CurrentAccount.Credit(sellingPrice);
    }

    public void ExchangeCar(Car carToGive, Car carToReceive, ICarDealer otherDealer)
    {
        if (!Inventory.Cars.Contains(carToGive) || !otherDealer.SearchCars(carToReceive.Manufacturer).Contains(carToReceive))
        {
            Console.WriteLine("Exchange failed. One of the cars is not available.");
            return;
        }

        var priceDifference = carToReceive.Price - carToGive.Price;
        if (CurrentAccount.Balance < priceDifference)
        {
            Console.WriteLine("Exchange failed. Insufficient balance.");
            return;
        }

        Inventory.RemoveCar(carToGive);
        otherDealer.BuyCar(carToGive);

        Inventory.AddCar(carToReceive);
        otherDealer.SellCar(carToReceive);

        if (priceDifference > 0)
        {
            CurrentAccount.Debit(priceDifference);
        }
        else
        {
            CurrentAccount.Credit(-priceDifference);
        }

        Console.WriteLine("Exchange completed successfully.");
    }

    public List<Car> SearchCars(string manufacturer)
    {
        return Inventory.Cars.Where(c => c.Manufacturer == manufacturer).ToList();
    }

    public List<Car> SearchCarsAcrossDealers(string manufacturer, List<ICarDealer> dealers)
    {
        List<Car> foundCars = new List<Car>();
        foreach (var dealer in dealers)
        {
            foundCars.AddRange(dealer.SearchCars(manufacturer));
        }
        return foundCars;
    }
}

