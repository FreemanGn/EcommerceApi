using System;
using AutoMapper;
using EcommerceApi.Dtos;
using EcommerceApi.Models;
using EcommerceApi.Repository.Interface;
using EcommerceApi.Services.Interface;
using Microsoft.Identity.Client.Extensions.Msal;

namespace EcommerceApi.Services
{
	public class ProductService : IProductService
	{
        private readonly IProductRepository _productRepository;
        private readonly IAzureStorage _azureStorage;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository,IAzureStorage azureStorage,IMapper mapper)
		{
            _productRepository = productRepository;
            _mapper = mapper;
            _azureStorage = azureStorage;
        }

        public async Task<ProductRequetDto> AddAsync(ProductRequetDto productRequetDto,IFormFile file)
        {
            var product = _mapper.Map<Product>(productRequetDto);

            BlobImageResponse? response = await _azureStorage.UploadAsync(file);

            if (response.Error != true)
            {
                product.ImageUrl = response.BlobImage.Uri;
                product.ImageName = response.BlobImage.Name;
                await _productRepository.AddAsync(product);
            }
            else
            {
                await _azureStorage.DeleteAsync(product.ImageName);
            }
            //Je dois modifier celà et renvoyer une erreur ou un succès
            return _mapper.Map<ProductRequetDto>(product);

        }

        public async Task<IEnumerable<ProductResponseDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();

            var productDtos = _mapper.Map<IEnumerable<ProductResponseDto>>(products);

            return  productDtos;
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            var productDto = _mapper.Map<ProductDto>(product);

            return productDto;
        }

        public async Task RemoveAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            BlobImageResponse? response = await _azureStorage.DeleteAsync(product.ImageName);

            if(response.Error != true)
            {
                await _productRepository.RemoveAsync(product);
            }
            //Je dois implementer le else en renvoyant une erreur 
        }

        public Task<bool> SkuExistsAsync(string sku)
        {
            return _productRepository.SkuExistsAsync(sku);
        }

        public async Task UpdateAsync(ProductDto productDto)
        {
            var product = await _productRepository.GetByIdAsync(productDto.Id);

            _mapper.Map(productDto, product);

            await _productRepository.UpdateAsync(product);
        }
    }
}

