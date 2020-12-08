using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common.Model
{
    public class CourseRequest
    {
        public string TextSearch { set; get; }
        public int StudyForm { set; get; } //Hinh thuc dao tao
        public string StudyDuration { set; get; }
        public byte StudyType { set; get; } //Loai hinh dao tao>>trong nuoc, quoc te, du hoc
        public string StudyTime { set; get; }
        public short MonthOpen { set; get; }
        public int TuitionFrom { set; get; }
        public int TuitionTo { set; get; }
        public byte NumRate { set; get; }
        public bool? IsHot { set; get; } = null;
        public bool? IsHotHomePage { set; get; } = null;
        public bool? IsFeatured { set; get; } = null;
        public int SpecialityID { set; get; }
        public int LocationID { set; get; }
        public int DisctricID { set; get; }
        public int NationalID { set; get; }
        public string LanguageInstruction { set; get; }
        public string Level { set; get; }
        public short SelectRegister { set; get; }
        public string Lecturer { set; get; }
        public byte RequireAdmission { set; get; } //yeu cau nhap hoc
        public byte SchoolType { set; get; }
        public string Address { set; get; }
        public byte CourseType { set; get; }
        public int CurrentPage { set; get; } = 1;
        public int PageSize { set; get; } = 12;
        public string ListTagId { get; set; }
        public int CourseID { set; get; }

        // Khoa dai >> 1:hoc phi thap->cao,2:hp:cao->thap,3:danh gia thap->cao,4:danh gia cao->thap, 5: chuong trinh pho bien thap ->cao,6: ct pho bien cao -> thap
        public byte Sort { set; get; }

        public int TypeView { get; set; } = 1;
    }

    public class Course
    {
        public int CoursesID { set; get; }
        public string Title { set; get; }
        public string Alias { set; get; }
        public string TitleDisplay { set; get; }
        public DateTime PublishDate { set; get; }
        public byte Status { set; get; }
        public bool IsHot { set; get; }
        public bool IsFeatured { set; get; }
        public bool IsHotHomePage { set; get; }
        public byte CourseType { set; get; }
        public string ImageUrl { set; get; }
        public int StudyForm { set; get; }
        public string StudyDuration { set; get; }
        public short StudyTime { set; get; }
        public byte StudyType { set; get; }
        public DateTime ExpireDatePromotion { set; get; }
        public short Level { set; get; }
        public string Lecturer { set; get; }
        public int Tuition { set; get; }
        public int TuitionIncentives { set; get; }
        public bool IsCerfiticate { set; get; }
        public short MonthOpen { set; get; }
        public string Logo { set; get; }
        public bool IsPractive { set; get; }
        public bool IsCommitWork { set; get; }
        public bool IsPayment { set; get; }
        public string Address { set; get; }
        public byte SchoolType { set; get; }
        public byte RequireAdmission { set; get; }
        public short LanguageInstruction { set; get; }
        public string Description { set; get; }
        public string SEODescription { set; get; }
        public string Content { set; get; }
        public string TuitionInfo { set; get; }
        public string BannerUrl { set; get; }
        public string Accreditation { set; get; }
        public int ViewCount { set; get; }
        public decimal NumRate { set; get; }
        public int RegisterFee { set; get; }
        public int SGK { set; get; }
        public int TuitionPeriod1 { set; get; }
        public int TuitionPeriod2 { set; get; }
        public int TuitionYear { set; get; }
        public string EducationName { set; get; }
        public string EducationAlias { set; get; }
        public string StudyFormDesc { set; get; }
        public string StudyTimeDesc { set; get; }
        public string LanguageInstructionDesc { set; get; }
        public string LevelStartDesc { set; get; }
        public string RequireAdmissionDesc { set; get; }
        public double R1 { set; get; }
        public double R2 { set; get; }
        public double R3 { set; get; }
        public double R4 { set; get; }
        public double R5 { set; get; }
        public int HpEdunet { set; get; }
        public int HpFirstDay { set; get; }
        public int HpTvEdunet { set; get; }
        public string NHname { set; get; }
        public int NumComment { set; get; }
        public int EduEstablishedYear { set; get; }
        public byte PaymentType { set; get; }
        public string Graduation { set; get; }
        public byte NumberSubject { set; get; }
        public string PaymentInfo { set; get; }
        public int SemesterFee { set; get; }
        public int YearFee { set; get; }
        public string ChargesDesc { set; get; }
        public string PaymentMethodInfo { set; get; }
        public byte RegisterType { set; get; }
        public string SchoolTypeDesc { set; get; }
        public int EduId { set; get; }
        public string Rated { set; get; }
        //public int R1Total { set; get; }
        //public int R2Total { set; get; }
        //public int R3Total { set; get; }
        //public int R4Total { set; get; }
        //public int R5Total { set; get; }
        public int CountRate { get; set; } //Tổng số lượt đánh giá
        public  string DateOpen { get; set; }
        public string StudyTypeDesc { get; set; }
        public string Doitacduhoc { get; set; }
        public int? UudaiEdunet { get; set; }
        public string[] Banners { get; set; }
    }


    public class RequestRegisterEdu
    {

        public string EduNameVI { set; get; }
        public string EduNameEn { set; get; }
        public string ShortName { set; get; }
        public string HeadOffice { set; get; }
        public string Ward { set; get; }
        public string District { set; get; }
        public string Location { set; get; }
        public string National { set; get; }
        public string EduPhone { set; get; }
        public string EduFax { set; get; }
        public string EduEmail { set; get; }
        public string Website { set; get; }
        public string President { set; get; }
        public string SchoolMaster { set; get; }
        public string SchoolType { set; get; }
        public string Career { set; get; }
        public int NumberLecturers { set; get; }
        public string Facilities { set; get; }
        public string FacilitiesMore { set; get; }
        public string TotalArea { set; get; }
        public string InfomationMore { set; get; }
        public string CoursesInfo { set; get; }
        public string Firstname { set; get; }
        public string Lastname { set; get; }
        public string Address { set; get; }
        public string Phone { set; get; }
        public string Email { set; get; }
        public string FileAttach { set; get; }
        public string ClientIP { set; get; }
        public List<EduDetail> EduDetailData { set; get; }
        public class EduDetail
        {
            public int ID { set; get; }
            public string EduName { set; get; } //ten ctrinh
            public string Acknowledge { set; get; } //kiem dinh, cong nhnan
            public string EduLevel { set; get; } // bac hoc
            public string EduAddress { set; get; } //dia diemn da tao
            public string EduType { set; get; } //hinh thuc dao tao
            public string CertificateRequire { set; get; } //yeu cau bang cap
            public string ForeignLanguageRequire { set; get; } // yeu cau ngoai ngu
            public string FeeAdmission { set; get; } // le phi xet tuyen
            public string OtherRequire { set; get; } //yeu cau khac
            public string Duration { set; get; } // thoi gian dao tao
            public string EduLang { set; get; } // ngon ngu giang day
            public string SubjectName { set; get; } // ten mon hoc          
            public string CreditNumber { set; get; } //so tin chi
            public string RatedGraduate { set; get; } // danh gia tot nghiep
            public string Tuition { set; get; } // Học phí
            public string TuitionIncentives { set; get; } // hoc phi uu dai
            public string Scholarship { set; get; } // hoc bong
            public string ScholarshipValue { set; get; } // muc hoc bong
            public string ScholarshipCondition { set; get; } // dieu kien nhan
            public string OpenDate { set; get; } // ngay khai giang
            public string OtherInfo { set; get; } // thong tin khác
        }
    }

    public class RequestComment
    {
        public int Id { set; get; }
        public int TargetID { set; get; } // --mã trường, mã khóa hoặc mã học bổng
        public int UserID { set; get; }
        public string Comment { set; get; }
        public int ParentID { set; get; }
        public byte Type { set; get; } // -- 1:trường, 2:khóa, 3:học bổng
        public int NumRate { set; get; }
        public string ClientIP { set; get; }
    }

    public class CommentData
    {
        public long Id { set; get; }
        public int TargetID { set; get; }
        public int UserID { set; get; }
        public string Comment { set; get; }
        public int ParentID { set; get; }
        public int NumberLike { set; get; }
        public byte Type { set; get; }
        public DateTime CreatedDate { set; get; }
        public int CreatedUnixDate { set; get; }
        public int Level { set; get; }
        public string Username { set; get; }
        public string Fullname { set; get; }
        public string Avatar { set; get; }
    }

    public class CoreAttribute
    {
        public string TableName { get; set; }
        public string FieldName { get; set; }
        public int FieldValue { get; set; }
        public int Description { get; set; }
        public byte GroupType { get; set; }
    }

    public class SpecilityCourse
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public byte CourseType { set; get; }
        public string Description { set; get; }
        public short Order { set; get; }
        public string CreatedUser { set; get; }
    }

    public class Location
    {
        public int LocationID { get; set; }
        public string LocationName { get; set; }
    }

    public class SendCourseRegister
    {

        public string Fullname { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string Address { set; get; }
        public string Content { set; get; }
        public string FileAttach { set; get; }
        public int CoursesID { set; get; }
        public int UserID { set; get; }
        public byte Type { set; get; } //--1:đăng ký trực tuyến, 2:đăng ký tư vấn (khóa ngắn)
        public string CreatedUser { set; get; }
        public string ClientIP { set; get; }
        public string CourseName { set; get; }
    }

    //ServiceID = 10001
    //ServiceKey = S1Z8HY0JVDM4HAOHUIHG
    public class RequestOrder
    {
        public int ServiceId { set; get; }
        public string ServiceKey { set; get; }
        public string Products { set; get; }
        public int CourseId { set; get; }
        public int UserId { set; get; }
        public int Amount { set; get; }
        public string OrderInfo { set; get; }
        public string ClientIP { set; get; }
        

    }



    public class RequestConfirmOrder
    {
        public long OrderID { set; get; }
        public byte Status { set; get; }
        public string TransRefID { set; get; }
        public string BankCode { set; get; }
        public string BankTransNo { set; get; }
        public string CardType { set; get; }
        public decimal PayDate { set; get; }
        public string PayResponseCode { set; get; }
        public string TmnCode { set; get; }
    }

    public class RequestPayment
    {
        public int CoursesID { set; get; }
        public int RegisterFee { set; get; }
        public int SGK { set; get; }
        public int SemesterFee1 { set; get; }
        public int SemesterFee2 { set; get; }
        public int YearFee { set; get; }
        public int TuitionFee { set; get; }
        public byte CourseType { set; get; }
    }

    public class Order
    {
        public long OrderId { set; get; }
        public int CourseId { set; get; }
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
    }

    public class RequestContactEducation
    {
        [Required]
        public string Fullname { set; get; }
        [Required]
        public string Email { set; get; }
        [Required]
        public string Phone { set; get; }
        public string Address { set; get; }
        public string Content { set; get; }
        public byte LevelType { set; get; } //Bằng cấp  1:Sau ĐH, 2: ĐH, 3:Cao đẳng, 4:Trung cấp, 5:THPT
        public byte LevelEnglish { set; get; }//trình độ TA - 1:TOEIC, 2:TOEFL, 3:IELTS
        public short EnglishPoint { set; get; }//điểm tiếng anh
        public string CertificateOther { set; get; } //-chứng chỉ khác
        public short CertificatePoint { set; get; } //--Số điểm tương ứng chứng chỉ
        public string LangOther { set; get; } //--Ngôn ngữ khác
        public string LangCertificateName { set; get; }//--Tên chứng chỉ ngôn ngữ #
        public short LangPoint { set; get; }// --Số điểm tương ứng với chứng chỉ ngôn ngữ #
        public short CourseLevel { set; get; }//--Bậc học 1:Chứng chỉ, 2:Cử nhân,3:Kỹ sư,4:Thạc sĩ,5:Tiến sĩ
        public int SpecialityID { set; get; }//-ngành học
        public int EducationID { set; get; }
        public DateTime EstimatedTime { set; get; }//--thời gian dự kiến nhập học
        public string EstimatedTimeString { set; get; }//MM/yyyy
        public int UserID { set; get; }
        public bool IsReceiveInfo { set; get; }//--có nhận thông tin ko
        public bool IsBonus { set; get; }//-có nhận thưởng khi tham gia khao sát ko
        public string ClientIP { set; get; }
        public string CourseName { set; get; }


    }


    public class RequestGetRate
    {

        public long RateID { set; get; }
        public DateTime FromDate { set; get; }
        public DateTime ToDate { set; get; }
        public int TargetID { set; get; }
        public byte Type { set; get; }
    }

    public class UserRate
    {
        public long RateID { set; get; }
        public int TargetID { set; get; }
        public int UserID { set; get; }
        public string Username { set; get; }
        public byte UserType { set; get; }
        public string Fullname { set; get; }
        public string Avatar { set; get; }
        public int NumRate { set; get; }
        public byte Type { set; get; }
        public string Comment { set; get; }
        public DateTime CreatedDate { set; get; }
    }

    public class RequestGetOrder
    {
        public long OrderID { set; get; }
        public int UserID { set; get; }
        public byte Stautus { set; get; }
        public byte CourseType { set; get; }
        public string TransRefID { set; get; }
        public string BankCode { set; get; }
        public string BankTransNo { set; get; }
        public string PayResponseCode { set; get; }
        public DateTime CreatedFromDate { set; get; }
        public DateTime CreatedToDate { set; get; }
        public DateTime PayFromDate { set; get; }
        public DateTime PayToDate { set; get; }
        public int CurrentPage { set; get; }
        public int PageSize { set; get; }
    }

    public class OrderPayment
    {
        public long STT { set; get; }
        public long OrderId { set; get; }
        public int CourseId { set; get; }
        public string Title { set; get; }
        public byte CourseType { set; get; }
        public int UserId { set; get; }
        public string Username { set; get; }
        public int Amount { set; get; }
        public string BankCode { set; get; }
        public string BankTransNo { set; get; }
        public string CardType { set; get; }
        public DateTime CreatedDate { set; get; }
        public string CreatedDateFormat { set; get; }
        public string OrderInfo { set; get; }
        public decimal PayDate { set; get; }
        public string ResponseCode { set; get; }
        public DateTime ResponseDate { set; get; }
        public string TransRefID { set; get; }
        public int ServiceId { set; get; }
        public byte Status { set; get; }
        public string TmnCode { set; get; }
        public string SpecialityName { set; get; }
    }

    public class CompareCourse
    {
        public  string CourseID { get; set; }
        public  string Title { get; set; }
        public  string Alias { get; set; }
        public string ImageUrl { get; set; }

    }

    public class RequestCourseFavorite
    {
        public int FavoriteId { get; set; }
        public int CourseId { get; set; }
        public int UserId { get; set; }
    }
}
