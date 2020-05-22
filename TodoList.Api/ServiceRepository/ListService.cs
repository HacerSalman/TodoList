using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Model.Context;
using TodoList.Model.Entities;
using TodoList.Model.Enum;
using TodoList.Model.ResponseModels;

namespace TodoList.Api.ServiceRepository
{
    public class ListService
    {
        public static UserListResponse GetUserList(TodoListDBContext db, long userId)
        {
            UserListResponse response = new UserListResponse();

            var list = from ul in db.UserList
                       join l in db.List
                       on ul.ListId equals l.Id
                       join lt in db.ListType
                       on l.Type equals lt.Id
                       where ul.UserId == userId &&
                       ul.Status == (byte)StatusType.Active &&
                       l.Status == (byte)StatusType.Active
                       select new List()
                       {
                           TypeName = lt.Name,
                           CreatedDate = l.CreatedDate,
                           Description = l.Description,
                           EndsAt = l.EndsAt,
                           Id = l.Id,
                           ModifierBy = l.ModifierBy,
                           OwnerBy = l.OwnerBy,
                           Priority = l.Priority,
                           StartsAt = l.StartsAt,
                           Status = l.Status,
                           Title = l.Title,
                           Type = l.Type,
                           UpdatedDate = l.UpdatedDate
                       };

            response.List = list.ToList();
            response.UserId = userId;

            return response;
            
        }
    }
}
