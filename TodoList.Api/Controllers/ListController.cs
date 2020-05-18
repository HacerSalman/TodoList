using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoList.Api.Internal;
using TodoList.Api.ServiceRepository;
using TodoList.Model.Context;
using TodoList.Model.Entities;
using TodoList.Model.Enum;
using TodoList.Model.ResponseModels;

namespace TodoList.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly TodoListDBContext db = null;
        public ListController(TodoListDBContext _db)
        {
            db = _db;
        }

        [HttpPost]
        /// <summary>
        /// Add notes to list
        /// </summary>  
        [ProducesResponseType(typeof(BaseResponse),200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Authorize()]
        public ActionResult AddList([FromBody] List list)
        {
            try
            {
                var token = new ClaimPrincipal(HttpContext.User);
                BaseResponse response = new BaseResponse();
                User user = db.User.FirstOrDefault(u => u.UserName == token.NameIdentifier);
                if (user == null)
                {
                    response.Message = "The user not found!";
                    return NotFound(response);
                }

                //Create list
                var newList = new List()
                {
                    CreatedDate = Utils.GetUnixTimeNow(),
                    Description = list.Description,
                    EndsAt = list.EndsAt,
                    ModifierBy = user.UserName,
                    OwnerBy = user.UserName,
                    Priority = list.Priority,
                    StartsAt = list.StartsAt,
                    Status = (byte)StatusType.Active,
                    Title = list.Title,
                    Type = list.Type,
                    UpdatedDate = Utils.GetUnixTimeNow()
                };
                db.List.Add(newList);
                db.SaveChanges();

                //Create user list
                var userList = new UserList()
                {
                    ListId = newList.Id,
                    ModifierBy = user.UserName,
                    OwnerBy = user.OwnerBy,
                    Status = (byte)StatusType.Active,
                    UpdatedDate = Utils.GetUnixTimeNow(),
                    CreatedDate = Utils.GetUnixTimeNow(),
                    UserId = user.Id
                };
                db.UserList.Add(userList);
                db.SaveChanges();

                response.Message = "The list added successfully";
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        /// <summary>
        ///Update the list
        /// </summary>  
        [ProducesResponseType(typeof(BaseResponse),200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Authorize()]
        public ActionResult UpdateList([FromBody] List list)
        {
            try
            {
                var token = new ClaimPrincipal(HttpContext.User);
                BaseResponse response = new BaseResponse();
                User user = db.User.FirstOrDefault(u => u.UserName == token.NameIdentifier);
                if (user == null)
                {
                    response.Message = "The user not found!";
                    return NotFound(response);
                }
                //Check user list
                var userList = db.UserList.FirstOrDefault(ul => ul.UserId == user.Id && ul.ListId == list.Id);
                if (userList == null)
                    return Unauthorized("Unauthorized!");

                //Check the list
                var currentList = db.List.Find(list.Id);
                if (currentList == null)
                {
                    response.Message = "The list not found!";
                    return NotFound(response);
                }

                currentList.ModifierBy = user.UserName;
                currentList.UpdatedDate = Utils.GetUnixTimeNow();
                currentList.Description = !string.IsNullOrEmpty(list.Description) ? list.Description : currentList.Description;
                currentList.EndsAt = list.EndsAt != null ? list.EndsAt : currentList.EndsAt;
                currentList.Priority = list.Priority != null ? list.Priority : currentList.Priority;
                currentList.StartsAt = list.StartsAt != 0 ? list.StartsAt : currentList.StartsAt;
                currentList.Title = !string.IsNullOrEmpty(list.Title) ? list.Title : currentList.Title;
                currentList.Type = list.Type != 0 ? list.Type : currentList.Type;
                db.SaveChanges();

                response.Message = "The list updated successfully";
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }         

        }

        [HttpPost]
        /// <summary>
        ///Add multiple list
        /// </summary>  
        [ProducesResponseType(typeof(UserListResponse),200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Authorize()]
        public ActionResult AddMultipleList([FromBody] List[] listArray)
        {
            try
            {
                var token = new ClaimPrincipal(HttpContext.User);
                UserListResponse response = new UserListResponse();
                User user = db.User.FirstOrDefault(u => u.UserName == token.NameIdentifier);
                if (user == null)
                {
                    response.Message = "The user not found!";
                    return NotFound(response);
                }

                foreach (var list in listArray)
                {
                    //Create list
                    var newList = new List()
                    {
                        CreatedDate = Utils.GetUnixTimeNow(),
                        Description = list.Description,
                        EndsAt = list.EndsAt,
                        ModifierBy = user.UserName,
                        OwnerBy = user.UserName,
                        Priority = list.Priority,
                        StartsAt = list.StartsAt,
                        Status = (byte)StatusType.Active,
                        Title = list.Title,
                        Type = list.Type,
                        UpdatedDate = Utils.GetUnixTimeNow()
                    };
                    db.List.Add(newList);
                    db.SaveChanges();

                    //Create user list
                    var userList = new UserList()
                    {
                        ListId = newList.Id,
                        ModifierBy = user.UserName,
                        OwnerBy = user.OwnerBy,
                        Status = (byte)StatusType.Active,
                        UpdatedDate = Utils.GetUnixTimeNow(),
                        CreatedDate = Utils.GetUnixTimeNow(),
                        UserId = user.Id
                    };
                    db.UserList.Add(userList);
                    db.SaveChanges();

                }

               response = ListService.GetUserList(db, user.Id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        /// <summary>
        ///Add list type to the list
        /// </summary>  
        [ProducesResponseType(typeof(UserListResponse),200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Authorize()]
        public ActionResult AddTypeToList(long listId,int typeId)
        {
            try
            {
                var token = new ClaimPrincipal(HttpContext.User);
                UserListResponse response = new UserListResponse();
                User user = db.User.FirstOrDefault(u => u.UserName == token.NameIdentifier);
                if (user == null)
                {
                    response.Message = "The user not found!";
                    return NotFound(response);
                }

                //Check user list
                var userList = db.UserList.FirstOrDefault(ul => ul.UserId == user.Id && ul.ListId == listId);
                if (userList == null)
                {
                    response.Message = "Unauthorized!";
                    return Unauthorized(response);
                }
                    
                //Check the list
                var currentList = db.List.FirstOrDefault(l => l.Id == listId);
                if (currentList == null)
                {
                    response.Message = "The list not found!";
                    return NotFound(response);
                }
                    
                //Get list type
                var listType = db.ListType.FirstOrDefault(l => l.Id == typeId && l.Status == (byte)StatusType.Active);
                if (listType == null)
                {
                    response.Message = "The list type not found!";
                    return NotFound(response);
                }
                  
                currentList.ModifierBy = user.UserName;
                currentList.UpdatedDate = Utils.GetUnixTimeNow();
                currentList.Type = listType.Id;

                db.SaveChanges();

                response = ListService.GetUserList(db, user.Id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete]
        /// <summary>
        ///Delete the list
        /// </summary>  
        [ProducesResponseType(typeof(BaseResponse),200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Authorize()]
        public ActionResult DeleteList(long listId)
        {
            try
            {
                var token = new ClaimPrincipal(HttpContext.User);
                BaseResponse response = new BaseResponse();
                User user = db.User.FirstOrDefault(u => u.UserName == token.NameIdentifier);
                if (user == null)
                {
                    response.Message = "The user not found!";
                    return NotFound(response);
                }

                //Check user list
                var userList = db.UserList.FirstOrDefault(ul => ul.UserId == user.Id && ul.ListId == listId);
                if (userList == null)
                {
                    response.Message = "Unauthorized!";
                    return Unauthorized(response);
                }                  

                //Delete user list
                userList.Status = 0;
                userList.UpdatedDate = Utils.GetUnixTimeNow();
                userList.ModifierBy = user.UserName;

                //Check the list
                var currentList = db.List.Find(listId);
                if (currentList == null)
                {
                    response.Message = "The list not found!";
                    return NotFound(response);
                }                 

                //Delete current list
                currentList.Status = (byte)StatusType.Passive;
                currentList.UpdatedDate = Utils.GetUnixTimeNow();
                currentList.ModifierBy = user.UserName;

                db.SaveChanges();

                response.Message = "The list deleted! successfully";
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet]
        /// <summary>
        ///Get user list
        /// </summary>  
        [ProducesResponseType(typeof(UserListResponse),200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Authorize()]
        public ActionResult GetUserList()
        {
            try
            {
                var token = new ClaimPrincipal(HttpContext.User);
                UserListResponse response = new UserListResponse();
                User user = db.User.FirstOrDefault(u => u.UserName == token.NameIdentifier);
                if (user == null)
                {
                    response.Message = "The user not found!";
                    return NotFound(response);
                }

                return Ok(ListService.GetUserList(db, user.Id));           
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}