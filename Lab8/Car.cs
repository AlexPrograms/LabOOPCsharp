namespace Lab8;

public class Car
{
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public decimal Price { get; set; }

    public Car(string manufacturer, string model, decimal price)
    {
        Manufacturer = manufacturer;
        Model = model;
        Price = price;
    }

    public override string ToString()
    {
        return $"{Manufacturer} {Model} - {Price:C}";
    }
}
