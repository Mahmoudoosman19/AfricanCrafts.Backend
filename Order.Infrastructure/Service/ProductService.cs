using Order.Application.Abstraction;
//using Order.Application.DTOs.CheckOut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Service
{
    //public class ProductService : IProductService
    //{
    //    private readonly HttpClient _httpClient;
    //    public ProductService(HttpClient httpClient)
    //    {
    //        _httpClient = httpClient;
    //    }

    //    public async Task<ProductIntegrationDto?> GetProductValidationDataAsync(Guid productId, Guid extensionId)
    //    {
    //        var response = await _httpClient.GetAsync($"api/products/{productId}/details?extensionId={extensionId}");

    //        if (!response.IsSuccessStatusCode)
    //            return null;

    //        return await response.Content.ReadFromJsonAsync<ProductIntegrationDto>();
    //    }
    //}
}
