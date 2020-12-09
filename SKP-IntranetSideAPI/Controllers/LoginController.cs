using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using SKP_IntranetSideAPI.Cruds;
using SKP_IntranetSideAPI.Models;

using static SKP_IntranetSideAPI.Helper_Classes.Salting;
namespace SKP_IntranetSideAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly LoginCrud _login;
        private readonly SaltCrud _salt;
        public LoginController(LoginCrud login, SaltCrud salt)
        {
            _login = login;
            _salt = salt;
        }

        //Get list of all users
        [HttpGet]
        public ActionResult<List<LoginModel>> Get() =>
             _login.Get();

        [HttpPost]
        public ActionResult LoginConfirm([FromBody] JsonElement LoginJson)
        {
            try
            {
                //Crypto?

                var content = JsonConvert.DeserializeObject<LoginModel>(LoginJson.GetRawText());
                LoginModel SavedContent = _login.GetUserByUserName(content.UserName);
                if (SavedContent == null)
                    SavedContent = _login.GetUser(content.UserName);
                if (SavedContent == null)
                    return NotFound();

                //salting
                bool loginResult = false;
                if (HashSalt(content.Password, Convert.FromBase64String(_salt.GetUserById(SavedContent.Id).SaltPass)).Pass == SavedContent.Password)
                    loginResult = true;
                return Ok(loginResult);
            }
            catch(Exception e)
            {
                logger.Error($"Error Code 1.2 - Error at HTTPPOST - {e.Message}");
                return null;
            }
        }
    }
}
