using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topcourse.DataAccess.DTO
{
    public class UsersLog
    {
        public int LogID { get; set; }
        public int UserID { get; set; }
        public int FunctionID { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Description { get; set; }
        public int LogType { get; set; }
        public string Fullname { get; set; }
        public string FunctionName { get; set; }
        public string ClientIP { get; set; }
        public string Username { get; set; }
    }
}
