

namespace gregslist_dotnet.Services;

public class CarsService
{
  public CarsService(CarsRepository carsRepository)
  {
    _carsRepository = carsRepository;
  }
  private readonly CarsRepository _carsRepository;


  internal List<Car> GetCars()
  {
    List<Car> cars = _carsRepository.GetCars();
    return cars;
  }

  internal Car GetCarById(int carId)
  {
    Car car = _carsRepository.GetCarById(carId);

    if (car == null)
    {
      throw new Exception($"No car found with the id of {carId}");
    }

    return car;
  }

  internal Car CreateCar(Car carData)
  {
    Car car = _carsRepository.CreateCar(carData);
    return car;
  }

  internal string DeleteCar(int carId, Account userInfo)
  {
    Car car = GetCarById(carId); //this.GetCarById()

    if (car.CreatorId != userInfo.Id)
    {
      throw new Exception($"YOU ARE NOT ALLOWED TO DELETE SOMEONE ELSE'S CAR, {userInfo.Name.ToUpper()}!");
    }

    _carsRepository.DeleteCar(carId);

    return $"Your {car.Make} {car.Model} has been deleted!";
  }

  internal Car UpdateCar(int carId, Car carUpdateData, Account userInfo)
  {
    Car car = GetCarById(carId); //this.GetCarById()

    if (car.CreatorId != userInfo.Id)
    {
      throw new Exception($"YOU ARE NOT ALLOWED TO UPDATE SOMEONE ELSE'S CAR, {userInfo.Name.ToUpper()}!");
    }

    // ?? is null coalescing operator, checks to see if the left-hand side is null and defaults to the right if it is
    car.Make = carUpdateData.Make ?? car.Make;
    car.Model = carUpdateData.Model ?? car.Model;
    // NOTE will only work if the int property is nullable in your model
    car.Price = carUpdateData.Price ?? car.Price;
    car.ImgUrl = carUpdateData.ImgUrl ?? car.ImgUrl;
    // NOTE will only work if the bool property is nullable in your model
    car.HasCleanTitle = carUpdateData.HasCleanTitle ?? car.HasCleanTitle;

    _carsRepository.UpdateCar(car);

    return car;
  }
}