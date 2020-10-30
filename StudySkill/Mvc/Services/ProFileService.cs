using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Mvc.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Mvc.Services
{
    public class ProFileService : IProfileService
    {

        private UserManager<ApplicationUser> _userManager;

        public ProFileService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        private async Task<List<Claim>> GetClaimsByUserAsync(ApplicationUser user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Subject,user.Id.ToString()),
                new Claim(JwtClaimTypes.PreferredUserName,user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var item in roles)
            {
                claims.Add(new Claim(JwtClaimTypes.Role, item));
            }

            if (!string.IsNullOrWhiteSpace(user.Avatar))
            {
                claims.Add(new Claim("Avatar", user.Avatar));
            }

            return claims;
        }


        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var userId = context.Subject.Claims.FirstOrDefault(a => a.Type == "sub")?.Value;
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                context.IssuedClaims = await GetClaimsByUserAsync(user);
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = false;

            var subjectId = context.Subject.Claims.FirstOrDefault(a => a.Type == "sub")?.Value;

            var user = await _userManager.FindByIdAsync(subjectId);

            context.IsActive = user != null;
        }
    }
}
