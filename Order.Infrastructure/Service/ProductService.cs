using Common.Domain.Shared;
using Order.Application.Abstraction;
using Order.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Order.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductIntegrationDto>> GetProductsBulkAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken = default)
        {
            var query = string.Join("&ids=", productIds);
            var response = await _httpClient.GetAsync($"api/product/get-products-bulk?ids={query}", cancellationToken);

            if (!response.IsSuccessStatusCode) return new List<ProductIntegrationDto>();

            var jsonString = await response.Content.ReadAsStringAsync();
            using var document = JsonDocument.Parse(jsonString);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            if (document.RootElement.ValueKind == JsonValueKind.Object && document.RootElement.TryGetProperty("data", out var dataElement))
            {
                return JsonSerializer.Deserialize<List<ProductIntegrationDto>>(dataElement.GetRawText(), options) ?? new List<ProductIntegrationDto>();
            }

            return new List<ProductIntegrationDto>();
        }
    }
}
