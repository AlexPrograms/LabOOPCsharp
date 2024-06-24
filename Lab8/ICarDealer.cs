namespace Lab8;

public interface ICarDealer
{
    void BuyCar(Car car);
    void SellCar(Car car);
    void ExchangeCar(Car carToGive, Car carToReceive, ICarDealer otherDealer);
    List<Car> SearchCars(string manufacturer);

}