using AutoMapper;
using eShop.UI.Http.Clients;

namespace eShop.UI.Admin.Services;

public class UIServiceAdmin(CategoryHttpClient categoryHttp, ProductHttpClient productHttp, IMapper mapper)
{
    public async Task PostProduct()
    {
        
    }
}
