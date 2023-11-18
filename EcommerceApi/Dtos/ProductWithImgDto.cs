using System;
namespace EcommerceApi.Dtos
{
	public class ProductWithImgDto
	{
        public IFormFile file { get; set; }
        public ProductDto ProductDto { get; set; }
		
	}
}

