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
    public class AccountService : ApplicationService, IAccountService
    {
        private readonly IAccountRepository _IAccountRepository;
        public AccountService(IRepositoryContext context,
    IAccountRepository iAccountRepository)
            : base(context)
        {
            this._IAccountRepository = iAccountRepository;
        }


        public DTO.AccountDTOList GetAll()
        {
            var Accounts = new AccountDTOList();
            ISpecification<Account> spec = Specification<Account>.Eval(item => true);

            this._IAccountRepository.GetAll(spec).ToList().ForEach(item =>
                Accounts.Add(AutoMapper.Mapper.Map<Account, AccountDTO>(item))
                );
            return Accounts;
        }

        public DTO.AccountDTO GetByKey(Guid id)
        {
            var model = this._IAccountRepository.GetByKey(id);
            return AutoMapper.Mapper.Map<Account, AccountDTO>(model);
        }

        public DTO.AccountDTO GetByDepartmentId(Guid id)
        {
            var model = this._IAccountRepository.GetByDepartmentId(id);

            return AutoMapper.Mapper.Map<Account, AccountDTO>(model);
        }

        public DTO.AccountDTO Create(DTO.AccountDTO dto)
        {

            var model = base.Create<AccountDTO, Account>(dto
                    , _IAccountRepository
                    , dtoAction => {});

            return model;
        }

        public List<DTO.AccountDTO> Update(List<DTO.AccountDTO> dtoList)
        {
            return base.Update<AccountDTO, List<AccountDTO>, Account>(dtoList
                    , _IAccountRepository
                    , dto => dto.Id
                    , (dto, entity) =>
                    {
                        entity.Balance = dto.Balance;
                        entity.ModifiedBy = dto.ModifiedBy;
                    });
        }

        public void Delete(List<DTO.AccountDTO> dtoList)
        {
            base.Update<AccountDTO, List<AccountDTO>, Account>(dtoList
                , _IAccountRepository
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
