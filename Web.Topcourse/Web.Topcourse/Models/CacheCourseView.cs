using System.Collections.Generic;

namespace Web.Topcourse.Models
{
    public class CacheCourseView
    {
        public int UserID { get; set; }
        public List<CourseViewItem> Logs { get; set; }        
    }
    public class CourseViewItem
    {
        public int CoursesID { set; get; }
        public string Title { set; get; }
        public string Alias { set; get; }
        public string EducationName { set; get; }
        public int ViewCount { set; get; }
        public double NumRate { set; get; }
        public int Tuition { set; get; }
        public int TuitionIncentives { set; get; }
        public string StudyFormDesc { set; get; }
        public string Accreditation { set; get; }
        public string ImageUrl { set; get; }
        public byte CourseType { set; get; }
        public string StudyDuration { set; get; }
        public string RequireAdmissionDesc { set; get; }
    }
}

//CoursesID: @Model.CoursesID,
//                        Title: '@Html.Raw(Model.Title)',
//                        Alias: '@Model.Alias',
//                        EducationName: '@Html.Raw(Model.EducationName)',
//                        ViewCount: @Model.ViewCount,
//                        NumRate: @Model.NumRate,
//                        TuitionIncentives: @Model.TuitionIncentives,
//                        StudyFormDesc: '@Html.Raw(Model.StudyFormDesc)',
//                        Accreditation: '@Html.Raw(Model.Accreditation)',
//                        ImageUrl: '@Model.ImageUrl',
//                        CourseType: @Model.CourseType