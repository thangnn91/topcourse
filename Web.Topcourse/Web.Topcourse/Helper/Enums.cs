namespace Web.Topcourse.Helper
{
    public class Enums
    {
        public enum RegisterType
        {
            Normal = 1,
            OAuthen = 4
        }
        public enum MailServiceID
        {
            SendMailDefault = 10001,
            SendMailActive = 10002
        }
        public enum ChangePassType
        {
            ChangePass = 4,
            ResetPassByEmail = 6
        }

        public enum OAuthSystemID
        {
            Google = 1,
            Facebook = 2
        }

        public enum TagGroupType
        {
            School = 1,
            ShortCourse = 2,
            LongCourse = 3,
            Sholarship = 4
        }

        public enum OrderStatus
        {
            Init = 0,
            Success = 1,
            Fail = 2,
            Pending =3,
            WaitingConfirm = 4
        }

        public enum PaymentService
        {
            CourseFee = 10001,
            TuitionFee = 10002,
            RegisterFee = 10003,
            DocumentFee = 10004,
            SemesterFee1 = 10005,
            SemesterFee2 = 10006
        }

        public enum BannerImageType
        {
            BannerSlider = 1,  //banner slider
            BannerAds = 2,     // banner bên dưới slider 
            BannerDomestic = 3, // banner cho du học trong nước
            BannerForeign = 4,  // banner cho quốc tế
            BannerAbroad = 5    // banner cho du học
        }
    }
}