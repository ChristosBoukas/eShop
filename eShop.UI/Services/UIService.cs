namespace eShop.UI.Services;

public class UIService(CategoryHttpClient categoryHttp, IMapper mapper)
{
    List<CategoryGetDTO> Categories { get; set; } = [];

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

}
