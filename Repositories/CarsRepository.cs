



namespace gregslist_dotnet.Repositories;

public class CarsRepository
{
  public CarsRepository(IDbConnection db)
  {
    _db = db;
  }

  private readonly IDbConnection _db;


  internal List<Car> GetCars()
  {
    string sql = @"
    SELECT 
    cars.*,
    accounts.*
    FROM cars
    INNER JOIN accounts ON accounts.id = cars.creator_id;";

    // NOTE we have multiple data types coming in on each row when we run this sql statement, so dapper needs to know how you are going to handle each piece of data with a mapping function (2nd argument)
    // NOTE you need to know which order the different pieces of data are coming in on the rows, and cast those into objects (classes) that support them. dapper will default to splitting up the data by the id column
    List<Car> cars = _db.Query(sql, (Car car, Account account) =>
    {
      car.Creator = account; // sticks account object onto car
      return car;
    }).ToList();

    return cars;
  }

  internal Car GetCarById(int carId)
  {
    string sql = @"
    SELECT 
    cars.*,
    accounts.*
    FROM cars
    INNER JOIN accounts ON accounts.id = cars.creator_id
    WHERE cars.id = @carId;";

    Car foundCar = _db.Query(sql, (Car car, Account account) =>
    {
      car.Creator = account;
      return car;
    }, new { carId }).SingleOrDefault();
    return foundCar;
  }

  internal Car CreateCar(Car carData)
  {
    string sql = @"
    INSERT INTO 
    cars (make, model, year, price, color, mileage, engine_type, img_url, has_clean_title, creator_id)
    VALUES (@Make, @Model, @Year, @Price, @Color, @Mileage, @EngineType, @ImgUrl, @HasCleanTitle, @CreatorId);
    
    SELECT
    cars.*,
    accounts.*
    FROM cars
    INNER JOIN accounts ON accounts.id = cars.creator_id 
    WHERE cars.id = LAST_INSERT_ID();";

    Car createdCar = _db.Query(sql, (Car car, Account account) =>
    {
      car.Creator = account;
      return car;
    }, carData).SingleOrDefault();

    return createdCar;
  }

  internal void DeleteCar(int carId)
  {
    string sql = "DELETE FROM cars WHERE id = @carId LIMIT 1;";

    int rowsAffected = _db.Execute(sql, new { carId });

    if (rowsAffected == 0)
    {
      throw new Exception("NO ROWS WERE DELETED");
    }

    if (rowsAffected > 1)
    {
      throw new Exception(rowsAffected + " ROWS WERE DELETED AND THAT IS REALLY BAD. CALL JAKE!!!!!");
    }
  }

  internal void UpdateCar(Car car)
  {
    string sql = @"
    UPDATE cars
    SET
    make = @Make,
    model = @Model,
    price = @Price,
    img_url = @ImgUrl,
    has_clean_title = @HasCleanTitle
    WHERE id = @Id
    LIMIT 1;";

    int rowsAffected = _db.Execute(sql, car);

    if (rowsAffected == 0)
    {
      throw new Exception("NO ROWS WERE UPDATED");
    }

    if (rowsAffected > 1)
    {
      throw new Exception(rowsAffected + " ROWS WERE UPDATED AND THAT IS REALLY BAD. CALL JAKE!!!!!");
    }
  }
}