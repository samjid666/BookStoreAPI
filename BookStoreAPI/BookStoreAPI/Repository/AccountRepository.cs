using BookStoreAPI.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreAPI.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _cofiguration;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,IConfiguration cofiguration)
        {
            _userManager = userManager;

            _signInManager = signInManager;
            _cofiguration = cofiguration;
        }



        public async Task<IdentityResult> SignUpAsync(SignUpModel signUpModel)
        {

            var user = new ApplicationUser()
            {

                FirstName = signUpModel.FirstName,
                LastName = signUpModel.LastName,
                Email = signUpModel.Email,
                UserName = signUpModel.Email,
            };

            return await _userManager.CreateAsync(user, signUpModel.Password);

        }
        public async Task<string> LoginAsync(SignInModel signInModel)
        {
            var result = await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, false, false);

            if (!result.Succeeded)
            {
                return null;
            }
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,signInModel.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())

            };
            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_cofiguration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _cofiguration["JWT:ValidIssuer"],
                audience: _cofiguration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
              
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
            );
           return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
