using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TetrisApiServer.Controllers
{
    [Produces("application/json")]
    [Route("api/Login")]
    public class LoginController : Controller
    {
        [HttpPost]
        public async Task<LoginRes> Process([FromBody] LoginReq request)
        {
            var loginResult = await DBMysql.LoginUser(request.UserID, request.UserPW);

            if(loginResult == false)
            {
                return new LoginRes() { Result = -1 };
            }

            SetLoginTime(request.UserID);

            var authToken = CreateAuthToken();
            var result = await DBRedis.SetValue(request.UserID, authToken);

            return new LoginRes() { Result = 0, AuthToken = authToken };

        }

        string CreateAuthToken()
        {
            string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            return token;
        }

        async void SetLoginTime(string userID)
        {
            var tick = DateTime.Now.Ticks.ToString();
        //    await DBMemcached.SetString(userID, tick);
        }
    }

    public class LoginReq
    {
        public string UserID;
        public string UserPW;
    }

    public class LoginRes
    {
        public short Result;
        public string AuthToken;
    }
}