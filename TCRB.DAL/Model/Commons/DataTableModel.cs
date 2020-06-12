using System.Collections.Generic;
using System.Linq;

namespace TCRB.DAL.Model.Commons
{
    public class DataTableResponseModel
    {
        public bool status { get; set; }
        public string message { get; set; }
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public object data { get; set; }
        public DataTableResponseModel()
        {
            draw = 30;
        }
    }

    public class DatableOption
    {
        public string orderby { get => order != null ? order[0]?.FirstOrDefault(r => r.Key == "dir").Value : null; }
        public int sortingby { get => order != null ? int.Parse(order[0].FirstOrDefault(r => r.Key == "column").Value) : 0; }
        public int start { get; set; }
        public int length { get; set; }
        public int draw { get; set; }
        public List<Dictionary<string, string>> order { get; set; }
    }
}
