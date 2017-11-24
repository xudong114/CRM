using Ingenious.Application.Interface;
using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ingenious.Infrastructure.Extensions;

namespace CRM.Areas.JJDManagement.Controllers
{
    public class EntityController : BaseController
    {
        public EntityController() { }

        private readonly IG_EntityService _IG_EntityService;
        private readonly IDictionaryService _IDictionaryService;
        public EntityController(IG_EntityService iG_EntityService,
            IDictionaryService iDictionaryService) 
        {
            this._IG_EntityService = iG_EntityService;
            this._IDictionaryService = iDictionaryService;
        }

        // GET: JJDManagement/Entity
        public ActionResult Index(int pageIndex = 1, string keyword = "", G_EntityDTO entity = default(G_EntityDTO))
        {
            const int pageSize = 50;
            entity = new G_EntityDTO();
            var result = this._IG_EntityService.GetAll(pageIndex, pageSize, keyword, entity.Province, entity.City, entity.Country);
            return View(result);
        }

        public ActionResult Create()
        {
            this.DataBind();
            return View();
        }

        [HttpPost]
        public ActionResult Create(G_EntityDTO entity)
        {
            if(ModelState.IsValid)
            {
                this._IG_EntityService.Create(entity);
                return RedirectToAction("index");
            }

            ViewBag.error = ModelState.GetErrorMsg();
            return View();
        }

        public ActionResult Edit(Guid id)
        {
            var model = this._IG_EntityService.GetByKey(id);
            this.DataBind();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(G_EntityDTO entity)
        {
            if (ModelState.IsValid)
            {
                this._IG_EntityService.Update(new List<G_EntityDTO> { entity });
                return RedirectToAction("index");
            }

            ViewBag.error = ModelState.GetErrorMsg();
            return View();
        }


        private void DataBind()
        {
            ViewBag.Industries =
                new SelectList(
                this._IDictionaryService.GetAll(Ingenious.Infrastructure.GlobalMessage.DataItem_ClientIndustryName)
                , "Name", "Name");
        }
    }
}