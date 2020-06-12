using System;

namespace TCRB.DAL.Model.UserLogin
{
    public class UserLoginModel
    {
        public Guid ID { get; set; }
        public string Code { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string Descript { get; set; }
    }
}
