namespace Topcourse.Utility
{
    public class Enums
    {
        /// <summary>
        /// Loại tài khoản trên 
        /// </summary>

        public enum FunctionId
        {
            System = 1,
            Function = 2,
            User = 3,
            UserLog = 4,
            ErrorSystem = 5,
            Service = 6,
            Report = 7,
            GrantUser = 12,
            GrantService = 16,
            ReportAccLogin = 29, // Tổng Hợp tài khoản đăng nhập
            ReportAccSecure = 30, // Tổng Hợp Tài Khoản Bảo Mật
            ReportAccVerify = 31, // Tổng Hợp Xác Nhận Thông tin tài khoản
            ReportAccRegister = 32, //Tông Hợp Tại Khoản Đăng Ký
            ReportTransactionInput = 34, //[GD-Nạp] Tổng hợp giao dịch
            ReportDynamicTransactionInput = 35, //Báo cáo tùy chỉnh GD nạp
            ReportTransactionOutput = 37, //[GD-Tiêu] Tổng hợp giao dịch
            ReportAccMonitor = 38, //Tổng Hợp Tài Khoản Khách Hàng
            ReportDynamicTransactionOut = 39, //Báo cáo tùy chỉnh GD tiêu
            TransactionInputByMerchant = 41, //[GD-Nap] Theo Bộ Phận
            TransactionInputByProduct = 43, //[GD-Nap] Theo Sản Phẩm
            TransactionInputByProvider = 44, //[GD-Nap] Theo Nhà Cung Cấp
            TransactionOutputByMerchant = 45, //[GD-Tiêu] Theo Bộ Phận
            TransactionOtherGeneral = 47, //[GD-Khác] Tổng hợp
            ReportDynamicAccount = 47, //[Tài Khoản] Báo cáo TK tùy chỉnh
            TransactionSummaryImExIn = 49, //Tổng hợp dữ liệu Xuất Nhập Tồn của TK
            ReportGeneralSystem = 50 //Tổng hợp báo cáo hệ thống tài khoản VTC
        }

        public enum SOURCEID
        {
            OTHERS = 0,
            WEB = 1,
            WAP = 2,
            ANDROID = 3,
            IOS = 4,
            WINPHONE = 5,
            PC = 6
        }

        public enum PlatformAttributeType
        {
            EMAIL_VALIDATION_STATUS = 20, //Cap nhat trang thai email
            ACCOUNT_VALIDATION_STATUS = 21 // Cap nhat trang thai tai khoan
        }

        public enum PlatformAttributeValue
        {
            Verified = 0, //Da xac thuc
            Init = 1, // Chua xac thuc
            Pending = 2 // Cho xac thuc
        }

        public enum Password
        {
            PasswordMinLength = 6,
            PasswordMaxLength = 18
        }

        public enum Username
        {
            UserNameMaxLength = 11,
            UserNameMinLength = 10
        }

        public enum Paytype
        {
            TopupOnlineDomestic = 1, //Nạp tiền online qua ngân hàng
            TopupOnlineInter = 2,   //Nạp tiền qua thẻ quốc tế
            TopupLinkBank = 19,      //Nạp tiền qua thẻ liên kết
            WithdrawOnline = 11, //Rút tiền nhanh 247
            PaymentOnlineBankDomestic = 7, //Thanh toán online qua ngân hàng
            PaymentOnlineBankInter = 8, //Thanh toán qua thẻ quốc tế
            PaymentLinkBank = 20, //Thanh toán qua thẻ liên kết
        }


        public enum SaleOrderStatus
        {
            INITIALIZE = 0,
            SUCCESS = 1,
            FAIL = 2,
            DOUBT = 3,
            PAYMENT_SUCCESS = 4,
            WAITING_TRANSACTION = 5
        }
    }
}
