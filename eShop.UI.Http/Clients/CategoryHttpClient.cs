namespace eShop.UI.Http.Clients;

public class CategoryHttpClient
{
    private readonly HttpClient _httpClient;
    string _baseAdress = "https://localhost:5000/api/";

    public CategoryHttpClient(HttpClient httpclient)
    {
        _httpClient = httpclient;
        _httpClient.BaseAddress = new Uri($"{_baseAdress}categorys");
    }

}
