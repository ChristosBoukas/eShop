namespace eShop.API.DTO;
public class SizePostDTO
{
    public string Name { get; set; } = string.Empty;
}
public class SizePutDTO : SizePostDTO
{
    public int Id { get; set; }
}
public class SizeGetDTO : SizePutDTO
{
}
