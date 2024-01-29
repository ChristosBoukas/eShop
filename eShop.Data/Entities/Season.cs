namespace eShop.Data.Entities;

public class Season : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public OptionType OptionType { get; set; }
    public List<Product>? Products { get; set; }
}
