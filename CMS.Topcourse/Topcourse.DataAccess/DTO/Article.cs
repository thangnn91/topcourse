using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topcourse.DataAccess.DTO
{
    public class Article
    {
        public Int64 STT { set; get; }
        public Int64 Id { set; get; }
        public string Alias { set; get; }
        public byte Type { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public string ImageUrl { set; get; }
        public string Tags { set; get; }
        public byte Status { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime PublishDate { set; get; }
        public DateTime UpdateDate { set; get; }
        public string UpdatedUser { set; get; }
        public byte LanguageID { set; get; }
        public string Content { set; get; }
        public int ViewCount { set; get; }
        public int SortOrder { set; get; }
        public string CreatedUser { set; get; }
        public int CateId { set; get; }
        public int SystemID { set; get; }
        public int MainCateID { set; get; } //Chuyên mục chính
        public string MainCateName { set; get; }
        public int Position { get; set; } //1:ArticleNext, 2: Prev
    }

    public class Article2Cate
    {
        public int CateId { set; get; }
        public Int64 ArticleId { set; get; }
    }

    public class Tags
    {
        public int STT { set; get; }
        public int TagId { set; get; }
        public int SystemID { set; get; }
        public string TagName { set; get; }
        public string TagKey { set; get; }
        public bool Status { set; get; }
        public string CreatedUser { set; get; }
        public DateTime CreatedDate { set; get; }
        public int NewId { set; get; }
    }

    public class NewsTag
    {
        public int NewId { set; get; }
        public int TagId { set; get; }
        public string TagContent { set; get; }
    }

    public class Banner
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public int NewsId { set; get; }
        public DateTime TimeCreate { set; get; }
        public int BannerOrder { set; get; }
        public string ImageLink { set; get; }
        public string ConnectLink { set; get; }
        public int Status { set; get; }
        public string Creator { set; get; }
        public int Type { set; get; } // 1 Trang chủ, 2 Tin tức
        public string ArticlesTitle { set; get; }
    }
}
