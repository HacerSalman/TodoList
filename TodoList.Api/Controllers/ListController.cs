using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoList.Model.Context;

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
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult AddList(long userId)
        {
            return Ok();
        }

        [HttpPut]
        /// <summary>
        ///Update the list
        /// </summary>  
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult UpdateList(long listId)
        {
            return Ok();

        }

        [HttpPost]
        /// <summary>
        ///Add multiple list
        /// </summary>  
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult AddMultipleList([FromQuery] long[] listId)
        {
            return Ok();
        }

        [HttpPut]
        /// <summary>
        ///Add list type to the list
        /// </summary>  
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult AddListType(long listId,int typeId)
        {
            return Ok();
        }

        [HttpPut]
        /// <summary>
        ///Delete the list
        /// </summary>  
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult DeleteList(long listId)
        {
            return Ok();

        }

    }
}