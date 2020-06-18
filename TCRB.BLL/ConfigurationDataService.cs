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
            var methodName = MethodBase.GetCurrentMethod().Name;
            var result = new DataTableResponseModel();

            try
            {
                _logger.LogInformation($"Start Function => {methodName}, Parameters => {JsonSerializer.Serialize(master)}");
                result = _dataAccess.ConfigurationDataAccess.InquiryMasterDatatable(option, master);
                _logger.LogInformation($"Finish Function => {methodName}, Result => {JsonSerializer.Serialize(result)}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Function => {methodName}");
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
                result = _dataAccess.ConfigurationDataAccess.InquiryTemplatename(Search);
                _logger.LogInformation($"Finish Function => {methodName}, Result => {JsonSerializer.Serialize(result)}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Function => {methodName}");
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
                result = _dataAccess.ConfigurationDataAccess.InquiryMaster(id);
                _logger.LogInformation($"Finish Function => {methodName}, Result => {JsonSerializer.Serialize(result)}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Function => {methodName}");
            }

            return result;
        }

        public ResponseModel Create(ConfigurationMaster master)
        {
            var methodName = MethodBase.GetCurrentMethod().Name;
            var response = new ResponseModel();

            try
            {
                _logger.LogInformation($"Start Function => {methodName}, Parameters => {JsonSerializer.Serialize(master)}");
                var result = _dataAccess.ConfigurationDataAccess.Create(master);
                _logger.LogInformation($"Finish Function => {methodName}, Result => {JsonSerializer.Serialize(result)}");

                response.Datas = result;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Function => {methodName}");
            }

            return response;
        }

        public ResponseModel Update(ConfigurationMaster master)
        {
            var methodName = MethodBase.GetCurrentMethod().Name;
            var response = new ResponseModel();

            try
            {
                _logger.LogInformation($"Start Function => {methodName}, Parameters => {JsonSerializer.Serialize(master)}");
                var result = _dataAccess.ConfigurationDataAccess.Update(master);
                _logger.LogInformation($"Finish Function => {methodName}, Result => {JsonSerializer.Serialize(result)}");

                response.Datas = result;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Function => {methodName}");
            }

            return response;
        }

        public ResponseModel Delete(ConfigurationMaster master)
        {
            var methodName = MethodBase.GetCurrentMethod().Name;
            var response = new ResponseModel();

            try
            {
                _logger.LogInformation($"Start Function => {methodName}, Parameters => {JsonSerializer.Serialize(master)}");
                var result = _dataAccess.ConfigurationDataAccess.Delete(master);
                _logger.LogInformation($"Finish Function => {methodName}, Result => {JsonSerializer.Serialize(result)}");

                response.Datas = result;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Function => {methodName}");
            }

            return response;
        }
        #endregion

        #region Detail
        public ResponseModel InquiryDetail(Guid masterID)
        {
            var methodName = MethodBase.GetCurrentMethod().Name;
            var result = new ResponseModel();

            try
            {
                _logger.LogInformation($"Start Function => {methodName}, Parameters => {JsonSerializer.Serialize(masterID)}");
                var datas = _dataAccess.ConfigurationDataAccess.InquiryDetail(masterID);
                _logger.LogInformation($"Finish Function => {methodName}, Result => {JsonSerializer.Serialize(datas)}");

                result.Datas = datas;
                result.Total = datas.Count;
                result.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Function => {methodName}");
            }

            return result;
        }

        public ResponseModel CreateDetail(List<ConfigurationDetail> detail)
        {
            var methodName = MethodBase.GetCurrentMethod().Name;
            var response = new ResponseModel();

            try
            {
                _logger.LogInformation($"Start Function => {methodName}, Parameters => {JsonSerializer.Serialize(detail)}");
                var result = _dataAccess.ConfigurationDataAccess.CreateDetail(detail);
                _logger.LogInformation($"Finish Function => {methodName}, Result => {JsonSerializer.Serialize(result)}");

                response.Datas = result;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Function => {methodName}");
            }

            return response;
        }

        public ResponseModel UpdateDetail(List<ConfigurationDetail> detail)
        {
            var methodName = MethodBase.GetCurrentMethod().Name;
            var response = new ResponseModel();

            try
            {
                _logger.LogInformation($"Start Function => {methodName}, Parameters => {JsonSerializer.Serialize(detail)}");
                var result = _dataAccess.ConfigurationDataAccess.UpdateDetail(detail);
                _logger.LogInformation($"Finish Function => {methodName}, Result => {JsonSerializer.Serialize(result)}");

                response.Datas = result;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Function => {methodName}");
            }

            return response;
        }

        public ResponseModel DeleteDetail(Guid masterID, List<Guid> detailsID)
        {
            var methodName = MethodBase.GetCurrentMethod().Name;
            var response = new ResponseModel();

            try
            {
                _logger.LogInformation($"Start Function => {methodName}, Parameters => {JsonSerializer.Serialize(new { masterID, detailsID })}");
                var result = _dataAccess.ConfigurationDataAccess.DeleteDetail(masterID, detailsID);
                _logger.LogInformation($"Finish Function => {methodName}, Result => {JsonSerializer.Serialize(result)}");

                response.Datas = result;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Function => {methodName}");
            }

            return response;
        }
        #endregion

    }
}