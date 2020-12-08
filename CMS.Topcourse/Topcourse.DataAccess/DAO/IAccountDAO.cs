using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topcourse.DataAccess.DTO;

namespace Topcourse.DataAccess.DAO
{
    public interface IAccountDAO
    {

        //User
        Account User_GetSingle(int userId, string username);
        Account User_GetFull(int userId, string username);
        List<Account> GetListUsers(int userId, string username, byte userType, byte status);
        int CMS_RegisterUser(Account account);
        int User_UpdateProfile(Account account);
        int User_Block(Account account);
        int User_UnBlock(Account account);
        int User_ChangePassword(int userId, string newpassword, byte logType, string createdUser, string description);
        int CMS_DeleteUser(int userId, string username, string createdUser);
        List<Location> GetLocation(int locationType, int? parentID, int locationID);



        // Education
        List<Education> Education_GetList(Request_Education requestData);

        Education Education_Detail(int eduId, string alias);

        int SP_Education_Insert(Education requestData);

        int SP_Education_Update(Education requestData);

        int SP_Education_Delete(Request_Education requestData);


        // comment
        int User_Comment_Insert(Comment_Request requestData);
        int User_Comment_Update(Comment_Request requestData);
        int User_Comment_Delete(Comment_Request requestData);
        List<CommentModel> Comment_GetList(Comment_Request requestData);


        // Order
        List<Order_Payment> OrderPayment_Get(long orderId, byte status, byte courseType, DateTime fromDate, DateTime toDate);

        List<Order_Payment> OrderPayment_GetProduct(long orderId);

        int SP_OrderPayment_ConfirmStatus(OrderPayment_ConfirmStatus requestData);
    }
}
