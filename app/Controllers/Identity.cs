
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;

namespace sunstealer.mvc.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("~/Identity")]
    public class Identity : Microsoft.AspNetCore.Mvc.Controller
    {
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "sunstealer.read")]
        public async Task<IActionResult> GetAsync()
        {
            // ajm: sandpit: for swagger => use user login token
            string accessToken = await HttpContext.GetTokenAsync("access_token");
            var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(jwtEncodedString: accessToken);
            return new Microsoft.AspNetCore.Mvc.JsonResult(token);
        }
    }
}
