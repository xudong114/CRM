using Ingenious.Application.Interface;
using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.ManagementCenter.Controllers
{
    public class DictionaryController : BaseController
    {
        private readonly IDictionaryService _IDictionaryService;
        
        public DictionaryController(
            IDictionaryService iDictionaryService)
        {
            this._IDictionaryService = iDictionaryService;
        }

        // GET: ManagementCenter/Dictionary
        public ActionResult Index(string category = "")
        {
            var list = this._IDictionaryService.GetAll(category);

            return View(list);
        }

        public ActionResult Create()
        {
            this.DataBind();
            return View();
        }

        [HttpPost]
        public ActionResult Create(DictionaryDTO dto)
        {
            if(ModelState.IsValid)
            {
                dto.CreatedBy = dto.ModifiedBy = this.User.Id;
                this._IDictionaryService.Create(dto);
                return RedirectToAction("Index");
            }
            
            return View();
        }

        public ActionResult Edit(Guid id)
        {
            var model = this._IDictionaryService.GetByKey(id);
            this.DataBind();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(DictionaryDTO dto)
        {
            if (ModelState.IsValid)
            {
                dto.ModifiedBy = this.User.Id;
                this._IDictionaryService.Update(new DictionaryDTOList { dto });
                return RedirectToAction("Index");
            }

            return View();
        }

        public void Resume(Guid id)
        {
            var dtoList = new List<DictionaryDTO> { new DictionaryDTO { Id = id, IsActive = true } };
            this._IDictionaryService.Delete(dtoList);
        }

        public void Disable(Guid[] ids, bool status = false)
        {
            var dtoList = new List<DictionaryDTO>();
            ids.ToList().ForEach(item =>
            {
                dtoList.Add(new DictionaryDTO { Id = item, IsActive = false, ModifiedBy = this.User.Id });
            });
            this._IDictionaryService.Delete(dtoList);
        }


        public void DataBind()
        {
            var cates = this._IDictionaryService.GetCategories();
            var selectList = new List<SelectListItem>();
            foreach(var item in cates)
            {
                selectList.Add(new SelectListItem(){ Text=item, Value=item});
            }

            ViewBag.Categories = selectList;
        }

    }
}