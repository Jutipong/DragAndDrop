﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace TCRB.DAL.EntityModel
{
    public partial class ConfigurationMaster
    {
        public Guid ID { get; set; }
        public string TemplateName { get; set; }
        public string InputFile { get; set; }
        public string OutputFile { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public bool? IsActive { get; set; }
    }
}