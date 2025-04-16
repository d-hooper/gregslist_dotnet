namespace gregslist_dotnet.Controllers;

// NOTE cs:api_controller

[ApiController]
[Route("api/[controller]")]
public class CarsController : ControllerBase
{
  public CarsController(CarsService carsService, Auth0Provider auth0Provider)
  {
    _carsService = carsService;
    _auth0Provider = auth0Provider;
  }

  private readonly CarsService _carsService;
  // NOTE don't forget to assign this a value!
  private readonly Auth0Provider _auth0Provider;


  [HttpGet]
  public ActionResult<List<Car>> GetAllCars()
  {
    try
    {
      List<Car> cars = _carsService.GetCars();
      return Ok(cars);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }


  [HttpGet("{carId}")]
  public ActionResult<Car> GetCarById(int carId)
  {
    try
    {
      Car car = _carsService.GetCarById(carId);
      return Ok(car);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }


  [Authorize] // you must be logged in to run the method directly below this data annotation
  [HttpPost]
  public async Task<ActionResult<Car>> CreateCar([FromBody] Car carData)
  {
    try
    {
      // NOTE the information about the person making the request using their bearer token
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      carData.CreatorId = userInfo.Id;
      Car car = _carsService.CreateCar(carData);
      return Ok(car);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [Authorize]
  [HttpDelete("{carId}")]
  public async Task<ActionResult<string>> DeleteCar(int carId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string message = _carsService.DeleteCar(carId, userInfo);
      return Ok(message);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [Authorize]
  [HttpPut("{carId}")]
  public async Task<ActionResult<Car>> UpdateCar(int carId, [FromBody] Car carUpdateData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Car car = _carsService.UpdateCar(carId, carUpdateData, userInfo);
      return Ok(car);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

}