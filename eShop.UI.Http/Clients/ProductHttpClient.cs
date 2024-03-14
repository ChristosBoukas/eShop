using eShop.Data.Shared.Interfaces;
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

    public async Task PostDTO<TPostDTO>(TPostDTO inDTO) where TPostDTO : class
    {
        try
        {
            var typeName = typeof(TPostDTO).Name;
            var node = typeName.EndsWith("PostDTO") ? typeName.Substring(0, typeName.Length - 7) : typeName;
            node = node.ToLower();

            // Serialize the DTO object to JSON
            string jsonContent = JsonSerializer.Serialize(inDTO,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            // Create StringContent object with JSON data
            StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            //Use the relative path, not the base address here
            string relativePath = $"/api/{node}s";
            using HttpResponseMessage response = await _httpClient.PostAsync(relativePath, content);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {

        }
    }

    public async Task GetDTO<TGetDTO>(TGetDTO inDTO) where TGetDTO : class
    {
        try
        {
            var typeName = typeof(TGetDTO).Name;
            var node = typeName.EndsWith("GetDTO") ? typeName.Substring(0, typeName.Length - 6) : typeName;
            node = node.ToLower();

            //Use the relative path, not the base address here
            string relativePath = $"/api/{node}s";
            using HttpResponseMessage response = await _httpClient.GetAsync(relativePath);
            response.EnsureSuccessStatusCode();

            var resultStream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<List<ProductGetDTO>>(resultStream,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch (Exception ex)
        {

        }
    }

    public async Task PutDTO<TPutDTO>(TPutDTO inDTO, int idToUpdate) where TPutDTO : class
    {
        try
        {
            var typeName = typeof(TPutDTO).Name;
            var node = typeName.EndsWith("PutDTO") ? typeName.Substring(0, typeName.Length - 6) : typeName;
            node = node.ToLower();

            // Serialize the DTO object to JSON
            //string jsonContent = JsonSerializer.Serialize(inDTO);

            // Create StringContent object with JSON data
            StringContent content = new (JsonSerializer.Serialize(inDTO), Encoding.UTF8, "application/json");

            //Use the relative path, not the base address here
            string relativePath = $"{node}s/{idToUpdate}";
            using HttpResponseMessage response = await _httpClient.PutAsync(relativePath, content);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {

        }
    }
}
