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
    public class G_NewsService : ApplicationService, IG_NewsService 
    {
        private readonly IG_NewsRepository _IG_NewsRepository;
        public G_NewsService(IRepositoryContext context,
            IG_NewsRepository IG_NewsRepository)
            : base(context)
        {
            this._IG_NewsRepository = IG_NewsRepository;
        }

        public List<G_NewsDTO> GetAll(string code = "", string title = "")
        {
            ISpecification<G_News> spec = Specification<G_News>.Eval(item => true);
            spec = new AndSpecification<G_News>(spec,
                Specification<G_News>.Eval(item =>
                  code == "" || item.Code.Equals(code)));

            spec = new AndSpecification<G_News>(spec,
                Specification<G_News>.Eval(item =>
                  title == "" || item.Title.Contains(title)));

            spec = new AndSpecification<G_News>(spec,
                Specification<G_News>.Eval(item => item.IsActive));

            var list = new List<G_NewsDTO>();
            this._IG_NewsRepository.GetAll(spec).ToList().ForEach(item =>
                list.Add(Mapper.Map<G_News, G_NewsDTO>(item))
                );
            return list;
        }

        public DTO.G_NewsDTO GetByKey(System.Guid id)
        {
            var ad = this._IG_NewsRepository.GetByKey(id);

            var adDTO = Mapper.Map<G_News, G_NewsDTO>(ad);

            return adDTO;
        }

        public G_NewsDTO Create(G_NewsDTO dto)
        {
            var account = base.F_Create<G_NewsDTO, G_News>(dto
                , _IG_NewsRepository
                , dtoAction => { });
            return account;
        }

        public List<G_NewsDTO> Update(System.Collections.Generic.List<G_NewsDTO> dtoList)
        {
            return base.F_Update<G_NewsDTO, List<G_NewsDTO>, G_News>(dtoList
                , _IG_NewsRepository
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

        public void Delete(System.Collections.Generic.List<G_NewsDTO> dtoList)
        {
            base.F_Update<G_NewsDTO, List<G_NewsDTO>, G_News>(dtoList
                , _IG_NewsRepository
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
