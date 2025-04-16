

namespace gregslist_dotnet.Repositories;

public class HousesRepository
{
  private readonly IDbConnection _db;

  public HousesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal List<House> GetHouses()
  {
    string sql = @"
    SELECT 
    houses.*,
    accounts.*
    FROM houses
    INNER JOIN accounts 
    ON accounts.id = houses.creator_id;";

    List<House> foundHouses = _db.Query(sql, (House house, Account account) =>
    {
      house.Creator = account;
      return house;
    }).ToList();
    return foundHouses;
  }

  internal House GetHouseById(int houseId)
  {
    string sql = @"
    SELECT 
    houses.*,
    accounts.*
    FROM houses
    INNER JOIN accounts ON accounts.id = houses.creator_id
    WHERE houses.id = @houseId;";

    House foundHouse = _db.Query(sql, (House house, Account account) =>
    {
      house.Creator = account;
      return house;
    }, new { houseId }).SingleOrDefault();
    return foundHouse;
  }


}