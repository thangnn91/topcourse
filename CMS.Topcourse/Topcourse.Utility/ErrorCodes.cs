using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topcourse.Utility
{
    public class ErrorCodes
    {
        #region[Core ErrorCode]
        public const int ACCOUNT_NOTFOUND = -50;// Tài khoản ko tồn tại
        public const int INVALID_PASSWORD = -53;// Mật khẩu ko hợp lệ
        public const int SYSTEM_ERROR = -99;//Lỗi hệ thống
        public const int PLATFORM_UNAUTHORIZED = -4000001;//Acess token deny from 1pay
        public const int ACCOUNT_EXISTED = -46;// tai khoan dan ton tai
        public const int ACCOUNT_BLOCKED = -48;// tai khoan dang bi khoa
        public const int ACCOUNT_UNACTIVE = -33;// tai khoan khong hoat dong
        public const int AUTHEN_SERVICEID_NOTFOUND = -100;// authen serviceid ko hop le
        public const int AUTHEN_SERVICEKEY_NOTFOUND = -101;// authen service key ko hop le
        public const int ACCOUNT_REGISTED_SECURE = -107; //TK đã đăng ký bảo mật
        public const int ACCOUNT_NOTEXISTED = -1;// tai khoan khong ton tai
        public const int NOT_PERMISSION = -2;// ko co quyen
        public const int INPUT_DATA_INVALID = -600;//thong tin dau vao ko hop le
        public const int OLDPASSWORD_NOTFOUND = -613;// mat khau cu khong dung
        public const int MOBILE_EXISTED = -641;//mobile da ton tai
        public const int MOBILE_NOTFOUND = -642;//mobile ko dung
        public const int EMAIL_EXISTED = -41;//email da ton tai
        public const int EMAIL_NOTFOUND = -42;//email khong dung
        public const int STATUS_INVALID = -144;//trang thai ko dung
        public const int DUPLICATE_DATA = -657;//trung du lieu
        public const int EMAIL_ACTIVED = -611;//email da active
        public const int MOBILE_ACTIVED = -612;//mobile da active
        public const int ACCOUNT_NOT_LOCK = -631;//tai khoan chua lock
        public const int UNLOCK_DENIED = -663;//(MK mới giống mật khẩu cũ khi đổi pass)
        public const int OLDMOBILE_INVALID = -654;//so dien thoai cu ko hop le
        public const int SECURITY_TOKEN_INVALID = -102;//token ko hop le
        public const int SECURITY_TOKEN_EXPRIZE = -103;//token het han
        public const int C_EMAIL_NOT_VERIFY = -628;//Email chưa xác thực
        public const int QUOTA_OTP_SMS = -665;//Vượt quá quota 3lần/1ngày của số điện thoại
        public const int SEND_EMAIL_FAIL = -666;//Gui mail that bai
        public const int CARDS_NOT_ENOUGH = -13;//số lượng thẻ ko đủ
        public const int NOT_ENOUGH_MONEY_CORE = -51;//Số dư không đủ
        public const int BET_TOO_FAST = -200;//Bạn đặt cửa quá nhanh giữa 2 lần, hãy thử lại
        public const int NICKNAME_REQUIRET = -200;//Bạn phải cập nhật Tên nhập vật để tiếp tục chơi
        public const int BETTIME_EXPRIZE = -207;//Hết thời gian đặt cửa
        public const int BETVALUE_INVALID = -212;//Giá trị đặt cửa không hợp lệ
        public const int BETMONEY_INVALID = -213;//Loại tiền không hợp lệ
        public const int BETTIME_INVALID = -214;//Chưa đến thời gian đặt cửa
        public const int BETGATEE_INVALID = -232;//Cửa đăt không hợp lệ
        public const int CREATEROOM_LIMITED = -217;//so luong ban qua lon
        public const int SEND_OTP_MAX = -632;//Số lần gửi OTP vượt quá số lượng cho phép (tối đa 3 lần/ngày).<br/> Xin vui lòng gửi tin nhắn RIK OTP đến 8095 để nhận lại mã kích hoạt", "Số lần gửi OTP vượt quá số lượng cho phép (tối đa 3 lần/ngày)
        public const int OTPINVALID_OR_USED = -111;// OTP không đúng hoặc đã sử dụng
        public const int ACCOUNT_NOTACTIVEINCORE = -49;// Chưa kích hoạt
        public const int OTP_EXPIRE = -6;// OTP hết han
        public const int OTP_INVALID = -7;// Không đúng OTP
        public const int TRANSACTION_PROCESSING = -19;// giao địch dang được xử lý

        #endregion
    }
}
