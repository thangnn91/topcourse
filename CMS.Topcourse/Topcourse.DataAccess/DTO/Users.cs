using System;

namespace Topcourse.DataAccess.DTO
{
    public class Users
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public bool IsAdministrator { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CreatedUser { get; set; }
        public bool Status { get; set; }
        public bool IsLock { get; set; } //0:mở , 1:đóng
        public int SystemID { set; get; }
        public bool IsMod { set; get; }

        public string List_SystemID { set; get; }
        public string List_GroupID { set; get; }
    }

    //Quyền chức năng
    public class UserFunction
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public int FunctionID { get; set; }
        public string FunctionName { get; set; }
        public string ActionName { get; set; }
        public int FatherID { get; set; }
        public string FatherName { get; set; }
        public bool IsGrant { get; set; }
        public bool IsInsert { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
        public int CreatedUserID { get; set; }
        public bool IsRead { get; set; }
    }


    // Group User 
    public class GroupUser
    {
        public int GroupID { set; get; }
        public string GroupName { set; get; }
        public bool Status { set; get; }
        public string CreatedUser { set; get; }
        public DateTime CreateDate { set; get; }
    }

    public class Group
    {
        public int GroupID { set; get; }
        public string Name { set; get; }
        public string Alias { set; get; }
        public bool IsActive { set; get; }
        public int SystemID { set; get; }
    }


    public class SystemUser
    {
        public int Id { set; get; }
        public string SystemName { set; get; }
        public string Description { set; get; }
        public DateTime CreatedDate { set; get; }
        public string CreatedUser { set; get; }
    }

    public class User_System
    {
        public int UserID { set; get; }
        public int SystemID { set; get; }
        public DateTime CreatedDate { set; get; }
        public string CreatedUser { set; get; }
        public bool IsMod { set; get; }
    }

}
