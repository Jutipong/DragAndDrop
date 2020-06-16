using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using TCRB.BLL;
using TCRB.DAL.Model.Authentication;
using TCRB.DAL.Model.Commons;
using TCRB.WEB.Models;

namespace TCRB.WEB.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private ConfigurationDataService _configurationDataService;

        public HomeController(ILogger<HomeController> logger, IMapper mapper, ConfigurationDataService configurationDataService)
        {
            _logger = logger;
            _mapper = mapper;
            _configurationDataService = configurationDataService;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Demo01()
        {
            _logger.LogInformation($"Start => {MethodBase.GetCurrentMethod().Name}", JsonSerializer.Serialize(""));

            var Master_Select2Single = new List<Select2Model>
            {
                new Select2Model{ id = "id_1", text = "text_1"},
                new Select2Model{ id = "id_2", text = "text_2"},
                new Select2Model{ id = "id_3", text = "text_3", selected = true},
                new Select2Model{ id = "id_4", text = "text_4"},
            };

            var Master_Select2Multiple = new List<Select2Model>
            {
                new Select2Model{ id = "id_1", text = "text_1", selected = true},
                new Select2Model{ id = "id_2", text = "text_2"},
                new Select2Model{ id = "id_3", text = "text_3", selected = true},
                new Select2Model{ id = "id_4", text = "text_4"},
            };

            var result = new Demo01ViewModel
            {
                Master_Select2Single = Master_Select2Single,
                Master_Select2Multiple = Master_Select2Multiple
            };

            _logger.LogInformation($"End => {MethodBase.GetCurrentMethod().Name}", JsonSerializer.Serialize(result));

            return View(result);
        }

        public JsonResult InquirySelect2(Select2SearchModel model)
        {
            var Master_Select2Multiple = new List<Select2Model>
            {
                new Select2Model{ id = "id_1", text = "text_1", selected = true},
                new Select2Model{ id = "id_2", text = "text_2"},
                new Select2Model{ id = "id_3", text = "text_3", selected = true},
                new Select2Model{ id = "id_4", text = "text_4"},
            };

            return Json(Master_Select2Multiple.Where(r => r.text.Contains(model.Search)).ToList());
        }

        public JsonResult InquiryDataTableUser(DatableOption option, UserModel model)
        {
            var users = new List<UserModel>();
            for (int i = 0; i < 30; i++)
            {
                users.Add(new UserModel { ID = Guid.NewGuid(), Code = $"000{ i.ToString() }", Name = $"Name{i.ToString()}", Last = $"Last{ i.ToString()}", CreateBy = $"CreateBy {i.ToString()}", CreateDate = DateTime.Now });
            }

            var query = (from user in users
                         where string.IsNullOrEmpty(model.Name) || model.Name.Contains(user.Name)
                         where string.IsNullOrEmpty(model.Last) || model.Last.Contains(user.Last)
                         select user);

            switch (option.sortingby)
            {
                case 1: query = (option.orderby == "asc" ? query.OrderBy(r => r.Code) : query.OrderByDescending(r => r.Code)); break;
                case 2: query = (option.orderby == "asc" ? query.OrderBy(r => r.Name) : query.OrderByDescending(r => r.Name)); break;
                case 3: query = (option.orderby == "asc" ? query.OrderBy(r => r.Last) : query.OrderByDescending(r => r.Last)); break;
                case 4: query = (option.orderby == "asc" ? query.OrderBy(r => r.CreateDate) : query.OrderByDescending(r => r.CreateDate)); break;
                case 5: query = (option.orderby == "asc" ? query.OrderBy(r => r.CreateBy) : query.OrderByDescending(r => r.CreateBy)); break;
                default: query = (option.orderby == "asc" ? query.OrderBy(r => r.Code) : query.OrderByDescending(r => r.Code)); break;
            }

            var datas = query.Skip(option.start).Take(option.length).ToList();
            var recordsTotal = query.Count();
            var result = new DataTableResponseModel { status = true, message = "success", data = datas, draw = option.draw, recordsTotal = recordsTotal, recordsFiltered = recordsTotal };
            return Json(result);
        }

        public JsonResult InquiryData(UserModel model)
        {
            return Json(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
