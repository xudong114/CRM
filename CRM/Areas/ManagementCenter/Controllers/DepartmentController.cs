using CRM.Areas.ManagementCenter.Models;
using Ingenious.Application.Interface;
using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.ManagementCenter.Controllers
{
    public class DepartmentController : BaseController
    {

        private readonly IDepartmentService _IDepartmentService;
        private readonly IAccountService _IAccountService;
        private readonly IBillService _IBillService;
        private readonly IRechargeService _IRechargeService;
        private readonly IPriceStrategyService _IPriceStrategyService;
        
        public DepartmentController(
            IDepartmentService iDepartmentService,
            IAccountService iAccountService,
            IBillService iBillService,
            IRechargeService iRechargeService,
            IPriceStrategyService iPriceStrategyService)
        {
            this._IDepartmentService = iDepartmentService;
            this._IAccountService = iAccountService;
            this._IBillService = iBillService;
            this._IRechargeService = iRechargeService;
            this._IPriceStrategyService = iPriceStrategyService;
        }

        // GET: ManagementCenter/Department
        public ActionResult Index(string keywords = "")
        {
            var list = this._IDepartmentService.GetAll(null,keywords);
            return View(list);
        }

        public ActionResult Branch(string keywords = "")
        {
            var list = this._IDepartmentService.GetAll(true, keywords);
            return View(list);
        }

        public ActionResult Detail(Guid id)
        {
            var model = new DepartmentComposite();
            model.DepartmentDTO = this._IDepartmentService.GetByKey(id);
            if(model.DepartmentDTO.IsBranch)
            {
                model.AccountDTO = this._IAccountService.GetByDepartmentId(id);
                model.BillDTOList = this._IBillService.GetByAccountId(model.AccountDTO.Id);
                model.RechargeDTOList = this._IRechargeService.GetByAccountId(model.AccountDTO.Id);
                //model.PriceStrategyDTO = this._IPriceStrategyService.get
            }
            return View(model);
        }

        public ActionResult Create()
        {
            this.DataBind();
            return View();
        }

        [HttpPost]
        public ActionResult Create(DepartmentDTO dept)
        {
            if(ModelState.IsValid)
            {
                dept.CreatedBy = dept.ModifiedBy = this.User.Id;
                dept = this._IDepartmentService.Create(dept);
                var account = new AccountDTO();
                account.CreatedBy = account.ModifiedBy = this.User.Id;
                account.Balance = 0m;
                account.DepartmentId = dept.Id;
                this._IAccountService.Create(account);

                return RedirectToAction("Index");
            }
            this.DataBind();
            return View();
        }

        public ActionResult Edit(Guid id)
        {
            var model = this._IDepartmentService.GetByKey(id);
            this.DataBind();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(DepartmentDTO dept)
        {
            if (ModelState.IsValid)
            {
                dept.ModifiedBy = this.User.Id;
                this._IDepartmentService.Update(new DepartmentDTOList { dept });
                return RedirectToAction("Index");
            }
            this.DataBind();
            return RedirectToAction("Edit", dept.Id);
        }

        public string Recharge(decimal money,Guid accountId)
        {
            var account = this._IAccountService.GetByKey(accountId);
            account.Balance += money;
            var accountList = this._IAccountService.Update(new AccountDTOList { account });
            
            var recharge = new RechargeDTO();
            recharge.AccountId = accountId;
            recharge.CreatedBy = recharge.ModifiedBy = this.User.Id;
            recharge.Money = money;
            this._IRechargeService.Create(recharge);
            return (accountList.Count > 0 ? "充值成功！" : "充值失败！");
        }

        public void Disable(Guid[] ids, bool status = false)
        {
            var dtoList = new List<DepartmentDTO>();
            ids.ToList().ForEach(item =>
            {
                dtoList.Add(new DepartmentDTO { Id = item, IsActive = status,ModifiedBy=this.User.Id });
            });
            this._IDepartmentService.Delete(dtoList);
        }

        public void Resume(Guid id)
        {
            List<DepartmentDTO> dtoList = new List<DepartmentDTO> { new DepartmentDTO { Id = id, IsActive = true, ModifiedBy = this.User.Id } };
            this._IDepartmentService.Delete(dtoList);
        }

        private void DataBind()
        {
            ViewBag.Departments = new SelectList(
            this._IDepartmentService.GetAll(), "Id", "Name");

            ViewBag.PriceStrategy = new SelectList(
            this._IPriceStrategyService.GetAll(), "Id", "Name");

        }
    }
}