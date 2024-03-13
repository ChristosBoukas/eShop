using System.Text;
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

    public async Task PostProduct(ProductPostDTO product)
    {
        try
        {
            // Serialize the DTO object to JSON
            string jsonContent = JsonSerializer.Serialize(product);

            // Create StringContent object with JSON data
            StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            //Use the relative path, not the base address here
            string relativePath = $"/api/products";
            using HttpResponseMessage response = await _httpClient.PostAsync(relativePath, content);
            response.EnsureSuccessStatusCode();


        }
        catch (Exception ex)
        {
            
        }
    }

    public async Task PostColor(ColorPostDTO color)
    {
        try
        {
            // Serialize the DTO object to JSON
            string jsonContent = JsonSerializer.Serialize(color);

            // Create StringContent object with JSON data
            StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            //Use the relative path, not the base address here
            string relativePath = $"/api/colors";
            using HttpResponseMessage response = await _httpClient.PostAsync(relativePath, content);
            response.EnsureSuccessStatusCode();


        }
        catch (Exception ex)
        {

        }
    }

}
