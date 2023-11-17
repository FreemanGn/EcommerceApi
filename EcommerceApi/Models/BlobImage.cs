using System;
namespace EcommerceApi.Models
{
	public class BlobImage
	{
        public string? Uri { get; set; }
        public string? Name { get; set; }
        public string? ContentType { get; set; }
        public Stream? Content { get; set; }
    }
}

