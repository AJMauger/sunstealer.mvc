using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace sunstealer.mvc.Controllers
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly Microsoft.Extensions.Logging.ILogger<HomeController> _logger;

        public HomeController(Microsoft.Extensions.Logging.ILogger<HomeController> logger)
        {
            _logger = logger;
            _logger.LogInformation("HomeController.HomeController()");
        }

        public async Task<IActionResult> IndexAsync()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            ViewBag.AccessToken = accessToken;

            var jwt = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(accessToken);

            // ajm: JArray not accessible ... ?! {{
            object obj;
            if (jwt.Payload.TryGetValue("scope", out obj))
            {
                var array = string.Join(",", obj).Replace("\r\n", "").Replace("\u0022", "").Replace("  ", "");
                jwt.Payload.Remove("scope");
                jwt.Payload.TryAdd("scope", array);
            }

            if(jwt.Payload.TryGetValue("amr", out obj))
            {
                var array = string.Join(",", obj).Replace("\r\n", "").Replace("\u0022", "").Replace("  ", "");
                jwt.Payload.Remove("amr");
                jwt.Payload.TryAdd("amr", array);
            }
            // ajm: }}

            var token = System.Text.Json.JsonSerializer.Serialize(jwt, new System.Text.Json.JsonSerializerOptions { MaxDepth = 10, WriteIndented = true }); ;

            ViewBag.Token = token;

            return View();
        }

        public Microsoft.AspNetCore.Mvc.IActionResult Privacy()
        {
            return View();
        }

        [Microsoft.AspNetCore.Mvc.ResponseCache(Duration = 0, Location = Microsoft.AspNetCore.Mvc.ResponseCacheLocation.None, NoStore = true)]
        public Microsoft.AspNetCore.Mvc.IActionResult Error()
        {
            return View(new sunstealer.mvc.Models.ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
