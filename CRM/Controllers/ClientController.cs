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
        private readonly IClientContactService _IClientContactService;
        private readonly IDepartmentService _IDepartmentService;
        private readonly IActivityService _ActivityService;
        private readonly IContractService _ContractService;
        private readonly IDictionaryService _DictionaryService;
        public ClientController(IClientService iClientService,
            IClientContactService iClientContactService,
            IDepartmentService iDepartmentService,
            IActivityService activityService,
            IContractService contractService,
            IDictionaryService dictionaryService)
        {
            this._IClientService = iClientService;
            this._IClientContactService = iClientContactService;
            this._IDepartmentService = iDepartmentService;
            this._ActivityService = activityService;
            this._ContractService = contractService;
            this._DictionaryService = dictionaryService;
        }

        // GET: Custome
        public ActionResult Index(string keywords = "")
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
                client.Province = string.IsNullOrWhiteSpace(client.Province) ? client.Province : client.Province.Replace("请选择", null);
                client.City = string.IsNullOrWhiteSpace(client.City) ? client.City : client.City.Replace("请选择", null);
                client.Country = string.IsNullOrWhiteSpace(client.Country) ? client.Country : client.Country.Replace("请选择", null);
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
            dept.ContractDTOList = this._ContractService.GetAll(string.Empty,id);
            var activityCategories = this._DictionaryService.GetAll(Ingenious.Infrastructure.GlobalMessage.DataItem_ClientActivityCategory);
            ViewBag.ActivityCategories = new SelectList(activityCategories, "Id", "Name");
            var activies = this._ActivityService.GetAll(dept.Id, this.User.Id);
            var model = new ClientComposite
            {
                Client = dept,
                ActivityDTOList = activies
            };
            return View(model);
        }

        public ActionResult Edit(Guid id)
        {
            var model = this._IClientService.GetByKey(id);
            this.DataBind();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ClientDTO client)
        {
            if (ModelState.IsValid)
            {
                client.ModifiedBy = this.User.Id;
                this._IClientService.Update(new ClientDTOList { client });
            }
            this.DataBind();
            return RedirectToAction("Edit", new { Id = client.Id });
        }

        public void Update(Guid id, string filedName, string newValue)
        {
            var client = this._IClientService.GetByKey(id);
            switch (filedName.ToLower())
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
        public ActionResult CreateActivity(Guid clientId, Guid cateId, string content)
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
            jss.MaxDepth = 2;
            jss.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;

            var list = new List<object>();
            var activity = this._ActivityService.GetByKey(activityDTO.Id);

            list.Add(new
            {
                UserName = activity.CreatedByUser.Name,
                ActivityCategoryName = activity.ActivityCategoryName,
                CreatedDate = activity.CreatedDate,
                ModifiedDate = activity.ModifiedDate,
                Content = activity.Content,
                TimeLabel = activity.TimeLabel
            });

            var obj = new
            {
                List = list
            };
            string jsonStr = jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(obj, jss);
           
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

            ViewBag.Grades =
                new SelectList(
                this._DictionaryService.GetAll(Ingenious.Infrastructure.GlobalMessage.DataItem_ClientGradeName)
                , "Id", "Name");
            ViewBag.Industries =
                new SelectList(
                this._DictionaryService.GetAll(Ingenious.Infrastructure.GlobalMessage.DataItem_ClientIndustryName)
                , "Id", "Name");
        }

        public JsonResult CreateClientContact(ClientContactDTO contact)
        {
            Newtonsoft.Json.JsonSerializerSettings jss = new Newtonsoft.Json.JsonSerializerSettings();
            jss.DateFormatString = "yyyy.MM.dd HH:mm:ss";
            jss.Formatting = Newtonsoft.Json.Formatting.Indented;
            jss.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            jss.MaxDepth = 2;
            jss.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;

            var clientContact = new ClientContactDTO();
            var list = new List<object>();
            if(ModelState.IsValid)
            {
                contact.CreatedBy = contact.ModifiedBy = this.User.Id;
                clientContact = this._IClientContactService.Create(contact);
                list.Add(new
                {
                    Name = clientContact.Name,
                    Title = clientContact.Title,
                    OfficePhone = clientContact.Phone,
                    Phone = clientContact.Phone,
                    Email = clientContact.Email
                });
            }

            var obj = new
            {
                List = list
            };
            string jsonStr = jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(obj, jss);

            return Json(jsonStr);
        }

        public void DeleteClientContact(Guid id)
        {
            this._IClientContactService.Delete(new ClientContactDTOList { new ClientContactDTO { Id = id, ModifiedBy = this.User.Id, IsActive = false } });
        }

    }
}