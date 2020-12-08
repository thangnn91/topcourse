using DataAccess.MailAPI.DAO;
using DataAccess.MailAPI.IDAO;

namespace DataAccess.MailAPI.Factory
{
    public class Factory : AbstractFactory
    {
        public override ISendMail SendMailDAO()
        {
            return (ISendMail)new SendMail();
        }
    }
}
