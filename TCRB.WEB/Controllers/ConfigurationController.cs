using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TCRB.BLL;
using TCRB.DAL.EntityModel;
using TCRB.DAL.Model.Authentication;
using TCRB.DAL.Model.Commons;
using TCRB.DAL.Model.Configuration;
using TCRB.HELPER;
using TCRB.WEB.Models;

namespace TCRB.WEB.Controllers
{
    [Authorize]
    public class ConfigurationController : Controller
    {
        private ConfigurationDataService _configurationDataService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly UserProfileModel _userProfile;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ConfigurationController(ConfigurationDataService configurationDataService, ILogger<ConfigurationController> logger, IMapper mapper, UserLogin userLogin, IHttpContextAccessor httpContextAccessor)
        {
            _configurationDataService = configurationDataService;
            _mapper = mapper;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _userProfile = userLogin.UserProfile();
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
            master.CreateBy = _userProfile.UserID;
            master.CreateDate = DateTime.Now;
            var result = _configurationDataService.Create(master);
            return Json(result);
        }
        public JsonResult Update(ConfigurationMaster master)
        {
            master.UpdateBy = _userProfile.UserID;
            master.UpdateDate = DateTime.Now;
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
            if (id == Guid.Empty)
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var obj = _configurationDataService.InquiryMaster(id);

            if (obj == null)
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var result = _mapper.Map<ConfigurationModel>(obj);
            return View(result);
        }

        public JsonResult InquiryDetail(Guid masterID)
        {
            var result = _configurationDataService.InquiryDetail(masterID);
            return Json(result);
        }

        public JsonResult CreateDetail(List<ConfigurationDetail> detail)
        {
            var resultCreate = new ResponseModel();
            var resultUpdate = new ResponseModel();
            var resultDelete = new ResponseModel();

            //แยก create, update, delete
            var creates = detail.Where(r => r.ID == Guid.Empty).ToList();
            var updates = detail.Where(r => r.ID != Guid.Empty).ToList();
            var deletes = updates.Select(r => r.ID).ToList();

            var dateNow = DateTime.Now;
            var userBy = _userProfile.UserID;

            #region Delete
            if (deletes.Any())
            {
                var masterID = detail.FirstOrDefault(r => r.ConfigurationID != Guid.Empty).ConfigurationID;
                if (masterID != null)
                {
                    resultDelete = _configurationDataService.DeleteDetail(masterID, deletes);
                }

                if (!resultDelete.Success || masterID == null)
                {
                    resultUpdate.Message = "Delete ConfigulationDetail Fail.";
                    return Json(resultDelete);
                }
            }
            else
            {
                resultDelete.Success = true;
            }
            #endregion

            #region Update
            if (updates.Any())
            {
                updates.ForEach(r => { r.UpdateDate = dateNow; r.UpdateBy = userBy; });
                resultUpdate = _configurationDataService.UpdateDetail(updates);

                if (!resultUpdate.Success)
                {
                    resultUpdate.Message = "Update ConfigulationDetail Fail.";
                    return Json(resultUpdate);
                }
            }
            else
            {
                resultUpdate.Success = true;
            }
            #endregion

            #region Create
            if (creates.Any())
            {
                creates.ForEach(r => { r.CreateDate = dateNow; r.CreateBy = userBy; });
                resultCreate = _configurationDataService.CreateDetail(creates);

                if (!resultCreate.Success)
                {
                    resultCreate.Message = "Create ConfigulationDetail Fail.";
                    return Json(resultCreate);
                }
            }
            else
            {
                resultCreate.Success = true;
            }
            #endregion

            var IsSuccess = resultCreate.Success && resultUpdate.Success && resultDelete.Success;
            var result = new ResponseModel
            {
                Success = IsSuccess,
                Message = IsSuccess
                ? EnumHttpStatus.SUCCESS.AsDescription()
                : EnumHttpStatus.INTERNAL_SERVER_ERROR.AsDescription()
            };

            return Json(result);
        }
        #endregion

    }
}
