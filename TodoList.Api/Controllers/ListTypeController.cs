using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoList.Api.Internal;
using TodoList.Model.Context;
using TodoList.Model.Entities;
using TodoList.Model.ResponseModels;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
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
                    ut.Status == 1 &&
                    lt.Status == 1
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
    }
}