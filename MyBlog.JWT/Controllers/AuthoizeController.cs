using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using MyBlog.Utility.ApiResult;
using MyBlog.Utility._MD5;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace MyBlog.JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthoizeController : ControllerBase
    {
        private readonly IWriterInfoService _iWriterInfoService;

        public AuthoizeController(IWriterInfoService iWriterInfoService)
        {
            _iWriterInfoService = iWriterInfoService;
        }

        [HttpPost("Login")]
        public async Task<ApiResult> Login(string username,string password)
        {
            string pwd = MD5Helper.MD5Encrypt32(password);
            var writer =await _iWriterInfoService.FindAsync(e => e.UserName == username && e.UserPwd == pwd);
            if(writer != null)
            {
                //声明信息，不能放敏感信息
                var claims = new Claim[]
                    {
                        new Claim(ClaimTypes.Name, writer.Name),
                        new Claim("Id",writer.Id.ToString()),
                        new Claim("UserName", writer.UserName.ToString())
                    };

                //密钥 16位随机
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("G8hs0bXKm17vRlw2tJvH9NMbq3Aq74Lb"));

                //issuer代表颁发Token的Web应用程序，audience是Token的受理者
                var token = new JwtSecurityToken(
                    issuer: "http://localhost:6060",
                    audience: "http://localhost:54614",
                    claims: claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddHours(1),
                    
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                return ApiResultHepler.Success(jwtToken);
            }
            else
            {
                return ApiResultHepler.Error("User info not correct");
            }
        }

    }
}

