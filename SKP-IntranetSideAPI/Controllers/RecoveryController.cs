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
namespace SKP_IntranetSideAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    class RecoveryController : ControllerBase 
    {
        private readonly LoginCrud _login;
        public RecoveryController(LoginCrud login)
        {
            _login = login;
        }

        [HttpPost]
        public ActionResult Confirm([FromBody] JsonElement js)
        {
            try
            {
                var result = false;
                var content = JsonConvert.DeserializeObject<RecoveryPost>(js.GetRawText());
                var userLogin = _login.GetUserByUserName(content.UserName);
                //finds user depending on given username or email
                if (userLogin == null)
                    userLogin = _login.GetUser(content.Email);

                //confirms match
                if (userLogin.Recovery.Contains(content.RecoveryPass))
                {
                    result = true;
                    userLogin.Recovery.Remove(content.RecoveryPass);
                    //Removes used recov pass
                    _login.Update(userLogin.UserName, userLogin);
                }
                return Ok(result);
            }
            catch
            {
                throw new Exception("Error code 5.2 - Error at RecoveryController Post");
            }
        }
    }
}
