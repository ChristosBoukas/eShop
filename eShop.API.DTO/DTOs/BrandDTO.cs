namespace eShop.API.DTO;

public class BrandPostDTO
{
    public string Name { get; set; } = string.Empty;
}
public class BrandPutDTO : BrandPostDTO
{
    public int Id { get; set; }
}
public class BrandGetDTO : BrandPutDTO
{
    //public List<FilterGetDTO>? Filters { get; set; }
}