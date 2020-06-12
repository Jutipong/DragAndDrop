using System;
using System.Collections.Generic;

namespace TCRB.DAL.Model.Authentication
{
    public class UserProfileModel
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Last { get; set; }
        public string Role { get; set; }
        public string IsLock { get; set; }
        public string IsActive { get; set; }
    }

    public class ClaimResponseModel
    {
        public string Sid { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string RoleName { get; set; }
        public string Spn { get; set; }

    }

    public class MenuModel
    {
        public string Controller { get; set; }
        public MenuItem Item { get; set; }
    }

    public class MenuAccess
    {
        public int Screenid { get; set; }
        public string Screenname { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
    }

    public class MenuItem
    {
        public List<string> Action { get; set; }
        public List<string> ScreenName { get; set; }
    }

    public class RolesPermissionModel
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }

    public class UserModel //For test.
    {
        public Guid ID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Last { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
