using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Model.Context;
using TodoList.Model.ResponseModels;

namespace TodoList.Api.ServiceRepository
{
    public class ListService
    {
        public static UserListResponse GetUserList(TodoListDBContext db, long userId)
        {
            UserListResponse response = null;
            var userIdList = db.UserList.Where(ul => ul.UserId == userId).Select( ul => ul.UserId).ToList();
            if(userIdList != null && userIdList.Count >0)
            {
                response.List = db.List.Where(l => userIdList.Contains(l.Id)).ToList();
            }
            response.UserId = userId;

            return response;
            
        }
    }
}
