using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCRB.DAL.EntityModel;
using TCRB.DAL.Model.Configuration;

namespace TCRB.WEB.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ConfigurationMaster, ConfigurationModel>();
        }
    }
}
