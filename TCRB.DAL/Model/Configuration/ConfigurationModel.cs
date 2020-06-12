using System;
using System.Collections.Generic;
using System.Text;
using TCRB.DAL.EntityModel;

namespace TCRB.DAL.Model.Configuration
{
    public class ConfigurationModel : ConfigurationMaster
    {
        public string FieldName { get; set; }
        public string Type { get; set; }
        //public List<ConfigurationDetail> Details { get; set; }
    }
}
