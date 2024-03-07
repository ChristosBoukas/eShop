namespace eShop.UI.Services;

public class UIService(CategoryHttpClient categoryHttp, ProductHttpClient productHttp, IMapper mapper, CartService cart) //IStorageService storage
{
    public CartService Cart { get; } = cart;
    List<CategoryGetDTO> Categories { get; set; } = [];
    public List<ProductGetDTO> Products { get; private set; } = [];
    public List<LinkGroup> CategoryLinkGroups { get; private set; } =
    [
        new LinkGroup
        {
            Name = "Categories"
        }
    ];

    public int CurrentCategoryId { get; set; }

    public async Task GetLinkGroup()
    {
        Categories = await categoryHttp.GetCategoriesAsync();
        CategoryLinkGroups[0].LinkOptions = mapper.Map<List<LinkOption>>(Categories);
        var linkOption = CategoryLinkGroups[0].LinkOptions.FirstOrDefault();
        
        linkOption!.IsSelected = true;
    }

    public async Task OnCategoryLinkClick(int id)
    {
        CurrentCategoryId = id;
        await GetProductsAsync();
        //Products.ForEach(p => p.Colors!.First().IsSelected = true);

        CategoryLinkGroups[0].LinkOptions.ForEach(l => l.IsSelected = false);
        CategoryLinkGroups[0].LinkOptions.Single(l => l.Id.Equals(CurrentCategoryId)).IsSelected = true;
    }

    public async Task GetProductsAsync() =>
        Products = await productHttp.GetProductsAsync(CurrentCategoryId);
}
