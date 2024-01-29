namespace eShop.API.DTO;
public class ColorPostDTO
{
    public string Name { get; set; } = string.Empty;
}
public class ColorPutDTO : ProductPostDTO
{
    public int Id { get; set; }
}
public class ColorGetDTO : ProductPutDTO
{
    //public List<FilterGetDTO>? Filters { get; set; }
}
