using ASM.Share.Interfaces;
using ASM.Share.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace ASM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IKhachHangService _khachHangService;
        public IConfiguration _configuration;

        public TokenController(IKhachHangService khachHangService, IConfiguration configuration)
        {
            _khachHangService = khachHangService;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IEnumerable<ViewToken>> Post(ViewWebLogin viewWebLogin)
        {
            List<ViewToken> list = new List<ViewToken>();
            if (viewWebLogin != null && !string.IsNullOrEmpty(viewWebLogin.Email) && !string.IsNullOrEmpty(viewWebLogin.Password))
            {
                var khachhang = await _khachHangService.LoginAsync(viewWebLogin);
                if (khachhang != null)
                {
                    if (khachhang != null)
                    {
                        var claims = new[]
                        {
                             new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                             new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),

                             new Claim("Id", khachhang.KhachHangID.ToString()),
                             new Claim("FullName", khachhang.FullName),
                             new Claim("Email", khachhang.EmailKH)
                        };
                         var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                         var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                         var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                        ViewToken viewToken = new ViewToken()
                        {
                            Token = new JwtSecurityTokenHandler().WriteToken(token),
                            KhachhangId = khachhang.KhachHangID
                        };

                        list.Add(viewToken);
                        return list;
                    }
                    else
                    {
                        return list;
                    }
                }
                else
                {
                    return list;
                }                
            }
            return list;
        }
    }
}
