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
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [AllowAnonymous]
        public ActionResult Login()
        {
            try
            {
                //Check Basic Authentication
                Microsoft.Extensions.Primitives.StringValues authorizationToken;
                HttpContext.Request.Headers.TryGetValue("Authorization", out authorizationToken);
                if (Utils.CheckBasicAuth(db, authorizationToken))
                    return Ok();
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
        [AllowAnonymous]
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
                var user = db.User.Where(u => u.UserName == signUpUser.UserName.Trim()).FirstOrDefault();
                if(user != null)
                {
                    return StatusCode(409, "The username is already using");
                }

                //Create new user
                var newUser = new User()
                {
                    CreatedDate = Utils.GetUnixTimeNow(),
                    FullName = signUpUser.UserName,
                    ModifierBy = signUpUser.UserName,
                    OwnerBy = signUpUser.FullName,
                    Password = Utils.EncodePassword(signUpUser.Password),
                    Status = 1,
                    UpdatedDate = Utils.GetUnixTimeNow(),
                    UserName = signUpUser.UserName
                };

                db.User.Add(newUser);
                db.SaveChanges();
                return Ok(newUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
          
        }

        [HttpDelete]
        [Route("user/{id}/delete")]
        /// <summary>
        /// Delete the user
        /// </summary>  
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult DeleteUser(long id)
        {
            try
            {
                //Check Basic Authentication
                Microsoft.Extensions.Primitives.StringValues authorizationToken;
                HttpContext.Request.Headers.TryGetValue("Authorization", out authorizationToken);
                if (Utils.CheckBasicAuth(db, authorizationToken))
                {
                    var user = db.User.Where(u => u.Id == id).FirstOrDefault();
                    if (user == null)
                    {
                        return NotFound("The user not found!");
                    }

                    db.User.Remove(user);
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
                //Check request body object
                if (request == null)
                {
                    return BadRequest("Request bosy can not be empty!");
                }

                //Check Basic Authentication
                Microsoft.Extensions.Primitives.StringValues authorizationToken;
                HttpContext.Request.Headers.TryGetValue("Authorization", out authorizationToken);
                if (Utils.CheckBasicAuth(db, authorizationToken))
                {
                    var user = db.User.Where(u => u.Id == request.Id).FirstOrDefault();
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
        [Route("user/{id}")]
        /// <summary>
        /// Get the user
        /// </summary>  
        [ProducesResponseType(typeof(User),200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult GetUser(long id)
        {
            try
            {
                //Check Basic Authentication
                Microsoft.Extensions.Primitives.StringValues authorizationToken;
                HttpContext.Request.Headers.TryGetValue("Authorization", out authorizationToken);
                if (Utils.CheckBasicAuth(db, authorizationToken))
                {
                    var user = db.User.Where(u => u.Id == id).FirstOrDefault();
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