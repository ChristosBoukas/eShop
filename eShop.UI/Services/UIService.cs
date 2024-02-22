using eShop.UI.Storage.Services;

namespace eShop.UI.Services;

public class UIService(CategoryHttpClient categoryHttp, ProductHttpClient productHttp, IMapper mapper, IStorageService storage)
{
    List<CategoryGetDTO> Categories { get; set; } = [];
    public List<ProductGetDTO> Products { get; private set; } = [];
    public List<ProductGetDTO> CartItems { get; set; } = [];
    public List<LinkGroup> CategoryLinkGroups { get; private set; } =
    [
        new LinkGroup
        {
            Name = "Categories"
            //,
            //LinkOptions = new(){
            //    new LinkOption { Id = 1, Name = "Women", IsSelected = true },
            //    new LinkOption { Id = 2, Name = "Men", IsSelected = false },
            //    new LinkOption { Id = 3, Name = "Children", IsSelected = false }
            //}
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

    public async Task<T> ReadStorage<T>(string key)// where T : class
    {
        //if (string.IsNullOrEmpty(key) || storage is not null) return new T();
        return await storage.GetAsync<T>(key);
    }

    public async Task<T> ReadSingleStorage<T>(string key)// where T : class
    {
        return await storage.GetAsync<T>(key);
    }

    public async Task SaveToStorage<T>(string key, T value)// where T : class
    {
        if (string.IsNullOrEmpty(key) || storage is not null) return;
        await storage.SetAsync<T>(key, value);
    }

    public async Task RemoveFromStorage(string key)// where T : class
    {
        if (string.IsNullOrEmpty(key) || storage is not null) return;
        await storage.RemoveAsync(key);
    }

}
