using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TCRB.BAL;
using TCRB.DAL.EntityModel;
using TCRB.DAL.Model.Authentication;
using TCRB.DAL.Model.Commons;
using TCRB.DAL.Model.Configuration;
using TCRB.WEB.Models;

namespace TCRB.WEB.Controllers
{
    [Authorize]
    public class ConfigurationController : Controller
    {
        private ConfigurationDataService _configurationDataService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ConfigurationController(ConfigurationDataService configurationDataService, ILogger<ConfigurationController> logger, IMapper mapper)
        {
            _configurationDataService = configurationDataService;
            _mapper = mapper;
            _logger = logger;
        }

        #region Master
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult InquiryMasterDatatable(DatableOption option, ConfigurationMaster master)
        {
            var result = _configurationDataService.InquiryMasterDatatable(option, master);
            return Json(result);
        }

        public JsonResult InquiryTemplatename(string Search)
        {
            var result = _configurationDataService.InquiryTemplatename(Search);
            return Json(result);
        }

        public JsonResult Create(ConfigurationMaster master)
        {
            master.CreateDate = DateTime.Now;
            master.CreateBy = UserLogin.User().UserID;
            var result = _configurationDataService.Create(master);
            return Json(result);
        }
        public JsonResult Update(ConfigurationMaster master)
        {
            master.UpdateDate = DateTime.Now;
            master.UpdateBy = UserLogin.User().UserID;
            var result = _configurationDataService.Update(master);
            return Json(result);
        }
        public JsonResult Delete(ConfigurationMaster master)
        {
            var result = _configurationDataService.Delete(master);
            return Json(result);
        }
        #endregion

        #region Detail
        public ActionResult Detail(Guid id)
        {
            var result = new ConfigurationModel();

            if (id == Guid.Empty)
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            //Todo _mapper
            var obj = _configurationDataService.InquiryMaster(id);
            result.ID = obj.ID;
            result.TemplateName = obj.TemplateName;
            result.InputFile = obj.InputFile;
            result.OutputFile = obj.OutputFile;
            result.CreateBy = obj.CreateBy;
            result.CreateDate = obj.CreateDate;
            result.UpdateBy = obj.UpdateBy;
            result.UpdateDate = obj.UpdateDate;
            result.IsActive = obj.IsActive;

            return View(result);
        }

        public JsonResult InquiryDetail(Guid masterID)
        {
            var result = _configurationDataService.InquiryDetail(masterID);
            return Json(result);
        }

        public JsonResult CreateDetail(List<ConfigurationDetail2> detail)
        {
            var createDate = DateTime.Now;
            var createBy = UserLogin.User().UserID;
            //detail.ForEach(item =>
            //{
            //    item.CreateDate = DateTime.Now;
            //    item.CreateBy = createBy;
            //});
            var result = _configurationDataService.CreateDetail(new List<ConfigurationDetail>());
            return Json(result);
        }
        public JsonResult UpdateDetail(ConfigurationDetail detail)
        {
            detail.UpdateDate = DateTime.Now;
            detail.UpdateBy = UserLogin.User().UserID;
            var result = _configurationDataService.UpdateDetail(detail);
            return Json(result);
        }
        public JsonResult DeleteDetail(ConfigurationDetail detail)
        {
            var result = _configurationDataService.DeleteDetail(detail);
            return Json(result);
        }
        #endregion

    }

    public class ConfigurationDetail2
    {
        //public Guid ID { get; set; }
        //public Guid ConfigurationID { get; set; }
        public string FieldName { get; set; }
        //public string Type { get; set; }
        //public bool Required { get; set; }
        //public int Length { get; set; }
        //public int Len { get; set; }
        //public int Des { get; set; }
        //public int Order { get; set; }
        //public DateTime? CreateDate { get; set; }
        //public string CreateBy { get; set; }
        //public DateTime? UpdateDate { get; set; }
        //public string UpdateBy { get; set; }
        //public bool? IsActive { get; set; }
    }
}
