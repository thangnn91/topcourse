using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Topcourse.DataAccess.DTO;

namespace CMS.Topcourse.Models
{
    //public class CommonModel
    //{
    //    public List<Groups> Groups { get; set; }
    //    public List<Departments> Departments { get; set; }
    //}
    //public class UserInfo
    //{
    //    public List<Groups> Groups { get; set; }
    //    public List<Departments> Departments { get; set; }
    //    public Users User { get; set; }
    //}
    public class Item
    {
        public int Id { get; set; }
        public string value { get; set; }
    }

    public class NestedOrder
    {
        public int Id { get; set; }
        public int FatherID { get; set; }
        public int Order { get; set; }
    }
    public class ModelFunctionDetail
    {
        public List<Functions> ListFunction { get; set; }
        public Functions FunctionDetail { get; set; }
    }
    //public class ResponseData
    //{
    //    public int ResponseCode { get; set; }
    //    public string Description { get; set; }
    //    public string Extend { get; set; }
    //}


    public class UserFunctionModel
    {
        public List<Functions> ListFunction { get; set; }
        public List<UserFunction> UserFunction { get; set; }
    }

    // TreeData bảng Service ( Dịch vụ ) 
    public class TreeData
    {
        public int serviceId { set; get; }
        public int parentServiceId { get; set; }
        public int groupService { get; set; } //GroupServiceId
        public int merchantId { get; set; }
        public List<TreeData> nodes { get; set; } //Child
        public string text { get; set; } //ServiceName
        public string commanDescription { get; set; }
        public int status { get; set; }
        public bool selectable { get; set; }
        public state state { get; set; }
        public string[] tags { get; set; }
    }


    public class state
    {
        public bool @checked { set; get; }
        public bool disabled { set; get; }
        public bool expanded { set; get; }
        public bool selected { set; get; }

    }

    
    public class TreeFunction
    {
        public int FuntionId { set; get; }
        public int ParentId { get; set; }
        public List<TreeFunction> nodes { get; set; }
        public string text { get; set; }
        public int status { get; set; }
        public bool selectable { get; set; }
        public state state { get; set; }
        public string icon { get; set; }
        public bool IsBtnGrant { get; set; }
    }

    public class TreeCategory
    {
        public int CategoryId { set; get; }
        public int ParentId { set; get; }
        public int SystemId { set; get; }
        public int LanguageId { set; get; }
        public List<TreeCategory> nodes { set; get; }
        public string text { set; get; }
        public bool status { get; set; }
        public bool selectable { get; set; }
        public string color { set; get; }
        public string backColor { set; get; }
        public state state { get; set; }
    }


    public class AddGrantFunction
    {
        public int FunctionId { get; set; }
        public bool IsGrant { get; set; }
        public bool IsInsert { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
    }

    public class DeleteGrantFunction
    {
        public int FunctionId { get; set; }
    }


    public class TreeDataFunction
    {
        public int FunctionID { get; set; }
        public string FunctionName { get; set; }
        public int ParentID { get; set; }
        public List<TreeDataFunction> Childrent { get; set; }
        public string CssIcon { get; set; }
        public bool IsReport { get; set; }
        public bool IsView { get; set; }
        public bool IsGrant { get; set; }
        public bool IsInsert { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
    }
    public class FunctionsGrant
    {
        public int FunctionID { get; set; }
        public int ParentID { get; set; }
        public string FunctionName { get; set; }
        public string FatherName { get; set; }
        public bool IsReport { get; set; }
        public string CssIcon { get; set; }
        public bool IsGrant { get; set; }
        public bool IsInsert { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
    }
    public class GrantService 
    {
        public int ServiceId { get; set; }
    }

    public class ChartLineTransaction
    {
        public DateTime Ngay { get; set; }
        public List<long> TotalVcoin { get; set; }
        public string Label { get; set; }
    }
    public class objectDynamic
    {
        [JsonProperty("CreatedTime")]
        public DateTime CreatedTime { get; set; }
        [JsonProperty("MerchantName")]
        public string MerchantName { get; set; }
        [JsonProperty("TotalVcoin")]
        public long TotalVcoin { get; set; }
        [JsonProperty("TotalAccount")]
        public long TotalAccount { get; set; }
        [JsonProperty("TotalTransaction")]
        public long TotalTransaction { get; set; }
        [JsonProperty("AvgVcoin")]
        public long AvgVcoin { get; set; }
    }

    public class AccountInfoFull
    {
        public long StarBalance { get; set; }
        public long FreezeStarBalance { get; set; }
    }

    public class Account
    {
        public int AccountID { get; set; }
        public string AccountName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int Status { get; set; }          // Trạng Thái
        public int AccountType { get; set; }     // Loại TK(1-TK cá nhân,2-TK doanh nghiệp,3-TK nghiệp vụ)
        public int VerifyType { get; set; }      //Xác thực tài khoản
        public string Address { get; set; }
        public DateTime BirthDay { get; set; }
        public int DistrictID { get; set; }
        public string Job { get; set; }
        public int LocationID { get; set; }
        public string Nationality { get; set; }
        public string Passport { get; set; }
        public string PermanentAddress { get; set; }
        public string Position { get; set; }
        public int WardID { get; set; }
        public string SecureName { get; set; }
    }
}