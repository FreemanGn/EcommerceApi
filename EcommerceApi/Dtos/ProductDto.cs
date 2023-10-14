using System.Text.Json.Serialization;
using EcommerceApi.Models;
using EcommerceApi.Models.Enums;


namespace EcommerceApi.Dtos
{
	public class ProductDto
	{
		public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Sku { get; set; }
        public Size Size { get; set; }

        public int CategoryId { get; set; }
    }
	
}

