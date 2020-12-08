using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topcourse.DataAccess.DTO;
namespace Topcourse.DataAccess.DAO
{
    public interface IUsersDAO
    {
        Users Authentication(string username, string email, string password, int systemId, ref int responseCode);

        Users SelectByUserID(int userId);
        Users GetByEmail(string email);
        Users GetByUsername(string Username);
        List<Users> GetListUsers(string Keyword, int isActive, int isGrant, string listgroup);

        int UpdateUsers(Users users, string listGroup, string listSystem);

        int DelleteUsers(int userId);
        int UpdateActiveUser(int Id);
        int ChangePassword(string UserName, string PasswordOld, string PasswordNew);
        int UpdateUserLock(int Id, bool isLock);


        #region quản trị nhóm người dùng
        int Group_InsertUpdate(Group group);
        int Group_Delete(int groupUserId);
        List<Group> GetAllGroupUser(int groupId, string name, bool isActive, int systemID);
        #endregion

        #region System 

        List<SystemUser> System_GetList();
        List<User_System> UserSystem_GetList(int userId, int systemId);
        int UserSystem_ActiveOrStandBy(int userId, int system, string createdUser);

        #endregion
    }
}
