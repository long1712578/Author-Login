using JwtExample.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtExample.Controllers
{
    [Route("api/Author")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest req)
        {
            if (req == null)
            {
                return BadRequest("User no value!!!");
            }else if (req.UserName == "")
            {
                return BadRequest("User no value of username!!!");
            }else if(req.Password.Length<7 || req.Password=="")
            {
                return BadRequest("User enter no format password!!!");
            }else if( req.UserName=="long" && req.Password == "long-123#")
            {
                //Tao 1 lop khoa bao mat moi
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("supperSecrectKey@345"));
                //Tao bien thong tin dang nhap(xac thuc)= khoa bao mat + thuat toan bam
                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                //Tao ma tuy chon thong bao de chung ta tao bao mat token
                var tokenOptions = new JwtSecurityToken(
                    issuer: "https://localhost:5001",
                    audience: "https://localhost:5001",
                    claims: new List<System.Security.Claims.Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signingCredentials
                    );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { Token = tokenString });
            }
            return Unauthorized();
        }
    }
}
