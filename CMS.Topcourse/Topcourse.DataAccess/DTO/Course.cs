using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topcourse.DataAccess.DTO
{
    public class Course
    {

        public int SGK { set; get; }
        public int CoursesID { set; get; }
        public string Title { set; get; }
        public string TitleDisplay { set; get; }
        public string Alias { set; get; }
        public DateTime CreatedDate { set; get; }
        public string CreatedUser { set; get; }
        public int ViewCount { set; get; }
        public decimal NumRate { set; get; }
        public int NumComment { set; get; }
        public string PublishUser { set; get; }
        public DateTime UpdateDate { set; get; }
        public string UpdateUser { set; get; }
        public byte Status { set; get; }
        public string Description { set; get; }
        public string Content { set; get; }
        public int StudyForm { set; get; }
        public string StudyDuration { set; get; }
        public string TitlePromotion { set; get; }
        public DateTime ExpireDatePromotion { set; get; }
        public string ExpireDatePromotion_String { set; get; }
        public string DateOpen { set; get; }
        public string DateOpen_String { set; get; }
        public Int16 MonthOpen { set; get; }
        public Int16 StudyTime { set; get; }
        public Int16 LanguageInstruction { set; get; }
        public Int16 Level { set; get; }
        public int SpecialityID { set; get; }
        public string Lecturer { set; get; }
        public int NationalID { set; get; }
        public int LocationID { set; get; }
        public int DistrictID { set; get; }
        public int Tuition { set; get; } // Học phí công bố
        public int TuitionIncentives { set; get; } //Học phí ưu đãi
        public int HpEdunet { get; set; }  // Học phí Edunet
        public int UudaiEdunet { get; set; }  // Ưu đãi Edunet
        public int HpFirstDay { get; set; } // Học phí đóng sớm
        public int HpTvEdunet { set; get; }//Học phí thành viên Edunet
        public string NHname { set; get; } //Tên trường đại học ngắn hạn
        public bool IsCerfiticate { set; get; } // cap chung chi
        public string Doitacduhoc { set; get; }// đói tác du học
        public bool IsPractive { set; get; } // 
        public bool IsCommitWork { set; get; }
        public string TuitionInfo { set; get; } // Thông tin học phí
        public bool IsHot { set; get; }
        public bool IsHotHomePage { set; get; } // show homepage
        public bool IsFeatured { set; get; }  // khóa nổi bật

        public bool IsPayment { set; get; }  // Cho phép thanh toán
        public string ImageUrl { set; get; }
        public string BannerUrl { set; get; }
        public string AdmissionFile { get; set; }
        public byte CourseType { get; set; }
        public int TuitionPeriod1 { get; set; }  // phí học kì 1
        public int TuitionPeriod2 { get; set; } // phí học kì 2
        public int TuitionYear { get; set; }  // phí 1 năm
        public int RegisterFee { get; set; } // phí đăng ký, phí giữ chỗ
        public string Accreditation { get; set; }
        public byte SchoolType { get; set; }
        public string Address { get; set; }
        public bool IsUserEdit { get; set; }
        public byte StudyType { get; set; }
        public int EducationID { set; get; }
        public byte RequireAdmission { get; set; } //--yêu cầu nhập học(1:Thi tuyển, 2:xét tuyển hồ sơ, 3:Thi tuyển & xet tuyển hồ sơ)
        public DateTime PublishDate { get; set; }
        public decimal Rate { get; set; }
        public string SEODescription { set; get; } // Nội dung SEO
        public bool IsPin { set; get; } // Gim lên đầu bài
        public bool IsCheck { set; get; } // gắn thẻ Tag
    }


    public class CourseRequest
    {
        public bool IsHot { set; get; }
        public int SpecialityID { set; get; } // danh mục khóa học
        public byte Status { set; get; }
        public string CreatedUser { set; get; }
        public DateTime MonthInYear { set; get; }
        public byte CourseType { set; get; }
    }


    public class Course_Register // đk
    {
        public long Id { set; get; }
        public string Fullname { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string Address { set; get; }
        public string Content { set; get; }
        public string FileAttach { set; get; }
        public int CourseID { set; get; }
        public int UserID { set; get; }
        public DateTime CreatedDate { set; get; }
        public byte Type { set; get; }
        public string ClientIP { set; get; }
        public string CreatedUser { get; set; }
        public int CoursesID { set; get; }
    }

    public class AttributesDict
    {
        public int SortIndex { set; get; }
        public string TableName { set; get; }
        public string FieldName { set; get; }
        public int FieldValue { set; get; }
        public string Description { set; get; }
        public byte GroupType { set; get; }
    }


    public class Specility
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public int Order { set; get; }
        public string CreatedUser { set; get; }
        public byte CourseType { get; set; }
    }


    public class Scholarship
    {
        public int Id { get; set; }
        public string Title { get; set; } // tieu de 
        public string TitleDisplay { get; set; } // tieu de hien thi
        public string Alias { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Avatar { get; set; }
        public string Banner { get; set; }
        public string SchoolName { get; set; }
        public byte Type { get; set; }
        public int Rate { get; set; }
        public string Address { get; set; }
        public string PublishDate_String { set; get; }
        public DateTime PublishDate { get; set; }
        public string CreatedUser { get; set; }
        public int NumberView { set; get; }
        public int NumComment { set; get; }
        public int Status { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsCheck { get; set; } // Gắn thẻ Tag
        public string SEODescription { set; get; } // noi dung seo
    }



    public class TagGroup
    {
        public string TagGroupName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public byte GroupType { get; set; }
        public string CreatedUser { get; set; }
        public int TagGroupId { get; set; }
        public string ListTag { get; set; }
        public string ListTagID { set; get; }
        public int Order { set; get; }
        public byte Position { set; get; }
    }

    public class Tag
    {
        public string TagName { get; set; }
        public string TagKey { get; set; }
        public bool Status { get; set; }
        public string CreatedUser { get; set; }
        public int TagId { get; set; }
        public int TagGroupId { set; get; }
        public int Order { set; get; }
    }


    public class TagLinkedTarget
    {
        public int TargetId { set; get; }
        public string Title { set; get; }
        public int TagGroupId { get; set; }
        public int TagId { get; set; }
        public string ListTagetId { get; set; }
        public string CreatedUser { get; set; }
        public bool IsCheck { set; get; }
    }

    public class Education_Contact
    {
        public long Id { set; get; }
        public string Fullname { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string Address { set; get; }
        public string Content { set; get; }
        public byte LevelType { set; get; }
        public byte LevelEnglish { set; get; }
        public short EnglishPoint { set; get; }
        public string CertificateOther { set; get; }
        public short CertificatePoint { set; get; }
        public string LangOther { set; get; }
        public string LangCertificateName { set; get; }
        public short LangPoint { set; get; }
        public short CourseLevel { set; get; }
        public int SpecialityID { set; get; }
        public int EducationID { set; get; }
        public DateTime EstimatedTime { set; get; }
        public bool IsReceiveInfo { set; get; }
        public bool IsBonus { set; get; }
        public string UpdateUser { set; get; }
        public string ClientIP { set; get; }
        public string CreatedUser { get; set; }
        public DateTime CreatedDate { set; get; }
    }


    public class RegisterEdu_Request
    {
        public long LogID { set; get; }
        public string EduNameVI { set; get; }
        public string EduNameEn { set; get; }
        public string ShortName { set; get; }

        public string HeadOffice { set; get; }
        public string Ward { set; get; }
        public string District { set; get; }
        public string Location { set; get; }
        public string National { set; get; }
        public string EduFax { set; get; }
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
        public string FileAttach { set; get; }
        public string ClientIP { set; get; }
        public DateTime CreatedTime { set; get; }
        public string EduPhone { set; get; }
        public string EduEmail { set; get; }
        public string Phone { set; get; }
        public string Email { set; get; }
    }

    public class EduDetail_Request
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
