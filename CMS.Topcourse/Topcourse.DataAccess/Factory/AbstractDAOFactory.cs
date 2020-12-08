using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topcourse.DataAccess.DAO;
using Topcourse.DataAccess.DAOImpl;

namespace Topcourse.DataAccess.Factory
{
    public abstract class AbstractDAOFactory
    {
        public static AbstractDAOFactory Instance()
        {
            try
            {
                return (AbstractDAOFactory)new ADODAOFactory();
            }
            catch (Exception ex)
            {

                throw new Exception("Couldn't create AbstractDAOFactory: ");
            }
        }
        public abstract IUsersDAO CreateUsersDAO();
        public abstract IUsersLogDAO CreateUsersLogDAO();
        public abstract IErrorLogDAO CreateErrorLogDAO();
        public abstract IFucntionsDAO CreateFunctionDAO();
        public abstract IUserRoleDAO CreateUserRoleDAO();
        public abstract IAccountDAO CreateAccountDAO();
        public abstract ICourseDAO CreateCourseDAO();
        public abstract IPostDAO CreatePostDAO();
    }
}
