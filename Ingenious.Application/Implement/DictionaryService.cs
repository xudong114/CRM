using AutoMapper;
using Ingenious.Application.Interface;
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using Ingenious.DTO;
using Ingenious.Infrastructure.Extensions;
using Ingenious.Repositories;
using Ingenious.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Implement
{
    public class DictionaryService : ApplicationService, IDictionaryService
    {
        private readonly IDictionaryRepository _IDictionaryRepository;
        private readonly IUserRepository _IUserRepository;

        public DictionaryService(IRepositoryContext context,
    IDictionaryRepository iDictionaryRepository,
            IUserRepository IUserRepository)
            : base(context)
        {
            this._IDictionaryRepository = iDictionaryRepository;
            this._IUserRepository = IUserRepository;
        }

        public DTO.DictionaryDTO GetByCode(string code)
        {
            return AutoMapper.Mapper.Map<Dictionary, DictionaryDTO>(this._IDictionaryRepository.GetByCode(code));
        }

        public DTO.DictionaryDTO GetByName(string name)
        {
            return AutoMapper.Mapper.Map<Dictionary, DictionaryDTO>(this._IDictionaryRepository.GetByName(name));
        }

        public DTO.DictionaryDTOList GetAll(string category = "")
        {
            var list = new DictionaryDTOList();
            ISpecification<Dictionary> spec = Specification<Dictionary>.Eval(d => true);
            spec = new AndSpecification<Dictionary>(spec, Specification<Dictionary>.Eval(item => item.Category.Contains(category)));

            this._IDictionaryRepository.GetAll(spec).ToList().ForEach(item =>
                list.Add(Mapper.Map<Dictionary, DictionaryDTO>(item)));

            this.AppendUserInfo(list, this._IUserRepository.Data);

            return list;
        }

        public DictionaryDTO GetByKey(Guid id)
        {
            return Mapper.Map<Dictionary, DictionaryDTO>(this._IDictionaryRepository.GetByKey(id));
        }

        public DictionaryDTO Create(DictionaryDTO dto)
        {
            return base.Create<DictionaryDTO, Dictionary>(dto
                , _IDictionaryRepository
                , dtoAction => { });
        }

        public List<DictionaryDTO> Update(List<DictionaryDTO> dtoList)
        {
            return base.Update<DictionaryDTO, List<DictionaryDTO>, Dictionary>(dtoList
                           , _IDictionaryRepository
                           , dto => dto.Id
                           , (dto, entity) =>
                           {

                               entity.Name = dto.Name;
                               entity.Code = dto.Code;
                               entity.Category = dto.Category;
                               entity.Description = dto.Description;

                               entity.ModifiedBy = dto.ModifiedBy;
                               entity.ModifiedDate = DateTime.Now;
                               entity.Version = dto.Version;
                           });
        }

        public void Delete(List<DictionaryDTO> dtoList)
        {
            base.Update<DictionaryDTO, List<DictionaryDTO>, Dictionary>(dtoList
             , _IDictionaryRepository
             , dto => dto.Id
             , (dto, entity) =>
             {
                 entity.IsActive = dto.IsActive;
                 entity.ModifiedDate = dto.ModifiedDate;
                 entity.ModifiedBy = dto.ModifiedBy;
             });
        }

        public List<string> GetCategories()
        {
            var list = new List<string>();
            var comparer = new Ingenious.Infrastructure.Extensions.EqualityComparer<Dictionary>(item => item.Category);
            this._IDictionaryRepository.Data.ToList().Distinct(comparer)
                .ToList()
                .ForEach(item => list.Add(item.Category));
            return list;
        }
    }

}
