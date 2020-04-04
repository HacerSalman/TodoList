﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoList.Api.Internal;
using TodoList.Model.Context;
using TodoList.Model.Entities;

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
        [ProducesResponseType(typeof(List),200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult AddList([FromBody] List list)
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
                    else
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
                            Status = 1,
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
                            Status = 1,
                            UpdatedDate = Utils.GetUnixTimeNow(),
                            CreatedDate = Utils.GetUnixTimeNow(),
                            UserId = user.Id
                        };
                        db.UserList.Add(userList);
                        db.SaveChanges();

                        return Ok(newList);
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
        ///Update the list
        /// </summary>  
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult UpdateList([FromBody] List list)
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
                    var userList = db.UserList.Where(ul => ul.UserId == user.Id && ul.ListId == list.Id);
                    if(userList == null)
                        return Unauthorized("Unauthorized!");

                    //Check the list
                    var currentList = db.List.Where(l => l.Id == list.Id).FirstOrDefault();
                    if (currentList == null)
                        return NotFound("The list not found!");

                    currentList.ModifierBy = user.UserName;
                    currentList.UpdatedDate = Utils.GetUnixTimeNow();
                    currentList.Description = !string.IsNullOrEmpty(list.Description) ? list.Description : currentList.Description;
                    currentList.EndsAt = list.EndsAt != null ? list.EndsAt : currentList.EndsAt;
                    currentList.Priority = list.Priority != null ? list.Priority : currentList.Priority;
                    currentList.StartsAt = list.StartsAt != null ? list.StartsAt : currentList.StartsAt;
                    currentList.Status = list.Status != null ? list.Status : currentList.Status;
                    currentList.Title = !string.IsNullOrEmpty(list.Title) ? list.Title : currentList.Title;
                    currentList.Type = list.Type != null ? list.Type : currentList.Type;
                    db.SaveChanges();
                }
                else
                    return Unauthorized("Unauthorized!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
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