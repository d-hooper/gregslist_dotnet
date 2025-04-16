

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

  internal House UpdateHouse(int houseId, Account userInfo, House houseData)
  {
    House house = GetHouseById(houseId);

    if (house.CreatorId != userInfo.Id)
    {
      throw new Exception($"you cannot update houses created by other users {userInfo.Name}".ToUpper());
    }

    house.Sqft = houseData.Sqft ?? house.Sqft;
    house.Bedrooms = houseData.Bedrooms ?? house.Bedrooms;
    house.Bathrooms = houseData.Bathrooms ?? house.Bathrooms;
    house.Price = houseData.Price ?? house.Price;
    house.HasPool = houseData.HasPool ?? house.HasPool;
    house.Description = houseData.Description ?? house.Description;
    house.ImgUrl = houseData.ImgUrl ?? house.ImgUrl;

    _repository.UpdatedHouse(house);

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