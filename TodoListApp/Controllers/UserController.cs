using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoList.Api.Internal;
using TodoList.Model.Context;

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
        public ActionResult Login()
        {
            try
            {
                //Check Basic Authentication
                Microsoft.Extensions.Primitives.StringValues authorizationToken;
                HttpContext.Request.Headers.TryGetValue("Authorization", out authorizationToken);
                if (!String.IsNullOrEmpty(authorizationToken.ToString()))
                {
                    string[] decodedCredentials = Encoding.ASCII.GetString(Convert.FromBase64String(authorizationToken.ToString().Replace("Basic ", ""))).Split(new[] { ':' });

                    var encodePassword = Utils.EncodePassword(decodedCredentials[1]);
                    var user = db.User.Where(u => u.UserName == decodedCredentials[0] && u.Password == encodePassword).FirstOrDefault();
                    if (user == null)
                        return Unauthorized("Username or password incorrect!");
                    else
                        return Ok();
                }
                return Unauthorized("Username or password incorrect!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }         
        }

    
    }
}