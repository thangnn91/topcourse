using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topcourse.DataAccess.DTO
{
    public class Category
    {
        public int Id { set; get; }
        public int ParentID { set; get; }
        public string Alias { set; get; }
        public int OrderNo { set; get; }
        public bool Status { set; get; }
        public string CreatedUser { set; get; }
        public string ModifyUser { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime ModifyDate { set; get; }
        public int SystemID { set; get; }
        public string ImageUrl { set; get; }
        public string CategoryName { set; get; }
        public string CategoryContent { set; get; }
        public string Tags { set; get; }
        public int LanguageID { set; get; }
    }

    public class SystemCategory
    {
        public int Id { set; get; }
        public string SystemName { set; get; }
    }

    public class SystemSplit
    {
        public int SystemId { set; get; }
        public int IsMod { set; get; }
    }
}
