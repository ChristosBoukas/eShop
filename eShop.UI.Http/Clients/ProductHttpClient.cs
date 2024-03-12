using System.Text.Json;
using System.Xml.Linq;

namespace eShop.UI.Http.Clients;

public class ProductHttpClient
{
    private readonly HttpClient _httpClient;
    string _baseAdress = "https://localhost:5500/api/";

    public ProductHttpClient(HttpClient httpclient)
    {
        _httpClient = httpclient;
        _httpClient.BaseAddress = new Uri($"{_baseAdress}products");
    }

    public async Task<List<ProductGetDTO>> GetProductsAsync(int categoryId)
    {
        try
        {
            // Use the relative path, not the base address here
            string relativePath = $"productsbycategory/{categoryId}";
            using HttpResponseMessage response = await _httpClient.GetAsync(relativePath);
            response.EnsureSuccessStatusCode();

            var resultStream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<List<ProductGetDTO>>(resultStream,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result ?? [];
        }
        catch (Exception ex)
        {
            return [];
        }
    }

    public async Task<List<ProductGetDTO>> PostProduct()
    {
        try
        {
            // Use the relative path, not the base address here
            string relativePath = $"/api/products";
            using HttpResponseMessage response = await _httpClient.GetAsync(relativePath);
            response.EnsureSuccessStatusCode();

            var resultStream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<List<ProductGetDTO>>(resultStream,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result ?? [];
        }
        catch (Exception ex)
        {
            return [];
        }
    }

}
