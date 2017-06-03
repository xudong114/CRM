using CRM.Models;
using Ingenious.Application.Interface;
using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Controllers
{
    public class ContractController : BaseController
    {
        private readonly IContractService _IContractService;
        private readonly IClientService _IClientService;
        private readonly IProductService _IProductService;
        private readonly IUserService _IUserService;
        private readonly IDepartmentService _IDepartmentService;
        private readonly IPriceStrategyService _IPriceStrategyService;
        private readonly IAccountService _IAccountService;
        private readonly IBillService _IBillService;
        public ContractController(IContractService iContractService,
            IClientService iClientService,
            IProductService iProductService,
            IUserService iUserService,
            IDepartmentService iDepartmentService,
            IPriceStrategyService iPriceStrategyService,
            IAccountService iAccountService,
            IBillService iBillService)
        {
            this._IContractService = iContractService;
            this._IClientService = iClientService;
            this._IProductService = iProductService;
            this._IUserService = iUserService;
            this._IDepartmentService = iDepartmentService;
            this._IPriceStrategyService = iPriceStrategyService;
            this._IAccountService = iAccountService;
            this._IBillService = iBillService;
        }


        // GET: Contract
        public ActionResult Index()
        {
            var list = this._IContractService.GetAll();
            return View(list);
        }

        public ActionResult Create()
        {
            var model = new ContractComposite();
            model.AccountDTO = this._IAccountService.GetByDepartmentId(this.User.BranchId);
            model.ClientDTOList = this._IClientService.GetAll();
            model.ProductDTOList = this._IProductService.GetAll();
            model.UserDTOList = this._IUserService.GetUsers(null, Ingenious.Infrastructure.Enum.UserStatusEnum.Available);
            model.PriceStrategyDTOList = this._IPriceStrategyService.GetAll();
            model.DepartmentDTOList = this._IDepartmentService.GetAll(true);
            this.DataBind();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ContractComposite contractComposite)
        {
            var contract = contractComposite.ContractDTO;
            var accountDTO = this._IAccountService.GetByDepartmentId(this.User.BranchId);
            if(ModelState.IsValid)
            {
                if(accountDTO.Balance<contract.TotalAmount)
                {
                    ModelState.AddModelError("overbalance", "余额不足，请充值！");
                    return View(contractComposite);
                }
                contract.ModifiedBy = contract.CreatedBy = this.User.Id;
                contract.CreatedDate = contract.ModifiedDate = DateTime.Now;
                contract.OwnerId = this.User.Id;
                contract.IsActive = true;
                contract.Version = 1;
                
                contract = this._IContractService.Create(contract);
  
                return RedirectToAction("Index");
            }

            contractComposite.AccountDTO = accountDTO;
            contractComposite.ClientDTOList = this._IClientService.GetAll();
            contractComposite.ProductDTOList = this._IProductService.GetAll();
            contractComposite.UserDTOList = this._IUserService.GetUsers(null, Ingenious.Infrastructure.Enum.UserStatusEnum.Available);
            contractComposite.PriceStrategyDTOList = this._IPriceStrategyService.GetAll();
            contractComposite.DepartmentDTOList = this._IDepartmentService.GetAll(true);
            this.DataBind();

            return View(contractComposite);
        }

        public ActionResult Edit(Guid id)
        {
            var model = new ContractComposite();
            model.ClientDTOList = this._IClientService.GetAll();
            model.ProductDTOList = this._IProductService.GetAll();
            model.UserDTOList = this._IUserService.GetUsers(null, Ingenious.Infrastructure.Enum.UserStatusEnum.Available);
            model.ContractDTO = this._IContractService.GetByKey(id);
            this.DataBind();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ContractComposite contractComposite)
        {
            var contract = contractComposite.ContractDTO;
            if (ModelState.IsValid)
            {
                contract.ModifiedBy = this.User.Id;
                this._IContractService.Update(new ContractDTOList { contract });
                return RedirectToAction("Index");
            }

            contractComposite.ClientDTOList = this._IClientService.GetAll();
            contractComposite.ProductDTOList = this._IProductService.GetAll();
            contractComposite.UserDTOList = this._IUserService.GetUsers(null, Ingenious.Infrastructure.Enum.UserStatusEnum.Available);

            this.DataBind();

            return View(contractComposite);
        }
        
        private void DataBind()
        {
            var departments = this._IDepartmentService.GetAll();
            Guid? partntId = null;
            if (!this.User.IsSupper)
            {
                partntId = this.User.DepartmentId;
            }
            ViewBag.HtmlString = this._IDepartmentService.GetChildren(partntId, departments);
        }

    }
}