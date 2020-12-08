using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class Post
    {
        public int PostID { set; get; }
        public string Title { set; get; }
        public string TitleDisplay { set; get; }
        public string Alias { set; get; }
        public DateTime CreatedDate { set; get; }
        public string CreatedUser { set; get; }
        public string Description { set; get; }
        public string Content { set; get; }
        public string ThumbailImage { set; get; }
        public bool HomeFlag { set; get; }
        public bool HotFlag { set; get; }
        public int Status { get; set; }
        public string MetaKeyword { set; get; }
        public string MetaDescription { set; get; }
        public string Author { set; get; }
        public int ViewCount { set; get; }
        public DateTime UpdateDate { set; get; }
        public string UpdateBy { set; get; }
    }

    public class PostRequest
    {
        public string Keyword { set; get; }
	    public int PageIndex { set; get; }
	    public int PageSize { set; get; }
    }
}
