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
    public class ClientController : BaseController
    {
        private readonly IClientService _IClientService;
        private readonly IDepartmentService _IDepartmentService;
        private readonly IActivityCategoryService _ActivityCategoryService;
        private readonly IActivityService _ActivityService;
        private readonly IContractService _ContractService;
        public ClientController(IClientService iClientService,
            IDepartmentService iDepartmentService,
            IActivityCategoryService activityCategoryService,
            IActivityService activityService,
            IContractService contractService)
        {
            this._IClientService = iClientService;
            this._IDepartmentService = iDepartmentService;
            this._ActivityCategoryService = activityCategoryService;
            this._ActivityService = activityService;
            this._ContractService = contractService;
        }

        // GET: Custome
        public ActionResult Index(string keywords="")
        {
            var model = new ClientComposite();
            model.ClientDTOList = this._IClientService.GetAll(keywords);
            return View(model);
        }

        public ActionResult Create()
        {
            this.DataBind();
            return View();
        }

        [HttpPost]
        public ActionResult Create(ClientDTO client)
        {
            if (ModelState.IsValid)
            {
                client.CreatedBy = client.ModifiedBy = this.User.Id;
                this._IClientService.Create(client);
                return RedirectToAction("Index");
            }
            this.DataBind();
            return View(client);
        }

        public ActionResult Detail(Guid id)
        {
            var dept = this._IClientService.GetByKey(id);
            dept.ContractDTOList = this._ContractService.GetAll();
            var activityCategories = this._ActivityCategoryService.GetAll();
            ViewBag.ActivityCategories = new SelectList(activityCategories, "Id", "Name");
            var activies = this._ActivityService.GetAll(dept.Id, this.User.Id);
            var model = new ClientComposite {
                Client = dept,
                ActivityDTOList = activies
            };
            return View(model);
        }

        public void Edit(Guid id,string filedName,string newValue)
        {
            var client = this._IClientService.GetByKey(id);
            switch(filedName.ToLower())
            {
                case "name":
                    {
                        client.Name = newValue;
                    }
                    break;
            }

            client.ModifiedBy = this.User.Id;

            this._IClientService.Update(new ClientDTOList { client });
        }

        [HttpPost]
        public ActionResult CreateActivity(Guid clientId,Guid cateId,string content)
        {
            var model = new ActivityDTO();
            model.ActivityCategoryId = cateId;
            model.Content = content;
            model.CreatedBy = model.ModifiedBy = this.User.Id;
            model.ReferenceId = clientId;
            model.UserId = this.User.Id;
            var activityDTO = this._ActivityService.Create(model);

            Newtonsoft.Json.JsonSerializerSettings jss = new Newtonsoft.Json.JsonSerializerSettings();
            jss.DateFormatString = "yyyy.MM.dd HH:mm:ss";
            jss.Formatting = Newtonsoft.Json.Formatting.Indented;
            jss.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            
            var activies = new ActivityDTOList { this._ActivityService.GetByKey(activityDTO.Id) };
            var obj = new
            {
                List = activies
            };

            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(obj, jss);

            return Json(jsonStr);
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