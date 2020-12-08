using MailService.Model;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace MailService.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISendMail" in both code and config file together.
    [ServiceContract]
    public interface ISendMailService
    {
        [OperationContract]
        ResponseData SendEmailManual(EmailModel requestSendMail);
    }
}
