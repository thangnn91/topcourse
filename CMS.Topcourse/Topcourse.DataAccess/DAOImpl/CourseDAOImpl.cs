using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBHelpers;
using Topcourse.DataAccess.DAO;
using Topcourse.DataAccess.DTO;
using Topcourse.Utility;

namespace Topcourse.DataAccess.DAOImpl
{
    public class CourseDAOImpl : ICourseDAO
    {
        private string CoreDBConnectionString = "Server=103.130.212.9;uid=cuongbx;password=Aa@123456;database=Topcourse.CoreDB;";

        // Insert course
        public int Insert_Course(Course requestData)
        {
            try
            {
                var pars = new SqlParameter[55];
                pars[0] = new SqlParameter("@_Title", string.IsNullOrEmpty(requestData.Title) ? DBNull.Value : (object)requestData.Title);
                pars[1] = new SqlParameter("@_TitleDisplay", string.IsNullOrEmpty(requestData.TitleDisplay) ? DBNull.Value : (object)requestData.TitleDisplay);
                pars[2] = new SqlParameter("@_Alias", string.IsNullOrEmpty(requestData.Alias) ? DBNull.Value : (object)requestData.Alias);
                pars[3] = new SqlParameter("@_Description", string.IsNullOrEmpty(requestData.Description) ? DBNull.Value : (object)requestData.Description); //--Mô tả 6 ô dạng (Img1,Text1;Img2,Text2;....)
                pars[4] = new SqlParameter("@_Content", string.IsNullOrEmpty(requestData.Content) ? DBNull.Value : (object)requestData.Content);
                pars[5] = new SqlParameter("@_StudyForm", requestData.StudyForm); //--Hình thức học
                pars[6] = new SqlParameter("@_StudyDuration", string.IsNullOrEmpty(requestData.StudyDuration) ? DBNull.Value : (object)requestData.StudyDuration); // --thời lượng
                pars[7] = new SqlParameter("@_StudyTime", requestData.StudyTime); //--1:Trong giờ hành chính, 2:ngoài giờ hành chính, 3:trong tuần, 4: cuối tuần
                pars[8] = new SqlParameter("@_StudyType", requestData.StudyType); // -Loại hình đào tạo - 1:Trong nước, 2:Liên kết, 3:Du học
                pars[9] = new SqlParameter("@_ExpireDatePromotion", requestData.ExpireDatePromotion == new DateTime() ? DBNull.Value : (object)requestData.ExpireDatePromotion);  //  --thời gian hết KM
                pars[10] = new SqlParameter("@_DateOpen", requestData.DateOpen);  //  --Ngày khai giảng
                pars[11] = new SqlParameter("@_LanguageInstruction", requestData.LanguageInstruction); // --ngôn ngữ giảng dạy
                pars[12] = new SqlParameter("@_Level", requestData.Level);  // --TRÌNH ĐỘ BẮT ĐẦU (Bậc học)
                pars[13] = new SqlParameter("@_SpecialityID", requestData.SpecialityID);  //  --Danh mục
                pars[14] = new SqlParameter("@_Lecturer", string.IsNullOrEmpty(requestData.Lecturer) ? DBNull.Value : (object)requestData.Lecturer);  //  --GIảng viên
                pars[15] = new SqlParameter("@_LocationID", requestData.LocationID);
                pars[16] = new SqlParameter("@_DistrictID", requestData.DistrictID);
                pars[17] = new SqlParameter("@_NationalID", requestData.NationalID);
                pars[18] = new SqlParameter("@_Tuition", requestData.Tuition);  //  --học phí
                pars[19] = new SqlParameter("@_TuitionIncentives", requestData.TuitionIncentives); // --học phí ưu đãi
                pars[20] = new SqlParameter("@_TuitionInfo", string.IsNullOrEmpty(requestData.TuitionInfo) ? DBNull.Value : (object)requestData.TuitionInfo);  // --Thông tin học phí
                pars[21] = new SqlParameter("@_Cerfiticate", requestData.IsCerfiticate);  // --có cấp chứng nhận ko ?
                pars[22] = new SqlParameter("@_Practive", requestData.IsPractive);  // --có thực tập ko ?
                pars[23] = new SqlParameter("@_CommitWork", requestData.IsCommitWork);  // --có cam kết việc làm ?
                pars[24] = new SqlParameter("@_IsHot", requestData.IsHot);
                pars[25] = new SqlParameter("@_IsHotHomePage", requestData.IsHotHomePage);
                pars[26] = new SqlParameter("@_IsFeatured", requestData.IsFeatured);
                pars[27] = new SqlParameter("@_IsPayment", requestData.IsPayment);
                pars[28] = new SqlParameter("@_IsUserEdit", requestData.IsUserEdit);
                pars[29] = new SqlParameter("@_Address", string.IsNullOrEmpty(requestData.Address) ? DBNull.Value : (object)requestData.Address);  // --địa chỉ đào tạo
                pars[30] = new SqlParameter("@_SchoolType", requestData.SchoolType);    // --Loại trg - 1:công lập, 2:Bán công, 3:Dân lập, 4:Tư thục
                pars[31] = new SqlParameter("@_ImageUrl", string.IsNullOrEmpty(requestData.ImageUrl) ? DBNull.Value : (object)requestData.ImageUrl);  // --ảnh đai diện
                pars[32] = new SqlParameter("@_BannerUrl", string.IsNullOrEmpty(requestData.BannerUrl) ? DBNull.Value : (object)requestData.BannerUrl);
                pars[33] = new SqlParameter("@_Accreditation", string.IsNullOrEmpty(requestData.Accreditation) ? DBNull.Value : (object)requestData.Accreditation);  //  --kiểm định
                pars[34] = new SqlParameter("@_RegisterFee", requestData.RegisterFee);   // --Phí ghi danh (giữ chỗ)
                pars[35] = new SqlParameter("@_SGK", requestData.SGK);
                pars[36] = new SqlParameter("@_TuitionPeriod1", requestData.TuitionPeriod1);  // --Phí học kỳ 1
                pars[37] = new SqlParameter("@_TuitionPeriod2", requestData.TuitionPeriod2); // --Phí học kỳ 2
                pars[38] = new SqlParameter("@_TuitionYear", requestData.TuitionYear);   // --Phí 1 năm
                pars[39] = new SqlParameter("@_AdmissionFile", string.IsNullOrEmpty(requestData.AdmissionFile) ? DBNull.Value : (object)requestData.AdmissionFile);  // --Hồ sơ nhập học
                pars[40] = new SqlParameter("@_CourseType", requestData.CourseType);    // --Loại khóa 1:Khóa ngắn, 2:Khóa dài
                pars[41] = new SqlParameter("@_EducationID", requestData.EducationID);  //  --Mã cơ sở đào tạo
                pars[42] = new SqlParameter("@_RequireAdmission", requestData.RequireAdmission);  // --yêu cầu nhập học (1:Thi tuyển, 2:xét tuyển hồ sơ, 3:Thi tuyển & xet tuyển hồ sơ)
                pars[43] = new SqlParameter("@_Rate", requestData.Rate);
                pars[44] = new SqlParameter("@_PublishDate", requestData.PublishDate);  // Ngày xuất bản - ngày đăng
                pars[45] = new SqlParameter("@_CreatedUser", string.IsNullOrEmpty(requestData.CreatedUser) ? DBNull.Value : (object)requestData.CreatedUser);
                pars[46] = new SqlParameter("@_SEODescription", string.IsNullOrEmpty(requestData.SEODescription) ? DBNull.Value : (object)requestData.SEODescription);
                pars[47] = new SqlParameter("@_IsPin", requestData.IsPin);
                pars[48] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                pars[49] = new SqlParameter("@_HpEdunet", requestData.HpEdunet);
                pars[50] = new SqlParameter("@_HpFirstDay", requestData.HpFirstDay);
                pars[51] = new SqlParameter("@_HpTvEdunet", requestData.HpTvEdunet);
                pars[52] = new SqlParameter("@_NHname", requestData.NHname);
                pars[53] = new SqlParameter("@_Doitacduhoc", requestData.Doitacduhoc);
                pars[54] = new SqlParameter("@_UudaiEdunet", requestData.UudaiEdunet);
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_Course_Insert", pars);
                return Convert.ToInt32(pars[48].Value);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return -696;
            }
        }

        public int Update_Course(Course requestData)
        {
            try
            {
                var pars = new SqlParameter[55];
                pars[0] = new SqlParameter("@_CourseID", requestData.CoursesID);
                pars[1] = new SqlParameter("@_Title", string.IsNullOrEmpty(requestData.Title) ? DBNull.Value : (object)requestData.Title);
                pars[2] = new SqlParameter("@_TitleDisplay", string.IsNullOrEmpty(requestData.TitleDisplay) ? DBNull.Value : (object)requestData.TitleDisplay);
                pars[3] = new SqlParameter("@_Alias", string.IsNullOrEmpty(requestData.Alias) ? DBNull.Value : (object)requestData.Alias);
                pars[4] = new SqlParameter("@_Description", string.IsNullOrEmpty(requestData.Description) ? DBNull.Value : (object)requestData.Description);
                pars[5] = new SqlParameter("@_StudyForm", requestData.StudyForm);
                pars[6] = new SqlParameter("@_StudyDuration", string.IsNullOrEmpty(requestData.StudyDuration) ? DBNull.Value : (object)requestData.StudyDuration);
                pars[7] = new SqlParameter("@_StudyTime", requestData.StudyTime);
                pars[8] = new SqlParameter("@_StudyType", requestData.StudyType);
                pars[9] = new SqlParameter("@_ExpireDatePromotion", requestData.ExpireDatePromotion == new DateTime() ? DBNull.Value : (object)requestData.ExpireDatePromotion);
                pars[10] = new SqlParameter("@_DateOpen", requestData.DateOpen);
                pars[11] = new SqlParameter("@_LanguageInstruction", requestData.LanguageInstruction);
                pars[12] = new SqlParameter("@_Level", requestData.Level);
                pars[13] = new SqlParameter("@_SpecialityID", requestData.SpecialityID);
                pars[14] = new SqlParameter("@_Lecturer", string.IsNullOrEmpty(requestData.Lecturer) ? DBNull.Value : (object)requestData.Lecturer);
                pars[15] = new SqlParameter("@_LocationID", requestData.LocationID);
                pars[16] = new SqlParameter("@_DistrictID", requestData.DistrictID);
                pars[17] = new SqlParameter("@_NationalID", requestData.NationalID);
                pars[18] = new SqlParameter("@_Tuition", requestData.Tuition);
                pars[19] = new SqlParameter("@_TuitionIncentives", requestData.TuitionIncentives);
                pars[20] = new SqlParameter("@_TuitionInfo", string.IsNullOrEmpty(requestData.TuitionInfo) ? DBNull.Value : (object)requestData.TuitionInfo);
                pars[21] = new SqlParameter("@_Cerfiticate", requestData.IsCerfiticate);
                pars[22] = new SqlParameter("@_Practive", requestData.IsPractive);
                pars[23] = new SqlParameter("@_CommitWork", requestData.IsCommitWork);
                pars[24] = new SqlParameter("@_IsHot", requestData.IsHot);
                pars[25] = new SqlParameter("@_IsHotHomePage", requestData.IsHotHomePage);
                pars[26] = new SqlParameter("@_IsFeatured", requestData.IsFeatured);
                pars[27] = new SqlParameter("@_IsPayment", requestData.IsPayment);
                pars[28] = new SqlParameter("@_IsUserEdit", requestData.IsUserEdit);
                pars[29] = new SqlParameter("@_Address", string.IsNullOrEmpty(requestData.Address) ? DBNull.Value : (object)requestData.Address);
                pars[30] = new SqlParameter("@_SchoolType", requestData.SchoolType);
                pars[31] = new SqlParameter("@_ImageUrl", string.IsNullOrEmpty(requestData.ImageUrl) ? DBNull.Value : (object)requestData.ImageUrl);
                pars[32] = new SqlParameter("@_BannerUrl", string.IsNullOrEmpty(requestData.BannerUrl) ? DBNull.Value : (object)requestData.BannerUrl);
                pars[33] = new SqlParameter("@_Accreditation", string.IsNullOrEmpty(requestData.Accreditation) ? DBNull.Value : (object)requestData.Accreditation);
                pars[34] = new SqlParameter("@_RegisterFee", requestData.RegisterFee);
                pars[35] = new SqlParameter("@_SGK", requestData.SGK);
                pars[36] = new SqlParameter("@_TuitionPeriod1", requestData.TuitionPeriod1);  // --Phí học kỳ 1
                pars[37] = new SqlParameter("@_TuitionPeriod2", requestData.TuitionPeriod2); // --Phí học kỳ 2
                pars[38] = new SqlParameter("@_TuitionYear", requestData.TuitionYear);   // --Phí 1 năm
                pars[39] = new SqlParameter("@_AdmissionFile", string.IsNullOrEmpty(requestData.AdmissionFile) ? DBNull.Value : (object)requestData.AdmissionFile);
                pars[40] = new SqlParameter("@_EducationID", requestData.EducationID);
                pars[41] = new SqlParameter("@_RequireAdmission", requestData.RequireAdmission);
                pars[42] = new SqlParameter("@_Content", string.IsNullOrEmpty(requestData.Content) ? DBNull.Value : (object)requestData.Content);
                pars[43] = new SqlParameter("@_Rate", requestData.Rate);
                pars[44] = new SqlParameter("@_UpdateUser", string.IsNullOrEmpty(requestData.UpdateUser) ? DBNull.Value : (object)requestData.UpdateUser);
                pars[45] = new SqlParameter("@_PublishDate", requestData.PublishDate);
                pars[46] = new SqlParameter("@_SEODescription", string.IsNullOrEmpty(requestData.SEODescription) ? DBNull.Value : (object)requestData.SEODescription);
                pars[47] = new SqlParameter("@_IsPin", requestData.IsPin);
                pars[48] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                pars[49] = new SqlParameter("@_HpEdunet", requestData.HpEdunet);
                pars[50] = new SqlParameter("@_HpFirstDay", requestData.HpFirstDay);
                pars[51] = new SqlParameter("@_HpTvEdunet", requestData.HpTvEdunet);
                pars[52] = new SqlParameter("@_NHname", requestData.NHname);
                pars[53] = new SqlParameter("@_Doitacduhoc", requestData.Doitacduhoc);
                pars[54] = new SqlParameter("@_UudaiEdunet", requestData.UudaiEdunet);
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_Course_Update", pars);
                return Convert.ToInt32(pars[48].Value);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return -696;
            }
        }

        //SP_Course_Delete
        public int Delete_Course(int courseId, string createdUser)
        {
            try
            {
                var pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@_CourseID", courseId);
                pars[1] = new SqlParameter("@_CreatedUser", createdUser);
                pars[2] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_Course_Delete", pars);
                return Convert.ToInt32(pars[2].Value);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return -696;
            }
        }

        // Update status  --1:duyệt, 2:hạ, 3:tạm xóa
        public int UpdateStatus_Course(int courseId, int status, string actionUser)
        {
            try
            {
                var pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@_CourseID", courseId);
                pars[1] = new SqlParameter("@_Status", status);
                pars[2] = new SqlParameter("@_ActionUser", actionUser);
                pars[3] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_Course_UpdateStatus", pars);
                return Convert.ToInt32(pars[3].Value);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return -696;
            }
        }

        // get list
        public List<Course> GetListCourse(CourseRequest requestData)
        {
            try
            {
                var pars = new SqlParameter[6];
                pars[0] = new SqlParameter("@_IsHot", DBNull.Value);
                pars[1] = new SqlParameter("@_SpecialityID", requestData.SpecialityID <= 0 ? DBNull.Value : (object)requestData.SpecialityID);
                pars[2] = new SqlParameter("@_Status", requestData.Status <= 0 ? DBNull.Value : (object)requestData.Status);
                pars[3] = new SqlParameter("@_CourseType", requestData.CourseType);  //,--Loại hình đào tạo - 1:ngắn , 2 dài
                pars[4] = new SqlParameter("@_CreatedUser", requestData.CreatedUser);
                pars[5] = new SqlParameter("@_MonthInYear", DBNull.Value);
                return new DBHelper(CoreDBConnectionString).GetListSP<Course>("SP_CMS_Course_GetPage", pars);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return new List<Course>();
            }
        }

        // get info
        public Course GetInfo_Course(int courseId)
        {
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@_CourseID", courseId);
                pars[1] = new SqlParameter("@_Alias", DBNull.Value);
                return new DBHelper(CoreDBConnectionString).GetInstanceSP<Course>("SP_CMS_Course_GetDetail", pars);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return new Course();
            }
        }

        // Lấy các trường du lieu fix theo loại
        public List<AttributesDict> GetList_Attributes_Dict(string tablename, string fieldName, byte groupType)
        {
            try
            {
                var pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@_Tablename", string.IsNullOrEmpty(tablename) ? DBNull.Value : (object)tablename);
                pars[1] = new SqlParameter("@_FieldName", string.IsNullOrEmpty(fieldName) ? DBNull.Value : (object)fieldName);
                pars[2] = new SqlParameter("@_GroupType", groupType);
                return new DBHelper(CoreDBConnectionString).GetListSP<AttributesDict>("SP_Core_Attributes_Dict_Get", pars);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return new List<AttributesDict>();
            }
        }


        // danh mục -  ngành nghề
        public List<Specility> GetList_Specility(int id, byte courseType)
        {
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@_Id", id <= 0 ? DBNull.Value : (object)id);
                pars[1] = new SqlParameter("@_CourseType", courseType <= 0 ? DBNull.Value : (object)courseType);
                return new DBHelper(CoreDBConnectionString).GetListSP<Specility>("SP_Courses_Specility_Get", pars);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return new List<Specility>();
            }
        }

        public int Speciality_Insert_Update(Specility requestData)
        {
            try
            {
                var pars = new SqlParameter[7];
                pars[0] = new SqlParameter("@_Id", requestData.Id);
                pars[1] = new SqlParameter("@_Name", string.IsNullOrEmpty(requestData.Name) ? DBNull.Value : (object)requestData.Name);
                pars[2] = new SqlParameter("@_CourseType", requestData.CourseType);
                pars[3] = new SqlParameter("@_Description", string.IsNullOrEmpty(requestData.Description) ? DBNull.Value : (object)requestData.Description);
                pars[4] = new SqlParameter("@_Order", requestData.Order);
                pars[5] = new SqlParameter("@_CreatedUser", string.IsNullOrEmpty(requestData.CreatedUser) ? DBNull.Value : (object)requestData.CreatedUser);
                pars[6] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_Courses_Speciality_Insert_Update", pars);
                return Convert.ToInt32(pars[6].Value);
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(ex.ToString());
                return -696;
            }
        }

        public int Speciality_Delete(Specility requestData)
        {
            try
            {
                var pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@_Id", requestData.Id);
                pars[1] = new SqlParameter("@_CreatedUser", string.IsNullOrEmpty(requestData.CreatedUser) ? DBNull.Value : (object)requestData.CreatedUser);
                pars[2] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_Courses_Speciality_Delete", pars);
                return Convert.ToInt32(pars[2].Value);
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(ex.ToString());
                return -696;
            }
        }


        // học bổng
        public int Scholarship_Insert(Scholarship requestData)
        {
            try
            {
                var pars = new SqlParameter[15];
                pars[0] = new SqlParameter("@_Title", string.IsNullOrEmpty(requestData.Title) ? DBNull.Value : (object)requestData.Title);
                pars[1] = new SqlParameter("@_TitleDisplay", string.IsNullOrEmpty(requestData.TitleDisplay) ? DBNull.Value : (object)requestData.TitleDisplay);
                pars[2] = new SqlParameter("@_Alias", string.IsNullOrEmpty(requestData.Alias) ? DBNull.Value : (object)requestData.Alias);
                pars[3] = new SqlParameter("@_Description", string.IsNullOrEmpty(requestData.Description) ? DBNull.Value : (object)requestData.Description);
                pars[4] = new SqlParameter("@_Content", string.IsNullOrEmpty(requestData.Content) ? DBNull.Value : (object)requestData.Content);
                pars[5] = new SqlParameter("@_Avatar", string.IsNullOrEmpty(requestData.Avatar) ? DBNull.Value : (object)requestData.Avatar);
                pars[6] = new SqlParameter("@_Banner", string.IsNullOrEmpty(requestData.Banner) ? DBNull.Value : (object)requestData.Banner);
                pars[7] = new SqlParameter("@_SchoolName", string.IsNullOrEmpty(requestData.SchoolName) ? DBNull.Value : (object)requestData.SchoolName);
                pars[8] = new SqlParameter("@_Type", requestData.Type);
                pars[9] = new SqlParameter("@_Rate", requestData.Rate);
                pars[10] = new SqlParameter("@_Address", string.IsNullOrEmpty(requestData.Address) ? DBNull.Value : (object)requestData.Address);
                pars[11] = new SqlParameter("@_PublishDate", requestData.PublishDate);
                pars[12] = new SqlParameter("@_SEODescription", string.IsNullOrEmpty(requestData.SEODescription) ? DBNull.Value : (object)requestData.SEODescription);
                pars[13] = new SqlParameter("@_CreatedUser", string.IsNullOrEmpty(requestData.CreatedUser) ? DBNull.Value : (object)requestData.CreatedUser);
                pars[14] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_Scholarship_Insert", pars);
                return Convert.ToInt32(pars[14].Value);
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(ex.ToString());
                return -696;
            }
        }

        public int Scholarship_Update(Scholarship requestData)
        {
            try
            {
                var pars = new SqlParameter[16];
                pars[0] = new SqlParameter("@_Id", requestData.Id);
                pars[1] = new SqlParameter("@_Title", string.IsNullOrEmpty(requestData.Title) ? DBNull.Value : (object)requestData.Title);
                pars[2] = new SqlParameter("@_TitleDisplay", string.IsNullOrEmpty(requestData.TitleDisplay) ? DBNull.Value : (object)requestData.TitleDisplay);
                pars[3] = new SqlParameter("@_Alias", string.IsNullOrEmpty(requestData.Alias) ? DBNull.Value : (object)requestData.Alias);
                pars[4] = new SqlParameter("@_Description", string.IsNullOrEmpty(requestData.Description) ? DBNull.Value : (object)requestData.Description);
                pars[5] = new SqlParameter("@_Content", string.IsNullOrEmpty(requestData.Content) ? DBNull.Value : (object)requestData.Content);
                pars[6] = new SqlParameter("@_Avatar", string.IsNullOrEmpty(requestData.Avatar) ? DBNull.Value : (object)requestData.Avatar);
                pars[7] = new SqlParameter("@_Banner", string.IsNullOrEmpty(requestData.Banner) ? DBNull.Value : (object)requestData.Banner);
                pars[8] = new SqlParameter("@_SchoolName", string.IsNullOrEmpty(requestData.SchoolName) ? DBNull.Value : (object)requestData.SchoolName);
                pars[9] = new SqlParameter("@_Type", requestData.Type);
                pars[10] = new SqlParameter("@_Rate", requestData.Rate);
                pars[11] = new SqlParameter("@_PublishDate", requestData.PublishDate);
                pars[12] = new SqlParameter("@_Address", string.IsNullOrEmpty(requestData.Address) ? DBNull.Value : (object)requestData.Address);
                pars[13] = new SqlParameter("@_UpdateUser", string.IsNullOrEmpty(requestData.CreatedUser) ? DBNull.Value : (object)requestData.CreatedUser);
                pars[14] = new SqlParameter("@_SEODescription", string.IsNullOrEmpty(requestData.SEODescription) ? DBNull.Value : (object)requestData.SEODescription);
                pars[15] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_Scholarship_Update", pars);
                return Convert.ToInt32(pars[15].Value);
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(ex.ToString());
                return -696;
            }
        }

        public int Scholarship_Delete(Scholarship requestData)
        {
            try
            {
                var pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@_Id", requestData.Id);
                pars[1] = new SqlParameter("@_CreatedUser", string.IsNullOrEmpty(requestData.CreatedUser) ? DBNull.Value : (object)requestData.CreatedUser);
                pars[2] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_Scholarship_Delete", pars);
                return Convert.ToInt32(pars[2].Value);
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(ex.ToString());
                return -696;
            }
        }

        public List<Scholarship> Scholarship_GetPage(Scholarship requestData)
        {
            try
            {
                var pars = new SqlParameter[5];
                pars[0] = new SqlParameter("@_TextSearch", DBNull.Value);
                pars[1] = new SqlParameter("@_Type", requestData.Type <= 0 ? DBNull.Value : (object)requestData.Type);
                pars[2] = new SqlParameter("@_Status", requestData.Status <= 0 ? DBNull.Value : (object)requestData.Status);
                pars[3] = new SqlParameter("@_FromDate", requestData.FromDate == new DateTime() ? DBNull.Value : (object)requestData.FromDate);
                pars[4] = new SqlParameter("@_ToDate", requestData.ToDate == new DateTime() ? DBNull.Value : (object)requestData.ToDate);
                return new DBHelper(CoreDBConnectionString).GetListSP<Scholarship>("SP_CMS_Scholarship_GetPage", pars);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return new List<Scholarship>();
            }
        }

        public Scholarship Scholarship_Get_Similar(int id)
        {
            try
            {
                var pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@_Id", id);
                return new DBHelper(CoreDBConnectionString).GetInstanceSP<Scholarship>("SP_Scholarship_Get_Detail", pars);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return new Scholarship();
            }
        }
        // 



        // Tag Group
        public int TagGroup_Insert(TagGroup requestData)
        {
            try
            {
                var pars = new SqlParameter[8];
                pars[0] = new SqlParameter("@_TagGroupName", string.IsNullOrEmpty(requestData.TagGroupName) ? DBNull.Value : (object)requestData.TagGroupName);
                pars[1] = new SqlParameter("@_Description", string.IsNullOrEmpty(requestData.Description) ? DBNull.Value : (object)requestData.Description);
                pars[2] = new SqlParameter("@_IsActive", requestData.IsActive);
                pars[3] = new SqlParameter("@_GroupType", requestData.GroupType);
                pars[4] = new SqlParameter("@_ListTagId", requestData.ListTag);
                pars[5] = new SqlParameter("@_Position", requestData.Position);
                pars[6] = new SqlParameter("@_CreatedUser", string.IsNullOrEmpty(requestData.CreatedUser) ? DBNull.Value : (object)requestData.CreatedUser);
                pars[7] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_TagGroup_Insert", pars);
                return Convert.ToInt32(pars[7].Value);
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(ex.ToString());
                return -696;
            }
        }


        public int TagGroup_Update(TagGroup requestData)
        {
            try
            {
                var pars = new SqlParameter[9];
                pars[0] = new SqlParameter("@_TagGroupId", requestData.TagGroupId);
                pars[1] = new SqlParameter("@_TagGroupName", string.IsNullOrEmpty(requestData.TagGroupName) ? DBNull.Value : (object)requestData.TagGroupName);
                pars[2] = new SqlParameter("@_Description", string.IsNullOrEmpty(requestData.Description) ? DBNull.Value : (object)requestData.Description);
                pars[3] = new SqlParameter("@_IsActive", requestData.IsActive);
                pars[4] = new SqlParameter("@_ListTagId", requestData.ListTag);
                pars[5] = new SqlParameter("@_GroupType", requestData.GroupType);
                pars[6] = new SqlParameter("@_Position", requestData.Position);
                pars[7] = new SqlParameter("@_CreatedUser", string.IsNullOrEmpty(requestData.CreatedUser) ? DBNull.Value : (object)requestData.CreatedUser);
                pars[8] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_TagGroup_Update", pars);
                return Convert.ToInt32(pars[8].Value);
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(ex.ToString());
                return -696;
            }
        }

        public int SP_TagGroup_Delete(TagGroup requestData)
        {
            try
            {
                var pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@_TagGroupId", requestData.TagGroupId);
                pars[1] = new SqlParameter("@_CreatedUser", string.IsNullOrEmpty(requestData.CreatedUser) ? DBNull.Value : (object)requestData.CreatedUser);
                pars[2] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_TagGroup_Delete", pars);
                return Convert.ToInt32(pars[2].Value);
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(ex.ToString());
                return -696;
            }
        }
        public int SP_TagGroup_UpdateOrder(TagGroup requestData)
        {
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@_TagGroupId", requestData.TagGroupId);
                pars[1] = new SqlParameter("@_Order", requestData.Order);
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_TagGroup_UpdateOrder", pars);
                return 1;
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(ex.ToString());
                return -696;
            }
        }


        //
        public List<TagGroup> TagGroup_Get(TagGroup requestData)
        {
            try
            {
                var pars = new SqlParameter[5];
                pars[0] = new SqlParameter("@_TagGroupId", requestData.TagGroupId <= 0 ? DBNull.Value : (object)requestData.TagGroupId);
                pars[1] = new SqlParameter("@_TagGroupName", string.IsNullOrEmpty(requestData.TagGroupName) ? DBNull.Value : (object)requestData.TagGroupName);
                pars[2] = new SqlParameter("@_IsActive", DBNull.Value);
                pars[3] = new SqlParameter("@_GroupType", requestData.GroupType <= 0 ? DBNull.Value : (object)requestData.GroupType);
                pars[4] = new SqlParameter("@_Position", requestData.Position == 99 ? DBNull.Value: (object)requestData.Position);
                return new DBHelper(CoreDBConnectionString).GetListSP<TagGroup>("SP_TagGroup_Get", pars);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return new List<TagGroup>();
            }
        }

        //Tag
        public int Tag_Insert(Tag requestData)
        {
            try
            {
                var pars = new SqlParameter[5];
                pars[0] = new SqlParameter("@_TagName", string.IsNullOrEmpty(requestData.TagName) ? DBNull.Value : (object)requestData.TagName);
                pars[1] = new SqlParameter("@_TagKey", string.IsNullOrEmpty(requestData.TagKey) ? DBNull.Value : (object)requestData.TagKey);
                pars[2] = new SqlParameter("@_Status", requestData.Status);
                pars[3] = new SqlParameter("@_CreatedUser", string.IsNullOrEmpty(requestData.CreatedUser) ? DBNull.Value : (object)requestData.CreatedUser);
                pars[4] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_Tag_Insert", pars);
                return Convert.ToInt32(pars[4].Value);
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(ex.ToString());
                return -696;
            }
        }

        public int SP_Tag_Update(Tag requestData)
        {
            try
            {
                var pars = new SqlParameter[6];
                pars[0] = new SqlParameter("@_TagId", requestData.TagId);
                pars[1] = new SqlParameter("@_TagName", string.IsNullOrEmpty(requestData.TagName) ? DBNull.Value : (object)requestData.TagName);
                pars[2] = new SqlParameter("@_TagKey", string.IsNullOrEmpty(requestData.TagKey) ? DBNull.Value : (object)requestData.TagKey);
                pars[3] = new SqlParameter("@_Status", requestData.Status);
                pars[4] = new SqlParameter("@_CreatedUser", string.IsNullOrEmpty(requestData.CreatedUser) ? DBNull.Value : (object)requestData.CreatedUser);
                pars[5] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_Tag_Update", pars);
                return Convert.ToInt32(pars[5].Value);
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(ex.ToString());
                return -696;
            }
        }

        public int SP_Tag_Delete(Tag requestData)
        {
            try
            {
                var pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@_TagId", requestData.TagId);
                pars[1] = new SqlParameter("@_CreatedUser", string.IsNullOrEmpty(requestData.CreatedUser) ? DBNull.Value : (object)requestData.CreatedUser);
                pars[2] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_Tag_Delete", pars);
                return Convert.ToInt32(pars[2].Value);
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(ex.ToString());
                return -696;
            }
        }
        public List<Tag> Tag_Get(Tag requestData)
        {
            try
            {
                var pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@_TagGroupId", requestData.TagGroupId <= 0 ? DBNull.Value : (object)requestData.TagGroupId);
                pars[1] = new SqlParameter("@_TagId", requestData.TagId <= 0 ? DBNull.Value : (object)requestData.TagId);
                pars[2] = new SqlParameter("@_TagName", string.IsNullOrEmpty(requestData.TagName) ? DBNull.Value : (object)requestData.TagName);
                pars[3] = new SqlParameter("@_Status", requestData.Status);
                return new DBHelper(CoreDBConnectionString).GetListSP<Tag>("SP_Tag_Get", pars);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return new List<Tag>();
            }
        }

        public int SP_Tag_UpdateOrder(Tag requestData)
        {
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@_TagId", requestData.TagId);
                pars[1] = new SqlParameter("@_Order", requestData.Order);
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_Tag_UpdateOrder", pars);
                return 1;
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(ex.ToString());
                return -696;
            }
        }

        // Gắn tag vs bài 
        public int TagLinkedTarget_Insert(TagLinkedTarget requestData)
        {
            try
            {
                var pars = new SqlParameter[5];
                pars[0] = new SqlParameter("@_TagGroupId", requestData.TagGroupId);
                pars[1] = new SqlParameter("@_TagId", requestData.TagId);
                pars[2] = new SqlParameter("@_ListTagetId", string.IsNullOrEmpty(requestData.ListTagetId) ? DBNull.Value : (object)requestData.ListTagetId);
                pars[3] = new SqlParameter("@_CreatedUser", string.IsNullOrEmpty(requestData.CreatedUser) ? DBNull.Value : (object)requestData.CreatedUser);
                pars[4] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_TagLinkedTarget_Insert", pars);
                return Convert.ToInt32(pars[4].Value);
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(ex.ToString());
                return -696;
            }
        }

        public List<TagLinkedTarget> TagLinkedTarget_Get(TagLinkedTarget request)
        {
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@_TagGroupId", request.TagGroupId);
                pars[1] = new SqlParameter("@_TagId", request.TagId);
                return new DBHelper(CoreDBConnectionString).GetListSP<TagLinkedTarget>("SP_TagLinkedTarget_Get", pars);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return new List<TagLinkedTarget>();
            }
        }



        // Đk trực tuyến
        public List<Course_Register> Course_Register_Get(DateTime fromDate, DateTime toDate, byte type)
        {
            try
            {
                var pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@_FromDate", fromDate == new DateTime() ? DBNull.Value : (object)fromDate);
                pars[1] = new SqlParameter("@_ToDate", toDate == new DateTime() ? DBNull.Value : (object)toDate);
                pars[2] = new SqlParameter("@_Type", type <= 0 ? DBNull.Value : (object)type); //--1:đăng ký trực tuyến, 2:đăng ký tư vấn (khóa ngắn)
                return new DBHelper(CoreDBConnectionString).GetListSP<Course_Register>("SP_Course_Register_Get", pars);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return new List<Course_Register>();
            }
        }

        public Course_Register Course_Register_Get_Detail(int id)
        {
            try
            {
                var pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@_Id", id);
                return new DBHelper(CoreDBConnectionString).GetInstanceSP<Course_Register>("SP_Course_Register_Get_Detail", pars);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return new Course_Register();
            }
        }

        //
        public int SP_Course_Register_Update(Course_Register requestData)
        {
            try
            {
                var pars = new SqlParameter[10];
                pars[0] = new SqlParameter("@_Id", requestData.Id);
                pars[1] = new SqlParameter("@_Fullname", string.IsNullOrEmpty(requestData.Fullname) ? DBNull.Value : (object)requestData.Fullname);
                pars[2] = new SqlParameter("@_Email", string.IsNullOrEmpty(requestData.Email) ? DBNull.Value : (object)requestData.Email);
                pars[3] = new SqlParameter("@_Phone", string.IsNullOrEmpty(requestData.Phone) ? DBNull.Value : (object)requestData.Phone);
                pars[4] = new SqlParameter("@_Address", string.IsNullOrEmpty(requestData.Address) ? DBNull.Value : (object)requestData.Address);
                pars[5] = new SqlParameter("@_Content", string.IsNullOrEmpty(requestData.Content) ? DBNull.Value : (object)requestData.Content);
                pars[6] = new SqlParameter("@_CoursesID", requestData.CoursesID);
                pars[7] = new SqlParameter("@_CreatedUser", string.IsNullOrEmpty(requestData.CreatedUser) ? DBNull.Value : (object)requestData.CreatedUser);
                pars[8] = new SqlParameter("@_ClientIP", string.IsNullOrEmpty(requestData.ClientIP) ? DBNull.Value : (object)requestData.ClientIP);
                pars[9] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_Course_Register_Update", pars);
                return Convert.ToInt32(pars[9].Value);
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(ex.ToString());
                return -696;
            }
        }

        //
        public int Course_Register_Delete(Course_Register requestData)
        {
            try
            {
                var pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@_Id", requestData.Id);
                pars[1] = new SqlParameter("@_CreatedUser", string.IsNullOrEmpty(requestData.CreatedUser) ? DBNull.Value : (object)requestData.CreatedUser);
                pars[2] = new SqlParameter("@_ClientIP", string.IsNullOrEmpty(requestData.ClientIP) ? DBNull.Value : (object)requestData.ClientIP);
                pars[3] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_Course_Register_Delete", pars);
                return Convert.ToInt32(pars[3].Value);
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(ex.ToString());
                return -696;
            }
        }


        // Liên hệ cơ sở - Trường
        public int Education_Contact_Update(Education_Contact requestData)
        {
            try
            {
                var pars = new SqlParameter[23];
                pars[0] = new SqlParameter("@_Id", requestData.Id);
                pars[1] = new SqlParameter("@_Fullname", string.IsNullOrEmpty(requestData.Fullname) ? DBNull.Value : (object)requestData.Fullname);
                pars[2] = new SqlParameter("@_Email", string.IsNullOrEmpty(requestData.Email) ? DBNull.Value : (object)requestData.Email);
                pars[3] = new SqlParameter("@_Phone", string.IsNullOrEmpty(requestData.Phone) ? DBNull.Value : (object)requestData.Phone);
                pars[4] = new SqlParameter("@_Address", string.IsNullOrEmpty(requestData.Address) ? DBNull.Value : (object)requestData.Address);
                pars[5] = new SqlParameter("@_Content", string.IsNullOrEmpty(requestData.Content) ? DBNull.Value : (object)requestData.Content);
                pars[6] = new SqlParameter("@_LevelType", requestData.LevelType);
                pars[7] = new SqlParameter("@_LevelEnglish", requestData.LevelEnglish);
                pars[8] = new SqlParameter("@_EnglishPoint", requestData.EnglishPoint);
                pars[9] = new SqlParameter("@_CertificateOther", string.IsNullOrEmpty(requestData.CertificateOther) ? DBNull.Value : (object)requestData.CertificateOther);
                pars[10] = new SqlParameter("@_CertificatePoint", requestData.CertificatePoint);
                pars[11] = new SqlParameter("@_LangOther", string.IsNullOrEmpty(requestData.LangOther) ? DBNull.Value : (object)requestData.LangOther);
                pars[12] = new SqlParameter("@_LangCertificateName", string.IsNullOrEmpty(requestData.LangCertificateName) ? DBNull.Value : (object)requestData.LangCertificateName);
                pars[13] = new SqlParameter("@_LangPoint", requestData.LangPoint);
                pars[14] = new SqlParameter("@_CourseLevel", requestData.CourseLevel);
                pars[15] = new SqlParameter("@_SpecialityID", requestData.SpecialityID);
                pars[16] = new SqlParameter("@_EducationID", requestData.EducationID);
                pars[17] = new SqlParameter("@_EstimatedTime", requestData.EstimatedTime);
                pars[18] = new SqlParameter("@_IsReceiveInfo", requestData.IsReceiveInfo);
                pars[19] = new SqlParameter("@_IsBonus", requestData.IsBonus);
                pars[20] = new SqlParameter("@_UpdateUser", string.IsNullOrEmpty(requestData.UpdateUser) ? DBNull.Value : (object)requestData.UpdateUser);
                pars[21] = new SqlParameter("@_ClientIP", string.IsNullOrEmpty(requestData.ClientIP) ? DBNull.Value : (object)requestData.ClientIP);
                pars[22] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_Education_Contact_Update", pars);
                return Convert.ToInt32(pars[22].Value);
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(ex.ToString());
                return -696;
            }
        }

        public List<Education_Contact> Education_Contact_Get(DateTime fromDate, DateTime toDate, int educationId, int specialityId, DateTime estimateTimeFrom, DateTime estimateTimeTo)
        {
            try
            {
                var pars = new SqlParameter[7];
                pars[0] = new SqlParameter("@_FromDate", fromDate == new DateTime() ? DBNull.Value : (object)fromDate);
                pars[1] = new SqlParameter("@_ToDate", toDate == new DateTime() ? DBNull.Value : (object)toDate);
                pars[2] = new SqlParameter("@_EducationID", educationId <= 0 ? DBNull.Value : (object)educationId); 
                pars[3] = new SqlParameter("@_SpecialityID", specialityId <= 0 ? DBNull.Value : (object)specialityId);
                pars[4] = new SqlParameter("@_IsBonus", DBNull.Value);
                pars[5] = new SqlParameter("@_EstimateTimeFrom", estimateTimeFrom == new DateTime() ? DBNull.Value : (object)estimateTimeFrom);
                pars[6] = new SqlParameter("@_EstimateTimeTo", estimateTimeTo == new DateTime() ? DBNull.Value : (object)estimateTimeTo);
                return new DBHelper(CoreDBConnectionString).GetListSP<Education_Contact>("SP_Education_Contact_Get", pars);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return new List<Education_Contact>();
            }
        }

        public Education_Contact Education_Contact_Get_Detail(int id)
        {
            try
            {
                var pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@_Id", id);
                return new DBHelper(CoreDBConnectionString).GetInstanceSP<Education_Contact>("SP_Education_Contact_Get_Detail", pars);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return new Education_Contact();
            }
        }

        public int Education_Contact_Delete(Education_Contact requestData)
        {
            try
            {
                var pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@_Id", requestData.Id);
                pars[1] = new SqlParameter("@_CreatedUser", string.IsNullOrEmpty(requestData.CreatedUser) ? DBNull.Value : (object)requestData.CreatedUser);
                pars[2] = new SqlParameter("@_ClientIP", Config.GetIP());
                pars[3] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(CoreDBConnectionString).ExecuteNonQuerySP("SP_Education_Contact_Delete", pars);
                return Convert.ToInt32(pars[3].Value);
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(ex.ToString());
                return -696;
            }
        }



        // Log đk cơ sở 
        public List<RegisterEdu_Request> RegisterEdu_Request_GetPage(DateTime fromDate, DateTime toDate, string email, string eduName, string phone)
        {
            try
            {
                var pars = new SqlParameter[5];
                pars[0] = new SqlParameter("@_FromDate", fromDate == new DateTime() ? DBNull.Value : (object)fromDate);
                pars[1] = new SqlParameter("@_ToDate", toDate == new DateTime() ? DBNull.Value : (object)toDate);
                pars[2] = new SqlParameter("@_Email", string.IsNullOrEmpty(email) ? DBNull.Value : (object)email);
                pars[3] = new SqlParameter("@_EduName", string.IsNullOrEmpty(eduName) ? DBNull.Value : (object)eduName);
                pars[4] = new SqlParameter("@_Phone", string.IsNullOrEmpty(phone) ? DBNull.Value : (object)phone);
                return new DBHelper(Config.CoreLogConnectionString).GetListSP<RegisterEdu_Request>("SP_RegisterEdu_Request_GetPage", pars);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return new List<RegisterEdu_Request>();
            }
        }

        public RegisterEdu_Request RegisterEdu_Request_Detail(int id)
        {
            try
            {
                var pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@_LogId", id);
                return new DBHelper(Config.CoreLogConnectionString).GetInstanceSP<RegisterEdu_Request>("SP_RegisterEdu_Request_Detail", pars);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return new RegisterEdu_Request();
            }
        }
    }
}
