namespace gregslist_dotnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HousesController : ControllerBase
{
  private readonly HousesService _housesService;
  private readonly Auth0Provider _auth0Provider;

  public HousesController(HousesService housesService, Auth0Provider auth0Provider)
  {
    _housesService = housesService;
    _auth0Provider = auth0Provider;
  }

  [HttpGet]
  public ActionResult<List<House>> GetAllHouses()
  {
    try
    {
      List<House> houses = _housesService.GetHouses();
      return Ok(houses);
    }
    catch (Exception exception)
    {

      throw new Exception(exception.Message);
    }
  }

  [HttpGet("{houseId}")]
  public ActionResult<House> GetHouseById(int houseId)
  {
    try
    {
      House house = _housesService.GetHouseById(houseId);
      return Ok(house);
    }
    catch (Exception exception)
    {

      throw new Exception(exception.Message);
    }
  }
}