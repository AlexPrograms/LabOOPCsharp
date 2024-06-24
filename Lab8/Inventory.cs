namespace Lab8
{
    public class Inventory
    {
        public List<Car> Cars { get; private set; }

        public Inventory()
        {
            Cars = new List<Car>();
        }

        public void AddCar(Car car)
        {
            Cars.Add(car);
        }

        public void RemoveCar(Car car)
        {
            Cars.Remove(car);
        }
    }
}