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
using TodoList.Model.ResponseModels;

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
        [ProducesResponseType(typeof(UserResponse),200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public ActionResult Login(string username, string password)
        {
            UserResponse response = new UserResponse();
            try
            {
                var encodePassword = Utils.EncodePassword(password.Trim());
                var user = db.User.FirstOrDefault(u => u.UserName == username.Trim() && u.Password == encodePassword && u.Status == (byte)StatusType.Active);
                if (user == null)
                {
                    response.Message = "Username or password incorrect!";
                    return Unauthorized(response);
                }                 
                else
                {
                    response = new UserResponse()
                    {
                        CreatedDate = user.CreatedDate,
                        FullName = user.FullName,
                        Status = user.Status,
                        Token = ClaimPrincipal.GenerateToken(user.UserName),
                        UserId = user.Id,
                        UserName = user.UserName
                    };
                    return Ok(response);
                }             
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
        [ProducesResponseType(typeof(BaseResponse),200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Authorize()]
        public ActionResult DeleteUser(long userId)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var token = new ClaimPrincipal(HttpContext.User);
                User user = db.User.FirstOrDefault(u => u.UserName == token.NameIdentifier);
                if (user == null)
                {
                    response.Message = "The user not found!";
                    return NotFound(response);
                }
                if(user.Id != userId)
                {
                    response.Message = "Unauthorized";
                    return Unauthorized(response);
                }

                //Delete user
                user.Status = (byte)StatusType.Passive;
                user.UpdatedDate = Utils.GetUnixTimeNow();
                user.ModifierBy = user.UserName;
                db.SaveChanges();

                response.Message = "The user deleted!";
                return Ok(response);             
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
        [ProducesResponseType(typeof(UserResponse),200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Authorize()]
        public ActionResult UpdateUser([FromBody] UpdateUserRequest request)
        {
            try
            {
                UserResponse response = new UserResponse();
                var token = new ClaimPrincipal(HttpContext.User);

                //Check request body object
                if (request == null)
                {
                    response.Message = "Request body can not be empty!";
                    return BadRequest(response);
                }

                //Check authorization
                if (token.NameIdentifier != request.Username)
                {
                    response.Message = "Request body can not be empty!";
                    return Unauthorized(response);
                }

                User user = db.User.FirstOrDefault(u => u.UserName == token.NameIdentifier);
                if (user == null)
                {
                    response.Message = "The user not found!";
                    return NotFound(response);
                }

                user.ModifierBy = user.UserName;
                user.UpdatedDate = Utils.GetUnixTimeNow();
                user.FullName = !string.IsNullOrEmpty(request.FullName) ? request.FullName : user.FullName;
                user.Password = !string.IsNullOrEmpty(request.Password) ? Utils.EncodePassword(request.Password) : user.Password;
                db.SaveChanges();

                response = new UserResponse()
                {
                    CreatedDate = user.CreatedDate,
                    FullName = user.FullName,
                    Message = "The user updated!",
                    Status = user.Status,
                    UserId = user.Id,
                    UserName = user.UserName
                };

                return Ok(response);

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
        [ProducesResponseType(typeof(UserResponse),200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Authorize()]
        public ActionResult GetUser()
        {
            try
            {
                var token = new ClaimPrincipal(HttpContext.User);
                UserResponse response = new UserResponse();
                User user = db.User.FirstOrDefault(u => u.UserName == token.NameIdentifier);
                if (user == null)
                {
                    response.Message = "The user not found!";
                    return NotFound(response);
                }
                response = new UserResponse()
                {
                    CreatedDate = user.CreatedDate,
                    FullName = user.FullName,
                    Status = user.Status,
                    UserId = user.Id,
                    UserName = user.UserName
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
           
        }

    }
}