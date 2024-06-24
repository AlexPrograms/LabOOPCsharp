namespace Lab8;

class Program
{
    static void Main(string[] args)
    {
        CarDealer carDealer = new CarDealer(100000m);
        List<ICarDealer> dealers = new List<ICarDealer> { carDealer };

        while (true)
        {
            Console.WriteLine("1. User Menu\n2. Admin Menu\nq. Quit");
            string choice = Console.ReadLine();

            if (choice == "q")
            {
                break;
            }

            switch (choice)
            {
                case "1":
                    UserMenu(carDealer, dealers);
                    break;
                case "2":
                    AdminMenu(carDealer, dealers);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }

    static void UserMenu(CarDealer carDealer, List<ICarDealer> dealers)
    {
        while (true)
        {
            try
            {
                Console.WriteLine("1. Buy Car\n2. Search Cars\nb. Back");
                string choice = Console.ReadLine();

                if (choice == "b")
                {
                    break;
                }

                switch (choice)
                {
                    case "1":
                        BuyCar(carDealer, dealers);
                        break;
                    case "2":
                        SearchCars(carDealer, dealers);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }

    static void BuyCar(CarDealer carDealer, List<ICarDealer> dealers)
    {
        Console.Write("Enter manufacturer: ");
        string manufacturer = Console.ReadLine();
        var cars = carDealer.SearchCarsAcrossDealers(manufacturer, dealers);
        if (cars.Count == 0)
        {
            Console.WriteLine("No cars found.");
            return;
        }
        for (int i = 0; i < cars.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {cars[i]}");
        }
        Console.Write("Choose a car to buy: ");
        if (int.TryParse(Console.ReadLine(), out int carChoice) && carChoice > 0 && carChoice <= cars.Count)
        {
            carDealer.SellCar(cars[carChoice - 1]);
            Console.WriteLine("Car purchased successfully.");
        }
        else
        {
            Console.WriteLine("Invalid choice. Try again.");
        }
    }

    static void SearchCars(CarDealer carDealer, List<ICarDealer> dealers)
    {
        Console.Write("Enter manufacturer: ");
        string manufacturer = Console.ReadLine();
        var cars = carDealer.SearchCarsAcrossDealers(manufacturer, dealers);
        if (cars.Count == 0)
        {
            Console.WriteLine("No cars found.");
            return;
        }
        foreach (var car in cars)
        {
            Console.WriteLine(car);
        }
    }

    static void AdminMenu(CarDealer carDealer, List<ICarDealer> dealers)
    {
        while (true)
        {
            try
            {
                Console.WriteLine("1. Add Car\n2. Check Balance\n3. Exchange Car\nb. Back");
                string choice = Console.ReadLine();

                if (choice == "b")
                {
                    break;
                }

                switch (choice)
                {
                    case "1":
                        AddCar(carDealer);
                        break;
                    case "2":
                        CheckBalance(carDealer);
                        break;
                    // case "3":
                    //     ExchangeCar(carDealer, dealers);
                    //     break;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }

    static void AddCar(CarDealer carDealer)
    {
        Console.Write("Enter manufacturer: ");
        string manufacturer = Console.ReadLine();
        Console.Write("Enter model: ");
        string model = Console.ReadLine();
        Console.Write("Enter price: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal price))
        {
            Car car = new Car(manufacturer, model, price);
            carDealer.BuyCar(car);
            Console.WriteLine("Car added successfully.");
        }
        else
        {
            Console.WriteLine("Invalid price. Try again.");
        }
    }

    static void CheckBalance(CarDealer carDealer)
    {
        Console.WriteLine($"Current Balance: {carDealer.CurrentAccount.Balance:C}");
    }

    // static void ExchangeCar(CarDealer carDealer, List<ICarDealer> dealers)
    // {
    //     Console.Write("Enter your car's manufacturer: ");
    //     string yourCarManufacturer = Console.ReadLine();
    //     Console.Write("Enter your car's model: ");
    //     string yourCarModel = Console.ReadLine();
    //
    //     Car yourCar = carDealer.Inventory.Cars.FirstOrDefault(c => c.Manufacturer == yourCarManufacturer && c.Model == yourCarModel);
    //
    //     if (yourCar == null)
    //     {
    //         Console.WriteLine("Your car is not in the inventory.");
    //         return;
    //     }
    //
    //     Console.Write("Enter the manufacturer of the car you want: ");
    //     string desiredCarManufacturer = Console.ReadLine();
    //     var desiredCars = carDealer.SearchCarsAcrossDealers(desiredCarManufacturer, dealers);
    //
    //     if (desiredCars.Count == 0)
    //     {
    //         Console.WriteLine("No cars found for exchange.");
    //         return;
    //     }
    //
    //     for (int i = 0; i < desiredCars.Count; i++)
    //     {
    //         Console.WriteLine($"{i + 1}. {desiredCars[i]}");
    //     }
    //     Console.Write("Choose a car to exchange: ");
    //     if (int.TryParse(Console.ReadLine(), out int carChoice) && carChoice > 0 && carChoice <= desiredCars.Count)
    //     {
    //         Car desiredCar = desiredCars[carChoice - 1];
    //         CarDealer otherDealer = (CarDealer)dealers.FirstOrDefault(d => d.Inventory.Cars.Contains(desiredCar));
    //
    //         if (otherDealer == null)
    //         {
    //             Console.WriteLine("Selected car is not available from any dealer.");
    //             return;
    //         }
    //
    //         carDealer.ExchangeCar(yourCar, desiredCar, otherDealer);
    //     }
    //     else
    //     {
    //         Console.WriteLine("Invalid choice. Try again.");
    //     }
    // }
}