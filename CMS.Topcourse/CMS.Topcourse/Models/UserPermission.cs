using System.Collections.Generic;

namespace CMS.Topcourse.Models
{
    public class UserPermission
    {
        //public int UserId { get; set; }
        //public string UserName { get; set; }
        //public List<UserFunctions> ListUserRoles { get; set; }
    }
    public class UserInfo
    {
        public int userId { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
    //public class UserServiceModel
    //{
    //    public List<Services> ListService { get; set; }
    //    public List<UserService> UserService { get; set; }
    //}
    public class UserServiceModel
    {
        public int UserID { get; set; }
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public bool check { get; set; }
    }
    public class UserRoleModel {
        public List<UserRoleCP> RoleContentProvider { get; set; }
        public List<UserRoleCP> RoleCampaign { get; set; }
    }
    public class UserRoleCP
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public int Type { get; set; }
        public int RoleView { get; set; } // 0:ALL, 1:NRU, 2:PU
        public string RoleName { get; set; }
        public bool check { get; set; }
    }
}