

namespace gregslist_dotnet.Services;

public class HousesService
{
  private readonly HousesRepository _repository;

  public HousesService(HousesRepository repository)
  {
    _repository = repository;
  }


  internal List<House> GetHouses()
  {
    List<House> houses = _repository.GetHouses();
    return houses;
  }

  internal House GetHouseById(int houseId)
  {

    House house = _repository.GetHouseById(houseId);

    if (house == null)
    {
      throw new Exception($"Invalid house id({houseId})");
    }
    return house;
  }

  internal House CreateHouse(House houseData)
  {
    House house = _repository.CreateHouse(houseData);
    return house;
  }

  internal string DeleteHouse(int houseId, Account userInfo)
  {

    House house = GetHouseById(houseId);

    if (house.CreatorId != userInfo.Id)
    {
      throw new Exception($"you cannot delete houses created by other users {userInfo.Name}".ToUpper());
    }

    _repository.DeleteHouse(houseId);

    return "Your house listing has been deleted.";
  }
}