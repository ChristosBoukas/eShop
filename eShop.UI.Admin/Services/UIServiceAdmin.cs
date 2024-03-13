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
    public ProductPutDTO? ProductPut { get; set; }
    public ColorPostDTO? Color { get; set; }
    public SizePostDTO? Size { get; set; }

    public async Task PostDTO<TPostDTO>(TPostDTO inDTO) where TPostDTO : class
    {
        await _productHttp.PostDTO(inDTO);
    }

    public async Task PutDTO<TPutDTO>(TPutDTO inDTO, int idToUpdate) where TPutDTO : class
    {
        await _productHttp.PutDTO(inDTO, idToUpdate);
    }
}
