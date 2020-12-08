using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topcourse.DataAccess.DTO;

namespace Topcourse.DataAccess.DAO
{
    public interface ICourseDAO
    {
        int Insert_Course(Course requestData);
        int Update_Course(Course requestData);
        int Delete_Course(int courseId, string createdUser);
        int UpdateStatus_Course(int courseId, int status, string actionUser);

        List<Course> GetListCourse(CourseRequest requestData);
        Course GetInfo_Course(int courseId);
        List<AttributesDict> GetList_Attributes_Dict(string tablename, string fieldName, byte groupType);

        // danh mục - ngành nghề
        List<Specility> GetList_Specility(int id, byte courseType);
        int Speciality_Insert_Update(Specility requestData);
        int Speciality_Delete(Specility requestData);

        // học bổng

        int Scholarship_Insert(Scholarship requestData);
        int Scholarship_Update(Scholarship requestData);
        int Scholarship_Delete(Scholarship requestData);
        List<Scholarship> Scholarship_GetPage(Scholarship requestData);
        Scholarship Scholarship_Get_Similar(int id);


        //Tag Group
        int TagGroup_Insert(TagGroup requestData);
        int TagGroup_Update(TagGroup requestData);
        int SP_TagGroup_Delete(TagGroup requestData);
        int SP_TagGroup_UpdateOrder(TagGroup requestData);
        List<TagGroup> TagGroup_Get(TagGroup requestData);

        // Tag
        int Tag_Insert(Tag requestData);
        int SP_Tag_Update(Tag requestData);
        int SP_Tag_Delete(Tag requestData);
        List<Tag> Tag_Get(Tag requestData);
        int SP_Tag_UpdateOrder(Tag requestData);


        // gắn thẻ vào bài 
        int TagLinkedTarget_Insert(TagLinkedTarget requestData);

        List<TagLinkedTarget> TagLinkedTarget_Get(TagLinkedTarget request);


        // đăng ký trực tuyến ,, :đăng ký tư vấn (khóa ngắn)
        List<Course_Register> Course_Register_Get(DateTime fromDate, DateTime toDate, byte type);

        Course_Register Course_Register_Get_Detail(int id);

        int SP_Course_Register_Update(Course_Register requestData);

        int Course_Register_Delete(Course_Register requestData);


        // liên hệ trường, cơ sở 
        int Education_Contact_Update(Education_Contact requestData);
        List<Education_Contact> Education_Contact_Get(DateTime fromDate, DateTime toDate, int educationId, int specialityId, DateTime estimateTimeFrom, DateTime estimateTimeTo);
        Education_Contact Education_Contact_Get_Detail(int id);
        int Education_Contact_Delete(Education_Contact requestData);

        // log dk co so
        List<RegisterEdu_Request> RegisterEdu_Request_GetPage(DateTime fromDate, DateTime toDate, string email, string eduName, string phone);

        RegisterEdu_Request RegisterEdu_Request_Detail(int id);

    }
}
