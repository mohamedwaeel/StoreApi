using AutoMapper;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using Store.Service.Services.ProductServices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductEntity = Store.Data.Entities.Product;

namespace Store.Service.Services.ProductServices
{
    public class ProductServices:IProductServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductServices(IUnitOfWork unitOfWork,IMapper mapper) {
            _unitOfWork=unitOfWork;
            _mapper=mapper;
        }

        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.Repository<ProductBrand, int>().GetAllAsNoTrackingAsync();
           var mappedBrands=_mapper.Map < IReadOnlyList < BrandTypeDetailsDto >>(brands);
            return mappedBrands;
        }

        public async Task<IReadOnlyList<ProductDetailsDto>> GetAllProductsAsync()
        {
            var products=await _unitOfWork.Repository<ProductEntity, int>().GetAllAsNoTrackingAsync();
          var mappedProducts=_mapper.Map < IReadOnlyList < ProductDetailsDto >>(products);
            return mappedProducts;
        }

        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.Repository<ProductType, int>().GetAllAsNoTrackingAsync();
            var mappedTypes = _mapper.Map<IReadOnlyList<BrandTypeDetailsDto>>(types);
            return mappedTypes;
        }

        public async Task<ProductDetailsDto> GetProductByIdAsynx(int? productId)
        {if (productId is null)
                throw new Exception("Id is null");
            var product = await _unitOfWork.Repository<ProductEntity, int>().GetByIdAsync(productId.Value);
            if (product is null)
                throw new Exception("prroduct is null");
           var mappedProduct= _mapper.Map<ProductDetailsDto>(product);  
            return mappedProduct;

             
        }
    }
}
