using Ingenious.Application.Interface;
using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.GoApp.Controllers
{
    public class BankController : BaseController
    {
        public BankController() { }

        private readonly IF_BankOptionService _IF_BankOptionService;
        private readonly IF_BankService _IF_BankService;
        private readonly IF_UserService _IF_UserService;
        private readonly IF_UserDetailService _IF_UserDetailService;
        public BankController(IF_BankOptionService iF_BankOptionService,
            IF_BankService iF_BankService, 
            IF_UserService iF_UserService,
            IF_UserDetailService iF_UserDetailService)
        {
            this._IF_BankOptionService = iF_BankOptionService;
            this._IF_BankService = iF_BankService;
            this._IF_UserService = iF_UserService;
            this._IF_UserDetailService = iF_UserDetailService;
        }


        // GET: GoApp/Bank
        public ActionResult Index()
        {
            var banks = this._IF_BankService.GetBanks();
            return View(banks);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(F_BankDTO bank)
        {
            if (ModelState.IsValid)
            {
                bank.CreatedBy = bank.ModifiedBy = this.User.Id;
                this._IF_BankService.Create(bank);
            }
            return RedirectToAction("index");
        }

        public ActionResult EditBankOption(Guid id)
        {
            var bankOption = this._IF_BankOptionService.GetBankOptionByBankId(id) ?? new F_BankOptionDTO { F_BankId = id };
            return View(bankOption);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditBankOption(F_BankOptionDTO bankOption)
        {
            if (bankOption.Id == Guid.Empty)
            {
                this._IF_BankOptionService.Create(bankOption);
            }
            else
            {
                this._IF_BankOptionService.Update(new List<F_BankOptionDTO> { bankOption });
            }
            return RedirectToAction("index");
        }

        public ActionResult Edit(Guid id)
        {
            var model = this._IF_BankService.GetByKey(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(F_BankDTO bank)
        {
            if (ModelState.IsValid)
            {
                bank.ModifiedBy = this.User.Id;
                this._IF_BankService.Update(new List<F_BankDTO> { bank });
            }
            return RedirectToAction("index");
        }

        public ActionResult Delete(string[] ids)
        {
            var list = new List<F_BankDTO>();
            foreach (var item in ids)
            {
                list.Add(new F_BankDTO { Id = Guid.Parse(item) });
            }
            this._IF_BankService.Delete(list);
            return RedirectToAction("index");
        }

        public ActionResult BankUser()
        {
            var list = this._IF_UserService.GetBankUser();
            return View(list);
        }

    }
}
