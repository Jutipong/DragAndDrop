using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Text.Json;
using TCRB.DAL;
using TCRB.DAL.EntityModel;
using TCRB.DAL.Model;
using TCRB.DAL.Model.Commons;
using TCRB.DAL.Model.Configuration;

namespace TCRB.BLL
{
    public class ConfigurationDataService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IDataAccessWrapper _dataAccess;

        public ConfigurationDataService(IDataAccessWrapper dataAccess, ILogger<ConfigurationDataService> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _dataAccess = dataAccess;
        }

        #region Master
        public DataTableResponseModel InquiryMasterDatatable(DatableOption option, ConfigurationMaster master)
        {
            var result = new DataTableResponseModel();

            try
            {
                _logger.LogInformation($"Start => {MethodBase.GetCurrentMethod().Name}");
                result = _dataAccess.ConfigurationDataAccess.InquiryMasterDatatable(option, master);
                _logger.LogInformation($"Finish => {MethodBase.GetCurrentMethod().Name}", JsonSerializer.Serialize(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Faile => {MethodBase.GetCurrentMethod().Name}");
            }

            return result;
        }

        public List<Select2Model> InquiryTemplatename(string Search)
        {
            var result = new List<Select2Model>();

            try
            {
                _logger.LogInformation($"Start => {MethodBase.GetCurrentMethod().Name}");
                result = _dataAccess.ConfigurationDataAccess.InquiryTemplatename(Search);
                _logger.LogInformation($"Finish => {MethodBase.GetCurrentMethod().Name}", JsonSerializer.Serialize(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Faile => {MethodBase.GetCurrentMethod().Name}");
            }

            return result;
        }

        public ConfigurationMaster InquiryMaster(Guid id)
        {
            var result = new ConfigurationMaster();

            try
            {
                _logger.LogInformation($"Start => {MethodBase.GetCurrentMethod().Name}");
                result = _dataAccess.ConfigurationDataAccess.InquiryMaster(id);
                _logger.LogInformation($"Finish => {MethodBase.GetCurrentMethod().Name}", JsonSerializer.Serialize(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Faile => {MethodBase.GetCurrentMethod().Name}");
            }

            return result;
        }

        public ResponseModel Create(ConfigurationMaster master)
        {
            var response = new ResponseModel();

            try
            {
                _logger.LogInformation($"Start => {MethodBase.GetCurrentMethod().Name}", JsonSerializer.Serialize(master));
                var result = _dataAccess.ConfigurationDataAccess.Create(master);
                _logger.LogInformation($"Finish => {MethodBase.GetCurrentMethod().Name}", JsonSerializer.Serialize(result));

                response.Datas = result;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Faile => {MethodBase.GetCurrentMethod().Name}");
            }

            return response;
        }

        public ResponseModel Update(ConfigurationMaster master)
        {
            var response = new ResponseModel();

            try
            {
                _logger.LogInformation($"Start => {MethodBase.GetCurrentMethod().Name}", JsonSerializer.Serialize(master));
                var result = _dataAccess.ConfigurationDataAccess.Update(master);
                _logger.LogInformation($"Finish => {MethodBase.GetCurrentMethod().Name}", JsonSerializer.Serialize(result));

                response.Datas = result;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Faile => {MethodBase.GetCurrentMethod().Name}");
            }

            return response;
        }

        public ResponseModel Delete(ConfigurationMaster master)
        {
            var response = new ResponseModel();

            try
            {
                _logger.LogInformation($"Start => {MethodBase.GetCurrentMethod().Name}", JsonSerializer.Serialize(master));
                var result = _dataAccess.ConfigurationDataAccess.Delete(master);
                _logger.LogInformation($"Finish => {MethodBase.GetCurrentMethod().Name}", JsonSerializer.Serialize(result));

                response.Datas = result;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Faile => {MethodBase.GetCurrentMethod().Name}");
            }

            return response;
        }
        #endregion

        #region Detail
        public ResponseModel InquiryDetail(Guid masterID)
        {
            var result = new ResponseModel();

            try
            {
                _logger.LogInformation($"Start => {MethodBase.GetCurrentMethod().Name}");
                var datas = _dataAccess.ConfigurationDataAccess.InquiryDetail(masterID);
                _logger.LogInformation($"Finish => {MethodBase.GetCurrentMethod().Name}", JsonSerializer.Serialize(result));

                result.Datas = datas;
                result.Total = datas.Count;
                result.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Faile => {MethodBase.GetCurrentMethod().Name}");
            }

            return result;
        }

        public ResponseModel CreateDetail(List<ConfigurationDetail> detail)
        {
            var response = new ResponseModel();

            try
            {
                _logger.LogInformation($"Start => {MethodBase.GetCurrentMethod().Name}", JsonSerializer.Serialize(detail));
                var result = _dataAccess.ConfigurationDataAccess.CreateDetail(detail);
                _logger.LogInformation($"Finish => {MethodBase.GetCurrentMethod().Name}", JsonSerializer.Serialize(result));

                response.Datas = result;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Faile => {MethodBase.GetCurrentMethod().Name}");
            }

            return response;
        }

        public ResponseModel UpdateDetail(List<ConfigurationDetail> detail)
        {
            var response = new ResponseModel();

            try
            {
                _logger.LogInformation($"Start => {MethodBase.GetCurrentMethod().Name}", JsonSerializer.Serialize(detail));
                var result = _dataAccess.ConfigurationDataAccess.UpdateDetail(detail);
                _logger.LogInformation($"Finish => {MethodBase.GetCurrentMethod().Name}", JsonSerializer.Serialize(result));

                response.Datas = result;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Faile => {MethodBase.GetCurrentMethod().Name}");
            }

            return response;
        }

        public ResponseModel DeleteDetail(Guid masterID, List<Guid> detailsID)
        {
            var response = new ResponseModel();

            try
            {
                _logger.LogInformation($"Start => {MethodBase.GetCurrentMethod().Name}", JsonSerializer.Serialize(detailsID));
                var result = _dataAccess.ConfigurationDataAccess.DeleteDetail(masterID, detailsID);
                _logger.LogInformation($"Finish => {MethodBase.GetCurrentMethod().Name}", JsonSerializer.Serialize(result));

                response.Datas = result;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Faile => {MethodBase.GetCurrentMethod().Name}");
            }

            return response;
        }
        #endregion

    }
}