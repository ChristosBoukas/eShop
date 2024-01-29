namespace eShop.API.DTO;

public class SeasonPostDTO
{
    public string Name { get; set; } = string.Empty;
}
public class SeasonPutDTO : SeasonPostDTO
{
    public int Id { get; set; }
}
public class SeasonGetDTO : SeasonPutDTO
{
    //public List<FilterGetDTO>? Filters { get; set; }
}