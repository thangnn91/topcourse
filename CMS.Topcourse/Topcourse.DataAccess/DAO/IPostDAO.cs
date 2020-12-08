using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topcourse.DataAccess.DTO;

namespace Topcourse.DataAccess.DAO
{
    public interface IPostDAO
    {
        int Insert_Post(Post requestData);
        int Update_Post(Post requestData);
        int UpdateStatus_Post(int postId, int status, string actionUser);
        int Delete_Post(int postId, string createdUser);
        //PageResult<Post> GetListPost(string keyword, int pageIndex, int pageSize);
        List<Post> GetAllPost(PostRequest requestData);
        Post GetInfo_Post(int postId);       

    }
}
