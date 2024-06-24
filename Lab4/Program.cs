using Figgle;

abstract class Animal
{
    public abstract void Sound();
    public abstract void Walk();
}

class Snake : Animal
{
    public override void Sound()
    {
        Console.WriteLine("Ssssssssshhhhss");
    }

    public override void Walk()
    {
        Console.WriteLine("crawl");
    }
}
class Cat : Animal
{
    public override void Sound()
    {
        Console.WriteLine("Meow, meow, ngga");
    }

    public override void Walk()
    {
        Console.WriteLine("Not moving (lazy)");
    }
}
class Dog : Animal
{
    public override void Sound()
    {
        Console.WriteLine("Sup, dude");
    }

    public override void Walk()
    {
        Console.WriteLine("Dog is running");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Snake python = new Snake();
        Cat stepan = new Cat();
        Dog labrador = new Dog();

        bool exit = false;
        
        while (!exit)
        {
            Console.Clear();
            Console.WriteLine(
                FiggleFonts.Rectangles.Render("What Does The Fox Say"));
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Animal Sounds");
            Console.WriteLine("2. Animal Movements");
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ShowSoundsMenu(python, stepan, labrador);
                    break;
                case "2":
                    ShowMovementsMenu(python, stepan, labrador);
                    break;
                case "3":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }

    static void ShowSoundsMenu(Snake python, Cat stepan, Dog labrador)
    {
        Console.Clear();
        Console.WriteLine("Animal Sounds:");
        Console.WriteLine("1. Snake");
        Console.WriteLine("2. Cat");
        Console.WriteLine("3. Dog");
        Console.Write("Enter your choice: ");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                python.Sound();
                break;
            case "2":
                stepan.Sound();
                break;
            case "3":
                labrador.Sound();
                break;
            default:
                Console.WriteLine("Invalid choice. Try again.");
                break;
        }

        Console.WriteLine("Press any key to return to the main menu...");
        Console.ReadKey();
    }

    static void ShowMovementsMenu(Snake python, Cat stepan, Dog labrador)
    {
        Console.Clear();
        Console.WriteLine("Animal Movements:");
        Console.WriteLine("1. Snake");
        Console.WriteLine("2. Cat");
        Console.WriteLine("3. Dog");
        Console.Write("Enter your choice: ");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                python.Walk();
                break;
            case "2":
                stepan.Walk();
                break;
            case "3":
                labrador.Walk();
                break;
            default:
                Console.WriteLine("Invalid choice. Try again.");
                break;
        }

        Console.WriteLine("Press any key to return to the main menu...");
        Console.ReadKey();
    }
}
