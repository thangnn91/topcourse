using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topcourse.DataAccess.DTO
{
   public class BankConfig
    {
        public int Id { set; get; }
        public string BankCode { set; get; }
        public string BankName { set; get; }
        public string Description { set; get; }
        public string BrandName { set; get; }
        public string BankNumber { set; get; }
        public string BankAccountHolder { set; get; }
    }
}
