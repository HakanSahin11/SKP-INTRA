using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SKP_IntranetSideAPI.Cruds;
using SKP_IntranetSideAPI.Models;

using static SKP_IntranetSideAPI.Helper_Classes.Salting;
namespace SKP_IntranetSideAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
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
            //crypto?

            var content = JsonConvert.DeserializeObject<LoginModel>(LoginJson.GetRawText());
            var savedLogin = _login.GetUser(content.UserName);
            if (savedLogin == null)
                return NotFound();

            //salting / recov controller

            bool loginResult = false;
            if (HashSalt(content.Password, Convert.FromBase64String(_salt.GetUserById(savedLogin.Id).SaltPass)).Pass == savedLogin.Password)
                loginResult = true;

            return Ok(new APIReqModel { Json = loginResult.ToString() });
        }

    }
}
