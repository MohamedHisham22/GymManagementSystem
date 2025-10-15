using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemCore.ViewModels.TrainerViewModels
{
    internal class TrainerDetailsViewModel
    {
        public string Email { get; set; } = null!;

        public string DateOfBirth { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Address { get; set; } = null!;
    }
}
