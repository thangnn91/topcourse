using System;
using System.Configuration;
using System.Linq;
using System.Web;
using Topcourse.DataAccess.DTO;
using Topcourse.DataAccess.Factory;
using Topcourse.Utility;

namespace CMS.Topcourse.Controllers.Common
{
    public class UserValidation : SSOInfo
    {
        public UserValidation()
        {
            try
            {
                Get();
            }
            catch
            {
                SignOut();
            }
        }


        public int CheckLogin(string userName, string PassWord)
        {
            var pop = new POP3Auth();
            int nRet = pop.CheckAuth(UserName.ToLower().Trim(), PassWord.Trim()) == "OK" ? 1 : -1;
            if (nRet == -1)
                return nRet;
            //DBSiteUser m_SiteUser = new DBSiteUser();
            //List<DBSiteUser> l_SiteUser = new DBSiteUser().GetByUserName(DBDBCommon.SiteName,UserName);
            //if (l_SiteUser == null || l_SiteUser.Count == 0)
            //    nRet = 0;
            return nRet;
        }

        /// <summary>
        /// Kiểm tra quyền theo chức năng truy cập -> nếu có quyền ? lưu session permission : return error
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        public bool CheckPermissionUser(string Action, int UserId, bool IsAdmin = false)
        {

            try
            {
                if (string.IsNullOrEmpty(Action))
                {
                    return false;
                }
                var m_function = AbstractDAOFactory.Instance().CreateFunctionDAO().
                    GetFunction_ByCondition(-1, -1, string.Empty, Action, 1, -1, ConfigurationManager.AppSettings["SystemID"]).SingleOrDefault();
                if (m_function == null || m_function.FunctionID <= 0)
                    return false;

                var permission = new UserFunction();
                //Nếu user là admin -> full quyền
                if (IsAdmin)
                {
                    permission.FunctionID = m_function.FunctionID;
                    permission.FunctionName = m_function.FunctionName;
                    permission.ActionName = Action;
                    permission.UserID = UserId;
                    permission.IsGrant = true;
                    permission.IsInsert = true;
                    permission.IsUpdate = true;
                    permission.IsDelete = true;
                }
                else //Lấy quyền của user khi truy cập chức năng
                {
                    permission = AbstractDAOFactory.Instance().CreateUserRoleDAO().CheckPermission(UserId, Action);
                }

                if (permission == null || permission.FunctionID == 0)
                {
                    HttpContext.Current.Session.Abandon();
                    return false;
                }
                HttpContext.Current.Session[SessionsManager.SESSION_PERMISSION] = permission;

                HttpContext.Current.Session[SessionsManager.SESSION_HISTORY] = permission.FunctionID;
                return true;
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(ex.ToString());
                return false;
            }
        }

        public override bool IsSigned()
        {
            bool b = base.IsSigned();
            if (!b)
            {
                //SignOut();
                return false;
            }
            return true;

        }

        public override sealed void SignOut()
        {
            base.SignOut();
            //SetCookie(CookieOwnerName, "");
            //SetCookie(CookieProcessIDName, "");
            //NewsletterSignOut();
        }

        public override bool NewsletterIsSigned()
        {
            bool b = base.NewsletterIsSigned();
            if (!b)
            {
                NewsletterSignOut();
                return false;
            }
            return true;

        }
    }
}