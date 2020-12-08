using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Topcourse.Utility
{
    public static class PolicyUtil
    {
        /// <summary>
        /// * >= 8 kí tự
        ///* Cả số cả chữ
        ///* Không chứa username (tên tài khoản)
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool CheckPassword(string password, int minLength, int maxLength)
        {
            password = password.Trim();
            if (password.Length < minLength || password.Length > maxLength)
            {
                return false;
            }

            var fillterChar = "ABCDEFGHIJKLMNOPQRSTUVXYZWabcdefghijklmnopqrstuvxyzw0123456789~!@#$%^&*()_+=<>?|:;[]";
            return password.All(t => fillterChar.IndexOf(t) >= 0);
        }


        public static bool CheckPassport(string passport)
        {
            passport = passport.Trim();
            if (passport.Length < 9 || passport.Length > 14)
            {
                return false;
            }
            return true;
        }


        public static bool CheckMobile(string mobile)
        {
            mobile = mobile.Trim();
            if (mobile.Length < 10 || mobile.Length > 11)
                return false;
            if (!mobile.StartsWith("0")) return false;

            if (!Regex.IsMatch(mobile.Trim(), @"^[0-9]{9,12}$", RegexOptions.IgnoreCase))
            {
                return false;
            }
            return true;
        }
        private static string[] m_Patterns = new string[] { @"^[0-9]{10}$", @"^\+[0-9]{2}\s+[0-9]{2}[0-9]{8}$", @"^[0-9]{3}-[0-9]{4}-[0-9]{4}$" };

        private static string MakeCombinedPattern()
        {
            return string.Join("|", m_Patterns
              .Select(item => "(" + item + ")"));
        }
        public static bool CheckUserName(string userName)
        {
            return CheckMobile(userName);
        }
        public static string verifyPhoneNumber(string phone)
        {
            if (phone.StartsWith("840"))
                return phone.Replace("840", "0");
            if (phone.StartsWith("84"))
                return phone.Replace("84", "0");
            if (!phone.StartsWith("0"))
                return 0 + phone;
            return phone;
        }



        public static bool CheckEmail(string email)
        {
            //Kiểm tra định dạng email
            return Regex.IsMatch(email.Trim(), @"^([0-9a-z]+[-._+&])*[0-9a-z]+@([-0-9a-z]+[.])+[a-z]{2,6}$", RegexOptions.IgnoreCase);
        }
        public static bool CheckBirthDate(string birthdate)
        {
            return Regex.IsMatch(birthdate.Trim(), @"/(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)/g", RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// Check xem email co phai gmail ko
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns>
        /// true: gmail
        /// </returns>
        public static bool CheckIsGmail(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress)) return false;
            var listgmail = new List<string> { "gmail.com", "googlemail.com" };
            var domain = emailAddress.Substring(emailAddress.IndexOf('@') + 1);
            return listgmail.Exists(e => domain.ToLower().StartsWith(e));
        }

        /// <summary>
        /// Kiểm tra độ mạnh của password
        /// pass phải từ 6-16 ký tự, bao gồm cả chữ cái và chữ số
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool IsPasswordStrong(string password)
        {
            return Regex.IsMatch(password.Trim(), @"^(?=.{6,16})(?=.*\d)(?=.*[a-zA-Z]).*$");
        }

        public static List<string> SplitFullNameIntoNameAndSurname(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
                return new List<string>(new string[] { string.Empty, string.Empty });
            var names = fullName.Split(' ');
            int arrayLength = names.Length;
            if (arrayLength == 1)
                return new List<string>(new string[] { names[0], names[0] });
            return new List<string>(new string[] { names[arrayLength - 1], string.Join(" ", names.Take(names.Length - 1)) });
        }

        public static int LineNumber([System.Runtime.CompilerServices.CallerLineNumber] int lineNumber = 0)
        {
            try
            {
                return lineNumber;
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return -99;
            }

        }
    }
}
