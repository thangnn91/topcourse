using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topcourse.DataAccess.DTO
{
    public class TransactionHistory
    {
        public long TransactionID { set; get; }
        public long RelationReceiptID { set; get; }
        public byte PayType { set; get; }
        public string PayTypeName { set; get; }
        public int ServiceID { set; get; }
        public int AccountID { set; get; }
        public long Amount { set; get; }
        public long CloseBalance { set; get; }
        public long OpenBalance { set; get; }
        public string Description { set; get; }
        public DateTime CreatedTime { set; get; }
        public int TotalRows { set; get; }
    }


    public class TransactionRefund  // Hoàn tiền
    {
        public long RelationReceiptID { set; get; }  
        public string Description { set; get; }
        public bool HasFee { set; get; }
        public int ServiceID { set; get; }
        public string ClientIP { set; get; }
        public string AccessToken { set; get; }
        public string AccountName { set; get; }
    }

    public class TransactionRequest
    {
        public int UserID { set; get; }
        public long Amount { set; get; }
        public long Fee { set; get; }
        public long RelatedFee { set; get; }
        public int RelatedUserID { get; set; }
        public string Description { set; get; }
        public byte PayType { set; get; }
        public int ServiceID { set; get; }
        public string BankCode { set; get; }
        public string BankAccount { set; get; }
        public string BankReferenceID { set; get; }
        public string PaymentReferenceID { set; get; }
        public string PaymentApp { get; set; }
        public long BillingOrderID { set; get; }
        public string Currency { set; get; }
        public string ClientIP { set; get; }
        public byte DeviceType { set; get; }
        public DateTime? OrderTime { set; get; }
        public string AccessToken { set; get; }
    }



    public class ResponsePayment
    {
        public PaymentModel PaymentData { get; set; }
        public SaleOrderConfirm SaleOrderData { get; set; }
    }

    public class PaymentModel
    {
        public long InvoiceOrderID { get; set; }
        public int ProductID { get; set; }
        public string ProductCode { get; set; }
        public string ProductAccount { get; set; }
        public long Amount { get; set; }
        public int Quantity { get; set; }
        public string DataSign { get; set; }
    }

    public class PaymentApiResponse
    {
        public int ResponseCode { get; set; } = -1;
        public string Description { get; set; } = "Init";
        public long InvoiceOrderID { get; set; }
        public List<CardData> CardList { get; set; }
        public class CardData
        {
            public string Serial { get; set; }
            public string Pin { get; set; }
        }
        public long OrderID { get; set; }
        public string BankAccountName { get; set; }
        public string BankAccount { get; set; }
        public string BankReferenceID { get; set; }
        public string RedirectURL { get; set; } // Link bay sang napas
    }
}
