using DataAccess.MailAPI.Model;

namespace DataAccess.MailAPI.IDAO
{
    public interface ISendMail
    {
        long InsertEmail(Email requestData);
        long EmailUpdateStatus(long logID, int status);
    }
}
