<<<<<<< HEAD
using System;
namespace EcommerceApi.Models
{
	public class Category : BaseEntity<int>
	{
		public string Name { get; set; }
		public string Image { get; set; }
		public string Description { get; set; }

		public List<Product> Products { get; set; }
=======
﻿using System;
namespace EcommerceApi.Models
{
	public class Category
	{
		public int Id { get; set; }
		public string Name { get; set; }

>>>>>>> eed7efd (global config (#1))
	}
}

