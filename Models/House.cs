using System.ComponentModel.DataAnnotations;

namespace gregslist_dotnet.Models;

public class House
{
  public int Id { get; set; }
  [Range(50, 100000)] public int? Sqft { get; set; }
  public byte? Bedrooms { get; set; }
  public double? Bathrooms { get; set; }
  [Url, MaxLength(200)] public string ImgUrl { get; set; }
  [MaxLength(200)] public string Description { get; set; }
  [Range(0, 1000000000)] public int? Price { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public bool? HasPool { get; set; }
  public string CreatorId { get; set; }
  public Account Creator { get; set; }
}