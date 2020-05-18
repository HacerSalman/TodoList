using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize()]
        public ActionResult GetUserListType()
        {
            try
            {
                var token = new ClaimPrincipal(HttpContext.User);
                UserListTypeResponse response = new UserListTypeResponse();
                User user = db.User.FirstOrDefault(u => u.UserName == token.NameIdentifier);
                if (user == null)
                {
                    response.Message = "The user not found!";
                    return NotFound(response);
                }

                //Get user type list
                var type =
                 from ut in db.UserListType
                 join lt in db.ListType
                 on ut.ListTypeId equals lt.Id
                 where ut.UserId == user.Id &&
                 ut.Status == (byte)StatusType.Active &&
                 lt.Status == (byte)StatusType.Active
                 select lt;

                response = new UserListTypeResponse()
                {
                    List = type.ToList(),
                    UserId = user.Id
                };

                return Ok(response);

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
        [ProducesResponseType(typeof(BaseResponse),200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [Authorize()]
        public ActionResult AddListType([FromBody] ListType listType)
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

                //Create list type
                var newListType = new ListType()
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

                response.Message = "The list type added successfully";
                return Ok(response);
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
        [ProducesResponseType(typeof(BaseResponse),200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [Authorize()]
        public ActionResult UpdateListType([FromBody] ListType listType)
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
                var userListType = db.UserListType.FirstOrDefault(ul => ul.UserId == user.Id && ul.ListTypeId == listType.Id);
                if (userListType == null)
                {
                    response.Message = "Unauthorized!";
                    return Unauthorized(response);
                }
                   
                //Check the list
                var currentListType = db.ListType.Find(listType.Id);
                if (currentListType == null)
                {
                    response.Message = "The list not found!";
                    return NotFound(response);
                }                

                currentListType.ModifierBy = user.UserName;
                currentListType.UpdatedDate = Utils.GetUnixTimeNow();
                currentListType.Description = !string.IsNullOrEmpty(listType.Description) ? listType.Description : currentListType.Description;
                currentListType.Name = !string.IsNullOrEmpty(listType.Name) ? listType.Name : currentListType.Name;
                db.SaveChanges();

                response.Message = "The list type updated successfully";
                return Ok(response);
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
        [ProducesResponseType(typeof(BaseResponse),200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [Authorize()]
        public ActionResult DeleteListType(int typeId)
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
                var userListType = db.UserListType.FirstOrDefault(ul => ul.UserId == user.Id && ul.ListTypeId == typeId);
                if (userListType == null)
                {
                    response.Message = "The user list type not found!";
                    return Unauthorized(response);
                }                 

                //Delete user list
                userListType.Status = (byte)StatusType.Passive;
                userListType.UpdatedDate = Utils.GetUnixTimeNow();
                userListType.ModifierBy = user.UserName;

                //Check the list
                var currentListType = db.ListType.Find(typeId);
                if (currentListType == null)
                {
                    response.Message = "The list type not found!";
                    return NotFound(response);
                }
               
                //Check list type in active list
                var activeList = db.List.Where(l => l.Type == currentListType.Id && l.Status == (byte)StatusType.Active).ToList();
                if (activeList.Count > 0)
                {
                    response.Message = "This list type using for active list. Firstly, delete the list and then try delete the list type.";
                    return StatusCode(414, response);
                }
                  
                //Delete current list
                currentListType.Status = (byte)StatusType.Passive;
                currentListType.UpdatedDate = Utils.GetUnixTimeNow();
                currentListType.ModifierBy = user.UserName;
                db.SaveChanges();

                response.Message = "The list type deleted successfully!";
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}