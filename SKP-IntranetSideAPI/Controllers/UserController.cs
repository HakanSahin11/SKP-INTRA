using System;
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

using static SKP_IntranetSideAPI.Helper_Classes.RecoveryKeyGen;
using static SKP_IntranetSideAPI.Helper_Classes.Salting;
namespace SKP_IntranetSideAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserCrud _users;
        private readonly LoginCrud _login;
        private readonly SaltCrud _salt;

        public UserController(UserCrud users, LoginCrud login, SaltCrud salt)
        {
            _users = users;
            _login = login;
            _salt = salt;
        }

        //Get all users
        [HttpGet]
        public ActionResult<List<UserModel>> Get() =>
           _users.Get().Result;

        [HttpGet("{username}")]
        public ActionResult Get(string username) =>
           Ok(_users.GetUser(username).Result);

        //Create
        [HttpPost]
        public ActionResult Create([FromBody] JsonElement jsUser)
        {
            try
            {
                //Add checking if user already exits by username or email
                var user = JsonConvert.DeserializeObject<NewUserModel>(jsUser.GetRawText());
                var matchUserName = _users.GetUser(user.UserName).Result;
                if (matchUserName != null || _users.GetUserByEmail(user.Email).Result != null)
                {
                    if (matchUserName != null)
                        return BadRequest("UserName already exists!");
                    else
                    {
                        return BadRequest("Email already exists!");
                    }
                }
                var recoveryCodes = GenerateRecovery();

                _users.Create(new UserModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserType = user.UserType,
                    Specialty = user.Specialty,
                    ProjectId = new List<ObjectId>()
                }).Wait();
                var id = _users.GetUser(user.UserName).Result.Id;
                var hash = HashSalt(user.Password, null);

                _login.Create(new LoginModel { Id = id, UserName = user.UserName, Email = user.Email, Password = hash.Pass, Recovery = recoveryCodes.HashKey });
                Task.Run(() => _salt.Create(new SaltModel(id, Convert.ToBase64String(GenerateSalt()), hash.Salt, recoveryCodes.SaltKey)));
                Task.WaitAll();
                return Ok(new APIReqModel { Json = string.Join(",", recoveryCodes.Key.ToArray()) });
            }
            catch
            {
              //  return BadRequest();
                throw new Exception("Error code 3.2 - UserController Post Create user error");
            }
        }

        [HttpPut]
        public ActionResult Update(string username, UserModel UserIn)
        {
            try
            {
                if (_users.GetUser(username).Result == null)
                    return NotFound();
                _users.Update(username, UserIn);
                return NoContent();
            }
            catch
            {
                throw new Exception("Error code 3.3 - UserController Put Update user error");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(ObjectId id)
        {
            //Checks if user exists
            if (_users.GetUserById(id).Result == null)
                return NotFound();
            Task.Run(() => _users.Delete(id));
            Task.Run(() => _login.Delete(id));
            Task.Run(() => _salt.Delete(id));
            return NoContent();
        }
    }
}
