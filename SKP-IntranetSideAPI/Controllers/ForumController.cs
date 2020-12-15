using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SKP_IntranetSideAPI.Cruds;
using SKP_IntranetSideAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SKP_IntranetSideAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumController : ControllerBase
    {
        //Minor bug, when 2 users have the same content it wont show both for some reason on the api display

        //   readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly UserCrud _users;
        private readonly ForumCrud _forum;
        public ForumController(UserCrud user, ForumCrud forum)
        {
            _users = user;
            _forum = forum;
        }

        [HttpGet]
        public ActionResult<List<ForumModel>> Get() =>
            _forum.Get().Result;

        [HttpPost]
        public ActionResult Create([FromBody] JsonElement Create)
        {
            try
            {
                var content = JsonConvert.DeserializeObject<ForumModel>(Create.GetRawText());
                content.UserId = _users.GetUser(content.Username).Result.Id;

                _forum.Create(content).Wait();
                return Ok();
            }
            catch(Exception e)
            {
                throw new Exception($"Error Code 5.2 - Error at HTTPPOST Forum - {e.Message}");
            }
        }
        [HttpPut]
        public ActionResult Update(JsonElement update)
        {
            try
            {
                var content = JsonConvert.DeserializeObject<ForumModel>(update.GetRawText());
                content.UserId = _users.GetUser(content.Username).Result.Id;
                if (content.UserId == null)
                    return NotFound();
                _forum.Update(content.Id, content);
                return Ok(true);
            }
            catch(Exception e)
            {
                throw new Exception($"Error Code 5.3 - Error at Put Update Forum - {e.Message}");
            }
        }

        [HttpDelete("{username}")]
        public ActionResult Delete(string username)
        {
            try
            {
                var id = _users.GetUser(username).Result.Id;
                if (id == null)
                    return NotFound();
                _forum.Delete(id);
                //add also for removal of usercontroller saved input when it gets added in the future
                return Ok(true);
            }
            catch (Exception e)
            {
                throw new Exception($"Error code 5.4 - ForumController Delete forum error - {e.Message}");
            }
        }
    }
}
