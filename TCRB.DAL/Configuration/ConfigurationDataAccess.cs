using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TCRB.DAL.EntityModel;
using TCRB.DAL.Model.Commons;
using TCRB.DAL.Model.Configuration;

namespace TCRB.DAL.Configuration
{
    public class ConfigurationDataAccess : IConfigurationDataAccess
    {
        private readonly TCRBDBContext _context;
        private readonly ILogger<ConfigurationDataAccess> _logger;

        public ConfigurationDataAccess(TCRBDBContext context, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ConfigurationDataAccess>();
            _context = context;
        }

        #region Master
        public DataTableResponseModel InquiryMasterDatatable(DatableOption option, ConfigurationMaster master)
        {
            var result = new DataTableResponseModel();

            try
            {
                _logger.LogInformation($"Start => Iquiry Inquiry ConfigurationMaster => Datatable");

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

                _logger.LogInformation($"Finish => Iquiry Inquiry ConfigurationMaster => Datatable");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fail => {MethodBase.GetCurrentMethod().Name}");
                throw new ArgumentException("Error: ", ex);
            }

            return result;
        }

        public List<Select2Model> InquiryTemplatename(string Search)
        {
            var result = new List<Select2Model>();

            try
            {
                _logger.LogInformation($"Start => Iquiry InquiryTemplatename for select2");
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



                _logger.LogInformation($"Finish => Iquiry InquiryTemplatename for select2");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fail => {MethodBase.GetCurrentMethod().Name}");
                throw new ArgumentException("Error: ", ex);
            }

            return result;
        }

        public ConfigurationMaster InquiryMaster(Guid id)
        {
            var result = new ConfigurationMaster();

            try
            {
                _logger.LogInformation($"Start => Iquiry Inquiry Configuration Master");
                var entity = _context.ConfigurationMaster.FirstOrDefault(r => r.ID == id);
                _logger.LogInformation($"Finish => Iquiry Inquiry Configuration Master");

                result = entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fail => {MethodBase.GetCurrentMethod().Name}");
                throw new ArgumentException("Error: ", ex);
            }

            return result;
        }

        public ResponseModel Create(ConfigurationMaster master)
        {
            var response = new ResponseModel();

            try
            {
                _logger.LogInformation($"Start => Insert ConfiguartionMaster");
                _context.ConfigurationMaster.Add(master);
                _logger.LogInformation($"Finish => Insert ConfiguartionMaster");

                _logger.LogInformation($"Start => SaveChange");
                _context.SaveChanges();
                _logger.LogInformation($"Finish => SaveChange");

                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fail => {MethodBase.GetCurrentMethod().Name}");
                throw new ArgumentException("Error: ", ex);
            }

            return response;
        }

        public ResponseModel Update(ConfigurationMaster configurationMaster)
        {
            var response = new ResponseModel();

            try
            {
                var master = _context.ConfigurationMaster.FirstOrDefault(master => master.ID == configurationMaster.ID);
                if (master != null)
                {
                    _logger.LogInformation($"Start => Update value ConfiguartionMaster");
                    master.TemplateName = configurationMaster.TemplateName;
                    master.InputFile = configurationMaster.InputFile;
                    master.OutputFile = configurationMaster.OutputFile;
                    master.IsActive = configurationMaster.IsActive;
                    master.UpdateDate = configurationMaster.UpdateDate;
                    master.UpdateBy = configurationMaster.UpdateBy;
                    _logger.LogInformation($"Finish => Update value ConfiguartionMaster");

                    _logger.LogInformation($"Start => SaveChange");
                    _context.SaveChanges();
                    _logger.LogInformation($"Finish => SaveChange");

                    response.Success = true;
                }
                else
                {
                    _logger.LogInformation($"Inquiry => ConfiguartionMaster Data no found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fail => {MethodBase.GetCurrentMethod().Name}");
                throw new ArgumentException("Error: ", ex);
            }

            return response;
        }

        public ResponseModel Delete(ConfigurationMaster configurationMaster)
        {
            var response = new ResponseModel();

            try
            {
                _logger.LogInformation($"Start => Delete ConfiguartionMaster");
                _context.ConfigurationMaster.Remove(_context.ConfigurationMaster.FirstOrDefault(r => r.ID == configurationMaster.ID));
                _logger.LogInformation($"Finish => Delete ConfiguartionMaster");

                _logger.LogInformation($"Start => Delete ConfiguartionMaster");
                _context.ConfigurationDetail.AddRange(_context.ConfigurationDetail.Where(r => r.ConfigurationID == configurationMaster.ID));
                _logger.LogInformation($"Finish => Delete ConfiguartionMaster");

                _logger.LogInformation($"Start => SaveChange");
                _context.SaveChanges();
                _logger.LogInformation($"Finish => SaveChange");

                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fail => {MethodBase.GetCurrentMethod().Name}");
                throw new ArgumentException("Error: ", ex);
            }

            return response;
        }
        #endregion

        #region Detail
        public List<ConfigurationDetail> InquiryDetail(Guid masterID)
        {
            var result = new List<ConfigurationDetail>();

            try
            {
                _logger.LogInformation($"Start => Iquiry Inquiry ConfigurationDetail => Datatable");
                result.AddRange(_context.ConfigurationDetail.Where(r => r.ConfigurationID == masterID).OrderBy(r => r.Order).ToList());
                _logger.LogInformation($"Finish => Iquiry Inquiry ConfigurationDetail => Datatable");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fail => {MethodBase.GetCurrentMethod().Name}");
                throw new ArgumentException("Error: ", ex);
            }

            return result;
        }

        public ResponseModel CreateDetail(List<ConfigurationDetail> detail)
        {
            var response = new ResponseModel();

            try
            {
                _logger.LogInformation($"Start => Insert ConfiguartionDetail");
                _context.ConfigurationDetail.AddRange(detail);
                _logger.LogInformation($"Finish => Insert ConfiguartionDetail");

                _logger.LogInformation($"Start => SaveChange");
                _context.SaveChanges();
                _logger.LogInformation($"Finish => SaveChange");

                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fail => {MethodBase.GetCurrentMethod().Name}");
                throw new ArgumentException("Error: ", ex);
            }

            return response;
        }

        public ResponseModel UpdateDetail(ConfigurationDetail detail)
        {
            var response = new ResponseModel();

            try
            {
                var entity = _context.ConfigurationDetail.FirstOrDefault(item => item.ID == detail.ID);
                if (entity != null)
                {
                    _logger.LogInformation($"Start => Update value ConfiguartionDetail");
                    entity.FieldName = detail.FieldName;
                    entity.Type = detail.Type;
                    entity.Required = detail.Required;
                    entity.Length = detail.Length;
                    entity.Len = detail.Len;
                    entity.Des = detail.Des;
                    entity.UpdateDate = DateTime.Now;
                    entity.UpdateBy = detail.UpdateBy;
                    _logger.LogInformation($"Finish => Update value ConfiguartionDetail");

                    _logger.LogInformation($"Start => SaveChange");
                    _context.SaveChanges();
                    _logger.LogInformation($"Finish => SaveChange");

                    response.Success = true;
                }
                else
                {
                    _logger.LogInformation($"Inquiry => ConfiguartionDetail Data no found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fail => {MethodBase.GetCurrentMethod().Name}");
                throw new ArgumentException("Error: ", ex);
            }

            return response;
        }

        public ResponseModel DeleteDetail(ConfigurationDetail detail)
        {
            var response = new ResponseModel();

            try
            {
                _logger.LogInformation($"Start => Delete ConfiguartionDetail");
                _context.ConfigurationDetail.Remove(_context.ConfigurationDetail.FirstOrDefault(r => r.ID == detail.ID));
                _logger.LogInformation($"Finish => Delete ConfiguartionDetail");

                _logger.LogInformation($"Start => SaveChange");
                _context.SaveChanges();
                _logger.LogInformation($"Finish => SaveChange");

                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fail => {MethodBase.GetCurrentMethod().Name}");
                throw new ArgumentException("Error: ", ex);
            }

            return response;
        }
        #endregion

    }
}
