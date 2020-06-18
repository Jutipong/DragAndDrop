using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using TCRB.DAL.EntityModel;
using TCRB.DAL.Model.Commons;
using TCRB.HELPER;

namespace TCRB.DAL.Configuration
{
    public class ConfigurationDataAccess : IConfigurationDataAccess
    {
        private readonly TCRBDBContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ConfigurationDataAccess> _logger;

        public ConfigurationDataAccess(TCRBDBContext context, ILoggerFactory loggerFactory, IMapper mapper)
        {
            _logger = loggerFactory.CreateLogger<ConfigurationDataAccess>();
            _mapper = mapper;
            _context = context;
        }

        #region Master
        public DataTableResponseModel InquiryMasterDatatable(DatableOption option, ConfigurationMaster master)
        {
            var methodName = MethodBase.GetCurrentMethod().Name;
            var result = new DataTableResponseModel();

            try
            {
                _logger.LogInformation($"Start Function => {methodName}, Parameters => {JsonSerializer.Serialize(master)}");

                var query = _context.ConfigurationMaster
                   .Where(r => (master.ID == Guid.Empty || r.ID == master.ID))
                   .Where(r => master.IsActive == null || r.IsActive == master.IsActive)
                   .OrderByDescending(r => r.CreateDate)
                   .ThenByDescending(r => r.UpdateDate)
                   .Select(item => new ConfigurationMaster
                   {
                       ID = item.ID,
                       TemplateName = item.TemplateName,
                       InputFile = item.InputFile,
                       OutputFile = item.OutputFile,
                       CreateDate = item.CreateDate,
                       CreateBy = item.CreateBy,
                       UpdateDate = item.UpdateDate,
                       IsActive = item.IsActive
                   });

                switch (option.sortingby)
                {
                    case 1: query = (option.orderby == "asc" ? query.OrderBy(r => r.TemplateName) : query.OrderByDescending(r => r.TemplateName)); break;
                    case 2: query = (option.orderby == "asc" ? query.OrderBy(r => r.InputFile) : query.OrderByDescending(r => r.InputFile)); break;
                    case 3: query = (option.orderby == "asc" ? query.OrderBy(r => r.OutputFile) : query.OrderByDescending(r => r.OutputFile)); break;
                    case 4: query = (option.orderby == "asc" ? query.OrderBy(r => r.CreateDate) : query.OrderByDescending(r => r.CreateDate)); break;
                    case 5: query = (option.orderby == "asc" ? query.OrderBy(r => r.CreateBy) : query.OrderByDescending(r => r.CreateBy)); break;
                    case 6: query = (option.orderby == "asc" ? query.OrderBy(r => r.UpdateDate) : query.OrderByDescending(r => r.UpdateDate)); break;
                    case 7: query = (option.orderby == "asc" ? query.OrderBy(r => r.UpdateBy) : query.OrderByDescending(r => r.UpdateBy)); break;
                    default: query = (option.orderby == "asc" ? query.OrderBy(r => r.CreateDate) : query.OrderByDescending(r => r.CreateDate)); break;
                }

                var datas = query.Skip(option.start).Take(option.length).ToList();
                var recordsTotal = query.Count();
                result = new DataTableResponseModel
                {
                    status = true,
                    message = "success",
                    data = datas,
                    draw = option.draw,
                    recordsTotal = recordsTotal,
                    recordsFiltered = recordsTotal
                };

                _logger.LogInformation($"Finish Function => {methodName}, Result => {JsonSerializer.Serialize(result)}");
            }
            catch (Exception ex)
            {
                var messageError = $"Error Function => {methodName}";
                _logger.LogError(ex, messageError);
                throw new ArgumentException(messageError, ex);
            }

            return result;
        }

        public List<Select2Model> InquiryTemplatename(string Search)
        {
            var methodName = MethodBase.GetCurrentMethod().Name;
            var result = new List<Select2Model>();

            try
            {
                _logger.LogInformation($"Start Function => {methodName}, Parameters => {JsonSerializer.Serialize(Search)}");

                result = (from cm in _context.ConfigurationMaster.AsEnumerable()
                          where (string.IsNullOrEmpty(Search) || cm.TemplateName.Contains(Search, StringComparison.OrdinalIgnoreCase))
                          select new Select2Model
                          {
                              id = cm.ID.ToString(),
                              text = cm.TemplateName
                          }).ToList();

                if (!string.IsNullOrEmpty(Search))
                {
                    result = result.Where(r => r.text == Search).ToList();
                }

                _logger.LogInformation($"Finish Function => {methodName}, Result => {JsonSerializer.Serialize(result)}");
            }
            catch (Exception ex)
            {
                var messageError = $"Error Function => {methodName}";
                _logger.LogError(ex, messageError);
                throw new ArgumentException(messageError, ex);
            }

            return result;
        }

        public ConfigurationMaster InquiryMaster(Guid id)
        {
            var methodName = MethodBase.GetCurrentMethod().Name;
            var result = new ConfigurationMaster();

            try
            {

                _logger.LogInformation($"Start Function => {methodName}, Parameters => {JsonSerializer.Serialize(id)}");
                var entity = _context.ConfigurationMaster.FirstOrDefault(r => r.ID == id);
                result = entity;
                _logger.LogInformation($"Finish Function => {methodName}, Result => {JsonSerializer.Serialize(result)}");
            }
            catch (Exception ex)
            {
                var messageError = $"Error Function => {methodName}";
                _logger.LogError(ex, messageError);
                throw new ArgumentException(messageError, ex);
            }

            return result;
        }

        public ResponseModel Create(ConfigurationMaster master)
        {
            var methodName = MethodBase.GetCurrentMethod().Name;
            var result = new ResponseModel();

            try
            {
                _logger.LogInformation($"Start Function => {methodName}, Parameters => {JsonSerializer.Serialize(master)}");
                _context.ConfigurationMaster.Add(master);
                _context.SaveChanges();
                result.Success = true;
                _logger.LogInformation($"Finish Function => {methodName}, Result => {JsonSerializer.Serialize(result)}");
            }
            catch (Exception ex)
            {
                var messageError = $"Error Function => {methodName}";
                _logger.LogError(ex, messageError);
                throw new ArgumentException(messageError, ex);
            }

            return result;
        }

        public ResponseModel Update(ConfigurationMaster configurationMaster)
        {
            var methodName = MethodBase.GetCurrentMethod().Name;
            var result = new ResponseModel();

            try
            {

                if (_context.ConfigurationMaster.Any(master => master.ID == configurationMaster.ID))
                {
                    _logger.LogInformation($"Start Function => {methodName}, Parameters => {JsonSerializer.Serialize(configurationMaster)}");
                    _context.ConfigurationMaster.Update(configurationMaster);
                    _context.SaveChanges();
                    result.Success = true;
                    _logger.LogInformation($"Finish Function => {methodName}, Result => {JsonSerializer.Serialize(result)}");
                }
                else
                {
                    _logger.LogInformation($"Start Function => {methodName}, Inquiry => ConfiguartionMaster Data no found.");
                }

            }
            catch (Exception ex)
            {
                var messageError = $"Error Function => {methodName}";
                _logger.LogError(ex, messageError);
                throw new ArgumentException(messageError, ex);
            }

            return result;
        }

        public ResponseModel Delete(ConfigurationMaster configurationMaster)
        {
            var methodName = MethodBase.GetCurrentMethod().Name;
            var result = new ResponseModel();

            try
            {
                _logger.LogInformation($"Start Function => {methodName}, Parameters => {JsonSerializer.Serialize(configurationMaster)}");
                _context.ConfigurationMaster.Remove(_context.ConfigurationMaster.FirstOrDefault(r => r.ID == configurationMaster.ID));
                _context.ConfigurationDetail.AddRange(_context.ConfigurationDetail.Where(r => r.ConfigurationID == configurationMaster.ID).ToList());
                _context.SaveChanges();
                result.Success = true;
                _logger.LogInformation($"Finish Function => {methodName}, Result => {JsonSerializer.Serialize(result)}");
            }
            catch (Exception ex)
            {
                var messageError = $"Error Function => {methodName}";
                _logger.LogError(ex, messageError);
                throw new ArgumentException(messageError, ex);
            }

            return result;
        }
        #endregion

        #region Detail
        public List<ConfigurationDetail> InquiryDetail(Guid masterID)
        {
            var methodName = MethodBase.GetCurrentMethod().Name;
            var result = new List<ConfigurationDetail>();

            try
            {
                _logger.LogInformation($"Start Function => {methodName}, Parameters => {JsonSerializer.Serialize(masterID)}");
                result.AddRange(_context.ConfigurationDetail.Where(r => r.ConfigurationID == masterID).OrderBy(r => r.Order).ToList());
                _logger.LogInformation($"Finish Function => {methodName}, Result => {JsonSerializer.Serialize(result)}");
            }
            catch (Exception ex)
            {
                var messageError = $"Error Function => {methodName}";
                _logger.LogError(ex, messageError);
                throw new ArgumentException(messageError, ex);
            }

            return result;
        }

        public ResponseModel CreateDetail(List<ConfigurationDetail> details)
        {
            var methodName = MethodBase.GetCurrentMethod().Name;
            var result = new ResponseModel();

            try
            {
                _logger.LogInformation($"Start Function => {methodName}, Parameters => {JsonSerializer.Serialize(details)}");
                _context.ConfigurationDetail.AddRange(details);
                _context.SaveChanges();
                result.Success = true;
                _logger.LogInformation($"Finish Function => {methodName}, Result => {JsonSerializer.Serialize(result)}");
            }
            catch (Exception ex)
            {
                var messageError = $"Error Function => {methodName}";
                _logger.LogError(ex, messageError);
                throw new ArgumentException(messageError, ex);
            }

            return result;
        }

        public ResponseModel UpdateDetail(List<ConfigurationDetail> details)
        {
            var methodName = MethodBase.GetCurrentMethod().Name;
            var result = new ResponseModel();

            try
            {
                _logger.LogInformation($"Start Function => {methodName}, Parameters => {JsonSerializer.Serialize(details)}");
                details.ForEach(detail =>
                {
                    if (_context.ConfigurationDetail.Any(r => r.ID == detail.ID))
                    {
                        _context.Update(detail);
                    }
                    else
                    {
                        _logger.LogInformation($"Inquiry => ConfiguartionDetail Data no found ID: {detail.ID}");
                    }
                });
                _context.SaveChanges();
                result.Success = true;
                _logger.LogInformation($"Finish Function => {methodName}, Result => {JsonSerializer.Serialize(result)}");

            }
            catch (Exception ex)
            {
                var messageError = $"Error Function => {methodName}";
                _logger.LogError(ex, messageError);
                throw new ArgumentException(messageError, ex);
            }

            return result;
        }

        public ResponseModel DeleteDetail(Guid masterID, List<Guid> detailsID)
        {
            var methodName = MethodBase.GetCurrentMethod().Name;
            var result = new ResponseModel();

            try
            {
                _logger.LogInformation($"Start Function => {methodName}, Parameters => {JsonSerializer.Serialize(new { masterID, detailsID })}");
                _context.ConfigurationDetail.RemoveRange(_context.ConfigurationDetail.Where(r => r.ConfigurationID == masterID && !detailsID.Contains(r.ID)));
                _context.SaveChanges();
                result.Success = true;
                _logger.LogInformation($"Finish Function => {methodName}, Result => {JsonSerializer.Serialize(result)}");
            }
            catch (Exception ex)
            {
                var messageError = $"Error Function => {methodName}";
                _logger.LogError(ex, messageError);
                throw new ArgumentException(messageError, ex);
            }

            return result;
        }
        #endregion

    }
}
