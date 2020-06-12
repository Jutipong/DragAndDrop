using System;
using System.Collections.Generic;
using TCRB.DAL.EntityModel;
using TCRB.DAL.Model.Commons;

namespace TCRB.DAL.Configuration
{
    public interface IConfigurationDataAccess
    {
        DataTableResponseModel InquiryMasterDatatable(DatableOption option, ConfigurationMaster master);
        List<Select2Model> InquiryTemplatename(string Search);
        ConfigurationMaster InquiryMaster(Guid id);
        ResponseModel Create(ConfigurationMaster master);
        ResponseModel Update(ConfigurationMaster master);
        ResponseModel Delete(ConfigurationMaster master);
        List<ConfigurationDetail> InquiryDetail(Guid masterID);
        ResponseModel CreateDetail(List<ConfigurationDetail> detail);
        ResponseModel UpdateDetail(ConfigurationDetail detail);
        ResponseModel DeleteDetail(ConfigurationDetail detail);
    }
}
