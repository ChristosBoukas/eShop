using AutoMapper;
using eShop.API.DTO;
using eShop.UI.Http.Clients;

namespace eShop.UI.Admin.Services;

public class UIServiceAdmin
{
    private readonly CategoryHttpClient _categoryHttp;
    private readonly ProductHttpClient _productHttp;
    private readonly IMapper _mapper;

    public UIServiceAdmin(CategoryHttpClient categoryHttp, ProductHttpClient productHttp, IMapper mapper)
    {
        _categoryHttp = categoryHttp;
        _productHttp = productHttp;
        _mapper = mapper;
    }

    public ProductPostDTO? Product { get; set; }
    public ColorPostDTO? Color { get; set; }

    public async Task PostProduct(ProductPostDTO product)
    {
        await _productHttp.PostProduct(product);
    }

    public async Task PostColor(ColorPostDTO color)
    {
        await _productHttp.PostColor(color);
    }
}
