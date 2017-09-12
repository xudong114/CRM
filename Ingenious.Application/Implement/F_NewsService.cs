using AutoMapper;
using Ingenious.Application.Interface;
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using Ingenious.DTO;
using Ingenious.Infrastructure;
using Ingenious.Infrastructure.Enum;
using Ingenious.Infrastructure.Helper;
using Ingenious.Repositories;
using Ingenious.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ingenious.Application.Implement
{
    public class F_NewsService : ApplicationService, IF_NewsService 
    {
        private readonly IF_NewsRepository _IF_NewsRepository;
        public F_NewsService(IRepositoryContext context,
            IF_NewsRepository IF_NewsRepository)
            : base(context)
        {
            this._IF_NewsRepository = IF_NewsRepository;
        }

        public List<F_NewsDTO> GetAll(string code = "", string title = "")
        {
            ISpecification<F_News> spec = Specification<F_News>.Eval(item => true);
            spec = new AndSpecification<F_News>(spec,
                Specification<F_News>.Eval(item =>
                  code == "" || item.Code.Equals(code)));

            spec = new AndSpecification<F_News>(spec,
                Specification<F_News>.Eval(item =>
                  title == "" || item.Title.Contains(title)));

            spec = new AndSpecification<F_News>(spec,
                Specification<F_News>.Eval(item => item.IsActive));

            var list = new List<F_NewsDTO>();
            this._IF_NewsRepository.GetAll(spec).ToList().ForEach(item =>
                list.Add(Mapper.Map<F_News, F_NewsDTO>(item))
                );
            return list;
        }

        public DTO.F_NewsDTO GetByKey(System.Guid id)
        {
            var ad = this._IF_NewsRepository.GetByKey(id);

            var adDTO = Mapper.Map<F_News, F_NewsDTO>(ad);

            return adDTO;
        }

        public F_NewsDTO Create(F_NewsDTO dto)
        {
            var account = base.F_Create<F_NewsDTO, F_News>(dto
                , _IF_NewsRepository
                , dtoAction => { });
            return account;
        }

        public List<F_NewsDTO> Update(System.Collections.Generic.List<F_NewsDTO> dtoList)
        {
            return base.F_Update<F_NewsDTO, List<F_NewsDTO>, F_News>(dtoList
                , _IF_NewsRepository
                , dto => dto.Id
                , (dto, entity) =>
                {
                    entity.Title = dto.Title;
                    entity.Content = dto.Content;
                    entity.Link = dto.Link;
                    entity.Logo = dto.Logo;
                    entity.Source = dto.Source;
                    entity.Code = dto.Code;
                    entity.ModifiedDate = dto.ModifiedDate;
                    entity.ModifiedBy = dto.ModifiedBy;
                });
        }

        public void Delete(System.Collections.Generic.List<F_NewsDTO> dtoList)
        {
            base.F_Update<F_NewsDTO, List<F_NewsDTO>, F_News>(dtoList
                , _IF_NewsRepository
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
