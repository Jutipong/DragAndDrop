using System;
using System.Collections.Generic;
using System.Text;

namespace TCRB.DAL.Model.Commons
{
    public class Select2Model
    {
        public string id { get; set; }
        public string text { get; set; }
        public bool selected { get; set; }
    }

    public class Select2SearchModel
    {
        public string Search { get; set; }
        public int Limit { get; set; }

        public Select2SearchModel()
        {
            this.Limit = 30;
        }
    }
}
