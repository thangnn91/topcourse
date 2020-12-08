using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topcourse.DataAccess.DTO
{
    public class Product
    {
        public int ProductID { set; get; }
        public int ParentID { set; get; }
        public string ProductCode { set; get; }
        public string ProductName { set; get; }
        public int ParValue { set; get; } // giá sp
        public byte AppliedVersion { set; get; } // hiển thị 0 ko , 1 web , 2 app , 3 cả 2
        public byte PriorityState { set; get; } // 1 New , 2 Hot , 3 Promotion
        public bool Disable { set; get; }   // trạng thái
        public byte ProductType { set; get; } //ProductType:1: Parent Product Service(loại dịch vụ) |2: Product Service(loại sản phẩn)|3: Product(sản phẩn)|4: Sub Product(sản phẩm con)
        public DateTime UpdatedTime { set; get; }

        public string ParentName { set; get; } // Tên dịch vụ cha

        public string GrandParentName { set; get; }
        public int GrandParentID { set; get; }


        // Detail
        public string Description { set; get; }  // mô tả
        public string Detail { set; get; }   // chi tiết
        public string Logo { set; get; } // ảnh logo


        public string CreatedUser { set; get; } // insert update
        public int FixAmount { set; get; } //

        // chiết khẩu 
        public double RateAmount { set; get; } // % chiết khấu
        public string AppliedTimeString { set; get; }
        public DateTime AppliedTime { set; get; } // ngày áp dụng
        public string ExpiryTimeString { set; get; }
        public DateTime ExpiryTime { set; get; } // ngày kết thúc
        public DateTime CreatedTime { set; get; } // Ngày set

        // list ProductId
        public string ListProductId { set; get; }
        public string AccountName { set; get; }

        // 
        public int ImportDiscountID { set; get; }
        public bool IsApplying { set; get; }


        // Chiết khấu bán hàng
        public int ParentProductServiceID { set; get; } // type 1 - Loai dich vu
        public int ProductServiceID { set; get; } // type 2 - loai sp
        public byte PolicyType { set; get; } // 1 chiết khấu, 2 phí
        public byte AppliedObjectType { set; get; }  //+ 1: System + 2: Customer UserType + 3: Customer UserID
        public int AppliedObjectID { set; get; }
        public long ExportRateID { set; get; }
    }

    public class LogProduct
    {
        public long LogID { set; get; }
        public int ProductID { set; get; }
        public string LogData { set; get; }
        public DateTime AppliedTime { set; get; }
        public DateTime ExpiryTime { set; get; }
        public string CreatedUser { set; get; }
        public DateTime CreatedTime { set; get; }
    }

    public class ProductOrder  // 
    {
        public long SaleOrderID { set; get; }
        public long CustomerUserID { set; get; }
        public string CustomerMobile { set; get; }
        public string CustomerEmail { set; get; }
        public string CustomerName { set; get; }
        public int ProductID { set; get; }
        public string ProductName { set; get; }
        public int Quantity { set; get; }
        public long GrandAmount { set; get; }
        public long TotalProductValue { set; get; }
        public long PaymentFee { set; get; }
        public string ProductAccount { set; get; }
        public string ProductDetail { set; get; }
        public Int16 OrderStatus { set; get; }
        public long PartnerOrderID { set; get; }
        public long PaymentOrderID { set; get; }
        public long TransactionID { get; set; }
        public string CreatedUser { set; get; }
        public DateTime CreatedTime { set; get; }
    }

    public class SaleOrderInfo
    {
        public long SaleOrderID { set; get; }
        public int CustomerUserID { set; get; }
        public string CustomerMobile { set; get; }
        public string CustomerEmail { set; get; }
        public string CustomerName { set; get; }
        public int ProductID { set; get; }
        public int Quantity { set; get; }
        public long GrandAmount { set; get; }
        public long PaymentFee { set; get; }
        public int TotalProductValue { set; get; }
        public string ProductAccount { set; get; }
        public string ProductDetail { set; get; }
        public short OrderStatus { set; get; }
        public byte PayType { set; get; }
        public byte DeviceType { set; get; }
        public string ClientIP { set; get; }
        public long PartnerOrderID { set; get; }
        public long PaymentOrderID { set; get; }
        public long TransactionID { set; get; }
        public long Discount { set; get; }
        public long Amount { set; get; }
        public long Fee { set; get; }
    }


}
