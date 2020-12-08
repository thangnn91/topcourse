using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topcourse.DataAccess.DTO;
namespace Topcourse.DataAccess.DAO
{
    public interface IUserRoleDAO
    {
        #region Quyền truy cập chức năng
        UserFunction CheckPermission(int UserID, string ActionName);
        int UserFunctionInsert(UserFunction RoleFunction);
        
        int UserFunctionInsertList(int UserID, string ListRole, int CreateUserID, bool IsAdmin); //Thêm ds quyền -> clear toàn bộ chức năng đang tồn tại
        
        //Thêm quyền chức năng cho danh sách user
        int UserFunctionInsertListByFunctionID(int FunctionID, string ListRole, int CreateUserID); 
        int UserFunctionDelete(int UserID, int FunctionID);
        int UserFunctionDeleteList(int UserID, string ListFunction);
        int UserFunctionDeleteAll(int UserID);

        List<UserFunction> UserFunction_GetByUserID(int UserID);
        #endregion

    }
}
