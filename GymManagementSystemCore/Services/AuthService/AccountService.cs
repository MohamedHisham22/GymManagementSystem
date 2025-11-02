using GymManagementSystemCore.ViewModels.AccountViewModels;
using GymManagementSystemDAL.Models.AuthModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemCore.Services.AuthService
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public ApplicationUser? ValidateUser(LoginViewModel loginView)
        {
            var user = _userManager.FindByEmailAsync(loginView.Email).Result;

            if (user is null) return null;

            var isPasswordValid = _userManager.CheckPasswordAsync(user, loginView.Password).Result;

            return isPasswordValid ? user : null;
        }
    }
}
