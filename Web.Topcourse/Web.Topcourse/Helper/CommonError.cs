using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Topcourse.Helper
{
    public class CommonError
    {
        public const int DONE = 0;// yeu cau nhap username
        public const int SYSTEM_ERROR = -99;// Captcha không đúng
        public const int INVALID_DATA = -600;// yeu cau nhap username

        public const int CAPTCHA_INVALID = -1001;// Captcha không đúng
        public const int USER_INACTIVE = -1002;
        public const int USER_BLOCK = -1003;
        public const int PASSWORD_INVALID = -1004;
        public const int USER_NOT_ACTIVE = -1005;
        public const int USER_NOT_EXIST = -1006;
        public const int USER_EXISTED = -1007;
        public const int SECURECODE_INVALID = -1008;
        public const int SIGN_INVALID = -1009;
        public const int DATA_EXPIRE = -1010;

        public static int GetErrorCode(int dbError)
        {
            switch (dbError)
            {
                case -600:
                    return INVALID_DATA;
                case -49:
                    return USER_INACTIVE;
                case -48:
                    return USER_BLOCK;
                case -53:
                    return PASSWORD_INVALID;
                case -33:
                    return USER_NOT_ACTIVE;
                case -46:
                    return USER_EXISTED;
                case -50:
                    return USER_NOT_EXIST;
                default:
                    return SYSTEM_ERROR;
            }
        }
    }
}