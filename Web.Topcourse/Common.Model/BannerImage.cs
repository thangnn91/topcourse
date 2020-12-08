using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class BannerImage
    {
        public int BannerId { get; set; }
        public string ImageUrl { get; set; }
        public string Link { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
        public int Type { get; set; }
        public string CreatedUser { get; set; }
    }
}
