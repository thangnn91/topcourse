namespace CMS.Topcourse.Models
{
    public class ReturnData
    {
        public int ResponseCode { get; set; }
        public string Description { get; set; }
        public string Extend { get; set; }


        #region Trả về dữ liệu

        ///// <summary>
        ///// Tổng hợp dữ liệu trả về client
        ///// </summary>
        ///// <param name="responseCode">ResponseCode trả về</param>
        ///// <returns></returns>
        //public static ReturnData GetReturnData(int responseCode)
        //{
        //    //VTCPay.Portal_v2.Utils.LanguageHelper.SetLangApi();
        //    return GetReturnData(responseCode, string.Empty);
        //}


        ///// <summary>
        ///// Tổng hợp dữ liệu trả về client
        ///// </summary>
        ///// <param name="responseCode">ResponseCode trả về</param>
        ///// <param name="extend">Option mở rộng</param>
        ///// <returns></returns>
        //public static ReturnData GetReturnData(int responseCode, string extend)
        //{
        //    try
        //    {
        //        var returnData = new ReturnData { ResponseCode = responseCode, Extend = extend };
        //        if (responseCode > 0)
        //        {
        //            returnData.Description = "Bạn đã giao dịch thành công.";
        //            return returnData;
        //        }
        //        switch (responseCode)
        //        {
        //            #region Danh sách mã lỗi chung -80xx
        //            //case -7000: // Chưa login
        //            //    returnData.Description = Resources.Response.Message.TransactionSuccess;
        //            //    break;
        //            //case -7001: // Không đủ số dư
        //            //    returnData.Description = Resources.Response.Message.NotEnoughMoney;
        //            //    returnData.Extend = extend;
        //            //    break;

        //            //case -7004: // Có sử dụng dịch vụ bảo mật ODP, OTP
        //            //    returnData.Description = Resources.Response.Message.UsingSecurity;
        //            //    break;
        //            //case -7005: // Mã odp, otp ko hợp lệ
        //            //    returnData.Description = Resources.Response.Message.SecureCodeInvalid;
        //            //    break;
        //            //case -7006: // Mã  otp hết hạn cần reload lại vị trí mới
        //            //    returnData.Description = Resources.Response.Message.OtpExpired;
        //            //    break;
        //            //case -7007: // Số dư trong tài khoản của bạn không đủ để thực hiện thao tác này
        //            //    returnData.Description = Resources.Response.Message.NotEnoughMoney;
        //            //    break;
        //            //case -7008: // Bạn đã nhận mã ODP quá 3 lần
        //            //    returnData.Description = Resources.Response.Message.OdpOver3;
        //            //    break;
        //            //case -7009: // ODP đang được gửi, bạn vui lòng chờ
        //            //    returnData.Description = Resources.Response.Message.OdpSending;
        //            //    break;
        //            //case -7010:
        //            //    returnData.Description = Resources.Response.Message.OdpSent;
        //            //    break;
        //            //case -7011:
        //            //    returnData.Description = string.Format(Resources.Response.Message.OdpEmaiSent, extend);
        //            //    break;
        //            //case -7012: //chưa đăng nhập thì redirect sang cổng thanh toán
        //            //    returnData.Description = Resources.Response.Message.UnloginGotoGate;
        //            //    break;
        //            //case -7013: //Nếu không đủ số dư thì redirct sang cổng thanh toán
        //            //    const string urlRecharge = "https://paygate.vtc.vn/vi-dien-tu/ho-so/nap-tien/nap-tien-vao-tai-khoan.html";
        //            //    var url365 = Config.Domain + "/#";
        //            //    returnData.Description = string.Format(Resources.Response.Message.NotMoneyGotoGate, extend, urlRecharge, url365);
        //            //    break;

        //            //case -7014: //Số di động không hợp lệ
        //            //    returnData.Description = Resources.Response.Message.PhoneNumberInvalid;
        //            //    break;
        //            //case -7015: //Gửi mã thẻ không thành công
        //            //    returnData.Description = Resources.Response.Message.SentCardCodeFail;
        //            //    break;
        //            //case -8999: // Exception
        //            //    returnData.Description = Resources.Response.Message.SystemBusy;
        //            //    break;
        //            #endregion Danh sách mã lỗi chung 70xx

        //            #region Danh sách thông báo lỗi Đăng ký tài khoản -81xx
        //            case -8100:
        //                returnData.Description = Resources.Response.Message.InputUserName;
        //                break;
        //            case -8101:
        //                returnData.Description = Resources.Response.Message.UserNameInvalid;
        //                break;
        //            case -8102:
        //                returnData.Description = Resources.Response.Message.InputPassword;
        //                break;
        //            case -8103:
        //                returnData.Description = Resources.Response.Message.PasswordPolicy;
        //                break;
        //            case -8108:
        //                returnData.Description = Resources.Response.Message.RetypePassword;
        //                break;
        //            case -8104:
        //                returnData.Description = Resources.Response.Message.RetypePassInvalid;
        //                break;
        //            case -8105:
        //                returnData.Description = Resources.Response.Message.InputEmail;
        //                break;
        //            case -8106:
        //                returnData.Description = Resources.Response.Message.EmailInvalid;
        //                break;
        //            case -8107:
        //                returnData.Description = Resources.Response.Message.EmailExisted;
        //                break;
        //            case -8109:
        //                returnData.Description = Resources.Response.Message.InputFullName;
        //                break;
        //            case -8110:
        //                returnData.Description = Resources.Response.Message.InputPassport;
        //                break;
        //            case -8111:
        //                returnData.Description = Resources.Response.Message.InputCaptcha;
        //                break;
        //            case -8112:
        //                returnData.Description = Resources.Response.Message.InputBussinessName;
        //                break;
        //            case -8113:
        //                returnData.Description = Resources.Response.Message.InputBussinessRegister;
        //                break;
        //            case -8114:
        //                returnData.Description = Resources.Response.Message.InputTaxCode;
        //                break;
        //            case -8115:
        //                returnData.Description = Resources.Response.Message.InputHeadOffice;
        //                break;
        //            case -8116:
        //                returnData.Description = Resources.Response.Message.InputMobileContact;
        //                break;
        //            case -8117:
        //                returnData.Description = Resources.Response.Message.SelectCity;
        //                break;
        //            case -8118:
        //                returnData.Description = Resources.Response.Message.SelectDistrict;
        //                break;
        //            case -8119:
        //                returnData.Description = Resources.Response.Message.InputAddressBussiness;
        //                break;
        //            case -8120:
        //                returnData.Description = Resources.Response.Message.AccountExisted;
        //                break;
        //            case -8121:
        //                returnData.Description = Resources.Response.Message.AccountUnActive;
        //                break;
        //            case -8122:
        //                returnData.Description = Resources.Response.Message.ActiveCodeInvalid;
        //                break;
        //            case -8123:
        //                returnData.Description = Resources.Response.Message.ActiveAccNotExist;
        //                break;
        //            case -8124:
        //                returnData.Description = Resources.Response.Message.VerifyCodeInvalid;
        //                break;
        //            case -8125:
        //                returnData.Description = Resources.Response.Message.NoteTelcoFastReg;
        //                break;
        //            case -8126:
        //                returnData.Description = Resources.Response.Message.DataNotValid;
        //                break;
        //            case -8127:
        //                returnData.Description = Resources.Response.Message.ServiceInvalid;
        //                break;
        //            case -8128:
        //                returnData.Description = Resources.Response.Message.CaptchaExpires;
        //                break;
        //            case -8129:
        //                returnData.Description = Resources.Response.Message.CaptchaInvalid;
        //                break;
        //            case -8130:
        //                returnData.Description = Resources.Profile.Account.AccountNotExists;
        //                break;
        //            case -8131:
        //                returnData.Description = Resources.Profile.Account.AccountLocked;
        //                break;
        //            case -8132:
        //                returnData.Description = Resources.Profile.Account.AccountNotExists;
        //                break;
        //            case -8133:
        //                returnData.Description = Resources.Response.Message.UserOrPasswordInvalid;
        //                break;
        //            case -8134:
        //                returnData.Description = Resources.Response.Message.RequestInvalid;
        //                break;
        //            case -8135:
        //                returnData.Description = Resources.Checkout.NotAllowLogin;
        //                break;
        //            case -8136:
        //                returnData.Description = Resources.Response.Message.PasswordOldNeedChange;
        //                break;
        //            case -8137: //TK không thể thực hiện chức năng
        //                returnData.Description = Resources.Response.Message.AccountNotUseFunction;
        //                break;
        //            #endregion Danh sách thông báo lỗi mua mã thẻ 71xx

        //            #region Danh sách thông báo lỗi Nạp tiền -82xx
        //            case -8201:
        //                returnData.Description = Resources.Response.Message.RechargeMinInvalid;
        //                break;
        //            case -8205:
        //                returnData.Description = Resources.Response.Message.BankNotExist;
        //                break;
        //            case -8206: // Vuot qua han muc cho phep
        //                returnData.Description = Resources.Wallet.RechargeMoney.Error665;
        //                break;
        //            case -8207: // So tien nho hon so tien toi thieu cho phep
        //                returnData.Description = Resources.Wallet.RechargeMoney.Error666;
        //                break;
        //            #endregion Danh sách thông báo lỗi Nạp tiền -82xx

        //            #region Danh sách thông báo lỗi nạp điện thoại trả trước, trả sau, dt cố định, nạp nhiều số 72xx
        //            case -7200: // số điện thoại không hợp lệ
        //                returnData.Description = "Số điện thoại không hợp lệ";
        //                break;
        //            case -7201: // Mệnh giá không hợp lệ
        //                returnData.Description = "Mệnh giá không hợp lệ";
        //                break;
        //            case -7202: // Giao dịch không thành công
        //                returnData.Description = "Giao dịch không thành công. Vui lòng quay lại sau.";
        //                break;
        //            case -7203: // Số tiền nạp phải từ 6000 đến 1 triệu (trả sau)
        //                returnData.Description = "Số tiền nạp phải từ 6.000 đến 1 triệu";
        //                break;
        //            case -7204: // Số tiền nạp phải chẵn nghìn (trả sau)
        //                returnData.Description = "Số tiền nạp phải chẵn nghìn";
        //                break;
        //            case -7205: // Số tiền thanh toán phải từ 10.000 và chẵn nghìn (điện thoại cố định)
        //                returnData.Description = "Số tiền thanh toán phải từ 10.000 và chẵn nghìn";
        //                break;
        //            case -7206: // Cần chọn file danh sách điện thoại để thanh toán (nạp nhiều số)
        //                returnData.Description = "Cần chọn file danh sách điện thoại để thanh toán";
        //                break;
        //            case -7207: // Danh sách số điện thoại không vượt quá 50 số (nạp nhiều số)
        //                returnData.Description = "Danh sách số điện thoại không vượt quá 50 số";
        //                break;
        //            #endregion Danh sách thông báo lỗi nạp điện thoại trả trước, trả sau, dt cố định, nạp nhiều số 72xx

        //            #region Danh sách thông báo lỗi nạp Partner 74xx
        //            case -7400: // chưa nhập tài khoản partner cần nạp
        //                returnData.Description = "Vui lòng nhập tài khoản cần nạp";
        //                break;
        //            case -7401: // tài khoản không hợp lệ
        //                returnData.Description = "Tài khoản nạp không hợp lệ";
        //                break;
        //            case 7401: // tài khoản  hợp lệ
        //                returnData.Description = "Tài khoản nạp hợp lệ";
        //                break;
        //            #endregion Danh sách thông báo lỗi nạp Partner 74xx

        //            #region Danh sách thông báo lỗi Gia hạn truyền hình VTC 75xx
        //            case -7500: // Mã dịch vụ không hợp lệ
        //                returnData.Description = "Mã dịch vụ không hợp lệ";
        //                break;
        //            case -7501: // Mã IC không hợp lệ
        //                returnData.Description = "Mã IC không hợp lệ";
        //                break;
        //            case -7502: //Lỗi ngoài khoảng giá trị gói
        //                returnData.Description = "Lỗi ngoài khoảng giá trị gói";
        //                break;
        //            case -7503: // Loại thẻ gia hạn không hợp đầu thu
        //                returnData.Description = "Loại thẻ gia hạn không hợp đầu thu";
        //                break;
        //            case -7504: // Sai gói kênh
        //                returnData.Description = "Sai gói kênh";
        //                break;
        //            case -7505: // Mã sản phẩm không đúng
        //                returnData.Description = "Mã sản phẩm không đúng";
        //                break;
        //            case -7506: // Giao dịch đang xử lý
        //                returnData.Description = "Giao dịch đang xử lý";
        //                break;
        //            case -7507: //Số lượng hàng không đủ
        //                returnData.Description = "Số lượng hàng không đủ";
        //                break;
        //            case -7508: // Lỗi trong quá trình tạo giao dịch
        //                returnData.Description = "Lỗi trong quá trình tạo giao dịch";
        //                break;
        //            case -7509: // Lỗi trong quá trình gọi service topup
        //                returnData.Description = "Lỗi trong quá trình gọi service topup";
        //                break;
        //            case -7510: // Giao dịch nghi vấn
        //                returnData.Description = "Giao dịch nghi vấn";
        //                break;
        //            #endregion Danh sách thông báo lỗi Gia hạn truyền hình VTC 75xx


        //            default:
        //                //Log4Net.LogInfo("Mã lỗi chưa định nghĩa:" + responseCode);
        //                return GetReturnData(-8999, string.Empty);
        //        }

        //        return returnData;
        //    }
        //    catch (Exception exception)
        //    {
        //        Log4Net.PublishException(exception);
        //        return GetReturnData(-8999, string.Empty);
        //    }
        //}

        #endregion Trả về dữ liệu
    }

}