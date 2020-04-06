using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoList.Api.Internal;
using TodoList.Model.Context;
using TodoList.Model.Entities;
using TodoList.Model.Enum;
using TodoList.Model.ResponseModels;

namespace TodoList.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ListTypeController : ControllerBase
    {
        private readonly TodoListDBContext db = null;
        public ListTypeController(TodoListDBContext _db)
        {
            db = _db;
        }

        [HttpGet]
        /// <summary>
        ///Get user list type
        /// </summary>  
        [ProducesResponseType(typeof(UserListTypeResponse), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public ActionResult GetUserListType()
        {
            try
            {
                User user = null;
                //Check Basic Authentication
                Microsoft.Extensions.Primitives.StringValues authorizationToken;
                HttpContext.Request.Headers.TryGetValue("Authorization", out authorizationToken);
                if (Utils.CheckBasicAuth(db, authorizationToken, ref user))
                {
                    //Get user type list
                   var type =  
                    from ut in db.UserListType
                    join lt in db.ListType
                    on ut.ListTypeId equals lt.Id
                    where ut.UserId == user.Id &&
                    ut.Status == (byte)StatusType.Active &&
                    lt.Status == (byte)StatusType.Active
                    select lt;

                    var response = new UserListTypeResponse()
                    {
                        List = type.ToList(),
                        UserId = user.Id
                    };

                    return Ok(response);
                }
                else
                    return Unauthorized("Unauthorized!");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost]
        /// <summary>
        ///Add list type
        /// </summary>  
        [ProducesResponseType(typeof(ListType),200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public ActionResult AddListType([FromBody] ListType listType)
        {
            try
            {
                User user = null;
                //Check Basic Authentication
                Microsoft.Extensions.Primitives.StringValues authorizationToken;
                HttpContext.Request.Headers.TryGetValue("Authorization", out authorizationToken);
                if (Utils.CheckBasicAuth(db, authorizationToken, ref user))
                {
                    if (user == null)
                    {
                        return NotFound("The user not found!");
                    }
                    else
                    {
                        //Create list type
                        var newListType= new ListType()
                        {
                            CreatedDate = Utils.GetUnixTimeNow(),
                            Description = listType.Description,                 
                            ModifierBy = user.UserName,
                            OwnerBy = user.UserName,
                            Status = (byte)StatusType.Active,
                            UpdatedDate = Utils.GetUnixTimeNow(),
                            Name = listType.Name
                        };
                        db.ListType.Add(newListType);
                        db.SaveChanges();

                        //Create user list type
                        var userListType = new UserListType()
                        {
                            ListTypeId = newListType.Id,
                            ModifierBy = user.UserName,
                            OwnerBy = user.OwnerBy,
                            Status = (byte)StatusType.Active,
                            UpdatedDate = Utils.GetUnixTimeNow(),
                            CreatedDate = Utils.GetUnixTimeNow(),
                            UserId = user.Id
                        };
                        db.UserListType.Add(userListType);
                        db.SaveChanges();

                        return Ok(newListType);
                    }

                }
                else
                    return Unauthorized("Unauthorized!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut]
        /// <summary>
        ///Update list type
        /// </summary>  
        [ProducesResponseType(typeof(ListType),200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public ActionResult UpdateListType([FromBody] ListType listType)
        {
            try
            {
                User user = null;
                //Check Basic Authentication
                Microsoft.Extensions.Primitives.StringValues authorizationToken;
                HttpContext.Request.Headers.TryGetValue("Authorization", out authorizationToken);
                if (Utils.CheckBasicAuth(db, authorizationToken, ref user))
                {
                    //Check user list
                    var userListType = db.UserListType.FirstOrDefault(ul => ul.UserId == user.Id && ul.ListTypeId == listType.Id);
                    if (userListType == null)
                        return Unauthorized("Unauthorized!");

                    //Check the list
                    var currentListType = db.ListType.Find(listType.Id);
                    if (currentListType == null)
                        return NotFound("The list not found!");

                    currentListType.ModifierBy = user.UserName;
                    currentListType.UpdatedDate = Utils.GetUnixTimeNow();
                    currentListType.Description = !string.IsNullOrEmpty(listType.Description) ? listType.Description : currentListType.Description;
                    currentListType.Name = !string.IsNullOrEmpty(listType.Name) ? listType.Name : currentListType.Name;                 
                    db.SaveChanges();

                    return Ok(currentListType);
                }
                else
                    return Unauthorized("Unauthorized!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }

        [HttpDelete]
        /// <summary>
        ///Delete list type
        /// </summary>  
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public ActionResult DeleteListType(int typeId)
        {
            try
            {
                User user = null;
                //Check Basic Authentication
                Microsoft.Extensions.Primitives.StringValues authorizationToken;
                HttpContext.Request.Headers.TryGetValue("Authorization", out authorizationToken);
                if (Utils.CheckBasicAuth(db, authorizationToken, ref user))
                {
                    //Check user list
                    var userListType = db.UserListType.FirstOrDefault(ul => ul.UserId == user.Id && ul.ListTypeId == typeId);
                    if (userListType == null)
                        return Unauthorized("Unauthorized!");

                    //Delete user list
                    userListType.Status = (byte)StatusType.Passive;
                    userListType.UpdatedDate = Utils.GetUnixTimeNow();
                    userListType.ModifierBy = user.UserName;

                    //Check the list
                    var currentListType = db.ListType.Find(typeId);
                    if (currentListType == null)
                        return NotFound("The list type not found!");

                    //Check list type in active list
                    var activeList = db.List.Where(l => l.Type == currentListType.Id && l.Status == (byte)StatusType.Active).ToList();
                    if (activeList.Count > 0)
                        return StatusCode(414, "This list type using for active list. Firstly, delete the list and then try delete the list type.");

                    //Delete current list
                    currentListType.Status = (byte)StatusType.Passive;
                    currentListType.UpdatedDate = Utils.GetUnixTimeNow();
                    currentListType.ModifierBy = user.UserName;

                    db.SaveChanges();

                    return Ok("The list type deleted!");
                }
                else
                    return Unauthorized("Unauthorized!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}