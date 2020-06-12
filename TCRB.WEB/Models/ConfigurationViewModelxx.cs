using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCRB.DAL.EntityModel;

namespace TCRB.WEB.Models
{
    public class ConfigurationViewModelxx : ConfigurationMaster
    {
        public List<ConfigurationDetail> Details { get; set; }
    }
}
