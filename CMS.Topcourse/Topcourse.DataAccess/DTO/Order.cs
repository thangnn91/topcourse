using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topcourse.DataAccess.DTO
{
    public class BankTransfer_Order // Order thông báo nạp tiền - rút tiền offline
    {
        public long BankTransferOrderID { set; get; }
        public long TransactionID { set; get; }
        public long OrderID { set; get; }
        public int BankID { set; get; }
        public string BankCode { set; get; }
        public string CommandCode { set; get; }
        public string FunctionName { set; get; }
        public int UserID { set; get; }
        public string AccountName { set; get; }
        public long Amount { set; get; }
        public string BankAccount { set; get; }
        public string BankAccountName { set; get; }
        public string BankReferenceID { set; get; }
        public string BankBranch { set; get; }
        public byte BankTransferType { set; get; }
        public DateTime BankCreatedTime { set; get; }
        public string OrgMessenger { set; get; }
        public string Description { set; get; }
        public string CustomerMobile { set; get; }
        public DateTime Createdtime { set; get; }
        public DateTime Executetime { set; get; }
        public DateTime EndTime { set; get; }
        public Int16 OrderStatus { set; get; } //= 0 chờ duyệt, = 1 thành công , = 2 thất bại  
        public string ConfirmUser { set; get; }
        public byte InformType { set; get; }
    }

    public class InvoiceBanking_Order // nạp rút online
    {
        public long InvoiceOrderID { set; get; }
        public long TransactionID { set; get; }
        public int BankID { set; get; }
        public string BankCode { set; get; }
        public string PartnerCode { set; get; }
        public string AccountName { set; get; }
        public string BankAccount { set; get; }
        public string BankAccountName { set; get; }
        public decimal BankAmount { set; get; }
        public long Amount { set; get; }
        public long GrandAmount { set; get; }
        public int Fee { set; get; }
        public int RelatedFee { set; get; }
        public int UserID { set; get; }
        public byte PayType { set; get; }
        public string BankReferenceID { set; get; }
        public string RedirectURL { set; get; }
        public string NotifyURL { set; get; }
        public DateTime ResponseTime { set; get; }
        public long ResponseUnixTime { set; get; }
        public string ResponseMessage { set; get; }
        public Int16 OrderStatus { set; get; }  //= 0 chờ duyệt (Init), = 1 thành công (Success), = 2 thất bại(  Fail/Cancel)  , 3 đang chờ ( In Progress), 4 Ready Progress , 5 đang chờ thanh toán ( Waiting Transaction(Progress Success)) 
    }


    public class OrderPayment
    {
        public long InvoiceOrderID { get; set; }
        public int UserID { set; get; }
        public string AccountName { set; get; }
        public long GrandAmount { get; set; }
        public long Amount { set; get; }
        public long Fee { set; get; }
        public int RelatedFee { set; get; }
        public decimal MerchantFee { get; set; }
        public decimal MerchantAmount { get; set; }
        public int MerchantUserID { get; set; }
        public string MerchantAccount { get; set; }
        public string MerchantReferenceID { get; set; }
        public int PaymentAppID { get; set; }
        public string PaymentApp { get; set; }
        public string RedirectURL { get; set; }
        public string NotifyURL { get; set; }
        public string BankRedirectURL { get; set; }
        public string BankNotifyURL { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public int ServiceID { get; set; }
        public byte PayType { get; set; }
        public byte DeviceType { get; set; }
        public string ClientIP { get; set; }
        public decimal BankFee { get; set; }
        public decimal BankAmount { get; set; }
        public int BankID { get; set; }
        public string BankCode { get; set; }
        public string PartnerCode { get; set; }
        public string BankAccount { get; set; }
        public string BankAccountName { get; set; }
        public string BankReferenceID { get; set; }
        public int OrderStatus { get; set; }
        public long TransactionID { get; set; }
        public DateTime CreatedTime { set; get; }
    }




    public class BankTransferOrder_Confirm // Duyệt - Hủy thông báo nạp tiền
    {
        public long BankTransferOrderID { set; get; }
        public string CommandCode { set; get; }
        public int UserID { set; get; }
        public string AccountName { set; get; }
        public string ConfirmUser { set; get; }
        public string Description { set; get; }
        public Int16 OrderStatus { set; get; } //= 0 chờ duyệt, = 1 thành công , = 2 thất bại
        public string bankReferenceID { set; get; }
        public DateTime BankCreatedTime { set; get; }
        public long TransactionID { set; get; }
    }


    public class Transaction_Deposit // Cộng tiền (Nạp) 
    {
        public int UserID { set; get; }
        public long Amount { set; get; }
        public long Fee { set; get; }
        public long RelatedFee { set; get; }
        public string Description { set; get; }
        public byte PayType { set; get; } //dùng để phân biệt các loại thanh toán khi cùng đầu dịch vụ (1:nạp tiền Online; 2: Nạp tiền thanh toán Online; 0:ko thuộc loại nào ~ NULL)
        public int ServiceID { set; get; }
        public string BankCode { set; get; }
        public string BankAccount { set; get; }
        public string BankReferenceID { set; get; }
        public long BillingOrderID { set; get; }
        public string Currency { set; get; }
        public byte DeviceType { set; get; }
        public string ClientIp { set; get; }
        public DateTime OrderTime { set; get; }
        public string Accesstoken { set; get; }
    }


    public class Transaction_WithdrawAccount
    {
        public int UserId { set; get; }
        public long Amount { set; get; }
        public long Fee { set; get; }
        public long RelateFee { set; get; }
        public string Description { set; get; }
        public byte PayType { set; get; }
        public int ServiceId { set; get; }
        public string BankCode { set; get; }
        public string BankAccount { set; get; }
        public string BankReferenceId { set; get; }
        public long BillingOrderId { set; get; }
        public string Currency { set; get; }
        public string ClientIp { set; get; }
        public DateTime OrderTime { set; get; }
        public string AccessToken { set; get; }
    }



    public class BankInfo // Bank
    {
        public int BankID { set; get; }
        public string BankCode { set; get; }
        public string BankName { set; get; }
        public string BinCode { set; get; }
        public byte BankType { set; get; }
        public byte BankServiceType { set; get; }
        public byte BankStatus { set; get; }
        public string WebSite { set; get; }
        public string Logo { set; get; }
        public string Address { set; get; }
        public string Description { set; get; }
        public string LogoMobileGrid { set; get; }
        public string LogoMobileIcon { set; get; }
        public string CardColor { set; get; }
        public DateTime CreatedTime { set; get; }
        public string UpdatedUser { set; get; }
    }

    public class Bank_Request
    {
        public int BankID { set; get; }
        public string BankCode { set; get; }
        public string BankName { set; get; }
        public string BinCode { set; get; }
        public byte BankType { set; get; }
        public byte BankServiceType { set; get; }
        public string Website { set; get; }
        public string Logo { set; get; }
        public string Address { set; get; }
        public string Description { set; get; }
        public byte Status { set; get; }
        public string LogoMobileGrid { set; get; }
        public string LogoMobileIcon { set; get; }
        public string CardColor { set; get; }
        public string CreatedUser { set; get; }
    }

    public class Bank_Account
    {
        public int BankAccountID { set; get; }
        public string AccountName { set; get; }
        public int BankID { set; get; }
        public string BankCode { set; get; }
        public string BankName { set; get; }
        public string BankBranch { set; get; }
        public string BankAccount { set; get; }
        public string BankAccountName { set; get; }
        public string BankAccountAddress { set; get; }
        public byte BankAccountType { set; get; }
        public string Description { set; get; }
        public bool IsDefault { set; get; }
        public string OpenDate { set; get; }
        public string UpdatedUser { set; get; }
    }


    public class Bank_Account_Request
    {
        public int BankAccountId { set; get; }
        public int UserId { set; get; }
        public int BankId { set; get; }
        public int CurBankId { set; get; }
        public string AccountName { set; get; }
        public string BankCode { set; get; }
        public string BankName { set; get; }
        public string BankBranch { set; get; }
        public string BankAccount { set; get; }
        public string BankAccountName { set; get; }
        public string BankAccountAddress { set; get; }
        public byte BankAccountType { set; get; }
        public string Description { set; get; }
        public bool IsDefaul { set; get; }
        public string OpenDate { set; get; }
        public string CreatedUser { set; get; }
    }


    public class GroupService
    {
        public int GroupServiceID { set; get; }
        public string GroupServiceName { set; get; }
    }

    public class Service
    {
        public int ServiceID { set; get; }
        public int GroupServiceID { set; get; }
        public string GroupServiceName { set; get; }
        public string ServiceName { set; get; }
        public string Description { set; get; }
        public byte AffectToBalance { set; get; }
        public bool Enable { set; get; }
        public int ParentID { set; get; }
    }


    public class RequestOrder
    {
        public int BankID { set; get; } //Bank id
        public string BankCode { set; get; } //bankCode
        public string BankName { set; get; } //bankCode
        public int UserID { set; get; }
        public string AccountName { set; get; }
        public string ConfirmUser { set; get; }
        public short OrderStatus { set; get; } //Trang thai order cap nhat offLine>>0:Init,1:success,2:Fail,3:Waitconfirm, 4:is recovery; online>>0:Init,1:success,2:Fail,3:Inprogess, 4:ready progeress,5: Waiting Transaction (Progress Success)
        public long Amount { set; get; }
        public long Fee { set; get; }
        public long BankFee { set; get; }
        public string BankAccount { set; get; }
        public string BankAccountName { set; get; }
        public string BankReferenceID { set; get; }
        public string BankBranch { set; get; }
        public byte BankTransferType { set; get; } ////1 : InternetBanking, 2:SMSBanking, 3:Ủy nhiệm chi, 4:Nộp tại quầy, 5:Chuyển khoản ATM
        public DateTime? BankCreatedTime { set; get; }
        public string StringBankCreatedTime { set; get; }
        public string Description { set; get; }
        public string BankLocation { set; get; }
        public DateTime ExecuteTime { set; get; }
        public DateTime EndTime { set; get; }
        public byte InformType { set; get; } //Doi voi nap tien>>1:nap tu bank,2:nap tu web/app, 3: nap tu sms
        public long TransactionID { set; get; }
        public string Captcha { get; set; }
        public bool IsDeposit { set; get; }
        public string VerifyCaptcha { get; set; }
        public string NotifyURL { get; set; }
        public string RedirectURL { get; set; }

        public long OrderID { set; get; } //thong tin buoc tao order

        public byte TransactionType { set; get; } //Su dung trong request rut tien>>1:rut off, 2: rut online

        //Nap online
        public string PartnerCode { get; set; }
        public string Signature { get; set; }
        public string OTP { get; set; } //Dung cho buoc confirm nap

        //Rut online
        public decimal BankAmount { set; get; }
        public byte PayType { set; get; }
        public int ServiceID { set; get; }
        public string Currency { set; get; }
        public string ClientIP { set; get; }
        public byte DeviceType { set; get; }

        //Nap qua the lien ket
        public int BankAccountID { set; get; }
    }


    public class SaleOrder
    {
        public int CustomerUserID { set; get; }
        public string CustomerMobile { set; get; }
        public string CustomerEmail { set; get; }
        public string CustomerName { set; get; }
        public int ProductID { set; get; }
        public long Amount { set; get; }
        public long Discount { set; get; }
        public long Fee { set; get; }
        public int Quantity { set; get; }
        public string ProductAccount { set; get; }
        public string ProductDetail { set; get; }
        public long PaymentFee { set; get; }
        public string Description { set; get; }
        public byte PayType { set; get; }
        public byte DeviceType { set; get; }
        public string ClientIP { set; get; }
        public string CreatedUser { set; get; }
    }

    public class PartnerOrder
    {
        public long SaleOrderID { set; get; }
        public string PartnerCode { set; get; }
        public int ProductID { set; get; }
        public string ProductCode { set; get; }
        public int Quantity { set; get; }
        public string Description { set; get; }
        public string ProductAccount { set; get; }
        public long Amount { set; get; }
        public long ProductValue { set; get; }
        public byte DeviceType { set; get; }
        public string ClientIP { set; get; }

        //Dung cho get info
        public long InvoiceOrderID { set; get; }
        public short OrderStatus { set; get; }

        //Dung cho update order
        public string PartnerReferenceID { set; get; }
        public DateTime ResponseTime { set; get; }
        public string ResponseMessage { set; get; }
        public string ConfirmUser { set; get; }
    }

    public class SaleOrderConfirm
    {
        public long SaleOrderID { set; get; }
        //- 0: Innit
        //- 1: Success
        //- 2: Fail/Cancel
        //- 3: Transaction InProgress(thanh toán ví treo)
        //- 4: Ready Parner Progress(transaction success)
        //- 5: Partner InProgress(gd partner treo)
        public short OrderStatus { set; get; }
        public string Description { set; get; }
        public long TransactionID { set; get; }
        public long PartnerOrderID { set; get; }
        public long PaymentOrderID { set; get; }
        public string ConfirmUser { set; get; }
        public string UserAgent { set; get; }
        public long BalanceAfter { set; get; }
    }

}
