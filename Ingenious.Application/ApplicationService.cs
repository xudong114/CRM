﻿
using AutoMapper;
using Ingenious.Domain;
using Ingenious.Domain.DataSource;
using Ingenious.Domain.Models;
using Ingenious.DTO;
using Ingenious.Infrastructure;
using Ingenious.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Ingenious.Application
{
    public class ApplicationService
    {
        public ApplicationService(IRepositoryContext context)
        {
            this.Context = context;
        }
        private IRepositoryContext Context { get; set; }

        public static void Initialize()
        {
            //初始化EF
            Ingenious.Repositories.EntityFramework.IngeniousDbContextInitializer.InitializeDatabase();

            #region Api.Go

            Mapper.CreateMap<F_User, F_UserDTO>();
            Mapper.CreateMap<F_UserDTO, F_User>();

            Mapper.CreateMap<F_UserDetail, F_UserDetailDTO>();
            Mapper.CreateMap<F_UserDetailDTO, F_UserDetail>();

            Mapper.CreateMap<F_Bank, F_BankDTO>();
            Mapper.CreateMap<F_BankDTO, F_Bank>();

            Mapper.CreateMap<F_Order, F_OrderDTO>();
            Mapper.CreateMap<F_OrderDTO, F_Order>();

            Mapper.CreateMap<ComplexOrder, F_OrderDTO>();
            Mapper.CreateMap<ComplexOrderRecord, F_OrderRecordDTO>();

            Mapper.CreateMap<F_OrderRecord, F_OrderRecordDTO>();
            Mapper.CreateMap<F_OrderRecordDTO, F_OrderRecord>();

            Mapper.CreateMap<F_File, F_FileDTO>();
            Mapper.CreateMap<F_FileDTO, F_File>();

            Mapper.CreateMap<F_Store, F_StoreDTO>();
            Mapper.CreateMap<F_StoreDTO, F_Store>();

            Mapper.CreateMap<F_Account, F_AccountDTO>();
            Mapper.CreateMap<F_AccountDTO, F_Account>();

            Mapper.CreateMap<F_WithdrawDepositRecord, F_WithdrawDepositRecordDTO>();
            Mapper.CreateMap<F_WithdrawDepositRecordDTO, F_WithdrawDepositRecord>();


            Mapper.CreateMap<F_Activity, F_ActivityDTO>();
            Mapper.CreateMap<F_ActivityDTO, F_Activity>();

            #endregion


            Mapper.CreateMap<User, UserDTO>();
            Mapper.CreateMap<UserDTO, User>();

            Mapper.CreateMap<UserDetail, UserDetailDTO>();
            Mapper.CreateMap<UserDetailDTO, UserDetail>();

            Mapper.CreateMap<Department, DepartmentDTO>();
            Mapper.CreateMap<DepartmentDTO, Department>();

            Mapper.CreateMap<Client, ClientDTO>();
            Mapper.CreateMap<ClientDTO, Client>();

            Mapper.CreateMap<Activity, ActivityDTO>();
            Mapper.CreateMap<ActivityDTO, Activity>();

            Mapper.CreateMap<Product, ProductDTO>();
            Mapper.CreateMap<ProductDTO, Product>();

            Mapper.CreateMap<PriceStrategy, PriceStrategyDTO>();
            Mapper.CreateMap<PriceStrategyDTO, PriceStrategy>();

            Mapper.CreateMap<PriceStrategyDetail, PriceStrategyDetailDTO>();
            Mapper.CreateMap<PriceStrategyDetailDTO, PriceStrategyDetail>();

            Mapper.CreateMap<Contract, ContractDTO>();
            Mapper.CreateMap<ContractDTO, Contract>();

            Mapper.CreateMap<Account, AccountDTO>();
            Mapper.CreateMap<AccountDTO, Account>();

            Mapper.CreateMap<Bill, BillDTO>();
            Mapper.CreateMap<BillDTO, Bill>();

            Mapper.CreateMap<Recharge, RechargeDTO>();
            Mapper.CreateMap<RechargeDTO, Recharge>();

            Mapper.CreateMap<Dictionary, DictionaryDTO>();
            Mapper.CreateMap<DictionaryDTO, Dictionary>();

            Mapper.CreateMap<ClientContact, ClientContactDTO>();
            Mapper.CreateMap<ClientContactDTO, ClientContact>();

        }


        public DTO Create<DTO, AggregateRoot>(DTO dtoModel
            , IRepository<AggregateRoot> repository
            , Action<DTO> processDTO = null
            , Action<AggregateRoot> processAggregateRoot = null)
            where DTO : ModelRoot
            where AggregateRoot : class , IAggregateRoot
        {
            if (dtoModel == null)
            {
                throw new ArgumentNullException("dtoModel is null.");
            }
            if (repository == null)
            {
                throw new ArgumentNullException("repository is null.");
            }

            if (processDTO != null)
            {
                processDTO(dtoModel);
            }
            
            dtoModel.Version = 1;
            dtoModel.IsActive = true;
            dtoModel.ModifiedDate = dtoModel.CreatedDate = DateTime.Now;

            var entity = Mapper.Map<DTO, AggregateRoot>(dtoModel);

            if (processAggregateRoot != null)
            {
                processAggregateRoot(entity);
            }
            
            repository.Add(entity);
            repository.Context.Commit();


            return Mapper.Map<AggregateRoot, DTO>(entity);
        }


        public DTOList Update<DTO, DTOList, TAggregateRoot>(DTOList dtoList
            , IRepository<TAggregateRoot> repository
            , Func<DTO, Guid> getIdFunc
            , Action<DTO, TAggregateRoot> updateAggregateRootAction
            )
            where DTO : ModelRoot
            where DTOList : List<DTO>, new()
            where TAggregateRoot : class ,IAggregateRoot
        {
            if (dtoList == null)
                throw new ArgumentNullException("dtoList is null.");
            if (repository == null)
                throw new ArgumentNullException("repository is null.");
            if (getIdFunc == null)
                throw new ArgumentNullException("getIdFunc is Guid.Empty.");
            if (updateAggregateRootAction == null)
                throw new ArgumentNullException("updateAggregateRootAction is null.");
            DTOList result = new DTOList();
            if (dtoList.Count > 0)
            {
                foreach (var dto in dtoList)
                {
                    var id = getIdFunc(dto);
                    var entity = repository.GetByKey(id);
                    //保存版本记录
                    //...
                    dto.Version = dto.Version + 1;
                    dto.ModifiedDate = DateTime.Now;

                    updateAggregateRootAction(dto, entity);
                    repository.Update(entity);
                    result.Add(Mapper.Map<TAggregateRoot, DTO>(entity));
                }
                repository.Context.Commit();
            }
            return result;
        }

        protected void Delete<AggregateRoot>(IdList ids,
            IRepository<AggregateRoot> repository,
            Action<Guid> preDeleteAction,
            Action<Guid> postDeleteAction)
            where AggregateRoot : class ,IAggregateRoot
        {
            if (ids == null)
                throw new ArgumentNullException("ids is null.");
            if (repository == null)
                throw new ArgumentNullException("repository is null.");
            foreach (var id in ids)
            {
                if (preDeleteAction != null)
                    preDeleteAction(id);
                var entity = repository.GetByKey(id);
                repository.Delete(entity);
                if (postDeleteAction != null)
                    postDeleteAction(id);
            }
            repository.Context.Commit();
        }

        protected void Delete<DTO, DTOList, AggregateRoot>(DTOList dtoList,
            IRepository<AggregateRoot> repository,
            Action<Guid> preDeleteAction,
            Action<Guid> postDeleteAction)
            where AggregateRoot : class ,IAggregateRoot
            where DTO : ModelRoot
            where DTOList : List<DTO>
        {
            if (dtoList == null)
                throw new ArgumentNullException("dtoList is null.");
            if (repository == null)
                throw new ArgumentNullException("repository is null.");
            foreach (var dto in dtoList)
            {
                if (preDeleteAction != null)
                    preDeleteAction(dto.Id);
                var entity = repository.GetByKey(dto.Id);
                repository.Delete(entity);
                if (postDeleteAction != null)
                    postDeleteAction(dto.Id);
            }
            repository.Context.Commit();
        }

        public void AppendUserInfo(IEnumerable<ModelRoot> dtoList, IQueryable<User> userList)
        {
            foreach(var item in dtoList)
            {
                item.CreatedByUser = AutoMapper.Mapper.Map<User, UserDTO>(userList.FirstOrDefault(i => i.Id == item.CreatedBy));
                item.ModifiedByUser = AutoMapper.Mapper.Map<User, UserDTO>(userList.FirstOrDefault(i => i.Id == item.ModifiedBy));
            }
        }

        public void AppendUserInfo(ModelRoot dto, IQueryable<User> userList)
        {
            dto.CreatedByUser = AutoMapper.Mapper.Map<User, UserDTO>(userList.FirstOrDefault(i => i.Id == dto.CreatedBy));
            dto.ModifiedByUser = AutoMapper.Mapper.Map<User, UserDTO>(userList.FirstOrDefault(i => i.Id == dto.ModifiedBy));
        }


        #region  Api.Go

        public DTO F_Create<DTO, AggregateRoot>(DTO dtoModel
    , IRepository<AggregateRoot> repository
    , Action<DTO> processDTO = null
    , Action<AggregateRoot> processAggregateRoot = null)
            where DTO : F_ModelRoot
            where AggregateRoot : class , IAggregateRoot
        {
            if (dtoModel == null)
            {
                throw new ArgumentNullException("dtoModel is null.");
            }
            if (repository == null)
            {
                throw new ArgumentNullException("repository is null.");
            }

            if (processDTO != null)
            {
                processDTO(dtoModel);
            }

            dtoModel.Version = 1;
            dtoModel.IsActive = true;
            dtoModel.ModifiedDate = dtoModel.CreatedDate = DateTime.Now;

            var entity = Mapper.Map<DTO, AggregateRoot>(dtoModel);

            if (processAggregateRoot != null)
            {
                processAggregateRoot(entity);
            }

            repository.Add(entity);
            repository.Context.Commit();


            return Mapper.Map<AggregateRoot, DTO>(entity);
        }


        public DTOList F_Update<DTO, DTOList, TAggregateRoot>(DTOList dtoList
            , IRepository<TAggregateRoot> repository
            , Func<DTO, Guid> getIdFunc
            , Action<DTO, TAggregateRoot> updateAggregateRootAction
            )
            where DTO : F_ModelRoot
            where DTOList : List<DTO>, new()
            where TAggregateRoot : class ,IAggregateRoot
        {
            if (dtoList == null)
                throw new ArgumentNullException("dtoList is null.");
            if (repository == null)
                throw new ArgumentNullException("repository is null.");
            if (getIdFunc == null)
                throw new ArgumentNullException("getIdFunc is Guid.Empty.");
            if (updateAggregateRootAction == null)
                throw new ArgumentNullException("updateAggregateRootAction is null.");
            DTOList result = new DTOList();
            if (dtoList.Count > 0)
            {
                foreach (var dto in dtoList)
                {
                    var id = getIdFunc(dto);
                    //System.Web.HttpContext.Current.Response.Write(id.ToString());
                    //System.Web.HttpContext.Current.Response.Flush();
                    //System.Web.HttpContext.Current.Response.End();
                    var entity = repository.GetByKey(id);

                    dto.Version = dto.Version + 1;
                    dto.ModifiedDate = DateTime.Now;

                    updateAggregateRootAction(dto, entity);
                    repository.Update(entity);
                    result.Add(Mapper.Map<TAggregateRoot, DTO>(entity));
                }
                repository.Context.Commit();
            }
            return result;
        }

        protected void F_Delete<AggregateRoot>(IdList ids,
            IRepository<AggregateRoot> repository,
            Action<Guid> preDeleteAction,
            Action<Guid> postDeleteAction)
            where AggregateRoot : class ,IAggregateRoot
        {
            if (ids == null)
                throw new ArgumentNullException("ids is null.");
            if (repository == null)
                throw new ArgumentNullException("repository is null.");
            foreach (var id in ids)
            {
                if (preDeleteAction != null)
                    preDeleteAction(id);
                var entity = repository.GetByKey(id);
                repository.Delete(entity);
                if (postDeleteAction != null)
                    postDeleteAction(id);
            }
            repository.Context.Commit();
        }

        protected void F_Delete<DTO, DTOList, AggregateRoot>(DTOList dtoList,
            IRepository<AggregateRoot> repository,
            Action<Guid> preDeleteAction,
            Action<Guid> postDeleteAction)
            where AggregateRoot : class ,IAggregateRoot
            where DTO : F_ModelRoot
            where DTOList : List<DTO>
        {
            if (dtoList == null)
                throw new ArgumentNullException("dtoList is null.");
            if (repository == null)
                throw new ArgumentNullException("repository is null.");
            foreach (var dto in dtoList)
            {
                if (preDeleteAction != null)
                    preDeleteAction(dto.Id);
                var entity = repository.GetByKey(dto.Id);
                repository.Delete(entity);
                if (postDeleteAction != null)
                    postDeleteAction(dto.Id);
            }
            repository.Context.Commit();
        }


        public void F_AppendUserInfo(IEnumerable<F_ModelRoot> dtoList, IQueryable<F_User> userList)
        {
            foreach (var item in dtoList)
            {
                item.CreatedByUser = AutoMapper.Mapper.Map<F_User, F_UserDTO>(userList.FirstOrDefault(i => i.Id == item.CreatedBy));
                item.ModifiedByUser = AutoMapper.Mapper.Map<F_User, F_UserDTO>(userList.FirstOrDefault(i => i.Id == item.ModifiedBy));
            }
        }

        public void F_AppendUserInfo(F_ModelRoot dto, IQueryable<F_User> userList)
        {
            dto.CreatedByUser = AutoMapper.Mapper.Map<F_User, F_UserDTO>(userList.FirstOrDefault(i => i.Id == dto.CreatedBy));
            dto.ModifiedByUser = AutoMapper.Mapper.Map<F_User, F_UserDTO>(userList.FirstOrDefault(i => i.Id == dto.ModifiedBy));
        }
        #endregion



    }
}
