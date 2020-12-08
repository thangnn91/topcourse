using Topcourse.DataAccess.DAO;
using Topcourse.DataAccess.IDAO;

namespace DataAccess.Topcourse.Factory
{
    public class Factory : AbstractFactory
    {
        public override ITopcourseDAO TopcourseDAO()
        {
            return (ITopcourseDAO)new TopcourseDAO();
        }
    }
}
