using Ingenious.Application.Interface;
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using Ingenious.DTO;
using Ingenious.Repositories;
using Ingenious.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Implement
{
    public class ProductService : ApplicationService, IProductService
    {

        private readonly IProductRepository _IProductRepository;

        public ProductService(IRepositoryContext context,
            IProductRepository iProductRepository)
            : base(context)
        {
            this._IProductRepository = iProductRepository;
        }

        public DTO.ProductDTOList GetAll(string keywords = "")
        {
            var products = new ProductDTOList();
            ISpecification<Product> spec = Specification<Product>.Eval(item => true);
            spec = new AndSpecification<Product>(spec,
                Specification<Product>.Eval(item =>
                    keywords == "" ||
                    item.Name.Contains(keywords)));

            this._IProductRepository.GetAll(spec).ToList().ForEach(item =>
                products.Add(AutoMapper.Mapper.Map<Product, ProductDTO>(item))
                );
            return products;
        }

        public DTO.ProductDTO GetByKey(Guid id)
        {
            var model = this._IProductRepository.GetByKey(id);

            return AutoMapper.Mapper.Map<Product, ProductDTO>(model);
        }

        public DTO.ProductDTO Create(DTO.ProductDTO dto)
        {
            return base.Create<ProductDTO, Product>(dto
                    , _IProductRepository
                    , dtoAction => {});
        }

        public List<DTO.ProductDTO> Update(List<DTO.ProductDTO> dtoList)
        {
            return base.Update<ProductDTO, List<ProductDTO>, Product>(dtoList
                    , _IProductRepository
                    , dto => dto.Id
                    , (dto, entity) =>
                    {
                        entity.IsSettlement = dto.IsSettlement;
                        entity.Code = dto.Code;
                        entity.Comment = dto.Comment;
                        entity.Name = dto.Name;
                        entity.ModifiedBy = dto.ModifiedBy;
                        entity.ModifiedDate = dto.ModifiedDate;
                    });
        }

        public void Delete(List<DTO.ProductDTO> dtoList)
        {
            base.Update<ProductDTO, List<ProductDTO>, Product>(dtoList
                , _IProductRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.IsActive = dto.IsActive;
                    entity.ModifiedDate = dto.ModifiedDate;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
        }
    }
}
