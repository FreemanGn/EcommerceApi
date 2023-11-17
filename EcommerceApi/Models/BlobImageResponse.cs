using System;
namespace EcommerceApi.Models
{
	public class BlobImageResponse
	{
        public string? Status { get; set; }
        public bool Error { get; set; }
		public BlobImage BlobImage { get; set; }
        public BlobImageResponse()
		{
			BlobImage = new BlobImage();
		}
	}
}

