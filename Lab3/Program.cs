using Figgle;

abstract class Animal { 
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
        Console.WriteLine("Sup, dude");
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
        Console.WriteLine("Raf");
    }

    public override void Walk()
    {
        Console.WriteLine("Dog is running");
    }
}
class Program {
    static void Main(string[] args) {
        
        Snake python = new Snake();
        Cat stepan = new Cat();
        Dog labrador = new Dog();
        Console.WriteLine(
            FiggleFonts.Standard.Render("What Does The Fox Say"));
        labrador.Sound();
        python.Sound();
        stepan.Sound();
        Console.WriteLine("\nAnimal Movements:");
        labrador.Walk();
        python.Walk();
        stepan.Walk();

    }
}
