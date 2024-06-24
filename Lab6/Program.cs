namespace Lab6
{
    public interface ICoffeeMachine
    {
        int WaterAmount { get; }
        int CoffeeBeansAmount { get; }
        int MilkAmount { get; }
        bool IsWaterHeated { get; }

        void MakeEspresso();
        void MakeLatte();
        void DisplayStatus();
    }

    public class CoffeeMachine : ICoffeeMachine
    {
        public int WaterAmount { get; private set; }
        public int CoffeeBeansAmount { get; private set; }
        public int MilkAmount { get; private set; }
        public bool IsWaterHeated { get; private set; }

        public CoffeeMachine(int initialWater, int initialCoffeeBeans, int initialMilk)
        {
            WaterAmount = initialWater;
            CoffeeBeansAmount = initialCoffeeBeans;
            MilkAmount = initialMilk;
            IsWaterHeated = false;
        }

        public void MakeEspresso()
        {
            const int espressoBeans = 20;
            const int waterAmount = 50;
            if (CoffeeBeansAmount >= espressoBeans && WaterAmount >= waterAmount)
            {
                HeatWater();
                GrindBeans(espressoBeans);
                CoffeeBeansAmount -= espressoBeans;
                WaterAmount -= waterAmount;
                Console.WriteLine("Made an Espresso!");
                TurnOffHeater();
            }
            else
            {
                Console.WriteLine("Not enough coffee beans or water to make an Espresso.");
            }
        }

        public void MakeLatte()
        {
            const int latteBeans = 25;
            const int waterAmount = 70;
            const int milkAmount = 180;
            if (CoffeeBeansAmount >= latteBeans && WaterAmount >= waterAmount && MilkAmount >= milkAmount)
            {
                HeatWater();
                GrindBeans(latteBeans);
                CoffeeBeansAmount -= latteBeans;
                WaterAmount -= waterAmount;
                MilkAmount -= milkAmount;
                Console.WriteLine("Made a Latte!");
                TurnOffHeater();
            }
            else
            {
                Console.WriteLine("Not enough coffee beans, water, or milk to make a Latte.");
            }
        }

        public void DisplayStatus()
        {
            Console.WriteLine($"Water Amount: {WaterAmount} ml");
            Console.WriteLine($"Coffee Beans Amount: {CoffeeBeansAmount} grams");
            Console.WriteLine($"Milk Amount: {MilkAmount} ml");
            Console.WriteLine($"Water Heated: {IsWaterHeated}");
        }

        private void HeatWater()
        {
            if (!IsWaterHeated)
            {
                Console.WriteLine("Heating water...");
                IsWaterHeated = true;
            }
        }

        private void GrindBeans(int amount)
        {
            if (CoffeeBeansAmount >= amount)
            {
                Console.WriteLine($"Grinding {amount} grams of coffee beans...");
            }
            else
            {
                throw new InvalidOperationException("Not enough coffee beans.");
            }
        }

        private void TurnOffHeater()
        {
            if (IsWaterHeated)
            {
                IsWaterHeated = false;
                Console.WriteLine("Water heater turned off.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ICoffeeMachine coffeeMachine = new CoffeeMachine(1000, 100, 500);

            while (true)
            {
                Console.WriteLine("\nChoose an operation: (1. Make Espresso, 2. Make Latte, 3. Display Status, 'q' to quit)");
                string choice = Console.ReadLine();

                if (choice == "q")
                {
                    break;
                }

                switch (choice)
                {
                    case "1":
                        coffeeMachine.MakeEspresso();
                        break;
                    case "2":
                        coffeeMachine.MakeLatte();
                        break;
                    case "3":
                        coffeeMachine.DisplayStatus();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }
    }
}
