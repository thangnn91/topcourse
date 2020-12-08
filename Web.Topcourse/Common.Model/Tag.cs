using System.Collections.Generic;

namespace Common.Model
{
    public class RequestGetTag
    {
        public int TagGroupId { set; get; }
        public byte GroupType { set; get; } // --1:Trường, 2:khóa ngắn, 3:khóa dài, 4:học bổng
    }

    public class Tag
    {
        public int TagGroupId { set; get; }
        public string TagGroupName { set; get; }
        public int TagId { set; get; }
        public string TagName { set; get; }
        public string TagKey { set; get; }
        public int TagOrder { set; get; }
        public int TagGroupOrder { set; get; }
        public byte Position { set; get; }
    }

    public class TagGroup
    {
        public int TagGroupId { set; get; }
        public string TagGroupName { set; get; }
        public int TagGroupOrder { set; get; }
        public List<Tag> Tags { set; get; }
    }

    public class AutoComplete
    {
        public int Id { set; get; }
        public string Alias { set; get; }
        public string Title { set; get; }
        public string EducationName { set; get; }
        public string Address { set; get; }
        public string LocationName { set; get; }
        public int Type { set; get; }
    }
}
