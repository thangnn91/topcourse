using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class RequestGetSchool
    {

        public string EduName { set; get; }
        public int? UserID { set; get; }
        public int? Type { set; get; }
        public bool? IsPartner { set; get; }
        public byte? TraningType { set; get; }
        public int? NationalID { set; get; }
        public string ListTagID { get; set; }
        public int CurrentPage { set; get; }
        public int PageSize { set; get; }
    }


    public class School
    {
        public long STT { set; get; }
        public int EduId { set; get; }
        public string EduName { set; get; }
        public string Alias { set; get; }
        public string EduAddress { set; get; }
        public int NumberView { set; get; }
        public decimal Rate { set; get; }
        public int NumComment { set; get; }
        public string Description { set; get; }
        public byte Type { set; get; }
        public bool IsPartner { set; get; }
        public string Avatar { set; get; }
        public string Logo { set; get; }
        public string Content { set; get; }
        public string EduMap { set; get; }
        public int EduEstablishedYear { set; get; }
        public int NationalID { set; get; }
        public string BranchInfo { set; get; }
        public string ContactInfo { set; get; }
        public string SlideImage { set; get; }
        public string SEODescription { set; get; }
        public List<string> ListSlide { set; get; }
    }

    public class SearchShool
    {
        public School School { set; get; }
        public List<Course> Courses { get; set; }
    }

    public class RequestScholarship
    {

        public string TextSearch { set; get; }
        public byte Type { set; get; } // --1:trong nc, 2:quoc te
        public string ListTagId { set; get; } // --có dạng TagGroup1,TagId1;TagGroup2,TagId2;
        public int CurrentPage { set; get; }
        public int PageSize { set; get; }

    }

    public class Scholarship
    {
        public long STT { set; get; }
        public int Id { set; get; }
        public string Title { set; get; }
        public string Alias { set; get; }
        public string TitleDisplay { set; get; }
        public string SEODescription { set; get; }
        public int NumberView { set; get; }
        public decimal Rate { set; get; }
        public int NumComment { set; get; }
        public DateTime PublishDate { set; get; }
        public string Avatar { set; get; }
        public string Description { set; get; }
        public string Content { set; get; }
        public string Banner { set; get; }
        public string SchoolName { set; get; }
        public byte Type { set; get; }
        public DateTime CreatedDate { set; get; }
        public string CreatedUser { set; get; }
        public string PublishUser { set; get; }
        public byte Status { set; get; }
        public string Address { set; get; }
    }
}
