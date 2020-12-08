using Common.Model;
using Common.Utilities.Database;
using Common.Utilities.Log;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Topcourse.DataAccess.IDAO;

namespace Topcourse.DataAccess.DAO
{
    public class TopcourseDAO : ITopcourseDAO
    {
#if DEBUG
        private string CoreConnectionString =
            "Server=103.130.212.9;uid=cuongbx;password=Aa@123456;database=Topcourse.CoreDB;";

        private string CoreLogConnectionString =
            "Server=103.130.212.9;uid=cuongbx;password=Aa@123456;database=Topcourse.CoreLogDB;";
#else
        private string CoreConnectionString =
            "Server=localhost;uid=cuongbx;password=Aa@123456;database=Topcourse.CoreDB;";

        private string CoreLogConnectionString =
            "Server=localhost;uid=cuongbx;password=Aa@123456;database=Topcourse.CoreLogDB;";
#endif


        public int RegisterStudent(ProfileStudent requestData)
        {
            try
            {
                var pars = new SqlParameter[10];
                pars[0] = new SqlParameter("@_Username", requestData.Username);
                pars[1] = new SqlParameter("@_Password", requestData.Password);
                pars[2] = new SqlParameter("@_Mobile",
                    string.IsNullOrEmpty(requestData.Mobile) ? DBNull.Value : (object) requestData.Mobile);
                pars[3] = new SqlParameter("@_Fullname",
                    string.IsNullOrEmpty(requestData.FullName) ? DBNull.Value : (object) requestData.FullName);
                pars[4] = new SqlParameter("@_RegisterType", requestData.RegisterType);
                pars[5] = new SqlParameter("@_UserProfileInfo",
                    string.IsNullOrEmpty(requestData.UserProfileInfo)
                        ? DBNull.Value
                        : (object) requestData.UserProfileInfo);
                pars[6] = new SqlParameter("@_DeviceType", requestData.DeviceType);
                pars[7] = new SqlParameter("@_ClientIP", requestData.ClientIP);
                pars[8] = new SqlParameter("@_CreatedUser",
                    string.IsNullOrEmpty(requestData.CreatedUser) ? DBNull.Value : (object) requestData.CreatedUser);
                pars[9] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) {Direction = ParameterDirection.Output};
                new DBHelper(CoreConnectionString).ExecuteNonQuerySP("SP_RegisterUserStudents", pars);
                return Convert.ToInt32(pars[9].Value);
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return -969;
            }
        }

        public int ConfirmUser(ProfileStudent requestData)
        {
            try
            {
                var pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@_UserID", requestData.UserID);
                pars[1] = new SqlParameter("@_Username", requestData.Username);
                pars[2] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) {Direction = ParameterDirection.Output};
                new DBHelper(CoreConnectionString).ExecuteNonQuerySP("SP_ConfirmUserByEmail", pars);
                return Convert.ToInt32(pars[2].Value);
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return -969;
            }
        }

        public int UserAuthen(UserAuthen requestData)
        {
            try
            {
                var pars = new SqlParameter[6];
                pars[0] = new SqlParameter("@_UserName", requestData.Username);
                pars[1] = new SqlParameter("@_Password", requestData.Password);
                pars[2] = new SqlParameter("@_Salt", requestData.Salt);
                pars[3] = new SqlParameter("@_DeviceType", requestData.DeviceType);
                pars[4] = new SqlParameter("@_ClientIP", requestData.ClientIP);
                pars[5] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) {Direction = ParameterDirection.Output};
                new DBHelper(CoreConnectionString).ExecuteNonQuerySP("SP_User_Authenticate", pars);
                return Convert.ToInt32(pars[5].Value);
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return -969;
            }
        }

        public UserInfo GetBasicInfo(string userName, int userID)
        {
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@_UserID", userID);
                pars[1] = new SqlParameter("@_UserName", userName);
                return new DBHelper(CoreConnectionString).GetInstanceSP<UserInfo>("SP_User_GetSingle", pars);
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return new UserInfo {UserID = -969};
            }
        }

        public UserInfo GetFullInfo(string userName, int userID)
        {
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@_UserID", userID);
                pars[1] = new SqlParameter("@_UserName", userName);
                return new DBHelper(CoreConnectionString).GetInstanceSP<UserInfo>("SP_User_GetFull", pars);
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return new UserInfo {UserID = -969};
            }
        }

        public int UpdatePassword(UpdatePassword requestData)
        {
            try
            {
                var pars = new SqlParameter[8];
                pars[0] = new SqlParameter("@_UserID", requestData.UserID);
                pars[1] = new SqlParameter("@_Password",
                    string.IsNullOrEmpty(requestData.Password) ? DBNull.Value : (object) requestData.Password);
                pars[2] = new SqlParameter("@_PasswordNew", requestData.PasswordNew);
                pars[3] = new SqlParameter("@_ClientIP", requestData.ClientIP);
                pars[4] = new SqlParameter("@_LogType", requestData.LogType);
                pars[5] = new SqlParameter("@_CreatedUser", requestData.CreatedUser);
                pars[6] = new SqlParameter("@_Description",
                    string.IsNullOrEmpty(requestData.Description) ? DBNull.Value : (object) requestData.Description);
                pars[7] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) {Direction = ParameterDirection.Output};
                new DBHelper(CoreConnectionString).ExecuteNonQuerySP("SP_User_ChangePassword", pars);
                return Convert.ToInt32(pars[7].Value);
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return -969;
            }
        }

        public int UpdateProfile(UserInfoUpdate requestData)
        {
            try
            {
                var pars = new SqlParameter[8];
                pars[0] = new SqlParameter("@_UserID", requestData.UserID);
                pars[1] = new SqlParameter("@_Mobile",
                    string.IsNullOrEmpty(requestData.Mobile) ? DBNull.Value : (object) requestData.Mobile);
                pars[2] = new SqlParameter("@_Fullname",
                    string.IsNullOrEmpty(requestData.Fullname) ? DBNull.Value : (object) requestData.Fullname);
                pars[3] = new SqlParameter("@_Email",
                    string.IsNullOrEmpty(requestData.Email) ? DBNull.Value : (object) requestData.Email);
                pars[4] = new SqlParameter("@_CreatedUser", requestData.CreatedUser);
                pars[5] = new SqlParameter("@_UserProfileInfo",
                    string.IsNullOrEmpty(requestData.UserProfileInfo)
                        ? DBNull.Value
                        : (object) requestData.UserProfileInfo);
                pars[6] = new SqlParameter("@_ClientIP", requestData.ClientIP);
                pars[7] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) {Direction = ParameterDirection.Output};
                new DBHelper(CoreConnectionString).ExecuteNonQuerySP("SP_User_UpdateProfile", pars);
                return Convert.ToInt32(pars[7].Value);
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return -969;
            }
        }

        public int UpdateAvatar(UpdateAvatar requestData)
        {
            try
            {
                var pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@_UserID", requestData.UserID);
                pars[1] = new SqlParameter("@_Avatar", requestData.Avatar);
                pars[2] = new SqlParameter("@_ClientIP", requestData.ClientIP);
                pars[3] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) {Direction = ParameterDirection.Output};
                new DBHelper(CoreConnectionString).ExecuteNonQuerySP("SP_User_UpdateAvatar", pars);
                return Convert.ToInt32(pars[3].Value);
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return -969;
            }
        }

        public OAuthenAccount GetAssociateUser(OAuthenAccount requestData)
        {
            try
            {
                var pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@_UserID", requestData.UserID);
                pars[1] = new SqlParameter("@_OAuthAccount", requestData.OAuthAccount);
                pars[2] = new SqlParameter("@_OAuthSystemID", requestData.OAuthSystemID);
                return new DBHelper(CoreConnectionString).GetInstanceSP<OAuthenAccount>("[SP_User_Associate_Get]",
                    pars);
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return new OAuthenAccount {OAuthID = -969};
            }
        }

        public int CreateAssociateUser(OAuthenAccount requestData)
        {
            try
            {
                var pars = new SqlParameter[5];
                pars[0] = new SqlParameter("@_UserID", requestData.UserID);
                pars[1] = new SqlParameter("@_OAuthAccount", requestData.OAuthAccount);
                pars[2] = new SqlParameter("@_OAuthSystemID", requestData.OAuthSystemID);
                pars[3] = new SqlParameter("@_ClientIP", requestData.ClientIP);
                pars[4] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) {Direction = ParameterDirection.Output};
                new DBHelper(CoreConnectionString).ExecuteNonQuerySP("[SP_User_Associate_Insert]", pars);
                return Convert.ToInt32(pars[4].Value);
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return -969;
            }
        }

        public List<Course> GetListCourse(CourseRequest requestData, ref int totalRecord)
        {
            try
            {
                var pars = new SqlParameter[27];
                pars[0] = new SqlParameter("@_TextSearch",
                    string.IsNullOrEmpty(requestData.TextSearch) ? DBNull.Value : (object) requestData.TextSearch);
                pars[1] = new SqlParameter("@_StudyForm", requestData.StudyForm);
                pars[2] = new SqlParameter("@_StudyDuration",
                    string.IsNullOrEmpty(requestData.StudyDuration)
                        ? DBNull.Value
                        : (object) requestData.StudyDuration);
                pars[3] = new SqlParameter("@_StudyType", requestData.StudyType);
                pars[4] = new SqlParameter("@_StudyTime",
                    string.IsNullOrEmpty(requestData.StudyTime) ? DBNull.Value : (object) requestData.StudyTime);
                pars[5] = new SqlParameter("@_MonthOpen", requestData.MonthOpen);
                pars[6] = new SqlParameter("@_TuitionFrom", requestData.TuitionFrom);
                pars[7] = new SqlParameter("@_TuitionTo", requestData.TuitionTo);
                pars[8] = new SqlParameter("@_NumRate", requestData.NumRate == 99 ? DBNull.Value : (object)requestData.NumRate);
                pars[9] = new SqlParameter("@_IsHot",
                    requestData.IsHot == null ? DBNull.Value : (object) requestData.IsHot);
                pars[10] = new SqlParameter("@_SpecialityID", requestData.SpecialityID);
                pars[11] = new SqlParameter("@_LocationID", requestData.LocationID);
                pars[12] = new SqlParameter("@_DisctricID", requestData.DisctricID);
                pars[13] = new SqlParameter("@_NationalID", requestData.NationalID);
                pars[14] = new SqlParameter("@_LanguageInstruction",
                    string.IsNullOrEmpty(requestData.LanguageInstruction)
                        ? DBNull.Value
                        : (object) requestData.LanguageInstruction);
                pars[15] = new SqlParameter("@_Level",
                    string.IsNullOrEmpty(requestData.Level) ? DBNull.Value : (object) requestData.Level);
                pars[16] = new SqlParameter("@_Lecturer",
                    string.IsNullOrEmpty(requestData.Lecturer) ? DBNull.Value : (object) requestData.Lecturer);
                pars[17] = new SqlParameter("@_RequireAdmission", requestData.RequireAdmission);
                pars[18] = new SqlParameter("@_SchoolType", requestData.SchoolType);
                pars[19] = new SqlParameter("@_Address",
                    string.IsNullOrEmpty(requestData.Address) ? DBNull.Value : (object) requestData.Address);
                pars[20] = new SqlParameter("@_ListTagId",
                    string.IsNullOrEmpty(requestData.ListTagId) ? DBNull.Value : (object) requestData.ListTagId);
                pars[21] = new SqlParameter("@_CourseType", requestData.CourseType);
                pars[22] = new SqlParameter("@_CurrentPage", requestData.CurrentPage);
                pars[23] = new SqlParameter("@_PageSize", requestData.PageSize);
                pars[24] = new SqlParameter("@_IsHotHomePage",
                    requestData.IsHotHomePage == null ? DBNull.Value : (object)requestData.IsHotHomePage);
                pars[25] = new SqlParameter("@_IsFeatured",
                    requestData.IsFeatured == null ? DBNull.Value : (object)requestData.IsFeatured);
                pars[26] = new SqlParameter("@_TotalRecord", SqlDbType.Int) {Direction = ParameterDirection.Output};
                var listCourse = new DBHelper(CoreConnectionString).GetListSP<Course>("SP_Course_GetPage", pars);
                totalRecord = Convert.ToInt32(pars[26].Value);
                return listCourse;
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return new List<Course>();
            }
        }

        public DataTable GetListCompare(string coursesId, int courseType)
        {
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@courseId", coursesId);
                pars[1] = new SqlParameter("@courseType", courseType);
                var listCourse = new DBHelper(CoreConnectionString).GetDataTableSP("SP_CompareCourse", pars);
                return listCourse;
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return new DataTable();
            }
        }

        public Course GetCourseDetail(CourseRequest requestData)
        {
            var pars = new SqlParameter[1];
            pars[0] = new SqlParameter("@_CourseID", requestData.CourseID);
            return new DBHelper(CoreConnectionString).GetInstanceSP<Course>("SP_Course_ViewDetail", pars);
        }


        public long RegisterEducation(RequestRegisterEdu requestData)
        {
            try
            {
                var pars = new SqlParameter[30];
                pars[0] = new SqlParameter("@_EduNameVI",
                    string.IsNullOrEmpty(requestData.EduNameVI) ? DBNull.Value : (object) requestData.EduNameVI);
                pars[1] = new SqlParameter("@_EduNameEn",
                    string.IsNullOrEmpty(requestData.EduNameEn) ? DBNull.Value : (object) requestData.EduNameEn);
                pars[2] = new SqlParameter("@_ShortName",
                    string.IsNullOrEmpty(requestData.ShortName) ? DBNull.Value : (object) requestData.ShortName);
                pars[3] = new SqlParameter("@_HeadOffice",
                    string.IsNullOrEmpty(requestData.HeadOffice) ? DBNull.Value : (object) requestData.HeadOffice);
                pars[4] = new SqlParameter("@_Ward",
                    string.IsNullOrEmpty(requestData.Ward) ? DBNull.Value : (object) requestData.Ward);
                pars[5] = new SqlParameter("@_District",
                    string.IsNullOrEmpty(requestData.District) ? DBNull.Value : (object) requestData.District);
                pars[6] = new SqlParameter("@_Location",
                    string.IsNullOrEmpty(requestData.Location) ? DBNull.Value : (object) requestData.Location);
                pars[7] = new SqlParameter("@_National",
                    string.IsNullOrEmpty(requestData.National) ? DBNull.Value : (object) requestData.National);
                pars[8] = new SqlParameter("@_EduPhone",
                    string.IsNullOrEmpty(requestData.EduPhone) ? DBNull.Value : (object) requestData.EduPhone);
                pars[9] = new SqlParameter("@_EduFax",
                    string.IsNullOrEmpty(requestData.EduFax) ? DBNull.Value : (object) requestData.EduFax);
                pars[10] = new SqlParameter("@_EduEmail",
                    string.IsNullOrEmpty(requestData.EduEmail) ? DBNull.Value : (object) requestData.EduEmail);
                pars[11] = new SqlParameter("@_Website",
                    string.IsNullOrEmpty(requestData.Website) ? DBNull.Value : (object) requestData.Website);
                pars[12] = new SqlParameter("@_President",
                    string.IsNullOrEmpty(requestData.President) ? DBNull.Value : (object) requestData.President);
                pars[13] = new SqlParameter("@_SchoolMaster",
                    string.IsNullOrEmpty(requestData.SchoolMaster) ? DBNull.Value : (object) requestData.SchoolMaster);
                pars[14] = new SqlParameter("@_SchoolType",
                    string.IsNullOrEmpty(requestData.SchoolType) ? DBNull.Value : (object) requestData.SchoolType);
                pars[15] = new SqlParameter("@_Career",
                    string.IsNullOrEmpty(requestData.Career) ? DBNull.Value : (object) requestData.Career);
                pars[16] = new SqlParameter("@_NumberLecturers", requestData.NumberLecturers);
                pars[17] = new SqlParameter("@_Facilities",
                    string.IsNullOrEmpty(requestData.Facilities) ? DBNull.Value : (object) requestData.Facilities);
                pars[18] = new SqlParameter("@_FacilitiesMore",
                    string.IsNullOrEmpty(requestData.FacilitiesMore)
                        ? DBNull.Value
                        : (object) requestData.FacilitiesMore);
                pars[19] = new SqlParameter("@_TotalArea",
                    string.IsNullOrEmpty(requestData.TotalArea) ? DBNull.Value : (object) requestData.TotalArea);
                pars[20] = new SqlParameter("@_InfomationMore",
                    string.IsNullOrEmpty(requestData.InfomationMore)
                        ? DBNull.Value
                        : (object) requestData.InfomationMore);
                pars[21] = new SqlParameter("@_CoursesInfo",
                    string.IsNullOrEmpty(requestData.CoursesInfo) ? DBNull.Value : (object) requestData.CoursesInfo);
                pars[22] = new SqlParameter("@_Firstname",
                    string.IsNullOrEmpty(requestData.Firstname) ? DBNull.Value : (object) requestData.Firstname);
                pars[23] = new SqlParameter("@_Lastname",
                    string.IsNullOrEmpty(requestData.Lastname) ? DBNull.Value : (object) requestData.Lastname);
                pars[24] = new SqlParameter("@_Address",
                    string.IsNullOrEmpty(requestData.Address) ? DBNull.Value : (object) requestData.Address);
                pars[25] = new SqlParameter("@_Phone",
                    string.IsNullOrEmpty(requestData.Phone) ? DBNull.Value : (object) requestData.Phone);
                pars[26] = new SqlParameter("@_Email",
                    string.IsNullOrEmpty(requestData.Email) ? DBNull.Value : (object) requestData.Email);
                pars[27] = new SqlParameter("@_FileAttach",
                    string.IsNullOrEmpty(requestData.FileAttach) ? DBNull.Value : (object) requestData.FileAttach);
                pars[28] = new SqlParameter("@_ClientIP",
                    string.IsNullOrEmpty(requestData.ClientIP) ? DBNull.Value : (object) requestData.ClientIP);
                pars[29] = new SqlParameter("@_ResponseStatus", SqlDbType.BigInt)
                {
                    Direction = ParameterDirection.Output
                };
                new DBHelper(CoreLogConnectionString).ExecuteNonQuerySP("SP_RegisterEdu_Request", pars);
                return Convert.ToInt64(pars[29].Value);
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return -969;
            }
        }

        public int InsertComment(RequestComment requestData)
        {
            try
            {
                var pars = new SqlParameter[6];
                pars[0] = new SqlParameter("@_TargetID", requestData.TargetID);
                pars[1] = new SqlParameter("@_UserID", requestData.UserID);
                pars[2] = new SqlParameter("@_Comment",
                    string.IsNullOrEmpty(requestData.Comment) ? DBNull.Value : (object) requestData.Comment);
                pars[3] = new SqlParameter("@_ParentID", requestData.ParentID);
                pars[4] = new SqlParameter("@_Type", requestData.Type);
                pars[5] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) {Direction = ParameterDirection.Output};
                new DBHelper(CoreConnectionString).ExecuteNonQuerySP("[SP_User_Comment_Insert]", pars);
                return Convert.ToInt32(pars[5].Value);
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return -969;
            }
        }

        public int InsertRate(RequestComment requestData)
        {
            try
            {
                var pars = new SqlParameter[7];
                pars[0] = new SqlParameter("@_TargetID", requestData.TargetID);
                pars[1] = new SqlParameter("@_UserID", requestData.UserID);
                pars[2] = new SqlParameter("@_Comment",
                    string.IsNullOrEmpty(requestData.Comment) ? DBNull.Value : (object) requestData.Comment);
                pars[3] = new SqlParameter("@_ClientIP",
                    string.IsNullOrEmpty(requestData.ClientIP) ? DBNull.Value : (object) requestData.ClientIP);
                pars[4] = new SqlParameter("@_Type", requestData.Type);
                pars[5] = new SqlParameter("@_NumRate", requestData.NumRate);
                pars[6] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) {Direction = ParameterDirection.Output};
                new DBHelper(CoreConnectionString).ExecuteNonQuerySP("SP_User_Rate_Insert", pars);
                return Convert.ToInt32(pars[6].Value);
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return -969;
            }
        }

        public List<CommentData> GetListComment(RequestComment requestData)
        {
            try
            {
                var pars = new SqlParameter[5];
                pars[0] = new SqlParameter("@_Id", requestData.Id);
                pars[1] = new SqlParameter("@_TargetID", requestData.TargetID);
                pars[2] = new SqlParameter("@_Type", requestData.Type);
                pars[3] = new SqlParameter("@_UserID", requestData.UserID);
                pars[4] = new SqlParameter("@_ParentID",
                    requestData.ParentID < 0 ? DBNull.Value : (object) requestData.ParentID);
                return new DBHelper(CoreConnectionString).GetListSP<CommentData>("[SP_User_Comment_GetList]", pars);

            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return new List<CommentData>();
            }
        }

        public List<UserRate> GetListRate(RequestGetRate requestData)
        {
            try
            {
                var pars = new SqlParameter[5];
                pars[0] = new SqlParameter("@_RateID", requestData.RateID);
                pars[1] = new SqlParameter("@_FromDate",
                    requestData.FromDate == DateTime.MinValue ? DBNull.Value : (object) requestData.FromDate);
                pars[2] = new SqlParameter("@_ToDate",
                    requestData.ToDate == DateTime.MinValue ? DBNull.Value : (object) requestData.ToDate);
                pars[3] = new SqlParameter("@_TargetID", requestData.TargetID);
                pars[4] = new SqlParameter("@_Type", requestData.Type);
                return new DBHelper(CoreConnectionString).GetListSP<UserRate>("SP_User_Rate_Get", pars);

            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return new List<UserRate>();
            }
        }

        public List<CoreAttribute> GetListAttribute(CoreAttribute requestData)
        {
            try
            {
                var pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@_Tablename",
                    string.IsNullOrEmpty(requestData.TableName) ? DBNull.Value : (object) requestData.TableName);
                pars[1] = new SqlParameter("@_FieldName",
                    string.IsNullOrEmpty(requestData.FieldName) ? DBNull.Value : (object) requestData.FieldName);
                pars[2] = new SqlParameter("@_GroupType", requestData.GroupType);
                return new DBHelper(CoreConnectionString).GetListSP<CoreAttribute>("[SP_User_Comment_GetList]", pars);

            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return new List<CoreAttribute>();
            }
        }


        public List<School> GetListSchool(RequestGetSchool requestData, ref int totalRecord)
        {
            try
            {
                var pars = new SqlParameter[10];
                pars[0] = new SqlParameter("@_EduName",
                    string.IsNullOrEmpty(requestData.EduName) ? DBNull.Value : (object) requestData.EduName);
                pars[1] = new SqlParameter("@_UserID", requestData.UserID);
                pars[2] = new SqlParameter("@_Type", requestData.Type);
                pars[3] = new SqlParameter("@_IsPartner", requestData.IsPartner);
                pars[4] = new SqlParameter("@_TraningType", requestData.TraningType);
                pars[5] = new SqlParameter("@_NationalID", requestData.NationalID);
                pars[6] = new SqlParameter("@_ListTagId",
                    string.IsNullOrEmpty(requestData.ListTagID) ? DBNull.Value : (object) requestData.ListTagID);
                pars[7] = new SqlParameter("@_CurrentPage", requestData.CurrentPage);
                pars[8] = new SqlParameter("@_PageSize", requestData.PageSize);
                pars[9] = new SqlParameter("@_TotalRecord", SqlDbType.Int) {Direction = ParameterDirection.Output};
                var listSchool = new DBHelper(CoreConnectionString).GetListSP<School>("SP_Education_Get", pars);
                totalRecord = Convert.ToInt32(pars[9].Value);
                return listSchool;
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return new List<School>();
            }
        }

        public List<School> GetListSchoolSimilar(int eduid)
        {
            try
            {
                var pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@_EduId", eduid);
                return new DBHelper(CoreConnectionString).GetListSP<School>("SP_Education_Get_Similar", pars);
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return new List<School>();
            }
        }

        public School GetSchoolDetail(int eduID, string alias)
        {
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@_EduId", eduID);
                pars[1] = new SqlParameter("@_Alias", string.IsNullOrEmpty(alias) ? DBNull.Value : (object) alias);
                return new DBHelper(CoreConnectionString).GetInstanceSP<School>("SP_Education_Detail", pars);
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return new School();
            }
        }

        public List<Course> GetCourseBySchool(int shoolID, int top, string tagIDs)
        {
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@_TOP", top);
                pars[1] = new SqlParameter("@_EduID", shoolID);
                //pars[2] = new SqlParameter("@_ListTagId", string.IsNullOrEmpty(tagIDs) ? DBNull.Value : (object)tagIDs);
                return new DBHelper(CoreConnectionString).GetListSP<Course>("SP_Course_GetByEducation", pars);
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return new List<Course>();
            }
        }

        public List<SpecilityCourse> GetListSpecilityCourse(int id, int courseType)
        {
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@_Id", id < 0 ? DBNull.Value : (object) id);
                pars[1] = new SqlParameter("@_CourseType", courseType);
                return new DBHelper(CoreConnectionString).GetListSP<SpecilityCourse>("SP_Courses_Specility_Get", pars);
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return new List<SpecilityCourse>();
            }
        }

        public List<Location> GetLocation(int locationType, int? parentID, int locationID)
        {
            try
            {
                var pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@_LocationLevel", locationType <= 0 ? DBNull.Value : (object) locationType);
                pars[1] = new SqlParameter("@_ParentID", parentID <= 0 ? DBNull.Value : (object) parentID);
                pars[2] = new SqlParameter("@_LocationID", locationID <= 0 ? DBNull.Value : (object) locationID);
                return new DBHelper(CoreConnectionString).GetListSP<Location>("SP_Common_GetLocations", pars);
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return new List<Location>();
            }
        }

        public List<Location> GetLocationByCourse()
        {
            try
            {
                return new DBHelper(CoreConnectionString).GetListSP<Location>("SP_Location_Get_ByCourses");
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return new List<Location>();
            }
        }

        public List<Tag> GetListTag(RequestGetTag requestData)
        {
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@_TagGroupId", requestData.TagGroupId);
                pars[1] = new SqlParameter("@_GroupType", requestData.GroupType);
                return new DBHelper(CoreConnectionString).GetListSP<Tag>("SP_TagLinkTagGroup_Get", pars);
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return new List<Tag>();
            }
        }

        public int SendCourseRegister(SendCourseRegister requestData)
        {
            try
            {
                var pars = new SqlParameter[12];
                pars[0] = new SqlParameter("@_Fullname",
                    string.IsNullOrEmpty(requestData.Fullname) ? DBNull.Value : (object) requestData.Fullname);
                pars[1] = new SqlParameter("@_Email",
                    string.IsNullOrEmpty(requestData.Email) ? DBNull.Value : (object) requestData.Email);
                pars[2] = new SqlParameter("@_Phone",
                    string.IsNullOrEmpty(requestData.Phone) ? DBNull.Value : (object) requestData.Phone);
                pars[3] = new SqlParameter("@_Address",
                    string.IsNullOrEmpty(requestData.Address) ? DBNull.Value : (object) requestData.Address);
                pars[4] = new SqlParameter("@_Content",
                    string.IsNullOrEmpty(requestData.Content) ? DBNull.Value : (object) requestData.Content);
                pars[5] = new SqlParameter("@_FileAttach",
                    string.IsNullOrEmpty(requestData.FileAttach) ? DBNull.Value : (object) requestData.FileAttach);
                pars[6] = new SqlParameter("@_CoursesID", requestData.CoursesID);
                pars[7] = new SqlParameter("@_UserID", requestData.UserID);
                pars[8] = new SqlParameter("@_Type", requestData.Type);
                pars[9] = new SqlParameter("@_CreatedUser",
                    string.IsNullOrEmpty(requestData.CreatedUser) ? DBNull.Value : (object) requestData.CreatedUser);
                pars[10] = new SqlParameter("@_ClientIP",
                    string.IsNullOrEmpty(requestData.ClientIP) ? DBNull.Value : (object) requestData.ClientIP);
                pars[11] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) {Direction = ParameterDirection.Output};
                new DBHelper(CoreConnectionString).ExecuteNonQuerySP("[SP_Course_Register_Insert]", pars);
                return Convert.ToInt32(pars[11].Value);
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return -969;
            }
        }

        public int SendEducationContact(RequestContactEducation requestData)
        {
            try
            {
                var pars = new SqlParameter[22];
                pars[0] = new SqlParameter("@_Fullname",
                    string.IsNullOrEmpty(requestData.Fullname) ? DBNull.Value : (object) requestData.Fullname);
                pars[1] = new SqlParameter("@_Email",
                    string.IsNullOrEmpty(requestData.Email) ? DBNull.Value : (object) requestData.Email);
                pars[2] = new SqlParameter("@_Phone",
                    string.IsNullOrEmpty(requestData.Phone) ? DBNull.Value : (object) requestData.Phone);
                pars[3] = new SqlParameter("@_Address",
                    string.IsNullOrEmpty(requestData.Address) ? DBNull.Value : (object) requestData.Address);
                pars[4] = new SqlParameter("@_Content",
                    string.IsNullOrEmpty(requestData.Content) ? DBNull.Value : (object) requestData.Content);
                pars[5] = new SqlParameter("@_LevelType", requestData.LevelType);
                pars[6] = new SqlParameter("@_LevelEnglish", requestData.LevelEnglish);
                pars[7] = new SqlParameter("@_EnglishPoint", requestData.EnglishPoint);
                pars[8] = new SqlParameter("@_CertificateOther",
                    string.IsNullOrEmpty(requestData.CertificateOther)
                        ? DBNull.Value
                        : (object) requestData.CertificateOther);
                pars[9] = new SqlParameter("@_CertificatePoint", requestData.CertificatePoint);
                pars[10] = new SqlParameter("@_LangOther",
                    string.IsNullOrEmpty(requestData.LangOther) ? DBNull.Value : (object) requestData.LangOther);
                pars[11] = new SqlParameter("@_LangCertificateName",
                    string.IsNullOrEmpty(requestData.LangCertificateName)
                        ? DBNull.Value
                        : (object) requestData.LangCertificateName);
                pars[12] = new SqlParameter("@_LangPoint", requestData.LangPoint);
                pars[13] = new SqlParameter("@_CourseLevel", requestData.CourseLevel);
                pars[14] = new SqlParameter("@_SpecialityID", requestData.SpecialityID);
                pars[15] = new SqlParameter("@_EducationID", requestData.EducationID);
                pars[16] = new SqlParameter("@_EstimatedTime", requestData.EstimatedTime);
                pars[17] = new SqlParameter("@_UserID", requestData.UserID);
                pars[18] = new SqlParameter("@_IsReceiveInfo", requestData.IsReceiveInfo);
                pars[19] = new SqlParameter("@_IsBonus", requestData.IsBonus);
                pars[20] = new SqlParameter("@_ClientIP",
                    string.IsNullOrEmpty(requestData.ClientIP) ? DBNull.Value : (object) requestData.ClientIP);
                pars[21] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) {Direction = ParameterDirection.Output};
                new DBHelper(CoreConnectionString).ExecuteNonQuerySP("SP_Education_Contact_Insert", pars);
                return Convert.ToInt32(pars[21].Value);
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return -969;
            }

        }

        public List<Course> CourseGetSimilar(int courseID)
        {
            try
            {
                return new DBHelper(CoreConnectionString).GetListSP<Course>("[SP_Course_Get_Similar]",
                    new SqlParameter("@_CourseID", courseID));
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return new List<Course>();
            }

        }

        public long CreateOrder(RequestOrder requestData, ref string createdDate)
        {
            try
            {
                var pars = new SqlParameter[10];
                pars[0] = new SqlParameter("@_ServiceId", requestData.ServiceId);
                pars[1] = new SqlParameter("@_ServiceKey",
                    string.IsNullOrEmpty(requestData.ServiceKey) ? DBNull.Value : (object) requestData.ServiceKey);
                pars[2] = new SqlParameter("@_Products", requestData.Products);
                pars[3] = new SqlParameter("@_CourseId", requestData.CourseId);
                pars[4] = new SqlParameter("@_UserId", requestData.UserId);
                pars[5] = new SqlParameter("@_Amount", requestData.Amount);
                pars[6] = new SqlParameter("@_OrderInfo",
                    string.IsNullOrEmpty(requestData.OrderInfo) ? DBNull.Value : (object) requestData.OrderInfo);
                pars[7] = new SqlParameter("@_ClientIP",
                    string.IsNullOrEmpty(requestData.ClientIP) ? DBNull.Value : (object) requestData.ClientIP);
                pars[8] = new SqlParameter("@_CreatedDateFormat", SqlDbType.VarChar, 20)
                {
                    Direction = ParameterDirection.Output
                };
                pars[9] = new SqlParameter("@_ResponseStatus", SqlDbType.BigInt)
                {
                    Direction = ParameterDirection.Output
                };
                new DBHelper(CoreConnectionString).ExecuteNonQuerySP("SP_OrderPayment_Create", pars);
                createdDate = pars[8].Value.ToString();
                return Convert.ToInt64(pars[9].Value);
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return -969;
            }
        }

        public int UpdateOrder(RequestConfirmOrder requestData)
        {
            try
            {
                var pars = new SqlParameter[10];
                pars[0] = new SqlParameter("@_OrderID", requestData.OrderID);
                pars[1] = new SqlParameter("@_Stautus", requestData.Status);
                pars[2] = new SqlParameter("@_TransRefID",
                    string.IsNullOrEmpty(requestData.TransRefID) ? DBNull.Value : (object) requestData.TransRefID);
                pars[3] = new SqlParameter("@_BankCode",
                    string.IsNullOrEmpty(requestData.BankCode) ? DBNull.Value : (object) requestData.BankCode);
                pars[4] = new SqlParameter("@_BankTransNo",
                    string.IsNullOrEmpty(requestData.BankTransNo) ? DBNull.Value : (object) requestData.BankTransNo);
                pars[5] = new SqlParameter("@_CardType",
                    string.IsNullOrEmpty(requestData.CardType) ? DBNull.Value : (object) requestData.CardType);
                pars[6] = new SqlParameter("@_PayDate",
                    requestData.PayDate <= 0 ? DBNull.Value : (object) requestData.PayDate);
                pars[7] = new SqlParameter("@_PayResponseCode",
                    string.IsNullOrEmpty(requestData.PayResponseCode)
                        ? DBNull.Value
                        : (object) requestData.PayResponseCode);
                pars[8] = new SqlParameter("@_TmnCode",
                    string.IsNullOrEmpty(requestData.TmnCode) ? DBNull.Value : (object) requestData.TmnCode);
                pars[9] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) {Direction = ParameterDirection.Output};
                new DBHelper(CoreConnectionString).ExecuteNonQuerySP("SP_OrderPayment_ConfirmStatus", pars);
                return Convert.ToInt32(pars[9].Value);
            }
            catch (Exception ex)
            {

                NLogManager.PublishException(ex);
                return -969;
            }
        }

        public Order GetOrderInfo(long orderId)
        {
            try
            {
                return new DBHelper(CoreConnectionString).GetInstanceSP<Order>("SP_OrderPayment_Get",
                    new SqlParameter("@_OrderID", orderId));
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return new Order();
            }

        }

        public List<AutoComplete> GetAutoCompletes(string textSearch, byte courseType)
        {
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@_TextSearch", textSearch);
                pars[1] = new SqlParameter("@_CourseType", courseType);
                return new DBHelper(CoreConnectionString).GetListSP<AutoComplete>("[SP_Course_GetAutocomplete]", pars);
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return new List<AutoComplete>();
            }
        }

        public List<Scholarship> GetListScholarship(RequestScholarship requestData, ref int totalRecord)
        {
            try
            {
                var pars = new SqlParameter[6];
                pars[0] = new SqlParameter("@_TextSearch",
                    string.IsNullOrEmpty(requestData.TextSearch) ? DBNull.Value : (object) requestData.TextSearch);
                pars[1] = new SqlParameter("@_Type", requestData.Type);
                pars[2] = new SqlParameter("@_ListTagId",
                    string.IsNullOrEmpty(requestData.ListTagId) ? DBNull.Value : (object) requestData.ListTagId);
                pars[3] = new SqlParameter("@_CurrentPage", requestData.CurrentPage);
                pars[4] = new SqlParameter("@_PageSize", requestData.PageSize);
                pars[5] = new SqlParameter("@_TotalRecord", SqlDbType.Int) {Direction = ParameterDirection.Output};
                var list = new DBHelper(CoreConnectionString).GetListSP<Scholarship>("SP_Scholarship_GetPage", pars);
                totalRecord = Convert.ToInt32(pars[5].Value);
                return list;
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return new List<Scholarship>();
            }
        }

        public Scholarship GetScholarshipDetail(int id)
        {
            try
            {
                return new DBHelper(CoreConnectionString).GetInstanceSP<Scholarship>("SP_Scholarship_Get_Detail",
                    new SqlParameter("@_Id", id));
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return new Scholarship();
            }
        }

        public List<OrderPayment> GetListOrder(RequestGetOrder requestData, ref int totalRecord)
        {
            try
            {
                var pars = new SqlParameter[15];
                pars[0] = new SqlParameter("@_OrderID", requestData.OrderID);
                pars[1] = new SqlParameter("@_UserID", requestData.UserID);
                pars[2] = new SqlParameter("@_Stautus", requestData.Stautus);
                pars[3] = new SqlParameter("@_CourseType", requestData.CourseType);
                pars[4] = new SqlParameter("@_TransRefID",
                    string.IsNullOrEmpty(requestData.TransRefID) ? DBNull.Value : (object) requestData.TransRefID);
                pars[5] = new SqlParameter("@_BankCode",
                    string.IsNullOrEmpty(requestData.BankCode) ? DBNull.Value : (object) requestData.BankCode);
                pars[6] = new SqlParameter("@_BankTransNo",
                    string.IsNullOrEmpty(requestData.BankTransNo) ? DBNull.Value : (object) requestData.BankTransNo);
                pars[7] = new SqlParameter("@_PayResponseCode",
                    string.IsNullOrEmpty(requestData.PayResponseCode)
                        ? DBNull.Value
                        : (object) requestData.PayResponseCode);
                pars[8] = new SqlParameter("@_CreatedFromDate",
                    requestData.CreatedFromDate == DateTime.MinValue
                        ? DBNull.Value
                        : (object) requestData.CreatedFromDate);
                pars[9] = new SqlParameter("@_CreatedToDate",
                    requestData.CreatedToDate == DateTime.MinValue ? DBNull.Value : (object) requestData.CreatedToDate);
                pars[10] = new SqlParameter("@_PayFromDate",
                    requestData.PayFromDate == DateTime.MinValue ? DBNull.Value : (object) requestData.PayFromDate);
                pars[11] = new SqlParameter("@_PayToDate",
                    requestData.PayToDate == DateTime.MinValue ? DBNull.Value : (object) requestData.PayToDate);
                pars[12] = new SqlParameter("@_CurrentPage", requestData.CurrentPage);
                pars[13] = new SqlParameter("@_PageSize", requestData.PageSize);
                pars[14] = new SqlParameter("@_TotalRecord", SqlDbType.Int) {Direction = ParameterDirection.Output};
                var list = new DBHelper(CoreConnectionString).GetListSP<OrderPayment>("SP_OrderPayment_GetPage", pars);
                totalRecord = Convert.ToInt32(pars[14].Value);
                return list;
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return new List<OrderPayment>();
            }
        }

        public int InsertCourseFavorite(RequestCourseFavorite requestCourseFavorite)
        {
            try
            {
                var pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@CourseID", requestCourseFavorite.CourseId);
                pars[1] = new SqlParameter("@UserId", requestCourseFavorite.UserId);
                pars[2] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) {Direction = ParameterDirection.Output};
                new DBHelper(CoreConnectionString).ExecuteNonQuerySP("[SP_User_Courses_Favorite_InsertUpdate]", pars);
                return Convert.ToInt32(pars[2].Value);
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return -969;
            }
        }

        public List<BannerImage> GetBannerImages()
        {

            try
            {
                var list = new DBHelper(CoreConnectionString).GetListSP<BannerImage>("SP_BannerImage_GetAll");
                return list;
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return new List<BannerImage>();
            }
        }

        public List<Course> GetCourseFavorites(int userId)
        {
            try
            {
                var list = new DBHelper(CoreConnectionString).GetListSP<Course>("SP_Course_GetFavorite", new SqlParameter("@_UserID", userId));
                return list;
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return new List<Course>();
            }
        }
        public List<Post> GetListPost(PostRequest requestData, ref int totalRecord)
        {
            try
            {
                var pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@keyword",
                    string.IsNullOrEmpty(requestData.Keyword) ? DBNull.Value : (object)requestData.Keyword);
                pars[1] = new SqlParameter("@pageIndex", requestData.PageIndex);
                pars[2] = new SqlParameter("@pageSize", requestData.PageSize);               
                pars[3] = new SqlParameter("@totalRow", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var listPost = new DBHelper(CoreConnectionString).GetListSP<Post>("SP_Get_Post_AllPaging", pars);
                totalRecord = Convert.ToInt32(pars[3].Value);
                return listPost;
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return new List<Post>();
            }
        }

        public Post PostDetail(int id)
        {
            try
            {
                var pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@_PostID", id);
                var model = new DBHelper(CoreConnectionString).GetInstanceSP<Post>("SP_Post_GetDetail", pars);
                return model;
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return new Post();
            }
        }

        public List<Post> GetPostRelated(int id)
        {
            try
            {
                var pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@id", id);
                var model = new DBHelper(CoreConnectionString).GetListSP<Post>("SP_Post_Related", pars);
                return model;
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return new List<Post>();
            }            
        }

        public List<Course> GetListRecommend(int courseId, int page, int size)
        {
            try
            {
                var pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@_CourseID", courseId);
                pars[1] = new SqlParameter("@_CurrentPage", page);
                pars[2] = new SqlParameter("@_PageSize", size);
                pars[3] = new SqlParameter("@_TotalRecord", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var listCourse = new DBHelper(CoreConnectionString).GetListSP<Course>("SP_Course_Get_Recommended", pars);
                return listCourse;
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return null;
            }
        }
    }
}
