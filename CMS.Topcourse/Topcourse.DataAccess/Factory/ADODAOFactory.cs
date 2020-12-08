using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topcourse.DataAccess.DAO;
using Topcourse.DataAccess.DAOImpl;
namespace Topcourse.DataAccess.Factory
{
    public class ADODAOFactory : AbstractDAOFactory
    {
        public override IUsersDAO CreateUsersDAO()
        {
            return new UsersDAOImpl();
        }

        public override IUsersLogDAO CreateUsersLogDAO()
        {
            return new UsersLogDAOImpl();
        }
        public override IErrorLogDAO CreateErrorLogDAO()
        {
            return new ErrorLogDAOImpl();
        }
        public override IFucntionsDAO CreateFunctionDAO()
        {
            return new FunctionsDAOImpl();
        }
        public override IUserRoleDAO CreateUserRoleDAO()
        {
            return new UserRoleDAOImpl();
        }

        public override IAccountDAO CreateAccountDAO()
        {
            return new AccountDAOImpl();
        }

        public override ICourseDAO CreateCourseDAO()
        {
            return new CourseDAOImpl();
        }

        public override IPostDAO CreatePostDAO()
        {
            return new PostDAOlmpl();
        }
    }
}
