using System;
using Topcourse.DataAccess.IDAO;

namespace DataAccess.Topcourse.Factory
{

    public abstract class AbstractFactory
    {
        public static AbstractFactory Instance()
        {
            try
            {
                return (AbstractFactory)new Factory();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't create AbstractDAOFactory: {ex.Message}");
            }
        }
        public abstract ITopcourseDAO TopcourseDAO();
    }
}
