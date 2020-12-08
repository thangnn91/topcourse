using System;
using System.Collections.Generic;
using Topcourse.DataAccess.DTO;

namespace Topcourse.DataAccess.DAO
{
    public interface IFucntionsDAO
    {
        int GetFunctionID(string functionname);

    	List<Functions> GetListFunctionByUserId(int userId, int grant, string systemId);

        List<Functions> GetListFunctions(string keyword, int parentId, int systemId, int status, int pageNumber, int pageSize, ref int totalRecord);

        int InsertUpdateFunction(Functions functions);

        int UpdateOrder(int functionId, int parentId, int order);

    	int DelleteFunction(int functionId);

        List<Functions> GetFunction_ByCondition(int fatherId, int functionId, string functionName, string actionName, int isactive, int isdisplay, string systemId);
    }
}
