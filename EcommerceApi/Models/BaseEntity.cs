﻿using System;
namespace EcommerceApi.Models
{
        public abstract class BaseEntity<Tkey>
        {
            public Tkey Id { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }

            public BaseEntity()
            {
                CreatedAt = DateTime.UtcNow;
                UpdatedAt = DateTime.UtcNow;
            }
        }
}

