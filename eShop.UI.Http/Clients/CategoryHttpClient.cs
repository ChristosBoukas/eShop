using System.Text.Json;

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

    public async Task<List<CategoryGetDTO>> GetCategoriesAsync()
    {
        try
        {
            using HttpResponseMessage response = await _httpClient.GetAsync("");
            response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<List<CategoryGetDTO>>(await response.Content.ReadAsStreamAsync(),
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result ?? [];

        }
        catch (Exception)
        {
            return [];
        }
    }
}
