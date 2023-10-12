﻿namespace EcommerceApi.Dtos
{
	public class CategoryDto
	{
        public int Id { get; set; }
        public string Name { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

