using GymManagementSystemCore.ViewModels.AccountViewModels;
using GymManagementSystemDAL.Models.AuthModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemCore.Services.AuthService
{
    public interface IAccountService
    {
        public ApplicationUser? ValidateUser(LoginViewModel loginView);
    }
}
