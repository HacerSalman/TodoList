using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Model.Context;
using TodoList.Model.Entities;
using TodoList.Model.ResponseModels;

namespace TodoList.Api.ServiceRepository
{
    public class ListService
    {
        public static UserListResponse GetUserList(TodoListDBContext db, long userId)
        {
            UserListResponse response = null;         

            var list = from ul in db.UserList
                    join l in db.List
                    on ul.ListId equals l.Id
                    where ul.UserId == userId &&
                    ul.Status == 1 &&
                    l.Status == 1
                    select l;

            response.List = list.ToList();
            response.UserId = userId;

            return response;
            
        }
    }
}
