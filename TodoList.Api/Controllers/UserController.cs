using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoList.Api.Internal;
using TodoList.Model.Context;
using TodoList.Model.Entities;
using TodoList.Model.Enum;
using TodoList.Model.RequestModels;

namespace TodoList.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TodoListDBContext db = null;
        public UserController(TodoListDBContext _db)
        {
            db = _db;
        }

        [HttpGet]
        /// <summary>
        /// Login
        /// </summary>  
        [ProducesResponseType(typeof(User),200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public ActionResult Login()
        {
            try
            {
                User user = null;
                //Check Basic Authentication
                Microsoft.Extensions.Primitives.StringValues authorizationToken;
                HttpContext.Request.Headers.TryGetValue("Authorization", out authorizationToken);
                if (Utils.CheckBasicAuth(db, authorizationToken,ref user))
                    return Ok(user);
                else
                    return Unauthorized("Username or password incorrect!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }         
        }

        [HttpPost]
        /// <summary>
        /// Sign up
        /// </summary>  
        [ProducesResponseType(typeof(User),200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public ActionResult SignUp([FromBody]SignUpRequest signUpUser)
        {            
            try
            {
                //Check request body object
                if(signUpUser == null)
                {
                    return BadRequest("Request bosy can not be empty!");
                }

                //Check username 
                var user = db.User.FirstOrDefault(u => u.UserName == signUpUser.UserName.Trim());
                if(user != null)
                {
                    if(user.Status == (byte)StatusType.Active)
                        return StatusCode(409, "The username is already using");
                    else
                    {
                        user.Status = (byte)StatusType.Active;
                        user.FullName = signUpUser.UserName;
                        user.ModifierBy = signUpUser.UserName;
                        user.UpdatedDate = Utils.GetUnixTimeNow();
                        user.Password = Utils.EncodePassword(signUpUser.Password);
                        db.SaveChanges();
                        return Ok(user);
                    }

                }
                else
                {
                    //Create new user
                    var newUser = new User()
                    {
                        CreatedDate = Utils.GetUnixTimeNow(),
                        FullName = signUpUser.FullName,
                        ModifierBy = signUpUser.UserName,
                        OwnerBy = signUpUser.UserName,
                        Password = Utils.EncodePassword(signUpUser.Password),
                        Status = (byte)StatusType.Active,
                        UpdatedDate = Utils.GetUnixTimeNow(),
                        UserName = signUpUser.UserName
                    };

                    db.User.Add(newUser);
                    db.SaveChanges();
                    return Ok(newUser);
                }
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
          
        }

        [HttpDelete]
        /// <summary>
        /// Delete the user
        /// </summary>  
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult DeleteUser()
        {
            try
            {
                User user = null;
                //Check Basic Authentication
                Microsoft.Extensions.Primitives.StringValues authorizationToken;
                HttpContext.Request.Headers.TryGetValue("Authorization", out authorizationToken);
                if (Utils.CheckBasicAuth(db, authorizationToken,ref user))
                {
                    if (user == null)
                    {
                        return NotFound("The user not found!");
                    }

                    //Delete user
                    user.Status = (byte)StatusType.Passive;
                    user.UpdatedDate = Utils.GetUnixTimeNow();
                    user.ModifierBy = user.UserName;
                    db.SaveChanges();
                    return Ok("The user deleted!");
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
        /// Update the user
        /// </summary>  
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult UpdateUser([FromBody] UpdateUserRequest request)
        {
            try
            {
                User user = null;
                //Check request body object
                if (request == null)
                {
                    return BadRequest("Request bosy can not be empty!");
                }

                //Check Basic Authentication
                Microsoft.Extensions.Primitives.StringValues authorizationToken;
                HttpContext.Request.Headers.TryGetValue("Authorization", out authorizationToken);
                if (Utils.CheckBasicAuth(db, authorizationToken,ref user))
                {
                    if (user == null)
                    {
                        return NotFound("The user not found!");
                    }

                    user.ModifierBy = user.UserName;
                    user.UpdatedDate = Utils.GetUnixTimeNow();
                    user.FullName = !string.IsNullOrEmpty(request.FullName) ? request.FullName : user.FullName;
                    user.Password = !string.IsNullOrEmpty(request.Password) ? Utils.EncodePassword(request.Password) : user.Password;
                
                    db.SaveChanges();
                    return Ok("The user updated!");
                }
                else
                    return Unauthorized("Unauthorized!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        /// <summary>
        /// Get the user
        /// </summary>  
        [ProducesResponseType(typeof(User),200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult GetUser()
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

                    return Ok(user);
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