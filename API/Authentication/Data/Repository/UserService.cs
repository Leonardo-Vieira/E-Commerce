using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Authentication.Models;
using IdentityModel;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Data.Repository {
    public class UserService : IUserService<User> 
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IdentityServerTools _tools;
        private readonly DataContext _context;
        public UserService (IdentityServerTools tools, UserManager<User> userManager, SignInManager<User> signInManager, DataContext context) 
        {
            _context = context;
            _tools = tools;
            _signInManager = signInManager;
            _userManager = userManager;

        }

        public async Task RegisterUser(User user, string password)
        {
            await _userManager.CreateAsync(user, password);
        }
        public async Task<User> FindByEmail(string email) 
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public Task SignIn(User user) 
        {
            return _signInManager.SignInAsync(user, true);
        }

        public Task SignOut(User user) 
        {
            return _signInManager.SignOutAsync();
        }

        public async Task<bool> ValidateCredentials(User user, string password) 
        {
            return await _userManager.CheckPasswordAsync (user, password);
        }
        public async Task<User> FindById(Guid id) 
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<string> GetToken(User user, string url) 
        {
            return await _tools.IssueJwtAsync(5000000, new Claim[] {
                 new Claim(JwtClaimTypes.Subject, user.Id),
                 new Claim("clb", user.IsCollaborator.ToString()),
                 new Claim("un", user.UserName),
                 new Claim(JwtClaimTypes.Audience, url)
            });
        }

        public bool VerifyIdentificationNumber(string identificationNumber) 
        {
            return _context.Users.FirstOrDefault(i => i.IdentificationNumber == identificationNumber) != null ? false : true;
        }

        
    }
}