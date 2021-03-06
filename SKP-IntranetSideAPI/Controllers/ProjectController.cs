﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Newtonsoft.Json;
using SKP_IntranetSideAPI.Cruds;
using SKP_IntranetSideAPI.Models;
namespace SKP_IntranetSideAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectCrud _project;
        private readonly UserCrud _user;
        public ProjectController(ProjectCrud project, UserCrud user)
        {
            _project = project;
            _user = user;
        }

        //Get
        [HttpGet]
        public ActionResult<List<ProjectModel>> Get() =>
                 _project.Get();

        //Create
        [HttpPost]
        public ActionResult Create([FromBody] JsonElement js)
        {
            try
            {
                var content = JsonConvert.DeserializeObject<ProjectReq>(js.GetRawText());
                content.Content.UserId = _user.GetUser(content.UserName).Result.Id;
                content.Content.Status = "Pending";
                _project.Create(content.Content);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
                throw new Exception("Error Code 2.2 - Project Controller Post request, create user");

            }
        }

        //Put
        [HttpPut]
        public ActionResult Update(string id, JsonElement User)
        {
            try
            {
                var UserIn = JsonConvert.DeserializeObject<ProjectModel>(User.GetRawText());

                if (_project.GetProjectById(ObjectId.Parse(id)) == null)
                    return NotFound();
                var savedProject = _project.GetProjectById(ObjectId.Parse(id));
                var updatedProject = new ProjectModel(savedProject.Id, savedProject.UserId, UserIn.AttachmentId, UserIn.Specialty, UserIn.Title, UserIn.Input, UserIn.Status);

                _project.Update(updatedProject.Id, updatedProject);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
                throw new Exception("Error Code 2.3 - Project Controller Put request, Update user");
            }
        }

        //Delete
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            //Checks if project exists
            if (_project.GetProjectById(ObjectId.Parse(id)) == null)
                return NotFound();
            Task.Run(() => _project.Delete(ObjectId.Parse(id)));
            return Ok();
        }
    }
}
