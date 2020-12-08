using System;

namespace Common.Model
{
    public class ProfileStudent
    {
        public long UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public string CreatedUser { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public byte Lang { get; set; } = 1;//1:vi; orther: en
        public string FullName { get; set; }//1:vi; orther: en      
        public int RegisterType { get; set; }
        public string UserProfileInfo { get; set; }
        public byte DeviceType { get; set; } = 1; //1:web; 2:mobile
        public string ClientIP { get; set; }
        public string CaptchaResponse { get; set; }
    }
    public class UserAuthen
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public byte DeviceType { get; set; } = 1;
        public string ClientIP { get; set; }
    }

    public class UserInfo
    {
        public int UserID { set; get; }
        public string Username { set; get; }
        //public string Password { set; get; }
        public string Email { set; get; }
        public string Mobile { set; get; }
        public byte UserType { set; get; }
        public byte Status { set; get; }
        public string CreatedUser { set; get; }
        public DateTime CreatedDate { set; get; }
        public string Fullname { set; get; }
        public byte VerifyType { set; get; }
        public string Firstname { set; get; }
        public string Lastname { set; get; }
        public string Address { set; get; }
        public string Gender { set; get; }
        public string Avatar { set; get; }
    }

    public class UserInfoUpdate: UserInfo
    {
        //Dung cho update
        public string UserProfileInfo { set; get; }       
        public string ClientIP { set; get; }
    }

    public class UpdatePassword
    {
        public int UserID { set; get; }
        public string Password { set; get; }//mk cu
        public string PasswordNew { set; get; }// mk moi
        public string ClientIP { set; get; }
        public byte LogType { set; get; }//--4:ChangePassword, 5: ResetPassBySMS, 6: ResetPassByEmail, 7: ResetPassBySupport(CMS)
        public string CreatedUser { set; get; }
        public string Description { set; get; }
    }

    public class UpdateAvatar
    {
        public int UserID { set; get; }
        public string Avatar { set; get; }
        public string ClientIP { set; get; }
    }

    public class OAuthenAccount
    {
        public int OAuthID { set; get; }
        public int UserID { set; get; }//userid
        public string OAuthAccount { set; get; } //unix id gg hoac fb tra ve
        public int OAuthSystemID { set; get; }//--1:GG,2:FB, 3:YH
        public DateTime CreatedTime { set; get; }
        public string ClientIP { set; get; }
    }

}
