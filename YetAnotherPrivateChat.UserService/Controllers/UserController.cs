using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using YetAnotherPrivateChat.UserService.Service;
using YetAnotherPrivateChat.UserService.Context;
using YetAnotherPrivateChat.Shared;
using YetAnotherPrivateChat.Shared.DTO;
using YetAnotherPrivateChat.Shared.HelperShared.JWT;

namespace YetAnotherPrivateChat.UserService.Controllers
{
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly Login _loginService;
        private readonly Refresh _refreshService;
        private readonly Register _registerService;
        public UserController(Login loginService, Refresh refreshService, Register registerService)
        {
            _loginService = loginService;
            _refreshService = refreshService;
            _registerService = registerService;
        }

        [HttpGet]
        public ActionResult<string> Health()
        {
            return "I'm alive";
        }

        [HttpGet("/login")]
        public async Task<ActionResult<JwtRefreshDTO>> Login([FromBody] UserDTO dto)
        {
            return await _loginService.AllowLogin(dto.Username, dto.Password);
        }

        [HttpGet("/refresh")]
        public async Task<ActionResult<JwtRefreshDTO>> RefreshToken()
        {
            var refreshToken = DecodeHeader.GetRefreshToken(context.Request.Headers);
            return await _refreshService.RefreshJWT(refreshToken.refreshToken.id, refreshToken.token);
        }

        [HttpPost("/register")]
        public async Task<ActionResult<JwtRefreshDTO>> Register([FromBody] UserDTO dto)
        {
            return await _registerService.RegisterUser(dto);
        }

        endpoints.MapGet("/userlist", async context =>
{
    var _context = new MyDbContext();
        var us = new UserList();

        //check if the JWT is valid, if there is none or invalid it will send a error back
        var jwt = DecodeHeader.GetJwtToken(context.Request.Headers);

        var result = await us.GetUsers(_context);
        await context.Response.WriteAsJsonAsync(result);
    });
endpoints.MapDelete("/logoff", async context =>
{
    var _context = new MyDbContext();
    var off = new Refresh();

    var refreshToken = DecodeHeader.GetRefreshToken(context.Request.Headers);

    await off.DisableRefreshToken(refreshToken.Item1.id, _context);
    await context.Response.WriteAsync("Logged off");

});
endpoints.MapDelete("/logoffall", async context =>
{
    var _context = new MyDbContext();
    var off = new Refresh();

    var refreshToken = DecodeHeader.GetRefreshToken(context.Request.Headers);

    await off.DisableAllRefreshToken(refreshToken.Item1.id, _context);
    await context.Response.WriteAsync("Logged off from all devices");
});
        }
 
}