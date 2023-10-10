using System;
using System.ComponentModel.DataAnnotations.Schema;
using EcommerceApi.Models.Enums;

namespace EcommerceApi.Models
{
	public class Product : BaseEntity<int>
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public string Image { get; set; }
		public string Sku { get; set; }
		public Size Size { get; set; }

		[ForeignKey(nameof(Category))]
		public int CategoryId { get; set; }
		public Category Category { get; set; }
	}
}

