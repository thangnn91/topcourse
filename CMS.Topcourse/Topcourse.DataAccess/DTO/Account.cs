using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topcourse.DataAccess.DTO
{
    public class Account
    {
        public int UserID { set; get; }
        public string Username { set; get; }
        public string Password { set; get; }
        public string Email { set; get; }
        public string Mobile { set; get; }
        public byte UserType { set; get; }
        public byte Status { set; get; }
        public string CreatedUser { set; get; }
        public DateTime CreatedDate { set; get; }
        public string Fullname { set; get; }
        public byte VerifyType { set; get; }

        // 
        public string UserProfileInfo { set; get; }
        public string Reason { set; get; }



        // Full 
        public string Address { set; get; }
        public int DistrictID { set; get; }
        public int WardID { set; get; }
        public int LocationID { set; get; }
        public int Nationality { set; get; }
        public int EduNumberOfStaff { set; get; }
        public string DelegatorInfo { set; get; }
        public string HeadMaster { set; get; }  //  hieu truong
        public string EduAcreage { set; get; } // dien tich
        public int EduAddress { set; get; } // dia chi tru so 
        public string EduAddressDesc { set; get; }
        public string EduDescriptionMore { set; get; } // mo ta them
        public string EduJob { set; get; } // nghe nghiep
        public int EduType { set; get; } // loai hinh dao tao
        public string EduTypeDesc { set; get; } // mo ta loai hinh dao tao
        public string EN_Name { set; get; }
        public string VN_Name { set; get; }
        public string Fax { set; get; }
        public string Website { set; get; }
    }



    public class Location
    {
        public int LocationID { get; set; }
        public string LocationName { get; set; }
    }

    public class Education
    {
        public int EduId { set; get; }
        public string EduName { set; get; }
        public string Alias { set; get; }
        public string EduNameVI { set; get; }
        public string EduNameEN { set; get; }
        public string ShortName { set; get; }
        public string EduAddress { set; get; }
        public int NumberView { set; get; }
        public int NumComment { set; get; }
        public decimal Rate { set; get; }
        public string Description { set; get; }
        public string Content { set; get; }
        public int UserID { set; get; }
        public int Type { set; get; }
        public bool IsPartner { set; get; }
        public byte TraningType { set; get; }
        public string Avatar { set; get; }
        public string Logo { set; get; }
        public int EduEstablishedYear { set; get; }
        public int NationalID { set; get; }
        public string EduMap { set; get; }
        public string ContactInfo { set; get; }
        public string BranchInfo { set; get; }
        //
        public string CreatedUser { get; set; }
        public string SlideImage { get; set; }

        public string SEODescription { set; get; } // noi dung seo

        public bool IsCheck { set; get; } // Check gắn thẻ (Tag)
    }


    public class Request_Education
    {
        public string EduName { set; get; }
        public int UserID { set; get; }
        public int Type { set; get; }
        public bool IsPartner { set; get; }
        public byte TraningType { set; get; }
        public int NationalID { set; get; }


        public int EduId { set; get; }

        public string CreatedUser { set; get; }
    }


    public class Comment_Request
    {
        public int Id { set; get; }
        public int TargetID { set; get; }
        public int UserID { set; get; }
        public string Comment { set; get; }
        public int ParentID { set; get; }
        public byte Type { set; get; } // 1 trường, 2 khóa , 3 học bổng
        public string UpdateUser { get; set; } 

        public byte CreateType { set; get; } // 1 tk da co, 2 tk moi
        public string Username { set; get; } // ten tai khoan
        public string Fullname { set; get; }
        public string Mobile { set; get; } // 
        public string Email { set; get; }
    }

    public class CommentModel   
    {
        public long Id { set; get; }
        public int TargetID { set; get; }
        public int UserID { set; get; }
        public  string Comment { set; get; }
        public int ParentID { set; get; }
        public int NumberLike { set; get; }
        public string Username { set; get; }
        public string Fullname { set; get; }
        public string Avatar { set; get; }
        public byte Type { set; get; }
        public DateTime CreatedDate { set; get; } 
    }

    public class Order_Payment 
    {
        public long OrderId { set; get; }
        public int CourseId { set; get; }
        public string Username { set; get; }
        public string Title { set; get; }
        public byte CourseType { set; get; }
        public int UserId { set; get; }
        public int Amount { set; get; }
        public string BankCode { set; get; }
        public string BankTransNo { set; get; }
        public string CardType { set; get; }
        public DateTime CreatedDate { set; get; }
        public string OrderInfo { set; get; }
        public decimal PayDate { set; get; }
        public string ResponseCode { set; get; }
        public DateTime ResponseDate { set; get; }
        public string TransRefID { set; get; }
        public int ServiceId { set; get; }
        public byte Status { set; get; }
        public string TmnCode { set; get; }

        //
        public int ProductId { set; get; }
        public string ProductName { set; get; }
    }


    public class OrderPayment_ConfirmStatus
    {
        public string TransRefID { set; get; }
        public long OrderID { set; get; }
        public byte Status { get; set; }
        public string BankCode { get; set; }
        public string BankTransNo { get; set; }
        public string CardType { get; set; }
        public decimal PayDate { get; set; }
        public string PayResponseCode { get; set; }
        public string TmnCode { get; set; }
        public string CreatedUser { get; set; }


        public int ResponseCode { set; get; }
        public string Description { get; set; }
        public long Amount { get; set; }
        public int TransactionStatus { get; set; }
    }

}
