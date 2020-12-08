using Common.Model;
using System.Collections.Generic;
using System.Data;

namespace Topcourse.DataAccess.IDAO
{
    public interface ITopcourseDAO
    {
        int RegisterStudent(ProfileStudent requestData);
        int ConfirmUser(ProfileStudent requestData);
        int UserAuthen(UserAuthen requestData);
        UserInfo GetBasicInfo(string userName, int userID);
        int UpdatePassword(UpdatePassword requestData);
        UserInfo GetFullInfo(string userName, int userID);
        int UpdateProfile(UserInfoUpdate requestData);
        int UpdateAvatar(UpdateAvatar requestData);
        OAuthenAccount GetAssociateUser(OAuthenAccount requestData);
        int CreateAssociateUser(OAuthenAccount requestData);
        List<Course> GetListCourse(CourseRequest requestData, ref int totalRecord);
        DataTable GetListCompare(string coursesId, int courseType);
        Course GetCourseDetail(CourseRequest requestData);

        long RegisterEducation(RequestRegisterEdu requestData);
        int InsertComment(RequestComment requestData);
        List<CommentData> GetListComment(RequestComment requestData);
        List<CoreAttribute> GetListAttribute(CoreAttribute requestData);

        List<School> GetListSchool(RequestGetSchool requestData, ref int totalRecord);
        List<School> GetListSchoolSimilar(int eduid);
        School GetSchoolDetail(int eduID, string alias);
        List<Course> GetCourseBySchool(int shoolID, int top, string tagIDs);
        List<SpecilityCourse> GetListSpecilityCourse(int id, int courseType);

        List<Location> GetLocation(int locationType, int? parentID, int locationID);

        List<Location> GetLocationByCourse();
        List<Tag> GetListTag(RequestGetTag requestData);
        List<Course> CourseGetSimilar(int courseID);
        int SendCourseRegister(SendCourseRegister requestData);
        int SendEducationContact(RequestContactEducation requestData);
        long CreateOrder(RequestOrder requestData, ref string createdDate);
        int UpdateOrder(RequestConfirmOrder requestData);
        Order GetOrderInfo(long orderId);
        List<AutoComplete> GetAutoCompletes(string textSearch, byte courseType);
        List<Scholarship> GetListScholarship(RequestScholarship requestData, ref int totalRecord);
        Scholarship GetScholarshipDetail(int id);
        int InsertRate(RequestComment requestData);
        List<UserRate> GetListRate(RequestGetRate requestData);

        List<OrderPayment> GetListOrder(RequestGetOrder requestData, ref int totalRecord);
        int InsertCourseFavorite(RequestCourseFavorite requestCourseFavorite);
        List<BannerImage> GetBannerImages();
        List<Course> GetCourseFavorites(int userId);
        List<Post> GetListPost(PostRequest requestData, ref int totalRecord);
        Post PostDetail(int id);
        List<Post> GetPostRelated(int id);
        List<Course> GetListRecommend(int courseId, int page, int size);
    }
}
