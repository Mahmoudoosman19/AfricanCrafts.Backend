using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Models
{
    public class ProductDetailsResponseWrapper
    {
        public int TotalCount { get; set; }
        public ProductIntegrationDto Data { get; set; } = null!;
    }
    public class ProductIntegrationDto
    {
        public Guid Id { get; set; } 
        public string NameAr { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public List<ExtensionIntegrationResponse> Extensions { get; set; } = new();
    }

    public class ExtensionIntegrationResponse
    {
        public Guid Id { get; set; }
        public string ColorCode { get; set; } = string.Empty;
        public decimal Fees { get; set; }
        public int Amount { get; set; }
        public SizeIntegrationResponse? Size { get; set; }
    }

    public class SizeIntegrationResponse
    {
        public string NameAr { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;
    }
}
