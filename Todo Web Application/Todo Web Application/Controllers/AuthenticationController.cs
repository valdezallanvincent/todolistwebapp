using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TodoWebApplication.Constants;
using TodoWebApplication.Service;
using TodoWebApplication.Service.DtoModel;
using TodoWebApplication.Service.DtoModel.RequestDto;
using TodoWebApplication.Service.Helper;
using TodoWebApplication.Service.Interface;

namespace TodoWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        ICryptographer _cryptographer;
        IAuthenticationLoginService _authenticationService;
        public AuthenticationController(ICryptographer cryptographer, IAuthenticationLoginService authenticationService)
        {
            _cryptographer = cryptographer;
            _authenticationService = authenticationService;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            var encryptedPassword = _cryptographer.Encrypt(request.Password);
            request.Password = encryptedPassword;
            var resultData = await _authenticationService.Login(request);
            if (!resultData.IsSuccessfulLogin)
            {
                var responseErrorModel = new ResponseErrorModel(400, "Incorrect Username or Password");
                return BadRequest(responseErrorModel);
            }
            var token = BuildToken(resultData);

            return Ok(token);
        }
        private TokenModel BuildToken(LoginResultDto user)
        {
            var tokenModel = new TokenModel();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("F291263C-61EE-46F6-B889-BE9808465D17"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = CreateClaimsForUser(user);

            var tokenExpiration = DateTime.Now.AddMinutes(Convert.ToInt32("43000"));
            var token = new JwtSecurityToken(Request.Scheme + "://" + Request.Host.ToString(),
                Request.Scheme + "://" + Request.Host.ToString(),
                claims,
                expires: tokenExpiration,
                signingCredentials: credentials);

            tokenModel.Token = new JwtSecurityTokenHandler().WriteToken(token);
            tokenModel.TokenExpiresAt = ToUnixTime(tokenExpiration);
            return tokenModel;
        }
        private static long ToUnixTime(DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((date - epoch).TotalSeconds);
        }

        private IEnumerable<Claim> CreateClaimsForUser(LoginResultDto user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimContants.UserId, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FullName ?? string.Empty),
                new Claim(ClaimContants.UserName, user.UserName),
            };

            foreach (var item in user.UserRoles)
            {
                claims.Add(new Claim(ClaimContants.Role, item.Role.Role));
            }

            return claims;
        }
    }
}
