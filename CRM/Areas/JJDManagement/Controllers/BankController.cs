using Ingenious.Application.Interface;
using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.JJDManagement.Controllers
{
    public class BankController : BaseController
    {
        public BankController() { }

        private readonly IG_BankService _IG_BankService;
        private readonly IG_UserService _IG_UserService;
        private readonly IG_UserDetailService _IG_UserDetailService;
        public BankController(
            IG_BankService IG_BankService,
            IG_UserService IG_UserService,
            IG_UserDetailService IG_UserDetailService)
        {
            this._IG_BankService = IG_BankService;
            this._IG_UserService = IG_UserService;
            this._IG_UserDetailService = IG_UserDetailService;
        }

        // GET: GoApp/Bank
        public ActionResult Index()
        {
            var banks = this._IG_BankService.GetBanks();
            return View(banks);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(G_BankDTO bank)
        {
            if (ModelState.IsValid)
            {
                bank.CreatedBy = bank.ModifiedBy = this.User.Id;
                this._IG_BankService.Create(bank);
            }
            return RedirectToAction("index");
        }

        public ActionResult Edit(Guid id)
        {
            var model = this._IG_BankService.GetByKey(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(G_BankDTO bank)
        {
            if (ModelState.IsValid)
            {
                bank.ModifiedBy = this.User.Id;
                this._IG_BankService.Update(new List<G_BankDTO> { bank });
            }
            return RedirectToAction("index");
        }

        public ActionResult Delete(string[] ids)
        {
            var list = new List<G_BankDTO>();
            foreach (var item in ids)
            {
                list.Add(new G_BankDTO { Id = Guid.Parse(item) });
            }
            this._IG_BankService.Delete(list);
            return RedirectToAction("index");
        }

        public ActionResult BankUser()
        {
            var list = this._IG_UserService.GetBankUser();
            return View(list);
        }

    }
}