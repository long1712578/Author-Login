using JwtExample.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JwtExample.Entities.JwtHandler
{
    public class JwtHandler
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtSettings;
        private readonly UserManager<User> _userManager;
        public JwtHandler(IConfiguration configuration, UserManager<User> userManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _jwtSettings = _configuration.GetSection("JwtSettings");
        }
public async Task<List<Claim>> GetClaims(User user)
        {
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.Email)
    };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
    }
}
